import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { Income } from '../../models';

@Component({
  selector: 'app-income-dialog',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatCheckboxModule,
  ],
  templateUrl: './income-dialog.html',
  styleUrl: './income-dialog.scss',
})
export class IncomeDialog implements OnInit {
  private readonly _dialogRef = inject(MatDialogRef<IncomeDialog>);
  private readonly _data = inject<{ income?: Income; budgetId?: string } | null>(MAT_DIALOG_DATA);
  private readonly _fb = inject(FormBuilder);

  form!: FormGroup;
  isEdit = false;

  ngOnInit(): void {
    const income = this._data?.income;
    this.isEdit = !!income;
    this.form = this._fb.group({
      budgetId: [income?.budgetId || this._data?.budgetId || '', Validators.required],
      description: [income?.description || '', Validators.required],
      amount: [income?.amount || 0, [Validators.required, Validators.min(0.01)]],
      source: [income?.source || '', Validators.required],
      incomeDate: [income?.incomeDate ? new Date(income.incomeDate) : new Date(), Validators.required],
      notes: [income?.notes || ''],
      isRecurring: [income?.isRecurring || false],
    });
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const result: Partial<Income> = {
        ...formValue,
        incomeDate: formValue.incomeDate?.toISOString(),
      };

      if (this.isEdit && this._data?.income) {
        result.incomeId = this._data.income.incomeId;
      }

      this._dialogRef.close(result);
    }
  }

  onCancel(): void {
    this._dialogRef.close();
  }
}
