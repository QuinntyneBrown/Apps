import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatCardModule } from '@angular/material/card';
import { CompanyService } from '../services';
import { Company, CreateCompany, UpdateCompany } from '../models';

@Component({
  selector: 'app-company-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatCheckboxModule,
    MatButtonModule
  ],
  template: `
    <h2 mat-dialog-title>{{ data ? 'Edit Company' : 'Add Company' }}</h2>
    <mat-dialog-content>
      <form [formGroup]="form" class="company-dialog__form">
        <mat-form-field class="company-dialog__field">
          <mat-label>Name</mat-label>
          <input matInput formControlName="name" required>
        </mat-form-field>

        <mat-form-field class="company-dialog__field">
          <mat-label>Industry</mat-label>
          <input matInput formControlName="industry">
        </mat-form-field>

        <mat-form-field class="company-dialog__field">
          <mat-label>Website</mat-label>
          <input matInput formControlName="website" type="url">
        </mat-form-field>

        <mat-form-field class="company-dialog__field">
          <mat-label>Location</mat-label>
          <input matInput formControlName="location">
        </mat-form-field>

        <mat-form-field class="company-dialog__field">
          <mat-label>Company Size</mat-label>
          <input matInput formControlName="companySize">
        </mat-form-field>

        <mat-form-field class="company-dialog__field">
          <mat-label>Culture Notes</mat-label>
          <textarea matInput formControlName="cultureNotes" rows="3"></textarea>
        </mat-form-field>

        <mat-form-field class="company-dialog__field">
          <mat-label>Research Notes</mat-label>
          <textarea matInput formControlName="researchNotes" rows="3"></textarea>
        </mat-form-field>

        <mat-checkbox formControlName="isTargetCompany">
          Target Company
        </mat-checkbox>
      </form>
    </mat-dialog-content>
    <mat-dialog-actions align="end">
      <button mat-button mat-dialog-close>Cancel</button>
      <button mat-raised-button color="primary" [disabled]="!form.valid" (click)="save()">
        Save
      </button>
    </mat-dialog-actions>
  `,
  styles: [`
    .company-dialog__form {
      display: flex;
      flex-direction: column;
      gap: 16px;
      min-width: 500px;
      padding: 16px 0;
    }

    .company-dialog__field {
      width: 100%;
    }
  `]
})
export class CompanyDialog {
  private fb = inject(FormBuilder);
  private dialogRef = inject(MatDialog);

  data?: Company;
  form: FormGroup;

  constructor() {
    this.form = this.fb.group({
      name: ['', Validators.required],
      industry: [''],
      website: [''],
      location: [''],
      companySize: [''],
      cultureNotes: [''],
      researchNotes: [''],
      isTargetCompany: [false]
    });

    if (this.data) {
      this.form.patchValue(this.data);
    }
  }

  save(): void {
    if (this.form.valid) {
      const result = this.form.value;
      this.dialogRef.closeAll();
    }
  }
}

@Component({
  selector: 'app-companies',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule
  ],
  template: `
    <div class="companies">
      <div class="companies__header">
        <h1 class="companies__title">Companies</h1>
        <button mat-raised-button color="primary" (click)="openDialog()">
          <mat-icon>add</mat-icon>
          Add Company
        </button>
      </div>

      <mat-card class="companies__card">
        <table mat-table [dataSource]="(companyService.companies$ | async) || []" class="companies__table">
          <ng-container matColumnDef="name">
            <th mat-header-cell *matHeaderCellDef>Name</th>
            <td mat-cell *matCellDef="let company">{{ company.name }}</td>
          </ng-container>

          <ng-container matColumnDef="industry">
            <th mat-header-cell *matHeaderCellDef>Industry</th>
            <td mat-cell *matCellDef="let company">{{ company.industry || '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="location">
            <th mat-header-cell *matHeaderCellDef>Location</th>
            <td mat-cell *matCellDef="let company">{{ company.location || '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="companySize">
            <th mat-header-cell *matHeaderCellDef>Size</th>
            <td mat-cell *matCellDef="let company">{{ company.companySize || '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="website">
            <th mat-header-cell *matHeaderCellDef>Website</th>
            <td mat-cell *matCellDef="let company">
              <a *ngIf="company.website" [href]="company.website" target="_blank" class="companies__link">
                Visit
              </a>
              <span *ngIf="!company.website">-</span>
            </td>
          </ng-container>

          <ng-container matColumnDef="isTargetCompany">
            <th mat-header-cell *matHeaderCellDef>Target</th>
            <td mat-cell *matCellDef="let company">
              <mat-icon *ngIf="company.isTargetCompany" class="companies__target-icon">star</mat-icon>
            </td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let company">
              <button mat-icon-button (click)="openDialog(company)">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" (click)="deleteCompany(company.companyId)">
                <mat-icon>delete</mat-icon>
              </button>
            </td>
          </ng-container>

          <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
          <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
        </table>

        <div *ngIf="(companyService.companies$ | async)?.length === 0" class="companies__empty">
          <p>No companies found. Add your first company to get started!</p>
        </div>
      </mat-card>
    </div>
  `,
  styles: [`
    .companies {
      padding: 24px;
    }

    .companies__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 24px;
    }

    .companies__title {
      margin: 0;
      font-size: 32px;
      font-weight: 500;
    }

    .companies__card {
      overflow-x: auto;
    }

    .companies__table {
      width: 100%;
    }

    .companies__link {
      color: #1976d2;
      text-decoration: none;
    }

    .companies__link:hover {
      text-decoration: underline;
    }

    .companies__target-icon {
      color: #ffc107;
    }

    .companies__empty {
      padding: 48px;
      text-align: center;
      color: rgba(0, 0, 0, 0.6);
    }
  `]
})
export class Companies implements OnInit {
  companyService = inject(CompanyService);
  private dialog = inject(MatDialog);

  displayedColumns = ['name', 'industry', 'location', 'companySize', 'website', 'isTargetCompany', 'actions'];

  ngOnInit(): void {
    this.companyService.getCompanies().subscribe();
  }

  openDialog(company?: Company): void {
    const dialogRef = this.dialog.open(CompanyDialog, {
      width: '600px',
      data: company
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (company) {
          const updateData: UpdateCompany = {
            companyId: company.companyId,
            ...result
          };
          this.companyService.updateCompany(updateData).subscribe();
        } else {
          const createData: CreateCompany = {
            userId: '00000000-0000-0000-0000-000000000000',
            ...result
          };
          this.companyService.createCompany(createData).subscribe();
        }
      }
    });
  }

  deleteCompany(id: string): void {
    if (confirm('Are you sure you want to delete this company?')) {
      this.companyService.deleteCompany(id).subscribe();
    }
  }
}
