import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { Expense, ExpenseCategory } from '../../models';

@Component({
  selector: 'app-expense-dialog',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatSelectModule,
    MatCheckboxModule,
  ],
  templateUrl: './expense-dialog.html',
  styleUrl: './expense-dialog.scss',
})
export class ExpenseDialog implements OnInit {
  private readonly _dialogRef = inject(MatDialogRef<ExpenseDialog>);
  private readonly _data = inject<{ expense?: Expense; budgetId?: string } | null>(MAT_DIALOG_DATA);
  private readonly _fb = inject(FormBuilder);

  form!: FormGroup;
  isEdit = false;
  categories = [
    { value: ExpenseCategory.Housing, label: 'Housing' },
    { value: ExpenseCategory.Transportation, label: 'Transportation' },
    { value: ExpenseCategory.Food, label: 'Food' },
    { value: ExpenseCategory.Healthcare, label: 'Healthcare' },
    { value: ExpenseCategory.Entertainment, label: 'Entertainment' },
    { value: ExpenseCategory.PersonalCare, label: 'Personal Care' },
    { value: ExpenseCategory.Education, label: 'Education' },
    { value: ExpenseCategory.DebtPayment, label: 'Debt Payment' },
    { value: ExpenseCategory.Savings, label: 'Savings' },
    { value: ExpenseCategory.Other, label: 'Other' },
  ];

  ngOnInit(): void {
    const expense = this._data?.expense;
    this.isEdit = !!expense;
    this.form = this._fb.group({
      budgetId: [expense?.budgetId || this._data?.budgetId || '', Validators.required],
      description: [expense?.description || '', Validators.required],
      amount: [expense?.amount || 0, [Validators.required, Validators.min(0.01)]],
      category: [expense?.category ?? ExpenseCategory.Other, Validators.required],
      expenseDate: [expense?.expenseDate ? new Date(expense.expenseDate) : new Date(), Validators.required],
      payee: [expense?.payee || ''],
      paymentMethod: [expense?.paymentMethod || ''],
      notes: [expense?.notes || ''],
      isRecurring: [expense?.isRecurring || false],
    });
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const result: Partial<Expense> = {
        ...formValue,
        expenseDate: formValue.expenseDate?.toISOString(),
      };

      if (this.isEdit && this._data?.expense) {
        result.expenseId = this._data.expense.expenseId;
      }

      this._dialogRef.close(result);
    }
  }

  onCancel(): void {
    this._dialogRef.close();
  }
}
