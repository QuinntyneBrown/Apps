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
import { Vehicle } from '../../models';

@Component({
  selector: 'app-vehicle-dialog',
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
  templateUrl: './vehicle-dialog.html',
  styleUrl: './vehicle-dialog.scss'
})
export class VehicleDialog {
  private _fb = inject(FormBuilder);
  form: FormGroup;

  constructor(
    public dialogRef: MatDialogRef<VehicleDialog>,
    @Inject(MAT_DIALOG_DATA) public data: { vehicle?: Vehicle }
  ) {
    this.form = this._fb.group({
      make: [data.vehicle?.make || '', Validators.required],
      model: [data.vehicle?.model || '', Validators.required],
      year: [data.vehicle?.year || new Date().getFullYear(), [Validators.required, Validators.min(1900), Validators.max(2100)]],
      trim: [data.vehicle?.trim || ''],
      vin: [data.vehicle?.vin || ''],
      currentMileage: [data.vehicle?.currentMileage || 0, [Validators.required, Validators.min(0)]],
      purchasePrice: [data.vehicle?.purchasePrice || null],
      purchaseDate: [data.vehicle?.purchaseDate ? new Date(data.vehicle.purchaseDate) : null],
      color: [data.vehicle?.color || ''],
      interiorType: [data.vehicle?.interiorType || ''],
      engineType: [data.vehicle?.engineType || ''],
      transmission: [data.vehicle?.transmission || ''],
      isCurrentlyOwned: [data.vehicle?.isCurrentlyOwned ?? true],
      notes: [data.vehicle?.notes || '']
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
