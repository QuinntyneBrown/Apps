import { Component, OnInit, inject } from '@angular/core';
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
import { MatNativeDateModule } from '@angular/material/core';
import { LiabilityService } from '../services';
import { Liability, LiabilityType, LiabilityTypeLabels } from '../models';

@Component({
  selector: 'app-liability-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  template: `
    <h2 mat-dialog-title>{{ data.liability ? 'Edit Liability' : 'Add Liability' }}</h2>
    <mat-dialog-content>
      <form [formGroup]="form" class="liability-dialog__form">
        <mat-form-field class="liability-dialog__field">
          <mat-label>Name</mat-label>
          <input matInput formControlName="name" required>
        </mat-form-field>

        <mat-form-field class="liability-dialog__field">
          <mat-label>Liability Type</mat-label>
          <mat-select formControlName="liabilityType" required>
            <mat-option *ngFor="let type of liabilityTypes" [value]="type.value">
              {{ type.label }}
            </mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field class="liability-dialog__field">
          <mat-label>Current Balance</mat-label>
          <input matInput type="number" formControlName="currentBalance" required>
        </mat-form-field>

        <mat-form-field class="liability-dialog__field">
          <mat-label>Original Amount</mat-label>
          <input matInput type="number" formControlName="originalAmount">
        </mat-form-field>

        <mat-form-field class="liability-dialog__field">
          <mat-label>Interest Rate (%)</mat-label>
          <input matInput type="number" formControlName="interestRate">
        </mat-form-field>

        <mat-form-field class="liability-dialog__field">
          <mat-label>Monthly Payment</mat-label>
          <input matInput type="number" formControlName="monthlyPayment">
        </mat-form-field>

        <mat-form-field class="liability-dialog__field">
          <mat-label>Creditor</mat-label>
          <input matInput formControlName="creditor">
        </mat-form-field>

        <mat-form-field class="liability-dialog__field">
          <mat-label>Account Number</mat-label>
          <input matInput formControlName="accountNumber">
        </mat-form-field>

        <mat-form-field class="liability-dialog__field">
          <mat-label>Due Date</mat-label>
          <input matInput [matDatepicker]="picker" formControlName="dueDate">
          <mat-datepicker-toggle matSuffix [for]="picker"></mat-datepicker-toggle>
          <mat-datepicker #picker></mat-datepicker>
        </mat-form-field>

        <mat-form-field class="liability-dialog__field">
          <mat-label>Notes</mat-label>
          <textarea matInput formControlName="notes" rows="3"></textarea>
        </mat-form-field>
      </form>
    </mat-dialog-content>
    <mat-dialog-actions align="end">
      <button mat-button mat-dialog-close>Cancel</button>
      <button mat-raised-button color="primary" (click)="save()" [disabled]="!form.valid">Save</button>
    </mat-dialog-actions>
  `,
  styles: [`
    .liability-dialog {
      &__form {
        display: flex;
        flex-direction: column;
        min-width: 400px;
        padding: 1rem 0;
      }

      &__field {
        width: 100%;
      }
    }
  `]
})
export class LiabilityDialog {
  private fb = inject(FormBuilder);
  private dialogRef = inject(MatDialog);

  data: { liability?: Liability } = {};
  form: FormGroup;
  liabilityTypes = Object.values(LiabilityType)
    .filter(v => typeof v === 'number')
    .map(v => ({ value: v as LiabilityType, label: LiabilityTypeLabels[v as LiabilityType] }));

  constructor() {
    this.form = this.fb.group({
      name: ['', Validators.required],
      liabilityType: [LiabilityType.CreditCard, Validators.required],
      currentBalance: [0, [Validators.required, Validators.min(0)]],
      originalAmount: [null],
      interestRate: [null],
      monthlyPayment: [null],
      creditor: [''],
      accountNumber: [''],
      dueDate: [null],
      notes: ['']
    });

    if (this.data.liability) {
      this.form.patchValue({
        ...this.data.liability,
        dueDate: this.data.liability.dueDate ? new Date(this.data.liability.dueDate) : null
      });
    }
  }

  save(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const result = {
        ...formValue,
        dueDate: formValue.dueDate ? formValue.dueDate.toISOString() : null
      };

      if (this.data.liability) {
        result.liabilityId = this.data.liability.liabilityId;
      }
    }
  }
}

@Component({
  selector: 'app-liabilities',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule
  ],
  template: `
    <div class="liabilities">
      <div class="liabilities__header">
        <h1 class="liabilities__title">Liabilities</h1>
        <button mat-raised-button color="primary" (click)="openDialog()">
          <mat-icon>add</mat-icon>
          Add Liability
        </button>
      </div>

      <div class="liabilities__table-container">
        <table mat-table [dataSource]="liabilities$ | async" class="liabilities__table">
          <ng-container matColumnDef="name">
            <th mat-header-cell *matHeaderCellDef>Name</th>
            <td mat-cell *matCellDef="let liability">{{ liability.name }}</td>
          </ng-container>

          <ng-container matColumnDef="liabilityType">
            <th mat-header-cell *matHeaderCellDef>Type</th>
            <td mat-cell *matCellDef="let liability">{{ getLiabilityTypeLabel(liability.liabilityType) }}</td>
          </ng-container>

          <ng-container matColumnDef="currentBalance">
            <th mat-header-cell *matHeaderCellDef>Current Balance</th>
            <td mat-cell *matCellDef="let liability">{{ liability.currentBalance | currency }}</td>
          </ng-container>

          <ng-container matColumnDef="interestRate">
            <th mat-header-cell *matHeaderCellDef>Interest Rate</th>
            <td mat-cell *matCellDef="let liability">{{ liability.interestRate ? (liability.interestRate + '%') : '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="monthlyPayment">
            <th mat-header-cell *matHeaderCellDef>Monthly Payment</th>
            <td mat-cell *matCellDef="let liability">{{ liability.monthlyPayment ? (liability.monthlyPayment | currency) : '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="creditor">
            <th mat-header-cell *matHeaderCellDef>Creditor</th>
            <td mat-cell *matCellDef="let liability">{{ liability.creditor || '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let liability">
              <button mat-icon-button color="primary" (click)="openDialog(liability)">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" (click)="delete(liability.liabilityId)">
                <mat-icon>delete</mat-icon>
              </button>
            </td>
          </ng-container>

          <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
          <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
        </table>
      </div>
    </div>
  `,
  styles: [`
    .liabilities {
      padding: 2rem;

      &__header {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-bottom: 2rem;
      }

      &__title {
        margin: 0;
        color: #333;
      }

      &__table-container {
        overflow-x: auto;
      }

      &__table {
        width: 100%;
        background: white;
        box-shadow: 0 2px 4px rgba(0,0,0,0.1);
      }
    }
  `]
})
export class Liabilities implements OnInit {
  private liabilityService = inject(LiabilityService);
  private dialog = inject(MatDialog);

  liabilities$ = this.liabilityService.liabilities$;
  displayedColumns = ['name', 'liabilityType', 'currentBalance', 'interestRate', 'monthlyPayment', 'creditor', 'actions'];

  ngOnInit(): void {
    this.liabilityService.getLiabilities().subscribe();
  }

  getLiabilityTypeLabel(type: LiabilityType): string {
    return LiabilityTypeLabels[type];
  }

  openDialog(liability?: Liability): void {
    const dialogRef = this.dialog.open(LiabilityDialog, {
      width: '500px',
      data: { liability }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (liability) {
          this.liabilityService.updateLiability(result).subscribe();
        } else {
          this.liabilityService.createLiability(result).subscribe();
        }
      }
    });
  }

  delete(id: string): void {
    if (confirm('Are you sure you want to delete this liability?')) {
      this.liabilityService.deleteLiability(id).subscribe();
    }
  }
}
