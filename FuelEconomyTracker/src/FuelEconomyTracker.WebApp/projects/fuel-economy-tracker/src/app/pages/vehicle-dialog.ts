import { Component, Inject, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatSnackBar } from '@angular/material/snack-bar';
import { VehiclesService } from '../services';
import { Vehicle } from '../models';

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
    MatCheckboxModule
  ],
  template: `
    <h2 mat-dialog-title>{{ isEdit ? 'Edit Vehicle' : 'Add Vehicle' }}</h2>
    <form [formGroup]="form" (ngSubmit)="onSubmit()">
      <mat-dialog-content class="vehicle-dialog__content">
        <mat-form-field appearance="outline" class="vehicle-dialog__field">
          <mat-label>Make</mat-label>
          <input matInput formControlName="make" required>
        </mat-form-field>

        <mat-form-field appearance="outline" class="vehicle-dialog__field">
          <mat-label>Model</mat-label>
          <input matInput formControlName="model" required>
        </mat-form-field>

        <mat-form-field appearance="outline" class="vehicle-dialog__field">
          <mat-label>Year</mat-label>
          <input matInput type="number" formControlName="year" required>
        </mat-form-field>

        <mat-form-field appearance="outline" class="vehicle-dialog__field">
          <mat-label>VIN</mat-label>
          <input matInput formControlName="vin">
        </mat-form-field>

        <mat-form-field appearance="outline" class="vehicle-dialog__field">
          <mat-label>License Plate</mat-label>
          <input matInput formControlName="licensePlate">
        </mat-form-field>

        <mat-form-field appearance="outline" class="vehicle-dialog__field">
          <mat-label>Tank Capacity (gallons)</mat-label>
          <input matInput type="number" formControlName="tankCapacity">
        </mat-form-field>

        <mat-form-field appearance="outline" class="vehicle-dialog__field">
          <mat-label>EPA City MPG</mat-label>
          <input matInput type="number" formControlName="epaCityMPG">
        </mat-form-field>

        <mat-form-field appearance="outline" class="vehicle-dialog__field">
          <mat-label>EPA Highway MPG</mat-label>
          <input matInput type="number" formControlName="epaHighwayMPG">
        </mat-form-field>

        <mat-checkbox formControlName="isActive" *ngIf="isEdit" class="vehicle-dialog__checkbox">
          Active
        </mat-checkbox>
      </mat-dialog-content>

      <mat-dialog-actions align="end">
        <button mat-button type="button" (click)="onCancel()">Cancel</button>
        <button mat-raised-button color="primary" type="submit" [disabled]="form.invalid">
          {{ isEdit ? 'Update' : 'Create' }}
        </button>
      </mat-dialog-actions>
    </form>
  `,
  styles: [`
    .vehicle-dialog__content {
      display: flex;
      flex-direction: column;
      min-width: 500px;
      padding: 1rem 0;
    }

    .vehicle-dialog__field {
      width: 100%;
      margin-bottom: 1rem;
    }

    .vehicle-dialog__checkbox {
      margin-bottom: 1rem;
    }
  `]
})
export class VehicleDialog {
  private fb = inject(FormBuilder);
  private vehiclesService = inject(VehiclesService);
  private snackBar = inject(MatSnackBar);
  private dialogRef = inject(MatDialogRef<VehicleDialog>);

  isEdit = false;
  form: FormGroup;

  constructor(@Inject(MAT_DIALOG_DATA) public data: Vehicle | null) {
    this.isEdit = !!data;
    this.form = this.fb.group({
      make: [data?.make || '', Validators.required],
      model: [data?.model || '', Validators.required],
      year: [data?.year || new Date().getFullYear(), Validators.required],
      vin: [data?.vin || ''],
      licensePlate: [data?.licensePlate || ''],
      tankCapacity: [data?.tankCapacity || null],
      epaCityMPG: [data?.epaCityMPG || null],
      epaHighwayMPG: [data?.epaHighwayMPG || null],
      isActive: [data?.isActive ?? true]
    });
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const request = {
        make: formValue.make,
        model: formValue.model,
        year: formValue.year,
        vin: formValue.vin || undefined,
        licensePlate: formValue.licensePlate || undefined,
        tankCapacity: formValue.tankCapacity || undefined,
        epaCityMPG: formValue.epaCityMPG || undefined,
        epaHighwayMPG: formValue.epaHighwayMPG || undefined,
        isActive: formValue.isActive
      };

      if (this.isEdit && this.data) {
        this.vehiclesService.update(this.data.vehicleId, request).subscribe({
          next: () => {
            this.snackBar.open('Vehicle updated successfully', 'Close', { duration: 3000 });
            this.dialogRef.close(true);
          },
          error: () => {
            this.snackBar.open('Error updating vehicle', 'Close', { duration: 3000 });
          }
        });
      } else {
        this.vehiclesService.create(request).subscribe({
          next: () => {
            this.snackBar.open('Vehicle created successfully', 'Close', { duration: 3000 });
            this.dialogRef.close(true);
          },
          error: () => {
            this.snackBar.open('Error creating vehicle', 'Close', { duration: 3000 });
          }
        });
      }
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
