import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { RouterLink } from '@angular/router';
import { BucketListItem, Category, Priority, ItemStatus } from '../../models';

@Component({
  selector: 'app-bucket-list-item-card',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule, MatChipsModule, RouterLink],
  template: `
    <mat-card class="bucket-list-item-card">
      <mat-card-header>
        <mat-card-title class="bucket-list-item-card__title">{{ item.title }}</mat-card-title>
        <mat-card-subtitle>
          <mat-chip-set class="bucket-list-item-card__chips">
            <mat-chip [class]="'bucket-list-item-card__chip--' + getCategoryClass()">
              {{ getCategoryLabel() }}
            </mat-chip>
            <mat-chip [class]="'bucket-list-item-card__chip--' + getPriorityClass()">
              {{ getPriorityLabel() }}
            </mat-chip>
            <mat-chip [class]="'bucket-list-item-card__chip--' + getStatusClass()">
              {{ getStatusLabel() }}
            </mat-chip>
          </mat-chip-set>
        </mat-card-subtitle>
      </mat-card-header>
      <mat-card-content>
        <p class="bucket-list-item-card__description">{{ item.description }}</p>
        <div class="bucket-list-item-card__dates" *ngIf="item.targetDate || item.completedDate">
          <p *ngIf="item.targetDate" class="bucket-list-item-card__date">
            <mat-icon>event</mat-icon>
            Target: {{ item.targetDate | date }}
          </p>
          <p *ngIf="item.completedDate" class="bucket-list-item-card__date">
            <mat-icon>check_circle</mat-icon>
            Completed: {{ item.completedDate | date }}
          </p>
        </div>
      </mat-card-content>
      <mat-card-actions>
        <button mat-button color="primary" [routerLink]="['/items', item.bucketListItemId]">
          <mat-icon>visibility</mat-icon>
          View Details
        </button>
        <button mat-button (click)="onEdit()">
          <mat-icon>edit</mat-icon>
          Edit
        </button>
        <button mat-button color="warn" (click)="onDelete()">
          <mat-icon>delete</mat-icon>
          Delete
        </button>
      </mat-card-actions>
    </mat-card>
  `,
  styles: [`
    .bucket-list-item-card {
      margin: 16px 0;

      &__title {
        font-size: 1.25rem;
        font-weight: 500;
      }

      &__chips {
        margin-top: 8px;
      }

      &__chip {
        &--travel { background-color: #2196F3; color: white; }
        &--adventure { background-color: #FF9800; color: white; }
        &--career { background-color: #4CAF50; color: white; }
        &--learning { background-color: #9C27B0; color: white; }
        &--health { background-color: #F44336; color: white; }
        &--relationships { background-color: #E91E63; color: white; }
        &--creative { background-color: #00BCD4; color: white; }
        &--other { background-color: #607D8B; color: white; }

        &--low { background-color: #8BC34A; color: white; }
        &--medium { background-color: #FFC107; color: white; }
        &--high { background-color: #FF5722; color: white; }
        &--critical { background-color: #D32F2F; color: white; }

        &--not-started { background-color: #9E9E9E; color: white; }
        &--in-progress { background-color: #2196F3; color: white; }
        &--completed { background-color: #4CAF50; color: white; }
        &--on-hold { background-color: #FF9800; color: white; }
        &--cancelled { background-color: #F44336; color: white; }
      }

      &__description {
        margin: 16px 0;
        color: rgba(0, 0, 0, 0.6);
      }

      &__dates {
        margin-top: 16px;
      }

      &__date {
        display: flex;
        align-items: center;
        gap: 8px;
        margin: 4px 0;
        font-size: 0.9rem;
        color: rgba(0, 0, 0, 0.6);

        mat-icon {
          font-size: 18px;
          width: 18px;
          height: 18px;
        }
      }
    }
  `]
})
export class BucketListItemCard {
  @Input() item!: BucketListItem;
  @Output() edit = new EventEmitter<BucketListItem>();
  @Output() delete = new EventEmitter<string>();

  onEdit(): void {
    this.edit.emit(this.item);
  }

  onDelete(): void {
    this.delete.emit(this.item.bucketListItemId);
  }

  getCategoryLabel(): string {
    return Category[this.item.category];
  }

  getCategoryClass(): string {
    return Category[this.item.category].toLowerCase();
  }

  getPriorityLabel(): string {
    return Priority[this.item.priority];
  }

  getPriorityClass(): string {
    return Priority[this.item.priority].toLowerCase();
  }

  getStatusLabel(): string {
    const status = ItemStatus[this.item.status];
    return status.replace(/([A-Z])/g, ' $1').trim();
  }

  getStatusClass(): string {
    return ItemStatus[this.item.status].replace(/([A-Z])/g, '-$1').toLowerCase().substring(1);
  }
}
