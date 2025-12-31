import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { BookingService, TripService } from '../services';
import { Booking } from '../models';

@Component({
  selector: 'app-bookings',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatSnackBarModule
  ],
  template: `
    <div class="bookings">
      <div class="bookings__header">
        <h1 class="bookings__title">Bookings</h1>
        <button mat-raised-button color="primary" (click)="showForm = !showForm" class="bookings__add-btn">
          <mat-icon>add</mat-icon>
          Add Booking
        </button>
      </div>

      <mat-card *ngIf="showForm" class="bookings__form-card">
        <mat-card-header>
          <mat-card-title>{{ editingBooking ? 'Edit Booking' : 'New Booking' }}</mat-card-title>
        </mat-card-header>
        <mat-card-content>
          <form [formGroup]="bookingForm" (ngSubmit)="saveBooking()" class="bookings__form">
            <mat-form-field appearance="outline" class="bookings__form-field">
              <mat-label>Trip</mat-label>
              <mat-select formControlName="tripId" required>
                <mat-option *ngFor="let trip of trips$ | async" [value]="trip.tripId">
                  {{ trip.name }}
                </mat-option>
              </mat-select>
            </mat-form-field>

            <mat-form-field appearance="outline" class="bookings__form-field">
              <mat-label>Type</mat-label>
              <mat-select formControlName="type" required>
                <mat-option value="Flight">Flight</mat-option>
                <mat-option value="Hotel">Hotel</mat-option>
                <mat-option value="Car Rental">Car Rental</mat-option>
                <mat-option value="Activity">Activity</mat-option>
                <mat-option value="Other">Other</mat-option>
              </mat-select>
            </mat-form-field>

            <mat-form-field appearance="outline" class="bookings__form-field">
              <mat-label>Confirmation Number</mat-label>
              <input matInput formControlName="confirmationNumber">
            </mat-form-field>

            <mat-form-field appearance="outline" class="bookings__form-field">
              <mat-label>Cost</mat-label>
              <input matInput type="number" formControlName="cost" step="0.01">
              <span matPrefix>$&nbsp;</span>
            </mat-form-field>

            <div class="bookings__form-actions">
              <button mat-button type="button" (click)="cancelEdit()">Cancel</button>
              <button mat-raised-button color="primary" type="submit" [disabled]="!bookingForm.valid">
                {{ editingBooking ? 'Update' : 'Create' }}
              </button>
            </div>
          </form>
        </mat-card-content>
      </mat-card>

      <div class="bookings__list">
        <mat-card *ngFor="let booking of bookings$ | async" class="bookings__card">
          <mat-card-header>
            <mat-card-title>{{ booking.type }}</mat-card-title>
            <mat-card-subtitle *ngIf="booking.confirmationNumber">
              Confirmation: {{ booking.confirmationNumber }}
            </mat-card-subtitle>
          </mat-card-header>
          <mat-card-content>
            <p class="bookings__cost" *ngIf="booking.cost">
              <mat-icon>attach_money</mat-icon>
              Cost: ${{ booking.cost | number:'1.2-2' }}
            </p>
            <p class="bookings__created">
              <mat-icon>schedule</mat-icon>
              Created: {{ booking.createdAt | date:'short' }}
            </p>
          </mat-card-content>
          <mat-card-actions>
            <button mat-button color="primary" (click)="editBooking(booking)">
              <mat-icon>edit</mat-icon>
              Edit
            </button>
            <button mat-button color="warn" (click)="deleteBooking(booking.bookingId)">
              <mat-icon>delete</mat-icon>
              Delete
            </button>
          </mat-card-actions>
        </mat-card>
      </div>
    </div>
  `,
  styles: [`
    .bookings {
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

      &__cost,
      &__created {
        display: flex;
        align-items: center;
        gap: 0.5rem;
        margin: 0.5rem 0;
      }
    }
  `]
})
export class Bookings implements OnInit {
  bookings$ = this.bookingService.bookings$;
  trips$ = this.tripService.trips$;
  bookingForm: FormGroup;
  showForm = false;
  editingBooking: Booking | null = null;

  constructor(
    private bookingService: BookingService,
    private tripService: TripService,
    private fb: FormBuilder,
    private snackBar: MatSnackBar
  ) {
    this.bookingForm = this.fb.group({
      tripId: ['', Validators.required],
      type: ['', Validators.required],
      confirmationNumber: [''],
      cost: ['']
    });
  }

  ngOnInit() {
    this.loadBookings();
    this.loadTrips();
  }

  loadBookings() {
    this.bookingService.getBookings().subscribe();
  }

  loadTrips() {
    const userId = '00000000-0000-0000-0000-000000000001';
    this.tripService.getTrips(userId).subscribe();
  }

  saveBooking() {
    if (this.bookingForm.invalid) return;

    const command = this.bookingForm.value;

    if (this.editingBooking) {
      this.bookingService.updateBooking(this.editingBooking.bookingId, {
        bookingId: this.editingBooking.bookingId,
        ...command
      }).subscribe({
        next: () => {
          this.snackBar.open('Booking updated successfully', 'Close', { duration: 3000 });
          this.cancelEdit();
        },
        error: () => {
          this.snackBar.open('Failed to update booking', 'Close', { duration: 3000 });
        }
      });
    } else {
      this.bookingService.createBooking(command).subscribe({
        next: () => {
          this.snackBar.open('Booking created successfully', 'Close', { duration: 3000 });
          this.cancelEdit();
        },
        error: () => {
          this.snackBar.open('Failed to create booking', 'Close', { duration: 3000 });
        }
      });
    }
  }

  editBooking(booking: Booking) {
    this.editingBooking = booking;
    this.showForm = true;
    this.bookingForm.patchValue(booking);
  }

  deleteBooking(bookingId: string) {
    if (confirm('Are you sure you want to delete this booking?')) {
      this.bookingService.deleteBooking(bookingId).subscribe({
        next: () => {
          this.snackBar.open('Booking deleted successfully', 'Close', { duration: 3000 });
        },
        error: () => {
          this.snackBar.open('Failed to delete booking', 'Close', { duration: 3000 });
        }
      });
    }
  }

  cancelEdit() {
    this.showForm = false;
    this.editingBooking = null;
    this.bookingForm.reset();
  }
}
