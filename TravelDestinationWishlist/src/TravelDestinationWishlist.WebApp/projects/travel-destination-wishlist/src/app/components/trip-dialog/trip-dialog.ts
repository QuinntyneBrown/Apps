import { Component, Inject, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { Trip, Destination } from '../../models';
import { DestinationService } from '../../services';

@Component({
  selector: 'app-trip-dialog',
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
  templateUrl: './trip-dialog.html',
  styleUrl: './trip-dialog.scss'
})
export class TripDialog implements OnInit {
  private destinationService = inject(DestinationService);
  form: FormGroup;
  destinations$ = this.destinationService.destinations$;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<TripDialog>,
    @Inject(MAT_DIALOG_DATA) public data: { trip?: Trip; userId: string }
  ) {
    this.form = this.fb.group({
      destinationId: [data.trip?.destinationId || '', Validators.required],
      startDate: [data.trip?.startDate ? new Date(data.trip.startDate) : null, Validators.required],
      endDate: [data.trip?.endDate ? new Date(data.trip.endDate) : null, Validators.required],
      totalCost: [data.trip?.totalCost || null],
      accommodation: [data.trip?.accommodation || ''],
      transportation: [data.trip?.transportation || ''],
      notes: [data.trip?.notes || '']
    });
  }

  ngOnInit(): void {
    this.destinationService.getDestinations(this.data.userId).subscribe();
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const result = {
        ...formValue,
        startDate: formValue.startDate?.toISOString(),
        endDate: formValue.endDate?.toISOString(),
        userId: this.data.userId,
        ...(this.data.trip && { tripId: this.data.trip.tripId })
      };
      this.dialogRef.close(result);
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
