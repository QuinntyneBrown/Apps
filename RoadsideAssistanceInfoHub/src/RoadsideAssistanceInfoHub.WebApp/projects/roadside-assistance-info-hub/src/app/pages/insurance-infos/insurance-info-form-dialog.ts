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
import { InsuranceInfo, Vehicle } from '../../models';

@Component({
  selector: 'app-insurance-info-form-dialog',
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
    <h2 mat-dialog-title>{{ data.insuranceInfo ? 'Edit Insurance Info' : 'Add Insurance Info' }}</h2>
    <mat-dialog-content>
      <form [formGroup]="form" class="insurance-form">
        <mat-form-field class="insurance-form__field insurance-form__field--full">
          <mat-label>Vehicle</mat-label>
          <mat-select formControlName="vehicleId" required>
            <mat-option *ngFor="let vehicle of vehicles" [value]="vehicle.vehicleId">
              {{ vehicle.year }} {{ vehicle.make }} {{ vehicle.model }}
            </mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field class="insurance-form__field">
          <mat-label>Insurance Company</mat-label>
          <input matInput formControlName="insuranceCompany" required>
        </mat-form-field>

        <mat-form-field class="insurance-form__field">
          <mat-label>Policy Number</mat-label>
          <input matInput formControlName="policyNumber" required>
        </mat-form-field>

        <mat-form-field class="insurance-form__field insurance-form__field--full">
          <mat-label>Policy Holder</mat-label>
          <input matInput formControlName="policyHolder" required>
        </mat-form-field>

        <mat-form-field class="insurance-form__field">
          <mat-label>Policy Start Date</mat-label>
          <input matInput [matDatepicker]="startPicker" formControlName="policyStartDate" required>
          <mat-datepicker-toggle matSuffix [for]="startPicker"></mat-datepicker-toggle>
          <mat-datepicker #startPicker></mat-datepicker>
        </mat-form-field>

        <mat-form-field class="insurance-form__field">
          <mat-label>Policy End Date</mat-label>
          <input matInput [matDatepicker]="endPicker" formControlName="policyEndDate" required>
          <mat-datepicker-toggle matSuffix [for]="endPicker"></mat-datepicker-toggle>
          <mat-datepicker #endPicker></mat-datepicker>
        </mat-form-field>

        <mat-form-field class="insurance-form__field">
          <mat-label>Agent Name</mat-label>
          <input matInput formControlName="agentName">
        </mat-form-field>

        <mat-form-field class="insurance-form__field">
          <mat-label>Agent Phone</mat-label>
          <input matInput formControlName="agentPhone">
        </mat-form-field>

        <mat-form-field class="insurance-form__field">
          <mat-label>Company Phone</mat-label>
          <input matInput formControlName="companyPhone">
        </mat-form-field>

        <mat-form-field class="insurance-form__field">
          <mat-label>Claims Phone</mat-label>
          <input matInput formControlName="claimsPhone">
        </mat-form-field>

        <mat-form-field class="insurance-form__field">
          <mat-label>Coverage Type</mat-label>
          <input matInput formControlName="coverageType">
        </mat-form-field>

        <mat-form-field class="insurance-form__field">
          <mat-label>Deductible</mat-label>
          <input matInput type="number" formControlName="deductible">
        </mat-form-field>

        <mat-form-field class="insurance-form__field">
          <mat-label>Premium</mat-label>
          <input matInput type="number" formControlName="premium">
        </mat-form-field>

        <mat-form-field class="insurance-form__field insurance-form__field--full">
          <mat-label>Notes</mat-label>
          <textarea matInput formControlName="notes" rows="3"></textarea>
        </mat-form-field>

        <mat-checkbox formControlName="includesRoadsideAssistance" class="insurance-form__checkbox">
          Includes Roadside Assistance
        </mat-checkbox>
      </form>
    </mat-dialog-content>
    <mat-dialog-actions align="end">
      <button mat-button (click)="dialogRef.close()">Cancel</button>
      <button mat-raised-button color="primary" (click)="onSubmit()" [disabled]="!form.valid">
        {{ data.insuranceInfo ? 'Update' : 'Create' }}
      </button>
    </mat-dialog-actions>
  `,
  styles: [`
    .insurance-form {
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

      &__checkbox {
        grid-column: 1 / -1;
      }
    }
  `]
})
export class InsuranceInfoFormDialog {
  private _fb = inject(FormBuilder);
  public dialogRef = inject(MatDialogRef<InsuranceInfoFormDialog>);
  public data: { insuranceInfo: InsuranceInfo | null, vehicles: Vehicle[] } = inject(MAT_DIALOG_DATA);

  form: FormGroup;
  vehicles: Vehicle[] = [];

  constructor() {
    this.form = this._fb.group({
      vehicleId: [this.data.insuranceInfo?.vehicleId || '', Validators.required],
      insuranceCompany: [this.data.insuranceInfo?.insuranceCompany || '', Validators.required],
      policyNumber: [this.data.insuranceInfo?.policyNumber || '', Validators.required],
      policyHolder: [this.data.insuranceInfo?.policyHolder || '', Validators.required],
      policyStartDate: [this.data.insuranceInfo?.policyStartDate ? new Date(this.data.insuranceInfo.policyStartDate) : '', Validators.required],
      policyEndDate: [this.data.insuranceInfo?.policyEndDate ? new Date(this.data.insuranceInfo.policyEndDate) : '', Validators.required],
      agentName: [this.data.insuranceInfo?.agentName || ''],
      agentPhone: [this.data.insuranceInfo?.agentPhone || ''],
      companyPhone: [this.data.insuranceInfo?.companyPhone || ''],
      claimsPhone: [this.data.insuranceInfo?.claimsPhone || ''],
      coverageType: [this.data.insuranceInfo?.coverageType || ''],
      deductible: [this.data.insuranceInfo?.deductible || null],
      premium: [this.data.insuranceInfo?.premium || null],
      includesRoadsideAssistance: [this.data.insuranceInfo?.includesRoadsideAssistance ?? false],
      notes: [this.data.insuranceInfo?.notes || '']
    });
  }

  onSubmit(): void {
    if (this.form.valid) {
      this.dialogRef.close(this.form.value);
    }
  }
}
