import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { TastingNoteService } from '../services';
import { TastingNote } from '../models';

@Component({
  selector: 'app-tasting-notes-list',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule],
  template: `
    <div class="tasting-notes-list">
      <div class="tasting-notes-list__header">
        <h1 class="tasting-notes-list__title">Tasting Notes</h1>
        <button mat-raised-button color="primary" (click)="createTastingNote()">
          <mat-icon>add</mat-icon>
          New Tasting Note
        </button>
      </div>

      <div class="tasting-notes-list__grid" *ngIf="(tastingNoteService.tastingNotes$ | async) as notes">
        <mat-card class="tasting-notes-list__card" *ngFor="let note of notes" (click)="viewTastingNote(note.tastingNoteId)">
          <mat-card-header>
            <mat-card-title>
              <div class="tasting-notes-list__rating">
                <mat-icon *ngFor="let star of getStars(note.rating)" class="tasting-notes-list__star">star</mat-icon>
                <mat-icon *ngFor="let star of getEmptyStars(note.rating)" class="tasting-notes-list__star--empty">star_border</mat-icon>
              </div>
            </mat-card-title>
            <mat-card-subtitle>{{ note.tastingDate | date }}</mat-card-subtitle>
          </mat-card-header>
          <mat-card-content>
            <div class="tasting-notes-list__section" *ngIf="note.appearance">
              <strong>Appearance:</strong> {{ note.appearance }}
            </div>
            <div class="tasting-notes-list__section" *ngIf="note.aroma">
              <strong>Aroma:</strong> {{ note.aroma }}
            </div>
            <div class="tasting-notes-list__section" *ngIf="note.flavor">
              <strong>Flavor:</strong> {{ note.flavor }}
            </div>
            <div class="tasting-notes-list__section" *ngIf="note.mouthfeel">
              <strong>Mouthfeel:</strong> {{ note.mouthfeel }}
            </div>
            <div class="tasting-notes-list__section" *ngIf="note.overallImpression">
              <strong>Overall:</strong> {{ note.overallImpression }}
            </div>
          </mat-card-content>
          <mat-card-actions>
            <button mat-button color="primary" (click)="viewTastingNote(note.tastingNoteId); $event.stopPropagation()">
              <mat-icon>visibility</mat-icon>
              View
            </button>
            <button mat-button color="accent" (click)="editTastingNote(note.tastingNoteId); $event.stopPropagation()">
              <mat-icon>edit</mat-icon>
              Edit
            </button>
            <button mat-button color="warn" (click)="deleteTastingNote(note.tastingNoteId); $event.stopPropagation()">
              <mat-icon>delete</mat-icon>
              Delete
            </button>
          </mat-card-actions>
        </mat-card>

        <div *ngIf="notes.length === 0" class="tasting-notes-list__empty">
          <mat-icon class="tasting-notes-list__empty-icon">rate_review</mat-icon>
          <h2>No tasting notes yet</h2>
          <p>Record your first tasting experience!</p>
          <button mat-raised-button color="primary" (click)="createTastingNote()">
            <mat-icon>add</mat-icon>
            Create Tasting Note
          </button>
        </div>
      </div>
    </div>
  `,
  styles: [`
    .tasting-notes-list {
      padding: 2rem;
      max-width: 1400px;
      margin: 0 auto;

      &__header {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-bottom: 2rem;
      }

      &__title {
        font-size: 2rem;
        margin: 0;
      }

      &__grid {
        display: grid;
        grid-template-columns: repeat(auto-fill, minmax(350px, 1fr));
        gap: 1.5rem;
      }

      &__card {
        cursor: pointer;
        transition: transform 0.2s, box-shadow 0.2s;

        &:hover {
          transform: translateY(-4px);
          box-shadow: 0 4px 20px rgba(0, 0, 0, 0.15);
        }
      }

      &__rating {
        display: flex;
        align-items: center;
      }

      &__star {
        color: #ffd700;
        font-size: 1.5rem;
        height: 1.5rem;
        width: 1.5rem;

        &--empty {
          color: #ccc;
          font-size: 1.5rem;
          height: 1.5rem;
          width: 1.5rem;
        }
      }

      &__section {
        margin-bottom: 0.75rem;
        font-size: 0.9rem;

        strong {
          display: block;
          margin-bottom: 0.25rem;
          color: #666;
        }
      }

      &__empty {
        grid-column: 1 / -1;
        text-align: center;
        padding: 4rem 2rem;
        color: #666;

        &-icon {
          font-size: 4rem;
          height: 4rem;
          width: 4rem;
          color: #ccc;
        }

        h2 {
          margin: 1rem 0;
        }

        p {
          margin-bottom: 2rem;
        }
      }
    }
  `]
})
export class TastingNotesList implements OnInit {
  constructor(
    public tastingNoteService: TastingNoteService,
    private router: Router
  ) {}

  ngOnInit() {
    this.tastingNoteService.getTastingNotes().subscribe();
  }

  getStars(rating: number): number[] {
    return Array(Math.min(rating, 5)).fill(0);
  }

  getEmptyStars(rating: number): number[] {
    return Array(Math.max(5 - rating, 0)).fill(0);
  }

  createTastingNote() {
    this.router.navigate(['/tasting-notes/new']);
  }

  viewTastingNote(id: string) {
    this.router.navigate(['/tasting-notes', id]);
  }

  editTastingNote(id: string) {
    this.router.navigate(['/tasting-notes', id, 'edit']);
  }

  deleteTastingNote(id: string) {
    if (confirm('Are you sure you want to delete this tasting note?')) {
      this.tastingNoteService.deleteTastingNote(id).subscribe();
    }
  }
}
