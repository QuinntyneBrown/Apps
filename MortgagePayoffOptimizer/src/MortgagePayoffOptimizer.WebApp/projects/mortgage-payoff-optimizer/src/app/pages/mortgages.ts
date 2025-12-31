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
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatNativeDateModule } from '@angular/material/core';
import { MatCardModule } from '@angular/material/card';
import { MortgageService } from '../services';
import { Mortgage, CreateMortgage, UpdateMortgage, MortgageType, MortgageTypeLabels } from '../models';

@Component({
  selector: 'app-mortgage-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatDatepickerModule,
    MatCheckboxModule,
    MatButtonModule,
    MatNativeDateModule
  ],
  template: `
    <h2 mat-dialog-title>{{ data ? 'Edit Mortgage' : 'Add Mortgage' }}</h2>
    <mat-dialog-content>
      <form [formGroup]="form" class="mortgage-form">
        <mat-form-field class="mortgage-form__field">
          <mat-label>Property Address</mat-label>
          <input matInput formControlName="propertyAddress" required>
        </mat-form-field>

        <mat-form-field class="mortgage-form__field">
          <mat-label>Lender</mat-label>
          <input matInput formControlName="lender" required>
        </mat-form-field>

        <mat-form-field class="mortgage-form__field">
          <mat-label>Original Loan Amount</mat-label>
          <input matInput type="number" formControlName="originalLoanAmount" required>
        </mat-form-field>

        <mat-form-field class="mortgage-form__field">
          <mat-label>Current Balance</mat-label>
          <input matInput type="number" formControlName="currentBalance" required>
        </mat-form-field>

        <mat-form-field class="mortgage-form__field">
          <mat-label>Interest Rate (%)</mat-label>
          <input matInput type="number" formControlName="interestRate" required>
        </mat-form-field>

        <mat-form-field class="mortgage-form__field">
          <mat-label>Loan Term (Years)</mat-label>
          <input matInput type="number" formControlName="loanTermYears" required>
        </mat-form-field>

        <mat-form-field class="mortgage-form__field">
          <mat-label>Monthly Payment</mat-label>
          <input matInput type="number" formControlName="monthlyPayment" required>
        </mat-form-field>

        <mat-form-field class="mortgage-form__field">
          <mat-label>Start Date</mat-label>
          <input matInput [matDatepicker]="picker" formControlName="startDate" required>
          <mat-datepicker-toggle matSuffix [for]="picker"></mat-datepicker-toggle>
          <mat-datepicker #picker></mat-datepicker>
        </mat-form-field>

        <mat-form-field class="mortgage-form__field">
          <mat-label>Mortgage Type</mat-label>
          <mat-select formControlName="mortgageType" required>
            <mat-option *ngFor="let type of mortgageTypes" [value]="type.value">
              {{ type.label }}
            </mat-option>
          </mat-select>
        </mat-form-field>

        <mat-checkbox formControlName="isActive" class="mortgage-form__checkbox">
          Active
        </mat-checkbox>

        <mat-form-field class="mortgage-form__field">
          <mat-label>Notes</mat-label>
          <textarea matInput formControlName="notes" rows="3"></textarea>
        </mat-form-field>
      </form>
    </mat-dialog-content>
    <mat-dialog-actions align="end">
      <button mat-button mat-dialog-close>Cancel</button>
      <button mat-raised-button color="primary" (click)="save()" [disabled]="!form.valid">
        Save
      </button>
    </mat-dialog-actions>
  `,
  styles: [`
    .mortgage-form {
      display: flex;
      flex-direction: column;
      gap: 1rem;
      min-width: 500px;
    }

    .mortgage-form__field {
      width: 100%;
    }

    .mortgage-form__checkbox {
      margin: 0.5rem 0;
    }
  `]
})
export class MortgageDialog {
  private fb = inject(FormBuilder);
  private dialogRef = inject(MatDialog);

  data: Mortgage | null = null;
  form: FormGroup;
  mortgageTypes = [
    { value: MortgageType.Fixed, label: MortgageTypeLabels[MortgageType.Fixed] },
    { value: MortgageType.ARM, label: MortgageTypeLabels[MortgageType.ARM] },
    { value: MortgageType.FHA, label: MortgageTypeLabels[MortgageType.FHA] },
    { value: MortgageType.VA, label: MortgageTypeLabels[MortgageType.VA] },
    { value: MortgageType.USDA, label: MortgageTypeLabels[MortgageType.USDA] }
  ];

  constructor() {
    this.form = this.fb.group({
      propertyAddress: ['', Validators.required],
      lender: ['', Validators.required],
      originalLoanAmount: [0, [Validators.required, Validators.min(0)]],
      currentBalance: [0, [Validators.required, Validators.min(0)]],
      interestRate: [0, [Validators.required, Validators.min(0)]],
      loanTermYears: [0, [Validators.required, Validators.min(1)]],
      monthlyPayment: [0, [Validators.required, Validators.min(0)]],
      startDate: ['', Validators.required],
      mortgageType: [MortgageType.Fixed, Validators.required],
      isActive: [true],
      notes: ['']
    });

    if (this.data) {
      this.form.patchValue({
        ...this.data,
        startDate: new Date(this.data.startDate)
      });
    }
  }

  save(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const mortgage = {
        ...formValue,
        startDate: formValue.startDate.toISOString()
      };

      // Dialog will close with the form value
      // The parent component will handle the actual save
    }
  }
}

@Component({
  selector: 'app-mortgages',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatDialogModule
  ],
  template: `
    <div class="mortgages">
      <div class="mortgages__header">
        <h1 class="mortgages__title">Mortgages</h1>
        <button mat-raised-button color="primary" (click)="openDialog()" class="mortgages__add-btn">
          <mat-icon>add</mat-icon>
          Add Mortgage
        </button>
      </div>

      <mat-card class="mortgages__card">
        <table mat-table [dataSource]="mortgages$ | async" class="mortgages__table">
          <ng-container matColumnDef="propertyAddress">
            <th mat-header-cell *matHeaderCellDef>Property Address</th>
            <td mat-cell *matCellDef="let mortgage">{{ mortgage.propertyAddress }}</td>
          </ng-container>

          <ng-container matColumnDef="lender">
            <th mat-header-cell *matHeaderCellDef>Lender</th>
            <td mat-cell *matCellDef="let mortgage">{{ mortgage.lender }}</td>
          </ng-container>

          <ng-container matColumnDef="currentBalance">
            <th mat-header-cell *matHeaderCellDef>Current Balance</th>
            <td mat-cell *matCellDef="let mortgage">{{ mortgage.currentBalance | currency }}</td>
          </ng-container>

          <ng-container matColumnDef="interestRate">
            <th mat-header-cell *matHeaderCellDef>Interest Rate</th>
            <td mat-cell *matCellDef="let mortgage">{{ mortgage.interestRate }}%</td>
          </ng-container>

          <ng-container matColumnDef="monthlyPayment">
            <th mat-header-cell *matHeaderCellDef>Monthly Payment</th>
            <td mat-cell *matCellDef="let mortgage">{{ mortgage.monthlyPayment | currency }}</td>
          </ng-container>

          <ng-container matColumnDef="mortgageType">
            <th mat-header-cell *matHeaderCellDef>Type</th>
            <td mat-cell *matCellDef="let mortgage">{{ getMortgageTypeLabel(mortgage.mortgageType) }}</td>
          </ng-container>

          <ng-container matColumnDef="isActive">
            <th mat-header-cell *matHeaderCellDef>Status</th>
            <td mat-cell *matCellDef="let mortgage">
              <span [class.mortgages__status--active]="mortgage.isActive"
                    [class.mortgages__status--inactive]="!mortgage.isActive">
                {{ mortgage.isActive ? 'Active' : 'Inactive' }}
              </span>
            </td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let mortgage">
              <button mat-icon-button (click)="openDialog(mortgage)" class="mortgages__action-btn">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" (click)="delete(mortgage.mortgageId)" class="mortgages__action-btn">
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
    .mortgages {
      padding: 2rem;
    }

    .mortgages__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 2rem;
    }

    .mortgages__title {
      margin: 0;
      font-size: 2rem;
      font-weight: 400;
    }

    .mortgages__add-btn {
      display: flex;
      align-items: center;
      gap: 0.5rem;
    }

    .mortgages__card {
      overflow-x: auto;
    }

    .mortgages__table {
      width: 100%;
    }

    .mortgages__status--active {
      color: #4caf50;
      font-weight: 500;
    }

    .mortgages__status--inactive {
      color: #f44336;
      font-weight: 500;
    }

    .mortgages__action-btn {
      margin-right: 0.5rem;
    }
  `]
})
export class Mortgages implements OnInit {
  private mortgageService = inject(MortgageService);
  private dialog = inject(MatDialog);

  mortgages$ = this.mortgageService.mortgages$;
  displayedColumns = ['propertyAddress', 'lender', 'currentBalance', 'interestRate', 'monthlyPayment', 'mortgageType', 'isActive', 'actions'];

  ngOnInit(): void {
    this.mortgageService.getMortgages().subscribe();
  }

  openDialog(mortgage?: Mortgage): void {
    const dialogRef = this.dialog.open(MortgageDialog, {
      width: '600px',
      data: mortgage
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (mortgage) {
          const updateMortgage: UpdateMortgage = {
            mortgageId: mortgage.mortgageId,
            ...result
          };
          this.mortgageService.updateMortgage(updateMortgage).subscribe();
        } else {
          const createMortgage: CreateMortgage = result;
          this.mortgageService.createMortgage(createMortgage).subscribe();
        }
      }
    });
  }

  delete(id: string): void {
    if (confirm('Are you sure you want to delete this mortgage?')) {
      this.mortgageService.deleteMortgage(id).subscribe();
    }
  }

  getMortgageTypeLabel(type: MortgageType): string {
    return MortgageTypeLabels[type];
  }
}
