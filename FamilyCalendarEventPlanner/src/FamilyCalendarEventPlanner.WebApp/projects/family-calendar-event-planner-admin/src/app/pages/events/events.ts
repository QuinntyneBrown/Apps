import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog } from '@angular/material/dialog';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatChipsModule } from '@angular/material/chips';
import { BehaviorSubject, switchMap } from 'rxjs';
import { EventsService } from '../../services/events.service';
import { CalendarEventDto, getEventTypeLabel, getEventStatusLabel } from '../../models/calendar-event-dto';
import { CreateOrEditEventDialog, CreateOrEditEventDialogResult } from '../../components/create-or-edit-event-dialog';

@Component({
  selector: 'app-events',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatTooltipModule,
    MatChipsModule
  ],
  templateUrl: './events.html',
  styleUrls: ['./events.scss']
})
export class Events {
  private eventsService = inject(EventsService);
  private dialog = inject(MatDialog);

  private refresh$ = new BehaviorSubject<void>(undefined);

  displayedColumns: string[] = ['title', 'eventType', 'startTime', 'endTime', 'location', 'status', 'actions'];

  events$ = this.refresh$.pipe(
    switchMap(() => this.eventsService.getEvents())
  );

  getEventTypeLabel(eventType: string): string {
    return getEventTypeLabel(eventType as any);
  }

  getEventStatusLabel(status: string): string {
    return getEventStatusLabel(status as any);
  }

  formatDateTime(dateTime: string): string {
    return new Date(dateTime).toLocaleString();
  }

  getStatusClass(status: string): string {
    switch (status) {
      case 'Scheduled': return 'status-scheduled';
      case 'Completed': return 'status-completed';
      case 'Cancelled': return 'status-cancelled';
      default: return '';
    }
  }

  onCreateEvent(): void {
    const dialogRef = this.dialog.open(CreateOrEditEventDialog, {
      width: '550px',
      data: {}
    });

    dialogRef.afterClosed().subscribe((result: CreateOrEditEventDialogResult) => {
      if (result?.action === 'create' && result.data) {
        this.eventsService.createEvent(result.data).subscribe({
          next: () => {
            this.refresh$.next();
          },
          error: (error) => {
            console.error('Error creating event:', error);
          }
        });
      }
    });
  }

  onEditEvent(event: CalendarEventDto): void {
    const dialogRef = this.dialog.open(CreateOrEditEventDialog, {
      width: '550px',
      data: { event }
    });

    dialogRef.afterClosed().subscribe((result: CreateOrEditEventDialogResult) => {
      if (result?.action === 'update' && result.data) {
        this.eventsService.updateEvent(event.eventId, { ...result.data, eventId: event.eventId }).subscribe({
          next: () => {
            this.refresh$.next();
          },
          error: (error) => {
            console.error('Error updating event:', error);
          }
        });
      }
    });
  }

  onDeleteEvent(event: CalendarEventDto): void {
    if (confirm(`Are you sure you want to delete "${event.title}"?`)) {
      this.eventsService.deleteEvent(event.eventId).subscribe({
        next: () => {
          this.refresh$.next();
        },
        error: (error) => {
          console.error('Error deleting event:', error);
        }
      });
    }
  }
}
