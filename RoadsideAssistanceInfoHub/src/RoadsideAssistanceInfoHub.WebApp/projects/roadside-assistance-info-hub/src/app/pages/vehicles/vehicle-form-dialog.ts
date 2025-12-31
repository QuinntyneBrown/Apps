import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { Vehicle } from '../../models';

@Component({
  selector: 'app-vehicle-form-dialog',
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
    <h2 mat-dialog-title>{{ data ? 'Edit Vehicle' : 'Add Vehicle' }}</h2>
    <mat-dialog-content>
      <form [formGroup]="form" class="vehicle-form">
        <mat-form-field class="vehicle-form__field">
          <mat-label>Make</mat-label>
          <input matInput formControlName="make" required>
        </mat-form-field>

        <mat-form-field class="vehicle-form__field">
          <mat-label>Model</mat-label>
          <input matInput formControlName="model" required>
        </mat-form-field>

        <mat-form-field class="vehicle-form__field">
          <mat-label>Year</mat-label>
          <input matInput type="number" formControlName="year" required>
        </mat-form-field>

        <mat-form-field class="vehicle-form__field">
          <mat-label>VIN</mat-label>
          <input matInput formControlName="vin">
        </mat-form-field>

        <mat-form-field class="vehicle-form__field">
          <mat-label>License Plate</mat-label>
          <input matInput formControlName="licensePlate">
        </mat-form-field>

        <mat-form-field class="vehicle-form__field">
          <mat-label>Color</mat-label>
          <input matInput formControlName="color">
        </mat-form-field>

        <mat-form-field class="vehicle-form__field">
          <mat-label>Current Mileage</mat-label>
          <input matInput type="number" formControlName="currentMileage">
        </mat-form-field>

        <mat-form-field class="vehicle-form__field">
          <mat-label>Owner Name</mat-label>
          <input matInput formControlName="ownerName">
        </mat-form-field>

        <mat-form-field class="vehicle-form__field vehicle-form__field--full">
          <mat-label>Notes</mat-label>
          <textarea matInput formControlName="notes" rows="3"></textarea>
        </mat-form-field>

        <mat-checkbox formControlName="isActive">Active</mat-checkbox>
      </form>
    </mat-dialog-content>
    <mat-dialog-actions align="end">
      <button mat-button (click)="dialogRef.close()">Cancel</button>
      <button mat-raised-button color="primary" (click)="onSubmit()" [disabled]="!form.valid">
        {{ data ? 'Update' : 'Create' }}
      </button>
    </mat-dialog-actions>
  `,
  styles: [`
    .vehicle-form {
      display: grid;
      grid-template-columns: 1fr 1fr;
      gap: 1rem;
      padding: 1rem 0;

      &__field {
        width: 100%;

        &--full {
          grid-column: 1 / -1;
        }
      }
    }

    mat-checkbox {
      grid-column: 1 / -1;
    }
  `]
})
export class VehicleFormDialog {
  private _fb = inject(FormBuilder);
  public dialogRef = inject(MatDialogRef<VehicleFormDialog>);
  public data: Vehicle | null = inject(MAT_DIALOG_DATA);

  form: FormGroup;

  constructor() {
    this.form = this._fb.group({
      make: [this.data?.make || '', Validators.required],
      model: [this.data?.model || '', Validators.required],
      year: [this.data?.year || new Date().getFullYear(), Validators.required],
      vin: [this.data?.vin || ''],
      licensePlate: [this.data?.licensePlate || ''],
      color: [this.data?.color || ''],
      currentMileage: [this.data?.currentMileage || null],
      ownerName: [this.data?.ownerName || ''],
      notes: [this.data?.notes || ''],
      isActive: [this.data?.isActive ?? true]
    });
  }

  onSubmit(): void {
    if (this.form.valid) {
      this.dialogRef.close(this.form.value);
    }
  }
}
