import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MarketComparison } from '../../models';

@Component({
  selector: 'app-market-comparison-form-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  templateUrl: './market-comparison-form-dialog.html',
  styleUrl: './market-comparison-form-dialog.scss'
})
export class MarketComparisonFormDialog implements OnInit {
  form!: FormGroup;

  constructor(
    private _fb: FormBuilder,
    private _dialogRef: MatDialogRef<MarketComparisonFormDialog>,
    @Inject(MAT_DIALOG_DATA) public data: MarketComparison | null
  ) {}

  ngOnInit(): void {
    this.form = this._fb.group({
      userId: [this.data?.userId || '', Validators.required],
      jobTitle: [this.data?.jobTitle || '', Validators.required],
      location: [this.data?.location || '', Validators.required],
      experienceLevel: [this.data?.experienceLevel || ''],
      minSalary: [this.data?.minSalary],
      maxSalary: [this.data?.maxSalary],
      medianSalary: [this.data?.medianSalary],
      dataSource: [this.data?.dataSource || ''],
      comparisonDate: [this.data?.comparisonDate ? new Date(this.data.comparisonDate) : new Date(), Validators.required],
      notes: [this.data?.notes || '']
    });
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      this._dialogRef.close({
        ...formValue,
        comparisonDate: formValue.comparisonDate.toISOString()
      });
    }
  }

  onCancel(): void {
    this._dialogRef.close();
  }
}
