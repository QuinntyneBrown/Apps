import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { NoteService, TagService, NoteLinkService, SearchQueryService } from '../services';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink, MatCardModule, MatButtonModule, MatIconModule],
  template: `
    <div class="dashboard">
      <h1 class="dashboard__title">Dashboard</h1>
      <div class="dashboard__cards">
        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-card-title class="dashboard__card-title">
              <mat-icon class="dashboard__card-icon">note</mat-icon>
              Notes
            </mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <p class="dashboard__card-count">{{ (noteService.notes$ | async)?.length || 0 }}</p>
            <p class="dashboard__card-description">Total notes in your knowledge base</p>
          </mat-card-content>
          <mat-card-actions>
            <a mat-button color="primary" routerLink="/notes">View Notes</a>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-card-title class="dashboard__card-title">
              <mat-icon class="dashboard__card-icon">label</mat-icon>
              Tags
            </mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <p class="dashboard__card-count">{{ (tagService.tags$ | async)?.length || 0 }}</p>
            <p class="dashboard__card-description">Total tags for organization</p>
          </mat-card-content>
          <mat-card-actions>
            <a mat-button color="primary" routerLink="/tags">View Tags</a>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-card-title class="dashboard__card-title">
              <mat-icon class="dashboard__card-icon">link</mat-icon>
              Note Links
            </mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <p class="dashboard__card-count">{{ (noteLinkService.noteLinks$ | async)?.length || 0 }}</p>
            <p class="dashboard__card-description">Connections between notes</p>
          </mat-card-content>
          <mat-card-actions>
            <a mat-button color="primary" routerLink="/note-links">View Links</a>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-card-title class="dashboard__card-title">
              <mat-icon class="dashboard__card-icon">search</mat-icon>
              Search Queries
            </mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <p class="dashboard__card-count">{{ (searchQueryService.searchQueries$ | async)?.length || 0 }}</p>
            <p class="dashboard__card-description">Saved search queries</p>
          </mat-card-content>
          <mat-card-actions>
            <a mat-button color="primary" routerLink="/search-queries">View Queries</a>
          </mat-card-actions>
        </mat-card>
      </div>
    </div>
  `,
  styles: [`
    .dashboard {
      padding: 2rem;

      &__title {
        margin: 0 0 2rem 0;
        color: #333;
      }

      &__cards {
        display: grid;
        grid-template-columns: repeat(auto-fill, minmax(280px, 1fr));
        gap: 1.5rem;
      }

      &__card {
        &-title {
          display: flex;
          align-items: center;
          gap: 0.5rem;
        }

        &-icon {
          color: #3f51b5;
        }

        &-count {
          font-size: 2.5rem;
          font-weight: bold;
          margin: 1rem 0;
          color: #3f51b5;
        }

        &-description {
          color: #666;
          margin: 0;
        }
      }
    }
  `]
})
export class Dashboard implements OnInit {
  readonly noteService = inject(NoteService);
  readonly tagService = inject(TagService);
  readonly noteLinkService = inject(NoteLinkService);
  readonly searchQueryService = inject(SearchQueryService);

  ngOnInit() {
    this.noteService.getNotes().subscribe();
    this.tagService.getTags().subscribe();
    this.noteLinkService.getNoteLinks().subscribe();
    this.searchQueryService.getSearchQueries().subscribe();
  }
}
