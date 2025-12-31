import { Component, Inject, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { ExpenseClaim, ExpenseClaimStatus, ExpenseCategoryType } from '../../models';

@Component({
  selector: 'app-expense-claim-dialog',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
  ],
  templateUrl: './expense-claim-dialog.html',
  styleUrl: './expense-claim-dialog.scss',
})
export class ExpenseClaimDialog {
  private readonly _fb = inject(FormBuilder);
  private readonly _dialogRef = inject(MatDialogRef<ExpenseClaimDialog>);

  form: FormGroup;
  statuses = Object.values(ExpenseClaimStatus);
  categoryTypes = Object.values(ExpenseCategoryType);

  constructor(@Inject(MAT_DIALOG_DATA) public data: { expenseClaim?: ExpenseClaim; employees: any[] }) {
    this.form = this._fb.group({
      employeeId: [data.expenseClaim?.employeeId || '', Validators.required],
      title: [data.expenseClaim?.title || '', Validators.required],
      description: [data.expenseClaim?.description || ''],
      amount: [data.expenseClaim?.amount || 0, [Validators.required, Validators.min(0)]],
      categoryType: [data.expenseClaim?.categoryType || '', Validators.required],
      expenseDate: [data.expenseClaim?.expenseDate || new Date(), Validators.required],
      status: [data.expenseClaim?.status || ExpenseClaimStatus.Draft, Validators.required],
      receiptUrl: [data.expenseClaim?.receiptUrl || ''],
    });
  }

  onSubmit(): void {
    if (this.form.valid) {
      this._dialogRef.close(this.form.value);
    }
  }

  onCancel(): void {
    this._dialogRef.close();
  }
}
