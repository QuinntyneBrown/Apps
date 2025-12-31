import { Component, Inject, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MarketComparison } from '../../models';

@Component({
  selector: 'app-market-comparison-dialog',
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
  templateUrl: './market-comparison-dialog.html',
  styleUrl: './market-comparison-dialog.scss'
})
export class MarketComparisonDialog {
  private _fb = inject(FormBuilder);
  form: FormGroup;

  constructor(
    public dialogRef: MatDialogRef<MarketComparisonDialog>,
    @Inject(MAT_DIALOG_DATA) public data: { comparison?: MarketComparison; vehicleId?: string }
  ) {
    this.form = this._fb.group({
      vehicleId: [data.comparison?.vehicleId || data.vehicleId || '', Validators.required],
      comparisonDate: [data.comparison?.comparisonDate ? new Date(data.comparison.comparisonDate) : new Date(), Validators.required],
      listingSource: [data.comparison?.listingSource || '', Validators.required],
      comparableYear: [data.comparison?.comparableYear || new Date().getFullYear(), [Validators.required, Validators.min(1900)]],
      comparableMake: [data.comparison?.comparableMake || '', Validators.required],
      comparableModel: [data.comparison?.comparableModel || '', Validators.required],
      comparableTrim: [data.comparison?.comparableTrim || ''],
      comparableMileage: [data.comparison?.comparableMileage || 0, [Validators.required, Validators.min(0)]],
      askingPrice: [data.comparison?.askingPrice || 0, [Validators.required, Validators.min(0)]],
      location: [data.comparison?.location || ''],
      condition: [data.comparison?.condition || ''],
      listingUrl: [data.comparison?.listingUrl || ''],
      daysOnMarket: [data.comparison?.daysOnMarket || null],
      isActive: [data.comparison?.isActive ?? true],
      notes: [data.comparison?.notes || '']
    });
  }

  onCancel(): void {
    this.dialogRef.close();
  }

  onSave(): void {
    if (this.form.valid) {
      this.dialogRef.close(this.form.value);
    }
  }
}
