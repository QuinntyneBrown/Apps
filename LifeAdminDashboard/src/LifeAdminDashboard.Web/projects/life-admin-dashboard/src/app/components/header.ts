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
      <span class="header__title">Life Admin Dashboard</span>
      <nav class="header__nav">
        <a mat-button routerLink="/" routerLinkActive="header__nav-link--active" [routerLinkActiveOptions]="{exact: true}" class="header__nav-link">
          Dashboard
        </a>
        <a mat-button routerLink="/admin-tasks" routerLinkActive="header__nav-link--active" class="header__nav-link">
          Admin Tasks
        </a>
        <a mat-button routerLink="/deadlines" routerLinkActive="header__nav-link--active" class="header__nav-link">
          Deadlines
        </a>
        <a mat-button routerLink="/renewals" routerLinkActive="header__nav-link--active" class="header__nav-link">
          Renewals
        </a>
      </nav>
    </mat-toolbar>
  `,
  styles: [`
    .header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      position: sticky;
      top: 0;
      z-index: 1000;

      &__title {
        font-size: 1.5rem;
        font-weight: 500;
      }

      &__nav {
        display: flex;
        gap: 0.5rem;
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
