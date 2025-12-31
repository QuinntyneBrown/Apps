import { Component, Inject, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { TripsService } from '../services';
import { Trip } from '../models';

@Component({
  selector: 'app-complete-trip-dialog',
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
  template: `
    <h2 mat-dialog-title>Complete Trip</h2>
    <form [formGroup]="form" (ngSubmit)="onSubmit()">
      <mat-dialog-content class="complete-trip-dialog__content">
        <mat-form-field appearance="outline" class="complete-trip-dialog__field">
          <mat-label>End Date</mat-label>
          <input matInput [matDatepicker]="picker" formControlName="endDate" required>
          <mat-datepicker-toggle matSuffix [for]="picker"></mat-datepicker-toggle>
          <mat-datepicker #picker></mat-datepicker>
        </mat-form-field>

        <mat-form-field appearance="outline" class="complete-trip-dialog__field">
          <mat-label>End Odometer</mat-label>
          <input matInput type="number" formControlName="endOdometer" required>
        </mat-form-field>

        <mat-form-field appearance="outline" class="complete-trip-dialog__field">
          <mat-label>End Location</mat-label>
          <input matInput formControlName="endLocation">
        </mat-form-field>
      </mat-dialog-content>

      <mat-dialog-actions align="end">
        <button mat-button type="button" (click)="onCancel()">Cancel</button>
        <button mat-raised-button color="primary" type="submit" [disabled]="form.invalid">
          Complete
        </button>
      </mat-dialog-actions>
    </form>
  `,
  styles: [`
    .complete-trip-dialog__content {
      display: flex;
      flex-direction: column;
      min-width: 500px;
      padding: 1rem 0;
    }

    .complete-trip-dialog__field {
      width: 100%;
      margin-bottom: 1rem;
    }
  `]
})
export class CompleteTripDialog {
  private fb = inject(FormBuilder);
  private tripsService = inject(TripsService);
  private snackBar = inject(MatSnackBar);
  private dialogRef = inject(MatDialogRef<CompleteTripDialog>);

  form: FormGroup;

  constructor(@Inject(MAT_DIALOG_DATA) public data: Trip) {
    this.form = this.fb.group({
      endDate: [new Date(), Validators.required],
      endOdometer: ['', Validators.required],
      endLocation: ['']
    });
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const request = {
        endDate: formValue.endDate,
        endOdometer: formValue.endOdometer,
        endLocation: formValue.endLocation || undefined
      };

      this.tripsService.complete(this.data.tripId, request).subscribe({
        next: () => {
          this.snackBar.open('Trip completed successfully', 'Close', { duration: 3000 });
          this.dialogRef.close(true);
        },
        error: () => {
          this.snackBar.open('Error completing trip', 'Close', { duration: 3000 });
        }
      });
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
