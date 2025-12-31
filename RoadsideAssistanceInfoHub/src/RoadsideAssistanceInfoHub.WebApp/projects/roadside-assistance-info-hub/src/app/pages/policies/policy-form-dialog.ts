import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { Policy, Vehicle } from '../../models';

@Component({
  selector: 'app-policy-form-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatCheckboxModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  template: `
    <h2 mat-dialog-title>{{ data.policy ? 'Edit Policy' : 'Add Policy' }}</h2>
    <mat-dialog-content>
      <form [formGroup]="form" class="policy-form">
        <mat-form-field class="policy-form__field policy-form__field--full">
          <mat-label>Vehicle</mat-label>
          <mat-select formControlName="vehicleId" required>
            <mat-option *ngFor="let vehicle of vehicles" [value]="vehicle.vehicleId">
              {{ vehicle.year }} {{ vehicle.make }} {{ vehicle.model }}
            </mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field class="policy-form__field">
          <mat-label>Provider</mat-label>
          <input matInput formControlName="provider" required>
        </mat-form-field>

        <mat-form-field class="policy-form__field">
          <mat-label>Policy Number</mat-label>
          <input matInput formControlName="policyNumber" required>
        </mat-form-field>

        <mat-form-field class="policy-form__field">
          <mat-label>Start Date</mat-label>
          <input matInput [matDatepicker]="startPicker" formControlName="startDate" required>
          <mat-datepicker-toggle matSuffix [for]="startPicker"></mat-datepicker-toggle>
          <mat-datepicker #startPicker></mat-datepicker>
        </mat-form-field>

        <mat-form-field class="policy-form__field">
          <mat-label>End Date</mat-label>
          <input matInput [matDatepicker]="endPicker" formControlName="endDate" required>
          <mat-datepicker-toggle matSuffix [for]="endPicker"></mat-datepicker-toggle>
          <mat-datepicker #endPicker></mat-datepicker>
        </mat-form-field>

        <mat-form-field class="policy-form__field policy-form__field--full">
          <mat-label>Emergency Phone</mat-label>
          <input matInput formControlName="emergencyPhone" required>
        </mat-form-field>

        <mat-form-field class="policy-form__field">
          <mat-label>Max Towing Distance (miles)</mat-label>
          <input matInput type="number" formControlName="maxTowingDistance">
        </mat-form-field>

        <mat-form-field class="policy-form__field">
          <mat-label>Service Calls Per Year</mat-label>
          <input matInput type="number" formControlName="serviceCallsPerYear">
        </mat-form-field>

        <mat-form-field class="policy-form__field">
          <mat-label>Annual Premium</mat-label>
          <input matInput type="number" formControlName="annualPremium">
        </mat-form-field>

        <mat-form-field class="policy-form__field policy-form__field--full">
          <mat-label>Notes</mat-label>
          <textarea matInput formControlName="notes" rows="3"></textarea>
        </mat-form-field>

        <div class="policy-form__checkboxes">
          <h4 class="policy-form__checkboxes-title">Coverage Options:</h4>
          <mat-checkbox formControlName="coversBatteryService">Battery Service</mat-checkbox>
          <mat-checkbox formControlName="coversFlatTire">Flat Tire</mat-checkbox>
          <mat-checkbox formControlName="coversFuelDelivery">Fuel Delivery</mat-checkbox>
          <mat-checkbox formControlName="coversLockout">Lockout Service</mat-checkbox>
        </div>
      </form>
    </mat-dialog-content>
    <mat-dialog-actions align="end">
      <button mat-button (click)="dialogRef.close()">Cancel</button>
      <button mat-raised-button color="primary" (click)="onSubmit()" [disabled]="!form.valid">
        {{ data.policy ? 'Update' : 'Create' }}
      </button>
    </mat-dialog-actions>
  `,
  styles: [`
    .policy-form {
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

      &__checkboxes {
        grid-column: 1 / -1;
        display: grid;
        grid-template-columns: 1fr 1fr;
        gap: 0.5rem;

        &-title {
          grid-column: 1 / -1;
          margin: 0.5rem 0;
          font-size: 0.875rem;
          font-weight: 500;
        }
      }
    }
  `]
})
export class PolicyFormDialog {
  private _fb = inject(FormBuilder);
  public dialogRef = inject(MatDialogRef<PolicyFormDialog>);
  public data: { policy: Policy | null, vehicles: Vehicle[] } = inject(MAT_DIALOG_DATA);

  form: FormGroup;
  vehicles: Vehicle[] = [];

  constructor() {
    this.form = this._fb.group({
      vehicleId: [this.data.policy?.vehicleId || '', Validators.required],
      provider: [this.data.policy?.provider || '', Validators.required],
      policyNumber: [this.data.policy?.policyNumber || '', Validators.required],
      startDate: [this.data.policy?.startDate ? new Date(this.data.policy.startDate) : '', Validators.required],
      endDate: [this.data.policy?.endDate ? new Date(this.data.policy.endDate) : '', Validators.required],
      emergencyPhone: [this.data.policy?.emergencyPhone || '', Validators.required],
      maxTowingDistance: [this.data.policy?.maxTowingDistance || null],
      serviceCallsPerYear: [this.data.policy?.serviceCallsPerYear || null],
      coveredServices: [this.data.policy?.coveredServices || []],
      annualPremium: [this.data.policy?.annualPremium || null],
      coversBatteryService: [this.data.policy?.coversBatteryService ?? false],
      coversFlatTire: [this.data.policy?.coversFlatTire ?? false],
      coversFuelDelivery: [this.data.policy?.coversFuelDelivery ?? false],
      coversLockout: [this.data.policy?.coversLockout ?? false],
      notes: [this.data.policy?.notes || '']
    });
  }

  onSubmit(): void {
    if (this.form.valid) {
      this.dialogRef.close(this.form.value);
    }
  }
}
