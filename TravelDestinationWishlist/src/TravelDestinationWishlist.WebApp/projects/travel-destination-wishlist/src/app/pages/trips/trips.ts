import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { TripService, DestinationService } from '../../services';
import { TripCard, TripDialog } from '../../components';
import { Trip } from '../../models';
import { map, combineLatest } from 'rxjs';

@Component({
  selector: 'app-trips',
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatDialogModule,
    MatSnackBarModule,
    TripCard
  ],
  templateUrl: './trips.html',
  styleUrl: './trips.scss'
})
export class Trips implements OnInit {
  private tripService = inject(TripService);
  private destinationService = inject(DestinationService);
  private dialog = inject(MatDialog);
  private snackBar = inject(MatSnackBar);

  private readonly userId = '00000000-0000-0000-0000-000000000001';

  tripsWithDestinations$ = combineLatest([
    this.tripService.trips$,
    this.destinationService.destinations$
  ]).pipe(
    map(([trips, destinations]) =>
      trips.map(trip => ({
        trip,
        destinationName: destinations.find(d => d.destinationId === trip.destinationId)?.name
      }))
    )
  );

  ngOnInit(): void {
    this.tripService.getTrips(this.userId).subscribe();
    this.destinationService.getDestinations(this.userId).subscribe();
  }

  onAdd(): void {
    const dialogRef = this.dialog.open(TripDialog, {
      width: '500px',
      data: { userId: this.userId }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.tripService.createTrip(result).subscribe({
          next: () => {
            this.snackBar.open('Trip added successfully', 'Close', { duration: 3000 });
          },
          error: () => {
            this.snackBar.open('Failed to add trip', 'Close', { duration: 3000 });
          }
        });
      }
    });
  }

  onEdit(trip: Trip): void {
    const dialogRef = this.dialog.open(TripDialog, {
      width: '500px',
      data: { trip, userId: this.userId }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.tripService.updateTrip(trip.tripId, result).subscribe({
          next: () => {
            this.snackBar.open('Trip updated successfully', 'Close', { duration: 3000 });
          },
          error: () => {
            this.snackBar.open('Failed to update trip', 'Close', { duration: 3000 });
          }
        });
      }
    });
  }

  onDelete(tripId: string): void {
    if (confirm('Are you sure you want to delete this trip?')) {
      this.tripService.deleteTrip(tripId, this.userId).subscribe({
        next: () => {
          this.snackBar.open('Trip deleted successfully', 'Close', { duration: 3000 });
        },
        error: () => {
          this.snackBar.open('Failed to delete trip', 'Close', { duration: 3000 });
        }
      });
    }
  }
}
