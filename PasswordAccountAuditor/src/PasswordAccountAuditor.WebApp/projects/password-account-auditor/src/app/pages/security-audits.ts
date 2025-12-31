import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatChipModule } from '@angular/material/chips';
import { MatCardModule } from '@angular/material/card';
import { Observable } from 'rxjs';
import { SecurityAuditService, AccountService } from '../services';
import { SecurityAudit, CreateSecurityAudit, UpdateSecurityAudit, AuditStatus, AuditStatusLabels, AuditType, AuditTypeLabels, Account } from '../models';

@Component({
  selector: 'app-security-audit-form-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule
  ],
  template: `
    <h2 mat-dialog-title>{{ data.audit ? 'Edit Security Audit' : 'Add Security Audit' }}</h2>
    <mat-dialog-content>
      <form [formGroup]="form" class="security-audit-form">
        <mat-form-field appearance="outline" class="security-audit-form__field">
          <mat-label>Account</mat-label>
          <mat-select formControlName="accountId" required>
            <mat-option *ngFor="let account of accounts" [value]="account.accountId">
              {{ account.accountName }} ({{ account.username }})
            </mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field appearance="outline" class="security-audit-form__field">
          <mat-label>Audit Type</mat-label>
          <mat-select formControlName="auditType" required>
            <mat-option *ngFor="let type of auditTypes" [value]="type.value">
              {{ type.label }}
            </mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field *ngIf="data.audit" appearance="outline" class="security-audit-form__field">
          <mat-label>Status</mat-label>
          <mat-select formControlName="status" required>
            <mat-option *ngFor="let status of statuses" [value]="status.value">
              {{ status.label }}
            </mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field appearance="outline" class="security-audit-form__field">
          <mat-label>Security Score (0-100)</mat-label>
          <input matInput formControlName="securityScore" type="number" min="0" max="100" required>
        </mat-form-field>

        <mat-form-field appearance="outline" class="security-audit-form__field">
          <mat-label>Findings</mat-label>
          <textarea matInput formControlName="findings" rows="3"></textarea>
        </mat-form-field>

        <mat-form-field appearance="outline" class="security-audit-form__field">
          <mat-label>Recommendations</mat-label>
          <textarea matInput formControlName="recommendations" rows="3"></textarea>
        </mat-form-field>

        <mat-form-field appearance="outline" class="security-audit-form__field">
          <mat-label>Notes</mat-label>
          <textarea matInput formControlName="notes" rows="2"></textarea>
        </mat-form-field>
      </form>
    </mat-dialog-content>
    <mat-dialog-actions align="end">
      <button mat-button mat-dialog-close>Cancel</button>
      <button mat-raised-button color="primary" (click)="save()" [disabled]="!form.valid">Save</button>
    </mat-dialog-actions>
  `,
  styles: [`
    .security-audit-form {
      display: flex;
      flex-direction: column;
      gap: 1rem;
      min-width: 500px;
      padding: 1rem 0;
    }

    .security-audit-form__field {
      width: 100%;
    }

    @media (max-width: 768px) {
      .security-audit-form {
        min-width: 300px;
      }
    }
  `]
})
export class SecurityAuditFormDialog {
  private fb = inject(FormBuilder);
  private accountService = inject(AccountService);
  data: { audit?: SecurityAudit } = {};

  form: FormGroup;
  accounts: Account[] = [];
  auditTypes = Object.keys(AuditType)
    .filter(key => !isNaN(Number(AuditType[key as any])))
    .map(key => ({
      value: AuditType[key as any],
      label: AuditTypeLabels[AuditType[key as any]]
    }));
  statuses = Object.keys(AuditStatus)
    .filter(key => !isNaN(Number(AuditStatus[key as any])))
    .map(key => ({
      value: AuditStatus[key as any],
      label: AuditStatusLabels[AuditStatus[key as any]]
    }));

  constructor() {
    this.form = this.fb.group({
      accountId: ['', Validators.required],
      auditType: [AuditType.Manual, Validators.required],
      status: [AuditStatus.Pending],
      securityScore: [0, [Validators.required, Validators.min(0), Validators.max(100)]],
      findings: [''],
      recommendations: [''],
      notes: ['']
    });

    this.accountService.accounts$.subscribe(accounts => {
      this.accounts = accounts;
    });

    if (this.data.audit) {
      const audit = this.data.audit;
      this.form.patchValue({
        accountId: audit.accountId,
        auditType: audit.auditType,
        status: audit.status,
        securityScore: audit.securityScore,
        findings: audit.findings,
        recommendations: audit.recommendations,
        notes: audit.notes
      });
    }
  }

  save() {
    if (this.form.valid) {
      // dialogRef.close(this.form.value) would be called here
    }
  }
}

@Component({
  selector: 'app-security-audits',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatChipModule,
    MatCardModule
  ],
  template: `
    <div class="security-audits">
      <div class="security-audits__header">
        <h1 class="security-audits__title">Security Audits</h1>
        <button mat-raised-button color="primary" (click)="openDialog()" class="security-audits__add-btn">
          <mat-icon>add</mat-icon>
          Add Security Audit
        </button>
      </div>

      <mat-card class="security-audits__card">
        <table mat-table [dataSource]="securityAudits$ | async" class="security-audits__table">
          <ng-container matColumnDef="auditType">
            <th mat-header-cell *matHeaderCellDef>Audit Type</th>
            <td mat-cell *matCellDef="let audit">
              <mat-chip>{{ getAuditTypeLabel(audit.auditType) }}</mat-chip>
            </td>
          </ng-container>

          <ng-container matColumnDef="status">
            <th mat-header-cell *matHeaderCellDef>Status</th>
            <td mat-cell *matCellDef="let audit">
              <mat-chip [class]="'security-audits__chip--' + getStatusClass(audit.status)">
                {{ getStatusLabel(audit.status) }}
              </mat-chip>
            </td>
          </ng-container>

          <ng-container matColumnDef="securityScore">
            <th mat-header-cell *matHeaderCellDef>Security Score</th>
            <td mat-cell *matCellDef="let audit">
              <div class="security-audits__score" [class]="'security-audits__score--' + getScoreClass(audit.securityScore)">
                {{ audit.securityScore }}
              </div>
            </td>
          </ng-container>

          <ng-container matColumnDef="auditDate">
            <th mat-header-cell *matHeaderCellDef>Audit Date</th>
            <td mat-cell *matCellDef="let audit">{{ audit.auditDate | date:'short' }}</td>
          </ng-container>

          <ng-container matColumnDef="findings">
            <th mat-header-cell *matHeaderCellDef>Findings</th>
            <td mat-cell *matCellDef="let audit">{{ audit.findings || 'N/A' }}</td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let audit">
              <button mat-icon-button color="primary" (click)="openDialog(audit)">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" (click)="delete(audit.securityAuditId)">
                <mat-icon>delete</mat-icon>
              </button>
            </td>
          </ng-container>

          <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
          <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
        </table>
      </mat-card>
    </div>
  `,
  styles: [`
    .security-audits {
      padding: 2rem;
    }

    .security-audits__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 2rem;
    }

    .security-audits__title {
      margin: 0;
      color: rgba(0, 0, 0, 0.87);
    }

    .security-audits__add-btn {
      display: flex;
      align-items: center;
      gap: 0.5rem;
    }

    .security-audits__card {
      overflow-x: auto;
    }

    .security-audits__table {
      width: 100%;
    }

    .security-audits__chip--pending {
      background-color: #9e9e9e !important;
      color: white !important;
    }

    .security-audits__chip--inprogress {
      background-color: #2196f3 !important;
      color: white !important;
    }

    .security-audits__chip--completed {
      background-color: #4caf50 !important;
      color: white !important;
    }

    .security-audits__chip--failed {
      background-color: #f44336 !important;
      color: white !important;
    }

    .security-audits__score {
      display: inline-flex;
      align-items: center;
      justify-content: center;
      min-width: 3rem;
      padding: 0.25rem 0.5rem;
      border-radius: 4px;
      font-weight: 500;
      color: white;
    }

    .security-audits__score--high {
      background-color: #4caf50;
    }

    .security-audits__score--medium {
      background-color: #ff9800;
    }

    .security-audits__score--low {
      background-color: #f44336;
    }

    @media (max-width: 768px) {
      .security-audits {
        padding: 1rem;
      }

      .security-audits__header {
        flex-direction: column;
        align-items: flex-start;
        gap: 1rem;
      }
    }
  `]
})
export class SecurityAudits implements OnInit {
  private securityAuditService = inject(SecurityAuditService);
  private accountService = inject(AccountService);
  private dialog = inject(MatDialog);

  securityAudits$: Observable<SecurityAudit[]> = this.securityAuditService.securityAudits$;
  displayedColumns = ['auditType', 'status', 'securityScore', 'auditDate', 'findings', 'actions'];

  ngOnInit() {
    this.securityAuditService.getSecurityAudits().subscribe();
    this.accountService.getAccounts().subscribe();
  }

  getAuditTypeLabel(type: AuditType): string {
    return AuditTypeLabels[type];
  }

  getStatusLabel(status: AuditStatus): string {
    return AuditStatusLabels[status];
  }

  getStatusClass(status: AuditStatus): string {
    switch (status) {
      case AuditStatus.Pending: return 'pending';
      case AuditStatus.InProgress: return 'inprogress';
      case AuditStatus.Completed: return 'completed';
      case AuditStatus.Failed: return 'failed';
      default: return '';
    }
  }

  getScoreClass(score: number): string {
    if (score >= 70) return 'high';
    if (score >= 40) return 'medium';
    return 'low';
  }

  openDialog(audit?: SecurityAudit) {
    const dialogRef = this.dialog.open(SecurityAuditFormDialog, {
      width: '600px',
      data: { audit }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (audit) {
          const updateData: UpdateSecurityAudit = {
            securityAuditId: audit.securityAuditId,
            ...result
          };
          this.securityAuditService.updateSecurityAudit(updateData).subscribe();
        } else {
          const createData: CreateSecurityAudit = {
            ...result
          };
          this.securityAuditService.createSecurityAudit(createData).subscribe();
        }
      }
    });
  }

  delete(id: string) {
    if (confirm('Are you sure you want to delete this security audit?')) {
      this.securityAuditService.deleteSecurityAudit(id).subscribe();
    }
  }
}
