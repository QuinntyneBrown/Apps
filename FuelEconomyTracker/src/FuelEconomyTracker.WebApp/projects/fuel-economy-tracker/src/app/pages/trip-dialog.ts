import { Component, Inject, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { TripsService, VehiclesService } from '../services';
import { Trip } from '../models';

@Component({
  selector: 'app-trip-dialog',
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
  template: `
    <h2 mat-dialog-title>{{ isEdit ? 'Edit Trip' : 'Add Trip' }}</h2>
    <form [formGroup]="form" (ngSubmit)="onSubmit()">
      <mat-dialog-content class="trip-dialog__content">
        <mat-form-field appearance="outline" class="trip-dialog__field" *ngIf="!isEdit">
          <mat-label>Vehicle</mat-label>
          <mat-select formControlName="vehicleId" required>
            <mat-option *ngFor="let vehicle of vehicles$ | async" [value]="vehicle.vehicleId">
              {{ vehicle.year }} {{ vehicle.make }} {{ vehicle.model }}
            </mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field appearance="outline" class="trip-dialog__field">
          <mat-label>Start Date</mat-label>
          <input matInput [matDatepicker]="startPicker" formControlName="startDate" required>
          <mat-datepicker-toggle matSuffix [for]="startPicker"></mat-datepicker-toggle>
          <mat-datepicker #startPicker></mat-datepicker>
        </mat-form-field>

        <mat-form-field appearance="outline" class="trip-dialog__field" *ngIf="isEdit">
          <mat-label>End Date</mat-label>
          <input matInput [matDatepicker]="endPicker" formControlName="endDate">
          <mat-datepicker-toggle matSuffix [for]="endPicker"></mat-datepicker-toggle>
          <mat-datepicker #endPicker></mat-datepicker>
        </mat-form-field>

        <mat-form-field appearance="outline" class="trip-dialog__field">
          <mat-label>Start Odometer</mat-label>
          <input matInput type="number" formControlName="startOdometer" required>
        </mat-form-field>

        <mat-form-field appearance="outline" class="trip-dialog__field" *ngIf="isEdit">
          <mat-label>End Odometer</mat-label>
          <input matInput type="number" formControlName="endOdometer">
        </mat-form-field>

        <mat-form-field appearance="outline" class="trip-dialog__field">
          <mat-label>Purpose</mat-label>
          <input matInput formControlName="purpose">
        </mat-form-field>

        <mat-form-field appearance="outline" class="trip-dialog__field">
          <mat-label>Start Location</mat-label>
          <input matInput formControlName="startLocation">
        </mat-form-field>

        <mat-form-field appearance="outline" class="trip-dialog__field" *ngIf="isEdit">
          <mat-label>End Location</mat-label>
          <input matInput formControlName="endLocation">
        </mat-form-field>

        <mat-form-field appearance="outline" class="trip-dialog__field" *ngIf="isEdit">
          <mat-label>Average MPG</mat-label>
          <input matInput type="number" step="0.1" formControlName="averageMPG">
        </mat-form-field>

        <mat-form-field appearance="outline" class="trip-dialog__field">
          <mat-label>Notes</mat-label>
          <textarea matInput formControlName="notes" rows="3"></textarea>
        </mat-form-field>
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
    .trip-dialog__content {
      display: flex;
      flex-direction: column;
      min-width: 500px;
      padding: 1rem 0;
    }

    .trip-dialog__field {
      width: 100%;
      margin-bottom: 1rem;
    }
  `]
})
export class TripDialog implements OnInit {
  private fb = inject(FormBuilder);
  private tripsService = inject(TripsService);
  private vehiclesService = inject(VehiclesService);
  private snackBar = inject(MatSnackBar);
  private dialogRef = inject(MatDialogRef<TripDialog>);

  vehicles$ = this.vehiclesService.vehicles$;
  isEdit = false;
  form: FormGroup;

  constructor(@Inject(MAT_DIALOG_DATA) public data: Trip | null) {
    this.isEdit = !!data;
    this.form = this.fb.group({
      vehicleId: [data?.vehicleId || '', this.isEdit ? [] : [Validators.required]],
      startDate: [data?.startDate ? new Date(data.startDate) : new Date(), Validators.required],
      endDate: [data?.endDate ? new Date(data.endDate) : null],
      startOdometer: [data?.startOdometer || '', Validators.required],
      endOdometer: [data?.endOdometer || null],
      purpose: [data?.purpose || ''],
      startLocation: [data?.startLocation || ''],
      endLocation: [data?.endLocation || ''],
      averageMPG: [data?.averageMPG || null],
      notes: [data?.notes || '']
    });
  }

  ngOnInit(): void {
    if (!this.isEdit) {
      this.vehiclesService.getAll(true).subscribe();
    }
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;

      if (this.isEdit && this.data) {
        const request = {
          startDate: formValue.startDate,
          endDate: formValue.endDate || undefined,
          startOdometer: formValue.startOdometer,
          endOdometer: formValue.endOdometer || undefined,
          purpose: formValue.purpose || undefined,
          startLocation: formValue.startLocation || undefined,
          endLocation: formValue.endLocation || undefined,
          averageMPG: formValue.averageMPG || undefined,
          notes: formValue.notes || undefined
        };

        this.tripsService.update(this.data.tripId, request).subscribe({
          next: () => {
            this.snackBar.open('Trip updated successfully', 'Close', { duration: 3000 });
            this.dialogRef.close(true);
          },
          error: () => {
            this.snackBar.open('Error updating trip', 'Close', { duration: 3000 });
          }
        });
      } else {
        const request = {
          vehicleId: formValue.vehicleId,
          startDate: formValue.startDate,
          startOdometer: formValue.startOdometer,
          purpose: formValue.purpose || undefined,
          startLocation: formValue.startLocation || undefined,
          notes: formValue.notes || undefined
        };

        this.tripsService.create(request).subscribe({
          next: () => {
            this.snackBar.open('Trip created successfully', 'Close', { duration: 3000 });
            this.dialogRef.close(true);
          },
          error: () => {
            this.snackBar.open('Error creating trip', 'Close', { duration: 3000 });
          }
        });
      }
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
