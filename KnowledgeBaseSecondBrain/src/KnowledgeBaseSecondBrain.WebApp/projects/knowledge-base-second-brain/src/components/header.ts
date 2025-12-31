import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterLink, RouterLinkActive, MatToolbarModule, MatButtonModule],
  template: `
    <mat-toolbar color="primary" class="header">
      <span class="header__title">Knowledge Base Second Brain</span>
      <span class="header__spacer"></span>
      <nav class="header__nav">
        <a mat-button routerLink="/dashboard" routerLinkActive="header__nav-item--active" class="header__nav-item">
          Dashboard
        </a>
        <a mat-button routerLink="/notes" routerLinkActive="header__nav-item--active" class="header__nav-item">
          Notes
        </a>
        <a mat-button routerLink="/tags" routerLinkActive="header__nav-item--active" class="header__nav-item">
          Tags
        </a>
        <a mat-button routerLink="/note-links" routerLinkActive="header__nav-item--active" class="header__nav-item">
          Note Links
        </a>
        <a mat-button routerLink="/search-queries" routerLinkActive="header__nav-item--active" class="header__nav-item">
          Search Queries
        </a>
      </nav>
    </mat-toolbar>
  `,
  styles: [`
    .header {
      &__title {
        font-size: 1.25rem;
        font-weight: 500;
      }

      &__spacer {
        flex: 1 1 auto;
      }

      &__nav {
        display: flex;
        gap: 0.5rem;
      }

      &__nav-item {
        color: white;

        &--active {
          background-color: rgba(255, 255, 255, 0.1);
        }
      }
    }
  `]
})
export class Header {}
