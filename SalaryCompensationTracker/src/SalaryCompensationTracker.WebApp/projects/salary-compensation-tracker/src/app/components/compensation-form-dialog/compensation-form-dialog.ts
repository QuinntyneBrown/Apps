import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { Compensation, CompensationType } from '../../models';

@Component({
  selector: 'app-compensation-form-dialog',
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
  templateUrl: './compensation-form-dialog.html',
  styleUrl: './compensation-form-dialog.scss'
})
export class CompensationFormDialog implements OnInit {
  form!: FormGroup;
  compensationTypes = Object.values(CompensationType);

  constructor(
    private _fb: FormBuilder,
    private _dialogRef: MatDialogRef<CompensationFormDialog>,
    @Inject(MAT_DIALOG_DATA) public data: Compensation | null
  ) {}

  ngOnInit(): void {
    this.form = this._fb.group({
      userId: [this.data?.userId || '', Validators.required],
      compensationType: [this.data?.compensationType || CompensationType.FullTime, Validators.required],
      employer: [this.data?.employer || '', Validators.required],
      jobTitle: [this.data?.jobTitle || '', Validators.required],
      baseSalary: [this.data?.baseSalary || 0, [Validators.required, Validators.min(0)]],
      currency: [this.data?.currency || 'USD', Validators.required],
      bonus: [this.data?.bonus],
      stockValue: [this.data?.stockValue],
      otherCompensation: [this.data?.otherCompensation],
      effectiveDate: [this.data?.effectiveDate ? new Date(this.data.effectiveDate) : new Date(), Validators.required],
      endDate: [this.data?.endDate ? new Date(this.data.endDate) : null],
      notes: [this.data?.notes || '']
    });
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const totalCompensation =
        (formValue.baseSalary || 0) +
        (formValue.bonus || 0) +
        (formValue.stockValue || 0) +
        (formValue.otherCompensation || 0);

      this._dialogRef.close({
        ...formValue,
        totalCompensation,
        effectiveDate: formValue.effectiveDate.toISOString(),
        endDate: formValue.endDate ? formValue.endDate.toISOString() : null
      });
    }
  }

  onCancel(): void {
    this._dialogRef.close();
  }
}
