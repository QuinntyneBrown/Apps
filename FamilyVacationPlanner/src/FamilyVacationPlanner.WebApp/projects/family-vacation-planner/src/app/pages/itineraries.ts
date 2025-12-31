import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { ItineraryService, TripService } from '../services';
import { Itinerary } from '../models';

@Component({
  selector: 'app-itineraries',
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
    MatDatepickerModule,
    MatNativeDateModule,
    MatSnackBarModule
  ],
  template: `
    <div class="itineraries">
      <div class="itineraries__header">
        <h1 class="itineraries__title">Itineraries</h1>
        <button mat-raised-button color="primary" (click)="showForm = !showForm" class="itineraries__add-btn">
          <mat-icon>add</mat-icon>
          Add Itinerary
        </button>
      </div>

      <mat-card *ngIf="showForm" class="itineraries__form-card">
        <mat-card-header>
          <mat-card-title>{{ editingItinerary ? 'Edit Itinerary' : 'New Itinerary' }}</mat-card-title>
        </mat-card-header>
        <mat-card-content>
          <form [formGroup]="itineraryForm" (ngSubmit)="saveItinerary()" class="itineraries__form">
            <mat-form-field appearance="outline" class="itineraries__form-field">
              <mat-label>Trip</mat-label>
              <mat-select formControlName="tripId" required>
                <mat-option *ngFor="let trip of trips$ | async" [value]="trip.tripId">
                  {{ trip.name }}
                </mat-option>
              </mat-select>
            </mat-form-field>

            <mat-form-field appearance="outline" class="itineraries__form-field">
              <mat-label>Date</mat-label>
              <input matInput [matDatepicker]="datePicker" formControlName="date" required>
              <mat-datepicker-toggle matSuffix [for]="datePicker"></mat-datepicker-toggle>
              <mat-datepicker #datePicker></mat-datepicker>
            </mat-form-field>

            <mat-form-field appearance="outline" class="itineraries__form-field">
              <mat-label>Activity</mat-label>
              <input matInput formControlName="activity">
            </mat-form-field>

            <mat-form-field appearance="outline" class="itineraries__form-field">
              <mat-label>Location</mat-label>
              <input matInput formControlName="location">
            </mat-form-field>

            <div class="itineraries__form-actions">
              <button mat-button type="button" (click)="cancelEdit()">Cancel</button>
              <button mat-raised-button color="primary" type="submit" [disabled]="!itineraryForm.valid">
                {{ editingItinerary ? 'Update' : 'Create' }}
              </button>
            </div>
          </form>
        </mat-card-content>
      </mat-card>

      <div class="itineraries__list">
        <mat-card *ngFor="let itinerary of itineraries$ | async" class="itineraries__card">
          <mat-card-header>
            <mat-card-title>{{ itinerary.activity || 'Activity' }}</mat-card-title>
            <mat-card-subtitle>{{ itinerary.date | date:'fullDate' }}</mat-card-subtitle>
          </mat-card-header>
          <mat-card-content>
            <p class="itineraries__location" *ngIf="itinerary.location">
              <mat-icon>location_on</mat-icon>
              {{ itinerary.location }}
            </p>
            <p class="itineraries__created">
              <mat-icon>schedule</mat-icon>
              Created: {{ itinerary.createdAt | date:'short' }}
            </p>
          </mat-card-content>
          <mat-card-actions>
            <button mat-button color="primary" (click)="editItinerary(itinerary)">
              <mat-icon>edit</mat-icon>
              Edit
            </button>
            <button mat-button color="warn" (click)="deleteItinerary(itinerary.itineraryId)">
              <mat-icon>delete</mat-icon>
              Delete
            </button>
          </mat-card-actions>
        </mat-card>
      </div>
    </div>
  `,
  styles: [`
    .itineraries {
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

      &__location,
      &__created {
        display: flex;
        align-items: center;
        gap: 0.5rem;
        margin: 0.5rem 0;
      }
    }
  `]
})
export class Itineraries implements OnInit {
  itineraries$ = this.itineraryService.itineraries$;
  trips$ = this.tripService.trips$;
  itineraryForm: FormGroup;
  showForm = false;
  editingItinerary: Itinerary | null = null;

  constructor(
    private itineraryService: ItineraryService,
    private tripService: TripService,
    private fb: FormBuilder,
    private snackBar: MatSnackBar
  ) {
    this.itineraryForm = this.fb.group({
      tripId: ['', Validators.required],
      date: ['', Validators.required],
      activity: [''],
      location: ['']
    });
  }

  ngOnInit() {
    this.loadItineraries();
    this.loadTrips();
  }

  loadItineraries() {
    this.itineraryService.getItineraries().subscribe();
  }

  loadTrips() {
    const userId = '00000000-0000-0000-0000-000000000001';
    this.tripService.getTrips(userId).subscribe();
  }

  saveItinerary() {
    if (this.itineraryForm.invalid) return;

    const formValue = this.itineraryForm.value;
    const command = {
      ...formValue,
      date: new Date(formValue.date).toISOString()
    };

    if (this.editingItinerary) {
      this.itineraryService.updateItinerary(this.editingItinerary.itineraryId, {
        itineraryId: this.editingItinerary.itineraryId,
        ...command
      }).subscribe({
        next: () => {
          this.snackBar.open('Itinerary updated successfully', 'Close', { duration: 3000 });
          this.cancelEdit();
        },
        error: () => {
          this.snackBar.open('Failed to update itinerary', 'Close', { duration: 3000 });
        }
      });
    } else {
      this.itineraryService.createItinerary(command).subscribe({
        next: () => {
          this.snackBar.open('Itinerary created successfully', 'Close', { duration: 3000 });
          this.cancelEdit();
        },
        error: () => {
          this.snackBar.open('Failed to create itinerary', 'Close', { duration: 3000 });
        }
      });
    }
  }

  editItinerary(itinerary: Itinerary) {
    this.editingItinerary = itinerary;
    this.showForm = true;
    this.itineraryForm.patchValue({
      tripId: itinerary.tripId,
      date: new Date(itinerary.date),
      activity: itinerary.activity,
      location: itinerary.location
    });
  }

  deleteItinerary(itineraryId: string) {
    if (confirm('Are you sure you want to delete this itinerary?')) {
      this.itineraryService.deleteItinerary(itineraryId).subscribe({
        next: () => {
          this.snackBar.open('Itinerary deleted successfully', 'Close', { duration: 3000 });
        },
        error: () => {
          this.snackBar.open('Failed to delete itinerary', 'Close', { duration: 3000 });
        }
      });
    }
  }

  cancelEdit() {
    this.showForm = false;
    this.editingItinerary = null;
    this.itineraryForm.reset();
  }
}
