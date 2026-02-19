import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatChipsModule } from '@angular/material/chips';
import { MatIconModule } from '@angular/material/icon';
import { Group } from '../models';

@Component({
  selector: 'app-group-card',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatChipsModule, MatIconModule],
  template: `
    <mat-card class="group-card" [class.group-card--inactive]="!group.isActive">
      <mat-card-header class="group-card__header">
        <mat-card-title class="group-card__title">{{ group.name }}</mat-card-title>
        <mat-card-subtitle class="group-card__subtitle" *ngIf="group.description">{{ group.description }}</mat-card-subtitle>
      </mat-card-header>
      <mat-card-content class="group-card__content">
        <div class="group-card__details">
          <mat-chip-set>
            <mat-chip>
              <mat-icon>people</mat-icon>
              {{ group.activeMemberCount }} members
            </mat-chip>
            <mat-chip *ngIf="!group.isActive" class="group-card__inactive-chip">Inactive</mat-chip>
          </mat-chip-set>
        </div>
        <p class="group-card__created">Created {{ group.createdAt | date: 'short' }}</p>
      </mat-card-content>
      <mat-card-actions class="group-card__actions">
        <button mat-button (click)="onView.emit(group)">View Details</button>
        <button mat-button (click)="onViewMembers.emit(group)">Members</button>
        <button mat-button (click)="onViewEvents.emit(group)">Events</button>
        <button mat-button (click)="onEdit.emit(group)" *ngIf="group.isActive">Edit</button>
        <button mat-button color="warn" (click)="onDeactivate.emit(group)" *ngIf="group.isActive">Deactivate</button>
      </mat-card-actions>
    </mat-card>
  `,
  styles: [`
    .group-card {
      margin-bottom: 1rem;
    }

    .group-card--inactive {
      opacity: 0.6;
    }

    .group-card__header {
      margin-bottom: 1rem;
    }

    .group-card__title {
      font-size: 1.25rem;
      margin-bottom: 0.25rem;
    }

    .group-card__details {
      margin-bottom: 1rem;
    }

    .group-card__created {
      font-size: 0.875rem;
      color: rgba(0, 0, 0, 0.6);
      margin: 0;
    }

    .group-card__inactive-chip {
      background-color: #9e9e9e !important;
      color: white !important;
    }

    .group-card__actions {
      display: flex;
      gap: 0.5rem;
      flex-wrap: wrap;
    }
  `]
})
export class GroupCard {
  @Input() group!: Group;
  @Output() onView = new EventEmitter<Group>();
  @Output() onEdit = new EventEmitter<Group>();
  @Output() onViewMembers = new EventEmitter<Group>();
  @Output() onViewEvents = new EventEmitter<Group>();
  @Output() onDeactivate = new EventEmitter<Group>();
}
