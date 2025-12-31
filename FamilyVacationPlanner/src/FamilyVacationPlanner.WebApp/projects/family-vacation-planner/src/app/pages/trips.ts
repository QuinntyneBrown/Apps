import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { TripService } from '../services';
import { Trip } from '../models';

@Component({
  selector: 'app-trips',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatDialogModule,
    MatSnackBarModule
  ],
  template: `
    <div class="trips">
      <div class="trips__header">
        <h1 class="trips__title">My Trips</h1>
        <button mat-raised-button color="primary" (click)="showForm = !showForm" class="trips__add-btn">
          <mat-icon>add</mat-icon>
          Add Trip
        </button>
      </div>

      <mat-card *ngIf="showForm" class="trips__form-card">
        <mat-card-header>
          <mat-card-title>{{ editingTrip ? 'Edit Trip' : 'New Trip' }}</mat-card-title>
        </mat-card-header>
        <mat-card-content>
          <form [formGroup]="tripForm" (ngSubmit)="saveTrip()" class="trips__form">
            <mat-form-field appearance="outline" class="trips__form-field">
              <mat-label>Name</mat-label>
              <input matInput formControlName="name" required>
            </mat-form-field>

            <mat-form-field appearance="outline" class="trips__form-field">
              <mat-label>Destination</mat-label>
              <input matInput formControlName="destination">
            </mat-form-field>

            <mat-form-field appearance="outline" class="trips__form-field">
              <mat-label>Start Date</mat-label>
              <input matInput [matDatepicker]="startPicker" formControlName="startDate">
              <mat-datepicker-toggle matSuffix [for]="startPicker"></mat-datepicker-toggle>
              <mat-datepicker #startPicker></mat-datepicker>
            </mat-form-field>

            <mat-form-field appearance="outline" class="trips__form-field">
              <mat-label>End Date</mat-label>
              <input matInput [matDatepicker]="endPicker" formControlName="endDate">
              <mat-datepicker-toggle matSuffix [for]="endPicker"></mat-datepicker-toggle>
              <mat-datepicker #endPicker></mat-datepicker>
            </mat-form-field>

            <div class="trips__form-actions">
              <button mat-button type="button" (click)="cancelEdit()">Cancel</button>
              <button mat-raised-button color="primary" type="submit" [disabled]="!tripForm.valid">
                {{ editingTrip ? 'Update' : 'Create' }}
              </button>
            </div>
          </form>
        </mat-card-content>
      </mat-card>

      <div class="trips__list">
        <mat-card *ngFor="let trip of trips$ | async" class="trips__card">
          <mat-card-header>
            <mat-card-title>{{ trip.name }}</mat-card-title>
            <mat-card-subtitle>{{ trip.destination || 'No destination set' }}</mat-card-subtitle>
          </mat-card-header>
          <mat-card-content>
            <p class="trips__dates" *ngIf="trip.startDate || trip.endDate">
              <mat-icon>date_range</mat-icon>
              {{ trip.startDate ? (trip.startDate | date) : 'TBD' }} - {{ trip.endDate ? (trip.endDate | date) : 'TBD' }}
            </p>
            <p class="trips__created">
              <mat-icon>schedule</mat-icon>
              Created: {{ trip.createdAt | date:'short' }}
            </p>
          </mat-card-content>
          <mat-card-actions>
            <button mat-button color="primary" (click)="editTrip(trip)">
              <mat-icon>edit</mat-icon>
              Edit
            </button>
            <button mat-button color="warn" (click)="deleteTrip(trip.tripId)">
              <mat-icon>delete</mat-icon>
              Delete
            </button>
          </mat-card-actions>
        </mat-card>
      </div>
    </div>
  `,
  styles: [`
    .trips {
      padding: 2rem;

      &__header {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-bottom: 2rem;
      }

      &__title {
        margin: 0;
        font-size: 2rem;
      }

      &__add-btn {
        display: flex;
        align-items: center;
        gap: 0.5rem;
      }

      &__form-card {
        margin-bottom: 2rem;
      }

      &__form {
        display: grid;
        grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
        gap: 1rem;
        margin-top: 1rem;
      }

      &__form-field {
        width: 100%;
      }

      &__form-actions {
        grid-column: 1 / -1;
        display: flex;
        gap: 1rem;
        justify-content: flex-end;
      }

      &__list {
        display: grid;
        grid-template-columns: repeat(auto-fill, minmax(350px, 1fr));
        gap: 1.5rem;
      }

      &__card {
        mat-card-actions {
          display: flex;
          gap: 0.5rem;
        }
      }

      &__dates,
      &__created {
        display: flex;
        align-items: center;
        gap: 0.5rem;
        margin: 0.5rem 0;
      }
    }
  `]
})
export class Trips implements OnInit {
  trips$ = this.tripService.trips$;
  tripForm: FormGroup;
  showForm = false;
  editingTrip: Trip | null = null;

  constructor(
    private tripService: TripService,
    private fb: FormBuilder,
    private snackBar: MatSnackBar
  ) {
    this.tripForm = this.fb.group({
      name: ['', Validators.required],
      destination: [''],
      startDate: [''],
      endDate: ['']
    });
  }

  ngOnInit() {
    this.loadTrips();
  }

  loadTrips() {
    // Using a sample userId for demo purposes
    const userId = '00000000-0000-0000-0000-000000000001';
    this.tripService.getTrips(userId).subscribe();
  }

  saveTrip() {
    if (this.tripForm.invalid) return;

    const formValue = this.tripForm.value;
    const command = {
      ...formValue,
      startDate: formValue.startDate ? new Date(formValue.startDate).toISOString() : undefined,
      endDate: formValue.endDate ? new Date(formValue.endDate).toISOString() : undefined
    };

    if (this.editingTrip) {
      this.tripService.updateTrip(this.editingTrip.tripId, {
        tripId: this.editingTrip.tripId,
        ...command
      }).subscribe({
        next: () => {
          this.snackBar.open('Trip updated successfully', 'Close', { duration: 3000 });
          this.cancelEdit();
        },
        error: () => {
          this.snackBar.open('Failed to update trip', 'Close', { duration: 3000 });
        }
      });
    } else {
      const userId = '00000000-0000-0000-0000-000000000001';
      this.tripService.createTrip({ userId, ...command }).subscribe({
        next: () => {
          this.snackBar.open('Trip created successfully', 'Close', { duration: 3000 });
          this.cancelEdit();
        },
        error: () => {
          this.snackBar.open('Failed to create trip', 'Close', { duration: 3000 });
        }
      });
    }
  }

  editTrip(trip: Trip) {
    this.editingTrip = trip;
    this.showForm = true;
    this.tripForm.patchValue({
      name: trip.name,
      destination: trip.destination,
      startDate: trip.startDate ? new Date(trip.startDate) : null,
      endDate: trip.endDate ? new Date(trip.endDate) : null
    });
  }

  deleteTrip(tripId: string) {
    if (confirm('Are you sure you want to delete this trip?')) {
      this.tripService.deleteTrip(tripId).subscribe({
        next: () => {
          this.snackBar.open('Trip deleted successfully', 'Close', { duration: 3000 });
        },
        error: () => {
          this.snackBar.open('Failed to delete trip', 'Close', { duration: 3000 });
        }
      });
    }
  }

  cancelEdit() {
    this.showForm = false;
    this.editingTrip = null;
    this.tripForm.reset();
  }
}
