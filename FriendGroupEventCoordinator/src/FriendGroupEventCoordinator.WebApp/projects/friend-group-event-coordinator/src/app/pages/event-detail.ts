import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatChipsModule } from '@angular/material/chips';
import { EventsService, RSVPsService } from '../services';
import { Event, RSVP } from '../models';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-event-detail',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatCardModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatChipsModule
  ],
  template: `
    <div class="event-detail" *ngIf="event$ | async as event; else loading">
      <div class="event-detail__header">
        <button mat-icon-button (click)="goBack()" class="event-detail__back-btn">
          <mat-icon>arrow_back</mat-icon>
        </button>
        <h1 class="event-detail__title">{{ event.title }}</h1>
      </div>

      <mat-card class="event-detail__info">
        <mat-card-content>
          <p class="event-detail__description">{{ event.description }}</p>
          <div class="event-detail__meta">
            <p><strong>Date:</strong> {{ event.startDateTime | date: 'medium' }}</p>
            <p *ngIf="event.endDateTime"><strong>End:</strong> {{ event.endDateTime | date: 'medium' }}</p>
            <p *ngIf="event.location"><strong>Location:</strong> {{ event.location }}</p>
            <p><strong>Type:</strong> {{ getEventTypeLabel(event.eventType) }}</p>
            <p><strong>Attendees:</strong> {{ event.confirmedAttendeeCount }}
              <span *ngIf="event.maxAttendees"> / {{ event.maxAttendees }}</span>
            </p>
            <p><strong>Status:</strong> {{ event.isCancelled ? 'Cancelled' : 'Active' }}</p>
          </div>

          <div class="event-detail__actions">
            <button mat-raised-button color="primary" *ngIf="!event.isCancelled">
              <mat-icon>check_circle</mat-icon>
              RSVP
            </button>
            <button mat-raised-button *ngIf="!event.isCancelled">
              <mat-icon>edit</mat-icon>
              Edit
            </button>
            <button mat-raised-button color="warn" (click)="cancelEvent(event)" *ngIf="!event.isCancelled">
              <mat-icon>cancel</mat-icon>
              Cancel Event
            </button>
          </div>
        </mat-card-content>
      </mat-card>

      <mat-card class="event-detail__rsvps">
        <mat-card-header>
          <mat-card-title>RSVPs</mat-card-title>
        </mat-card-header>
        <mat-card-content>
          <div *ngIf="rsvps$ | async as rsvps; else loadingRsvps">
            <p *ngIf="rsvps.length === 0">No RSVPs yet.</p>
            <div *ngFor="let rsvp of rsvps" class="event-detail__rsvp-item">
              <mat-chip-set>
                <mat-chip [class]="'event-detail__rsvp-chip--' + getRSVPResponseLabel(rsvp.response).toLowerCase()">
                  {{ getRSVPResponseLabel(rsvp.response) }}
                </mat-chip>
              </mat-chip-set>
              <span *ngIf="rsvp.additionalGuests > 0"> +{{ rsvp.additionalGuests }} guest<span *ngIf="rsvp.additionalGuests > 1">s</span></span>
              <p *ngIf="rsvp.notes" class="event-detail__rsvp-notes">{{ rsvp.notes }}</p>
            </div>
          </div>
          <ng-template #loadingRsvps>
            <mat-spinner diameter="30"></mat-spinner>
          </ng-template>
        </mat-card-content>
      </mat-card>
    </div>

    <ng-template #loading>
      <div class="event-detail__loading">
        <mat-spinner></mat-spinner>
      </div>
    </ng-template>
  `,
  styles: [`
    .event-detail {
      padding: 2rem;
    }

    .event-detail__header {
      display: flex;
      align-items: center;
      gap: 1rem;
      margin-bottom: 2rem;
    }

    .event-detail__back-btn {
      margin-right: 0.5rem;
    }

    .event-detail__title {
      margin: 0;
      font-size: 2rem;
    }

    .event-detail__info {
      margin-bottom: 2rem;
    }

    .event-detail__description {
      font-size: 1.1rem;
      margin-bottom: 1.5rem;
    }

    .event-detail__meta p {
      margin: 0.5rem 0;
    }

    .event-detail__actions {
      display: flex;
      gap: 1rem;
      margin-top: 1.5rem;
      flex-wrap: wrap;
    }

    .event-detail__rsvps {
      margin-top: 2rem;
    }

    .event-detail__rsvp-item {
      padding: 1rem;
      border-bottom: 1px solid #e0e0e0;
    }

    .event-detail__rsvp-item:last-child {
      border-bottom: none;
    }

    .event-detail__rsvp-notes {
      margin-top: 0.5rem;
      font-style: italic;
      color: rgba(0, 0, 0, 0.6);
    }

    .event-detail__rsvp-chip--yes {
      background-color: #4caf50 !important;
      color: white !important;
    }

    .event-detail__rsvp-chip--no {
      background-color: #f44336 !important;
      color: white !important;
    }

    .event-detail__rsvp-chip--maybe {
      background-color: #ff9800 !important;
      color: white !important;
    }

    .event-detail__rsvp-chip--pending {
      background-color: #9e9e9e !important;
      color: white !important;
    }

    .event-detail__loading {
      display: flex;
      justify-content: center;
      align-items: center;
      padding: 3rem;
    }
  `]
})
export class EventDetail implements OnInit {
  event$!: Observable<Event>;
  rsvps$!: Observable<RSVP[]>;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private eventsService: EventsService,
    private rsvpsService: RSVPsService
  ) {}

  ngOnInit(): void {
    const eventId = this.route.snapshot.paramMap.get('id');
    if (eventId) {
      this.event$ = this.eventsService.getEvent(eventId);
      this.rsvps$ = this.rsvpsService.getRSVPsByEvent(eventId);
    }
  }

  goBack(): void {
    this.router.navigate(['/events']);
  }

  getEventTypeLabel(type: number): string {
    const types = ['Social', 'Meal', 'Sports', 'Outdoor', 'Cultural', 'Game Night', 'Travel', 'Celebration', 'Meeting', 'Other'];
    return types[type] || 'Other';
  }

  getRSVPResponseLabel(response: number): string {
    const responses = ['Pending', 'Yes', 'No', 'Maybe'];
    return responses[response] || 'Pending';
  }

  cancelEvent(event: Event): void {
    if (confirm(`Are you sure you want to cancel "${event.title}"?`)) {
      this.eventsService.cancelEvent(event.eventId).subscribe(() => {
        this.router.navigate(['/events']);
      });
    }
  }
}
