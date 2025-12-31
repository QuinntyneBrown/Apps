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
import { Budget, BudgetStatus } from '../../models';

@Component({
  selector: 'app-budget-dialog',
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
  ],
  templateUrl: './budget-dialog.html',
  styleUrl: './budget-dialog.scss',
})
export class BudgetDialog implements OnInit {
  private readonly _dialogRef = inject(MatDialogRef<BudgetDialog>);
  private readonly _data = inject<Budget | null>(MAT_DIALOG_DATA);
  private readonly _fb = inject(FormBuilder);

  form!: FormGroup;
  isEdit = false;
  BudgetStatus = BudgetStatus;
  statuses = [
    { value: BudgetStatus.Draft, label: 'Draft' },
    { value: BudgetStatus.Active, label: 'Active' },
    { value: BudgetStatus.Completed, label: 'Completed' },
  ];

  ngOnInit(): void {
    this.isEdit = !!this._data;
    this.form = this._fb.group({
      name: [this._data?.name || '', Validators.required],
      period: [this._data?.period || '', Validators.required],
      startDate: [this._data?.startDate ? new Date(this._data.startDate) : null, Validators.required],
      endDate: [this._data?.endDate ? new Date(this._data.endDate) : null, Validators.required],
      totalIncome: [this._data?.totalIncome || 0, [Validators.required, Validators.min(0)]],
      totalExpenses: [this._data?.totalExpenses || 0, [Validators.required, Validators.min(0)]],
      status: [this._data?.status ?? BudgetStatus.Draft, Validators.required],
      notes: [this._data?.notes || ''],
    });
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const result: Partial<Budget> = {
        ...formValue,
        startDate: formValue.startDate?.toISOString(),
        endDate: formValue.endDate?.toISOString(),
      };

      if (this.isEdit && this._data) {
        result.budgetId = this._data.budgetId;
      }

      this._dialogRef.close(result);
    }
  }

  onCancel(): void {
    this._dialogRef.close();
  }
}
