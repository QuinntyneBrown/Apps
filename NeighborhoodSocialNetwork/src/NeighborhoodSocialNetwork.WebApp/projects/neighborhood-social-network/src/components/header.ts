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
      <span class="header__title">Neighborhood Social Network</span>
      <nav class="header__nav">
        <a mat-button routerLink="/" routerLinkActive="header__nav-link--active" [routerLinkActiveOptions]="{exact: true}" class="header__nav-link">
          Dashboard
        </a>
        <a mat-button routerLink="/neighbors" routerLinkActive="header__nav-link--active" class="header__nav-link">
          Neighbors
        </a>
        <a mat-button routerLink="/events" routerLinkActive="header__nav-link--active" class="header__nav-link">
          Events
        </a>
        <a mat-button routerLink="/messages" routerLinkActive="header__nav-link--active" class="header__nav-link">
          Messages
        </a>
        <a mat-button routerLink="/recommendations" routerLinkActive="header__nav-link--active" class="header__nav-link">
          Recommendations
        </a>
      </nav>
    </mat-toolbar>
  `,
  styles: [`
    .header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 20px;

      &__title {
        font-size: 1.5rem;
        font-weight: 500;
      }

      &__nav {
        display: flex;
        gap: 8px;
      }

      &__nav-link {
        color: white;

        &--active {
          background-color: rgba(255, 255, 255, 0.1);
        }
      }
    }
  `]
})
export class Header {}
