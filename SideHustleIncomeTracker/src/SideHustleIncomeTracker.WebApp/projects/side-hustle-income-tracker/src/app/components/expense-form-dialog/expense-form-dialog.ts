import { Component, inject, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { Expense } from '../../models';
import { ExpenseService, BusinessService } from '../../services';

@Component({
  selector: 'app-expense-form-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatCheckboxModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatSelectModule
  ],
  templateUrl: './expense-form-dialog.html',
  styleUrl: './expense-form-dialog.scss'
})
export class ExpenseFormDialog implements OnInit {
  private _fb = inject(FormBuilder);
  private _expenseService = inject(ExpenseService);
  private _businessService = inject(BusinessService);
  private _dialogRef = inject(MatDialogRef<ExpenseFormDialog>);

  form: FormGroup;
  isEditMode: boolean;
  businesses$ = this._businessService.businesses$;

  constructor(@Inject(MAT_DIALOG_DATA) public data: { expense?: Expense }) {
    this.isEditMode = !!data?.expense;

    this.form = this._fb.group({
      businessId: [data?.expense?.businessId || '', Validators.required],
      description: [data?.expense?.description || '', Validators.required],
      amount: [data?.expense?.amount || 0, [Validators.required, Validators.min(0)]],
      expenseDate: [data?.expense?.expenseDate ? new Date(data.expense.expenseDate) : new Date(), Validators.required],
      category: [data?.expense?.category || ''],
      vendor: [data?.expense?.vendor || ''],
      isTaxDeductible: [data?.expense?.isTaxDeductible ?? true],
      notes: [data?.expense?.notes || '']
    });
  }

  ngOnInit(): void {
    this._businessService.getAll().subscribe();
  }

  onSubmit(): void {
    if (this.form.invalid) {
      return;
    }

    const formValue = this.form.value;
    const expenseData = {
      ...formValue,
      expenseDate: formValue.expenseDate.toISOString()
    };

    if (this.isEditMode && this.data.expense) {
      this._expenseService.update(this.data.expense.expenseId, expenseData).subscribe({
        next: () => this._dialogRef.close(true),
        error: (error) => console.error('Error updating expense:', error)
      });
    } else {
      this._expenseService.create(expenseData).subscribe({
        next: () => this._dialogRef.close(true),
        error: (error) => console.error('Error creating expense:', error)
      });
    }
  }

  onCancel(): void {
    this._dialogRef.close(false);
  }
}
