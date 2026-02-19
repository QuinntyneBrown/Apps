import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatChipsModule } from '@angular/material/chips';
import { MatIconModule } from '@angular/material/icon';
import { Event } from '../models';

@Component({
  selector: 'app-event-card',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatChipsModule, MatIconModule],
  template: `
    <mat-card class="event-card" [class.event-card--cancelled]="event.isCancelled">
      <mat-card-header class="event-card__header">
        <mat-card-title class="event-card__title">{{ event.title }}</mat-card-title>
        <mat-card-subtitle class="event-card__subtitle">{{ event.startDateTime | date: 'medium' }}</mat-card-subtitle>
      </mat-card-header>
      <mat-card-content class="event-card__content">
        <p class="event-card__description">{{ event.description }}</p>
        <div class="event-card__details">
          <mat-chip-set>
            <mat-chip>{{ getEventTypeLabel(event.eventType) }}</mat-chip>
            <mat-chip *ngIf="event.location">
              <mat-icon>location_on</mat-icon>
              {{ event.location }}
            </mat-chip>
            <mat-chip>
              <mat-icon>people</mat-icon>
              {{ event.confirmedAttendeeCount }}
              <span *ngIf="event.maxAttendees"> / {{ event.maxAttendees }}</span>
            </mat-chip>
            <mat-chip *ngIf="event.isCancelled" class="event-card__cancelled-chip">Cancelled</mat-chip>
          </mat-chip-set>
        </div>
      </mat-card-content>
      <mat-card-actions class="event-card__actions">
        <button mat-button (click)="onView.emit(event)">View Details</button>
        <button mat-button (click)="onRSVP.emit(event)" *ngIf="!event.isCancelled">RSVP</button>
        <button mat-button (click)="onEdit.emit(event)" *ngIf="!event.isCancelled">Edit</button>
        <button mat-button color="warn" (click)="onCancel.emit(event)" *ngIf="!event.isCancelled">Cancel Event</button>
      </mat-card-actions>
    </mat-card>
  `,
  styles: [`
    .event-card {
      margin-bottom: 1rem;
    }

    .event-card--cancelled {
      opacity: 0.6;
    }

    .event-card__header {
      margin-bottom: 1rem;
    }

    .event-card__title {
      font-size: 1.25rem;
      margin-bottom: 0.25rem;
    }

    .event-card__description {
      margin-bottom: 1rem;
    }

    .event-card__details {
      margin-top: 0.5rem;
    }

    .event-card__cancelled-chip {
      background-color: #f44336 !important;
      color: white !important;
    }

    .event-card__actions {
      display: flex;
      gap: 0.5rem;
      flex-wrap: wrap;
    }
  `]
})
export class EventCard {
  @Input() event!: Event;
  @Output() onView = new EventEmitter<Event>();
  @Output() onEdit = new EventEmitter<Event>();
  @Output() onRSVP = new EventEmitter<Event>();
  @Output() onCancel = new EventEmitter<Event>();

  getEventTypeLabel(type: number): string {
    const types = ['Social', 'Meal', 'Sports', 'Outdoor', 'Cultural', 'Game Night', 'Travel', 'Celebration', 'Meeting', 'Other'];
    return types[type] || 'Other';
  }
}
