import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { CommonModule } from '@angular/common';
import { Vehicle, VehicleType } from '../../models';

export interface VehicleDialogData {
  vehicle?: Vehicle;
  mode: 'create' | 'edit';
}

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
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  templateUrl: './vehicle-dialog.html',
  styleUrl: './vehicle-dialog.scss'
})
export class VehicleDialog implements OnInit {
  private fb = inject(FormBuilder);
  private dialogRef = inject(MatDialogRef<VehicleDialog>);
  public data = inject<VehicleDialogData>(MAT_DIALOG_DATA);

  vehicleForm!: FormGroup;
  vehicleTypes = Object.values(VehicleType);

  ngOnInit(): void {
    this.vehicleForm = this.fb.group({
      make: [this.data.vehicle?.make || '', Validators.required],
      model: [this.data.vehicle?.model || '', Validators.required],
      year: [this.data.vehicle?.year || new Date().getFullYear(), [Validators.required, Validators.min(1900), Validators.max(new Date().getFullYear() + 1)]],
      vin: [this.data.vehicle?.vin || ''],
      licensePlate: [this.data.vehicle?.licensePlate || ''],
      vehicleType: [this.data.vehicle?.vehicleType || VehicleType.Sedan, Validators.required],
      currentMileage: [this.data.vehicle?.currentMileage || 0, [Validators.required, Validators.min(0)]],
      purchaseDate: [this.data.vehicle?.purchaseDate ? new Date(this.data.vehicle.purchaseDate) : null],
      notes: [this.data.vehicle?.notes || '']
    });
  }

  onSubmit(): void {
    if (this.vehicleForm.valid) {
      const formValue = this.vehicleForm.value;
      const result = {
        ...formValue,
        purchaseDate: formValue.purchaseDate ? formValue.purchaseDate.toISOString() : null
      };
      this.dialogRef.close(result);
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
