import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    RouterLink,
    RouterLinkActive,
    MatToolbarModule,
    MatButtonModule,
    MatIconModule
  ],
  template: `
    <mat-toolbar color="primary" class="header">
      <span class="header__title">Men's Group Discussion Tracker</span>
      <span class="header__spacer"></span>
      <nav class="header__nav">
        <a mat-button routerLink="/" routerLinkActive="header__nav-link--active" [routerLinkActiveOptions]="{exact: true}" class="header__nav-link">
          <mat-icon>dashboard</mat-icon>
          Dashboard
        </a>
        <a mat-button routerLink="/groups" routerLinkActive="header__nav-link--active" class="header__nav-link">
          <mat-icon>groups</mat-icon>
          Groups
        </a>
        <a mat-button routerLink="/meetings" routerLinkActive="header__nav-link--active" class="header__nav-link">
          <mat-icon>event</mat-icon>
          Meetings
        </a>
        <a mat-button routerLink="/topics" routerLinkActive="header__nav-link--active" class="header__nav-link">
          <mat-icon>topic</mat-icon>
          Topics
        </a>
        <a mat-button routerLink="/resources" routerLinkActive="header__nav-link--active" class="header__nav-link">
          <mat-icon>library_books</mat-icon>
          Resources
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

      &__nav-link {
        display: flex;
        align-items: center;
        gap: 0.25rem;

        &--active {
          background-color: rgba(255, 255, 255, 0.1);
        }

        mat-icon {
          font-size: 1.25rem;
          width: 1.25rem;
          height: 1.25rem;
        }
      }
    }
  `]
})
export class Header {}
