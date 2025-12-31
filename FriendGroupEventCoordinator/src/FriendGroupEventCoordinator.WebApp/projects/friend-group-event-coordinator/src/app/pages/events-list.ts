import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { EventsService } from '../services';
import { EventCard } from '../components';
import { Event } from '../models';

@Component({
  selector: 'app-events-list',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatDialogModule,
    EventCard
  ],
  template: `
    <div class="events-list">
      <div class="events-list__header">
        <h1 class="events-list__title">Events</h1>
        <button mat-raised-button color="primary" class="events-list__create-btn">
          <mat-icon>add</mat-icon>
          Create Event
        </button>
      </div>

      <div class="events-list__content" *ngIf="events$ | async as events; else loading">
        <div class="events-list__empty" *ngIf="events.length === 0">
          <p>No events found. Create your first event to get started!</p>
        </div>
        <div class="events-list__grid" *ngIf="events.length > 0">
          <app-event-card
            *ngFor="let event of events"
            [event]="event"
            (onView)="viewEvent($event)"
            (onEdit)="editEvent($event)"
            (onRSVP)="rsvpEvent($event)"
            (onCancel)="cancelEvent($event)"
          ></app-event-card>
        </div>
      </div>

      <ng-template #loading>
        <div class="events-list__loading">
          <mat-spinner></mat-spinner>
        </div>
      </ng-template>
    </div>
  `,
  styles: [`
    .events-list {
      padding: 2rem;
    }

    .events-list__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 2rem;
    }

    .events-list__title {
      margin: 0;
      font-size: 2rem;
    }

    .events-list__create-btn {
      display: flex;
      align-items: center;
      gap: 0.5rem;
    }

    .events-list__grid {
      display: grid;
      grid-template-columns: repeat(auto-fill, minmax(350px, 1fr));
      gap: 1.5rem;
    }

    .events-list__loading,
    .events-list__empty {
      display: flex;
      justify-content: center;
      align-items: center;
      padding: 3rem;
    }
  `]
})
export class EventsList implements OnInit {
  events$ = this.eventsService.events$;

  constructor(
    private eventsService: EventsService,
    private router: Router,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.eventsService.getEvents().subscribe();
  }

  viewEvent(event: Event): void {
    this.router.navigate(['/events', event.eventId]);
  }

  editEvent(event: Event): void {
    // TODO: Open edit dialog
    console.log('Edit event', event);
  }

  rsvpEvent(event: Event): void {
    // TODO: Open RSVP dialog
    console.log('RSVP event', event);
  }

  cancelEvent(event: Event): void {
    if (confirm(`Are you sure you want to cancel "${event.title}"?`)) {
      this.eventsService.cancelEvent(event.eventId).subscribe();
    }
  }
}
