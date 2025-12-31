import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { TripsService } from '../services';
import { TripDialog } from './trip-dialog';
import { CompleteTripDialog } from './complete-trip-dialog';

@Component({
  selector: 'app-trips',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatTableModule,
    MatCardModule,
    MatDialogModule,
    MatSnackBarModule
  ],
  templateUrl: './trips.html',
  styleUrl: './trips.scss'
})
export class Trips implements OnInit {
  private tripsService = inject(TripsService);
  private dialog = inject(MatDialog);
  private snackBar = inject(MatSnackBar);

  trips$ = this.tripsService.trips$;
  displayedColumns = ['startDate', 'endDate', 'distance', 'purpose', 'startLocation', 'endLocation', 'averageMPG', 'actions'];

  ngOnInit(): void {
    this.loadTrips();
  }

  loadTrips(): void {
    this.tripsService.getAll().subscribe();
  }

  openDialog(trip?: any): void {
    const dialogRef = this.dialog.open(TripDialog, {
      width: '600px',
      data: trip || null
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadTrips();
      }
    });
  }

  openCompleteDialog(trip: any): void {
    const dialogRef = this.dialog.open(CompleteTripDialog, {
      width: '600px',
      data: trip
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadTrips();
      }
    });
  }

  deleteTrip(id: string): void {
    if (confirm('Are you sure you want to delete this trip?')) {
      this.tripsService.delete(id).subscribe({
        next: () => {
          this.snackBar.open('Trip deleted successfully', 'Close', { duration: 3000 });
        },
        error: () => {
          this.snackBar.open('Error deleting trip', 'Close', { duration: 3000 });
        }
      });
    }
  }
}
