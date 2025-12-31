import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { DestinationService } from '../../services';
import { DestinationCard, DestinationDialog } from '../../components';
import { Destination } from '../../models';

@Component({
  selector: 'app-destinations',
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatDialogModule,
    MatSnackBarModule,
    DestinationCard
  ],
  templateUrl: './destinations.html',
  styleUrl: './destinations.scss'
})
export class Destinations implements OnInit {
  private destinationService = inject(DestinationService);
  private dialog = inject(MatDialog);
  private snackBar = inject(MatSnackBar);

  private readonly userId = '00000000-0000-0000-0000-000000000001';

  destinations$ = this.destinationService.destinations$;

  ngOnInit(): void {
    this.destinationService.getDestinations(this.userId).subscribe();
  }

  onAdd(): void {
    const dialogRef = this.dialog.open(DestinationDialog, {
      width: '500px',
      data: { userId: this.userId }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.destinationService.createDestination(result).subscribe({
          next: () => {
            this.snackBar.open('Destination added successfully', 'Close', { duration: 3000 });
          },
          error: () => {
            this.snackBar.open('Failed to add destination', 'Close', { duration: 3000 });
          }
        });
      }
    });
  }

  onEdit(destination: Destination): void {
    const dialogRef = this.dialog.open(DestinationDialog, {
      width: '500px',
      data: { destination, userId: this.userId }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.destinationService.updateDestination(destination.destinationId, result).subscribe({
          next: () => {
            this.snackBar.open('Destination updated successfully', 'Close', { duration: 3000 });
          },
          error: () => {
            this.snackBar.open('Failed to update destination', 'Close', { duration: 3000 });
          }
        });
      }
    });
  }

  onDelete(destinationId: string): void {
    if (confirm('Are you sure you want to delete this destination?')) {
      this.destinationService.deleteDestination(destinationId, this.userId).subscribe({
        next: () => {
          this.snackBar.open('Destination deleted successfully', 'Close', { duration: 3000 });
        },
        error: () => {
          this.snackBar.open('Failed to delete destination', 'Close', { duration: 3000 });
        }
      });
    }
  }

  onMarkVisited(destinationId: string): void {
    const visitedDate = new Date().toISOString();
    this.destinationService.markDestinationVisited(destinationId, visitedDate, this.userId).subscribe({
      next: () => {
        this.snackBar.open('Destination marked as visited', 'Close', { duration: 3000 });
      },
      error: () => {
        this.snackBar.open('Failed to mark destination as visited', 'Close', { duration: 3000 });
      }
    });
  }
}
