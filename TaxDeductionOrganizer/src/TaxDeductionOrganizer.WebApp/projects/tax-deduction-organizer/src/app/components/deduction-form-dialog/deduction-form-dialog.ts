import { Component, Inject, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatDialogModule, MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { Deduction, DeductionCategory, TaxYear } from '../../models';

export interface DeductionFormDialogData {
  deduction?: Deduction;
  taxYears: TaxYear[];
}

@Component({
  selector: 'app-deduction-form-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  templateUrl: './deduction-form-dialog.html',
  styleUrl: './deduction-form-dialog.scss'
})
export class DeductionFormDialog {
  private _fb = inject(FormBuilder);
  private _dialogRef = inject(MatDialogRef<DeductionFormDialog>);

  deductionForm: FormGroup;
  categories = Object.values(DeductionCategory).filter(v => typeof v === 'number') as DeductionCategory[];
  categoryNames: { [key: number]: string } = {
    [DeductionCategory.MedicalExpenses]: 'Medical Expenses',
    [DeductionCategory.CharitableDonations]: 'Charitable Donations',
    [DeductionCategory.MortgageInterest]: 'Mortgage Interest',
    [DeductionCategory.StateAndLocalTaxes]: 'State and Local Taxes',
    [DeductionCategory.BusinessExpenses]: 'Business Expenses',
    [DeductionCategory.EducationExpenses]: 'Education Expenses',
    [DeductionCategory.HomeOffice]: 'Home Office',
    [DeductionCategory.Other]: 'Other'
  };
  taxYears: TaxYear[];
  isEdit: boolean;

  constructor(@Inject(MAT_DIALOG_DATA) public data: DeductionFormDialogData) {
    this.taxYears = data.taxYears;
    this.isEdit = !!data.deduction;

    this.deductionForm = this._fb.group({
      deductionId: [data.deduction?.deductionId || ''],
      taxYearId: [data.deduction?.taxYearId || '', Validators.required],
      description: [data.deduction?.description || '', Validators.required],
      amount: [data.deduction?.amount || 0, [Validators.required, Validators.min(0.01)]],
      date: [data.deduction?.date ? new Date(data.deduction.date) : new Date(), Validators.required],
      category: [data.deduction?.category ?? DeductionCategory.Other, Validators.required],
      notes: [data.deduction?.notes || '']
    });
  }

  onSubmit(): void {
    if (this.deductionForm.valid) {
      const formValue = this.deductionForm.value;
      const result = {
        ...formValue,
        date: formValue.date.toISOString()
      };
      this._dialogRef.close(result);
    }
  }

  onCancel(): void {
    this._dialogRef.close();
  }
}
