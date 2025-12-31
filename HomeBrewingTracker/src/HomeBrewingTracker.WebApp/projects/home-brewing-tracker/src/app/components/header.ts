import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterLink, RouterLinkActive, MatToolbarModule, MatButtonModule, MatIconModule],
  template: `
    <mat-toolbar color="primary" class="header">
      <span class="header__title">Home Brewing Tracker</span>
      <nav class="header__nav">
        <a mat-button routerLink="/" routerLinkActive="header__nav-link--active" [routerLinkActiveOptions]="{exact: true}" class="header__nav-link">
          <mat-icon>home</mat-icon>
          Home
        </a>
        <a mat-button routerLink="/recipes" routerLinkActive="header__nav-link--active" class="header__nav-link">
          <mat-icon>book</mat-icon>
          Recipes
        </a>
        <a mat-button routerLink="/batches" routerLinkActive="header__nav-link--active" class="header__nav-link">
          <mat-icon>science</mat-icon>
          Batches
        </a>
        <a mat-button routerLink="/tasting-notes" routerLinkActive="header__nav-link--active" class="header__nav-link">
          <mat-icon>rate_review</mat-icon>
          Tasting Notes
        </a>
      </nav>
    </mat-toolbar>
  `,
  styles: [`
    .header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      padding: 0 1rem;

      &__title {
        font-size: 1.5rem;
        font-weight: 500;
      }

      &__nav {
        display: flex;
        gap: 0.5rem;

        &-link {
          display: flex;
          align-items: center;
          gap: 0.25rem;

          mat-icon {
            font-size: 1.25rem;
            height: 1.25rem;
            width: 1.25rem;
          }

          &--active {
            background-color: rgba(255, 255, 255, 0.1);
          }
        }
      }
    }
  `]
})
export class Header {}
