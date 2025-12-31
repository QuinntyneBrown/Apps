import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatChipsModule } from '@angular/material/chips';
import { TripsService } from '../services';
import { Trip } from '../models';

@Component({
  selector: 'app-trips',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule, MatTableModule, MatChipsModule],
  template: `
    <div class="trips">
      <div class="trips__header">
        <h1 class="trips__title">Fishing Trips</h1>
        <button mat-raised-button color="primary" (click)="onCreateTrip()">
          <mat-icon>add</mat-icon>
          New Trip
        </button>
      </div>

      <mat-card>
        <mat-card-content>
          <table mat-table [dataSource]="trips$ | async" class="trips__table">
            <ng-container matColumnDef="tripDate">
              <th mat-header-cell *matHeaderCellDef>Date</th>
              <td mat-cell *matCellDef="let trip">{{ trip.tripDate | date:'short' }}</td>
            </ng-container>

            <ng-container matColumnDef="spotName">
              <th mat-header-cell *matHeaderCellDef>Spot</th>
              <td mat-cell *matCellDef="let trip">{{ trip.spotName || 'Unknown' }}</td>
            </ng-container>

            <ng-container matColumnDef="duration">
              <th mat-header-cell *matHeaderCellDef>Duration</th>
              <td mat-cell *matCellDef="let trip">
                {{ trip.durationInHours ? trip.durationInHours + ' hrs' : 'N/A' }}
              </td>
            </ng-container>

            <ng-container matColumnDef="weather">
              <th mat-header-cell *matHeaderCellDef>Weather</th>
              <td mat-cell *matCellDef="let trip">{{ trip.weatherConditions || 'N/A' }}</td>
            </ng-container>

            <ng-container matColumnDef="temperature">
              <th mat-header-cell *matHeaderCellDef>Water/Air Temp</th>
              <td mat-cell *matCellDef="let trip">
                {{ trip.waterTemperature || 'N/A' }} / {{ trip.airTemperature || 'N/A' }}
              </td>
            </ng-container>

            <ng-container matColumnDef="catchCount">
              <th mat-header-cell *matHeaderCellDef>Catches</th>
              <td mat-cell *matCellDef="let trip">
                <mat-chip-set>
                  <mat-chip highlighted>{{ trip.catchCount }}</mat-chip>
                </mat-chip-set>
              </td>
            </ng-container>

            <ng-container matColumnDef="actions">
              <th mat-header-cell *matHeaderCellDef>Actions</th>
              <td mat-cell *matCellDef="let trip">
                <button mat-icon-button (click)="onEditTrip(trip)">
                  <mat-icon>edit</mat-icon>
                </button>
                <button mat-icon-button color="warn" (click)="onDeleteTrip(trip)">
                  <mat-icon>delete</mat-icon>
                </button>
              </td>
            </ng-container>

            <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
            <tr mat-row *matRowDef="let row; columns: displayedColumns;" class="trips__row"></tr>
          </table>

          <div *ngIf="(trips$ | async)?.length === 0" class="trips__empty">
            <mat-icon>directions_boat</mat-icon>
            <p>No trips yet. Create your first fishing trip!</p>
            <button mat-raised-button color="primary" (click)="onCreateTrip()">
              Create Trip
            </button>
          </div>
        </mat-card-content>
      </mat-card>
    </div>
  `,
  styles: [`
    .trips {
      padding: 24px;
    }

    .trips__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 24px;
    }

    .trips__title {
      margin: 0;
      font-size: 32px;
      font-weight: 500;
    }

    .trips__table {
      width: 100%;
    }

    .trips__row:hover {
      background-color: rgba(0, 0, 0, 0.04);
    }

    .trips__empty {
      display: flex;
      flex-direction: column;
      align-items: center;
      justify-content: center;
      padding: 48px;
      text-align: center;
    }

    .trips__empty mat-icon {
      font-size: 72px;
      width: 72px;
      height: 72px;
      color: rgba(0, 0, 0, 0.26);
      margin-bottom: 16px;
    }

    .trips__empty p {
      margin: 0 0 24px 0;
      color: rgba(0, 0, 0, 0.6);
    }
  `]
})
export class Trips implements OnInit {
  trips$ = this.tripsService.trips$;
  displayedColumns: string[] = ['tripDate', 'spotName', 'duration', 'weather', 'temperature', 'catchCount', 'actions'];

  constructor(private tripsService: TripsService) {}

  ngOnInit(): void {
    this.tripsService.getTrips().subscribe();
  }

  onCreateTrip(): void {
    // TODO: Open dialog to create trip
    console.log('Create trip');
  }

  onEditTrip(trip: Trip): void {
    // TODO: Open dialog to edit trip
    console.log('Edit trip', trip);
  }

  onDeleteTrip(trip: Trip): void {
    if (confirm(`Are you sure you want to delete this trip?`)) {
      this.tripsService.deleteTrip(trip.tripId).subscribe();
    }
  }
}
