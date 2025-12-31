import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { PersonTagService } from '../services';
import { PersonTag, CreatePersonTagCommand } from '../models';

@Component({
  selector: 'app-people',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatListModule
  ],
  template: `
    <div class="people">
      <div class="people__header">
        <h1 class="people__title">People Tags</h1>
      </div>

      <mat-card class="people__create-card">
        <mat-card-header>
          <mat-card-title>Add Person Tag</mat-card-title>
        </mat-card-header>
        <mat-card-content class="people__create-content">
          <mat-form-field class="people__create-field">
            <mat-label>Photo ID</mat-label>
            <input matInput [(ngModel)]="newPersonTag.photoId" placeholder="Enter photo ID">
          </mat-form-field>
          <mat-form-field class="people__create-field">
            <mat-label>Person Name</mat-label>
            <input matInput [(ngModel)]="newPersonTag.personName" placeholder="Enter person name">
          </mat-form-field>
          <mat-form-field class="people__create-field">
            <mat-label>X Coordinate (optional)</mat-label>
            <input matInput type="number" [(ngModel)]="newPersonTag.coordinateX" placeholder="X">
          </mat-form-field>
          <mat-form-field class="people__create-field">
            <mat-label>Y Coordinate (optional)</mat-label>
            <input matInput type="number" [(ngModel)]="newPersonTag.coordinateY" placeholder="Y">
          </mat-form-field>
          <button mat-raised-button color="primary" (click)="createPersonTag()"
                  [disabled]="!newPersonTag.photoId || !newPersonTag.personName">
            <mat-icon>add</mat-icon>
            Add Person Tag
          </button>
        </mat-card-content>
      </mat-card>

      <div class="people__filter">
        <mat-form-field class="people__filter-field">
          <mat-label>Filter by Name</mat-label>
          <input matInput [(ngModel)]="filterName" (ngModelChange)="onFilterChange()" placeholder="Enter name to filter">
        </mat-form-field>
      </div>

      <div class="people__list">
        @for (personTag of personTags$ | async; track personTag.personTagId) {
          <mat-card class="people__card">
            <mat-card-content class="people__card-content">
              <div class="people__card-info">
                <mat-icon class="people__card-icon">person</mat-icon>
                <div class="people__card-details">
                  <h3 class="people__card-name">{{ personTag.personName }}</h3>
                  <p class="people__card-photo">Photo ID: {{ personTag.photoId }}</p>
                  @if (personTag.coordinateX !== null && personTag.coordinateY !== null) {
                    <p class="people__card-coords">Position: ({{ personTag.coordinateX }}, {{ personTag.coordinateY }})</p>
                  }
                  <p class="people__card-date">Created: {{ personTag.createdAt | date:'short' }}</p>
                </div>
              </div>
              <button mat-icon-button color="warn" (click)="deletePersonTag(personTag.personTagId)">
                <mat-icon>delete</mat-icon>
              </button>
            </mat-card-content>
          </mat-card>
        }
      </div>

      @if ((personTags$ | async)?.length === 0) {
        <div class="people__empty">
          <mat-icon class="people__empty-icon">person</mat-icon>
          <p class="people__empty-text">No person tags yet. Add your first person tag!</p>
        </div>
      }
    </div>
  `,
  styles: [`
    .people {
      padding: 2rem;

      &__header {
        margin-bottom: 2rem;
      }

      &__title {
        margin: 0;
        font-size: 2rem;
      }

      &__create-card {
        margin-bottom: 2rem;
      }

      &__create-content {
        display: grid;
        grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
        gap: 1rem;
        align-items: flex-start;

        button {
          margin-top: 0.5rem;
        }
      }

      &__create-field {
        width: 100%;
      }

      &__filter {
        margin-bottom: 2rem;

        &-field {
          width: 100%;
          max-width: 400px;
        }
      }

      &__list {
        display: grid;
        gap: 1rem;
      }

      &__card {
        &-content {
          display: flex;
          justify-content: space-between;
          align-items: center;
          padding: 1rem !important;
        }

        &-info {
          display: flex;
          align-items: flex-start;
          gap: 1rem;
        }

        &-icon {
          color: #2196f3;
          font-size: 2.5rem;
          width: 2.5rem;
          height: 2.5rem;
        }

        &-details {
          display: flex;
          flex-direction: column;
          gap: 0.25rem;
        }

        &-name {
          margin: 0;
          font-size: 1.25rem;
          font-weight: 500;
        }

        &-photo,
        &-coords,
        &-date {
          margin: 0;
          color: #666;
          font-size: 0.875rem;
        }
      }

      &__empty {
        text-align: center;
        padding: 4rem;
        color: #999;

        &-icon {
          font-size: 96px;
          width: 96px;
          height: 96px;
          margin-bottom: 1rem;
        }

        &-text {
          font-size: 1.25rem;
        }
      }
    }
  `]
})
export class People implements OnInit {
  personTags$ = this.personTagService.personTags$;
  filterName = '';

  newPersonTag = {
    photoId: '',
    personName: '',
    coordinateX: undefined as number | undefined,
    coordinateY: undefined as number | undefined
  };

  constructor(private personTagService: PersonTagService) {}

  ngOnInit(): void {
    this.personTagService.loadPersonTags();
  }

  onFilterChange(): void {
    this.personTagService.loadPersonTags(undefined, this.filterName || undefined);
  }

  createPersonTag(): void {
    if (!this.newPersonTag.photoId || !this.newPersonTag.personName) return;

    const command: CreatePersonTagCommand = {
      photoId: this.newPersonTag.photoId,
      personName: this.newPersonTag.personName,
      coordinateX: this.newPersonTag.coordinateX,
      coordinateY: this.newPersonTag.coordinateY
    };

    this.personTagService.createPersonTag(command).subscribe(() => {
      this.newPersonTag = {
        photoId: '',
        personName: '',
        coordinateX: undefined,
        coordinateY: undefined
      };
      this.personTagService.loadPersonTags(undefined, this.filterName || undefined);
    });
  }

  deletePersonTag(id: string): void {
    if (confirm('Are you sure you want to delete this person tag?')) {
      this.personTagService.deletePersonTag(id).subscribe(() => {
        this.personTagService.loadPersonTags(undefined, this.filterName || undefined);
      });
    }
  }
}
