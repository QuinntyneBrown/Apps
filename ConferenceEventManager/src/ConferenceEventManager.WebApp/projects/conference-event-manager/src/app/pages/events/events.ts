import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatChipsModule } from '@angular/material/chips';
import { EventService } from '../../services';
import { Event, EventType } from '../../models';
import { EventDialog } from '../../components/dialogs';

@Component({
  selector: 'app-events',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatDialogModule,
    MatSnackBarModule,
    MatChipsModule
  ],
  templateUrl: './events.html',
  styleUrl: './events.scss'
})
export class Events implements OnInit {
  events: Event[] = [];
  displayedColumns: string[] = ['name', 'eventType', 'startDate', 'endDate', 'location', 'isVirtual', 'actions'];

  constructor(
    private eventService: EventService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.loadEvents();
  }

  loadEvents(): void {
    this.eventService.getAll().subscribe({
      next: (events) => {
        this.events = events;
      },
      error: (error) => {
        console.error('Error loading events:', error);
        this.snackBar.open('Error loading events', 'Close', { duration: 3000 });
      }
    });
  }

  openCreateDialog(): void {
    const dialogRef = this.dialog.open(EventDialog, {
      width: '600px',
      data: {}
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.eventService.create(result).subscribe({
          next: () => {
            this.snackBar.open('Event created successfully', 'Close', { duration: 3000 });
            this.loadEvents();
          },
          error: (error) => {
            console.error('Error creating event:', error);
            this.snackBar.open('Error creating event', 'Close', { duration: 3000 });
          }
        });
      }
    });
  }

  openEditDialog(event: Event): void {
    const dialogRef = this.dialog.open(EventDialog, {
      width: '600px',
      data: { event }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.eventService.update(event.eventId, { ...result, eventId: event.eventId }).subscribe({
          next: () => {
            this.snackBar.open('Event updated successfully', 'Close', { duration: 3000 });
            this.loadEvents();
          },
          error: (error) => {
            console.error('Error updating event:', error);
            this.snackBar.open('Error updating event', 'Close', { duration: 3000 });
          }
        });
      }
    });
  }

  deleteEvent(event: Event): void {
    if (confirm(`Are you sure you want to delete "${event.name}"?`)) {
      this.eventService.delete(event.eventId).subscribe({
        next: () => {
          this.snackBar.open('Event deleted successfully', 'Close', { duration: 3000 });
          this.loadEvents();
        },
        error: (error) => {
          console.error('Error deleting event:', error);
          this.snackBar.open('Error deleting event', 'Close', { duration: 3000 });
        }
      });
    }
  }

  getEventTypeName(type: EventType): string {
    return EventType[type];
  }
}
