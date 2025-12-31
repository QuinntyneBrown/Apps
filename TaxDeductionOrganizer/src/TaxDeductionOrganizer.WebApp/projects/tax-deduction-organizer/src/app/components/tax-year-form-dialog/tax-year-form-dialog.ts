import { Component, Inject, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatDialogModule, MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { TaxYear } from '../../models';

export interface TaxYearFormDialogData {
  taxYear?: TaxYear;
}

@Component({
  selector: 'app-tax-year-form-dialog',
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
    MatNativeDateModule
  ],
  templateUrl: './tax-year-form-dialog.html',
  styleUrl: './tax-year-form-dialog.scss'
})
export class TaxYearFormDialog {
  private _fb = inject(FormBuilder);
  private _dialogRef = inject(MatDialogRef<TaxYearFormDialog>);

  taxYearForm: FormGroup;
  isEdit: boolean;

  constructor(@Inject(MAT_DIALOG_DATA) public data: TaxYearFormDialogData) {
    this.isEdit = !!data.taxYear;

    this.taxYearForm = this._fb.group({
      taxYearId: [data.taxYear?.taxYearId || ''],
      year: [data.taxYear?.year || new Date().getFullYear(), [Validators.required, Validators.min(1900), Validators.max(2100)]],
      isFiled: [data.taxYear?.isFiled || false],
      filingDate: [data.taxYear?.filingDate ? new Date(data.taxYear.filingDate) : null],
      notes: [data.taxYear?.notes || '']
    });
  }

  onSubmit(): void {
    if (this.taxYearForm.valid) {
      const formValue = this.taxYearForm.value;
      const result = {
        ...formValue,
        filingDate: formValue.filingDate ? formValue.filingDate.toISOString() : null
      };
      this._dialogRef.close(result);
    }
  }

  onCancel(): void {
    this._dialogRef.close();
  }
}
