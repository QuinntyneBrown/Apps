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
import { BreachAlertService, AccountService } from '../services';
import { BreachAlert, CreateBreachAlert, UpdateBreachAlert, AlertStatus, AlertStatusLabels, BreachSeverity, BreachSeverityLabels, Account } from '../models';

@Component({
  selector: 'app-breach-alert-form-dialog',
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
    <h2 mat-dialog-title>{{ data.alert ? 'Edit Breach Alert' : 'Add Breach Alert' }}</h2>
    <mat-dialog-content>
      <form [formGroup]="form" class="breach-alert-form">
        <mat-form-field appearance="outline" class="breach-alert-form__field">
          <mat-label>Account</mat-label>
          <mat-select formControlName="accountId" required>
            <mat-option *ngFor="let account of accounts" [value]="account.accountId">
              {{ account.accountName }} ({{ account.username }})
            </mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field appearance="outline" class="breach-alert-form__field">
          <mat-label>Severity</mat-label>
          <mat-select formControlName="severity" required>
            <mat-option *ngFor="let severity of severities" [value]="severity.value">
              {{ severity.label }}
            </mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field *ngIf="data.alert" appearance="outline" class="breach-alert-form__field">
          <mat-label>Status</mat-label>
          <mat-select formControlName="status" required>
            <mat-option *ngFor="let status of statuses" [value]="status.value">
              {{ status.label }}
            </mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field appearance="outline" class="breach-alert-form__field">
          <mat-label>Breach Date</mat-label>
          <input matInput formControlName="breachDate" type="datetime-local">
        </mat-form-field>

        <mat-form-field appearance="outline" class="breach-alert-form__field">
          <mat-label>Source</mat-label>
          <input matInput formControlName="source">
        </mat-form-field>

        <mat-form-field appearance="outline" class="breach-alert-form__field">
          <mat-label>Description</mat-label>
          <textarea matInput formControlName="description" rows="3" required></textarea>
        </mat-form-field>

        <mat-form-field appearance="outline" class="breach-alert-form__field">
          <mat-label>Data Compromised</mat-label>
          <textarea matInput formControlName="dataCompromised" rows="2"></textarea>
        </mat-form-field>

        <mat-form-field appearance="outline" class="breach-alert-form__field">
          <mat-label>Recommended Actions</mat-label>
          <textarea matInput formControlName="recommendedActions" rows="3"></textarea>
        </mat-form-field>

        <mat-form-field appearance="outline" class="breach-alert-form__field">
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
    .breach-alert-form {
      display: flex;
      flex-direction: column;
      gap: 1rem;
      min-width: 500px;
      padding: 1rem 0;
    }

    .breach-alert-form__field {
      width: 100%;
    }

    @media (max-width: 768px) {
      .breach-alert-form {
        min-width: 300px;
      }
    }
  `]
})
export class BreachAlertFormDialog {
  private fb = inject(FormBuilder);
  private accountService = inject(AccountService);
  data: { alert?: BreachAlert } = {};

  form: FormGroup;
  accounts: Account[] = [];
  severities = Object.keys(BreachSeverity)
    .filter(key => !isNaN(Number(BreachSeverity[key as any])))
    .map(key => ({
      value: BreachSeverity[key as any],
      label: BreachSeverityLabels[BreachSeverity[key as any]]
    }));
  statuses = Object.keys(AlertStatus)
    .filter(key => !isNaN(Number(AlertStatus[key as any])))
    .map(key => ({
      value: AlertStatus[key as any],
      label: AlertStatusLabels[AlertStatus[key as any]]
    }));

  constructor() {
    this.form = this.fb.group({
      accountId: ['', Validators.required],
      severity: [BreachSeverity.Medium, Validators.required],
      status: [AlertStatus.New],
      breachDate: [''],
      source: [''],
      description: ['', Validators.required],
      dataCompromised: [''],
      recommendedActions: [''],
      notes: ['']
    });

    this.accountService.accounts$.subscribe(accounts => {
      this.accounts = accounts;
    });

    if (this.data.alert) {
      const alert = this.data.alert;
      this.form.patchValue({
        accountId: alert.accountId,
        severity: alert.severity,
        status: alert.status,
        breachDate: alert.breachDate ? new Date(alert.breachDate).toISOString().slice(0, 16) : '',
        source: alert.source,
        description: alert.description,
        dataCompromised: alert.dataCompromised,
        recommendedActions: alert.recommendedActions,
        notes: alert.notes
      });
    }
  }

  save() {
    if (this.form.valid) {
      const formValue = this.form.value;
      const result = {
        ...formValue,
        breachDate: formValue.breachDate || undefined
      };
      // dialogRef.close(result) would be called here
    }
  }
}

@Component({
  selector: 'app-breach-alerts',
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
    <div class="breach-alerts">
      <div class="breach-alerts__header">
        <h1 class="breach-alerts__title">Breach Alerts</h1>
        <button mat-raised-button color="primary" (click)="openDialog()" class="breach-alerts__add-btn">
          <mat-icon>add</mat-icon>
          Add Breach Alert
        </button>
      </div>

      <mat-card class="breach-alerts__card">
        <table mat-table [dataSource]="breachAlerts$ | async" class="breach-alerts__table">
          <ng-container matColumnDef="severity">
            <th mat-header-cell *matHeaderCellDef>Severity</th>
            <td mat-cell *matCellDef="let alert">
              <mat-chip [class]="'breach-alerts__chip--' + getSeverityClass(alert.severity)">
                {{ getSeverityLabel(alert.severity) }}
              </mat-chip>
            </td>
          </ng-container>

          <ng-container matColumnDef="status">
            <th mat-header-cell *matHeaderCellDef>Status</th>
            <td mat-cell *matCellDef="let alert">
              <mat-chip [class]="'breach-alerts__chip--' + getStatusClass(alert.status)">
                {{ getStatusLabel(alert.status) }}
              </mat-chip>
            </td>
          </ng-container>

          <ng-container matColumnDef="description">
            <th mat-header-cell *matHeaderCellDef>Description</th>
            <td mat-cell *matCellDef="let alert">{{ alert.description }}</td>
          </ng-container>

          <ng-container matColumnDef="source">
            <th mat-header-cell *matHeaderCellDef>Source</th>
            <td mat-cell *matCellDef="let alert">{{ alert.source || 'N/A' }}</td>
          </ng-container>

          <ng-container matColumnDef="detectedDate">
            <th mat-header-cell *matHeaderCellDef>Detected Date</th>
            <td mat-cell *matCellDef="let alert">{{ alert.detectedDate | date:'short' }}</td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let alert">
              <button mat-icon-button color="primary" (click)="openDialog(alert)">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" (click)="delete(alert.breachAlertId)">
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
    .breach-alerts {
      padding: 2rem;
    }

    .breach-alerts__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 2rem;
    }

    .breach-alerts__title {
      margin: 0;
      color: rgba(0, 0, 0, 0.87);
    }

    .breach-alerts__add-btn {
      display: flex;
      align-items: center;
      gap: 0.5rem;
    }

    .breach-alerts__card {
      overflow-x: auto;
    }

    .breach-alerts__table {
      width: 100%;
    }

    .breach-alerts__chip--critical {
      background-color: #d32f2f !important;
      color: white !important;
    }

    .breach-alerts__chip--high {
      background-color: #f44336 !important;
      color: white !important;
    }

    .breach-alerts__chip--medium {
      background-color: #ff9800 !important;
      color: white !important;
    }

    .breach-alerts__chip--low {
      background-color: #4caf50 !important;
      color: white !important;
    }

    .breach-alerts__chip--new {
      background-color: #2196f3 !important;
      color: white !important;
    }

    .breach-alerts__chip--acknowledged {
      background-color: #ff9800 !important;
      color: white !important;
    }

    .breach-alerts__chip--resolved {
      background-color: #4caf50 !important;
      color: white !important;
    }

    .breach-alerts__chip--dismissed {
      background-color: #9e9e9e !important;
      color: white !important;
    }

    @media (max-width: 768px) {
      .breach-alerts {
        padding: 1rem;
      }

      .breach-alerts__header {
        flex-direction: column;
        align-items: flex-start;
        gap: 1rem;
      }
    }
  `]
})
export class BreachAlerts implements OnInit {
  private breachAlertService = inject(BreachAlertService);
  private accountService = inject(AccountService);
  private dialog = inject(MatDialog);

  breachAlerts$: Observable<BreachAlert[]> = this.breachAlertService.breachAlerts$;
  displayedColumns = ['severity', 'status', 'description', 'source', 'detectedDate', 'actions'];

  ngOnInit() {
    this.breachAlertService.getBreachAlerts().subscribe();
    this.accountService.getAccounts().subscribe();
  }

  getSeverityLabel(severity: BreachSeverity): string {
    return BreachSeverityLabels[severity];
  }

  getSeverityClass(severity: BreachSeverity): string {
    switch (severity) {
      case BreachSeverity.Critical: return 'critical';
      case BreachSeverity.High: return 'high';
      case BreachSeverity.Medium: return 'medium';
      case BreachSeverity.Low: return 'low';
      default: return '';
    }
  }

  getStatusLabel(status: AlertStatus): string {
    return AlertStatusLabels[status];
  }

  getStatusClass(status: AlertStatus): string {
    switch (status) {
      case AlertStatus.New: return 'new';
      case AlertStatus.Acknowledged: return 'acknowledged';
      case AlertStatus.Resolved: return 'resolved';
      case AlertStatus.Dismissed: return 'dismissed';
      default: return '';
    }
  }

  openDialog(alert?: BreachAlert) {
    const dialogRef = this.dialog.open(BreachAlertFormDialog, {
      width: '600px',
      data: { alert }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (alert) {
          const updateData: UpdateBreachAlert = {
            breachAlertId: alert.breachAlertId,
            ...result
          };
          this.breachAlertService.updateBreachAlert(updateData).subscribe();
        } else {
          const createData: CreateBreachAlert = {
            ...result
          };
          this.breachAlertService.createBreachAlert(createData).subscribe();
        }
      }
    });
  }

  delete(id: string) {
    if (confirm('Are you sure you want to delete this breach alert?')) {
      this.breachAlertService.deleteBreachAlert(id).subscribe();
    }
  }
}
