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
      <span class="header__title">Performance Review Prep Tool</span>
      <nav class="header__nav">
        <a mat-button routerLink="/" routerLinkActive="header__nav-link--active" [routerLinkActiveOptions]="{exact: true}" class="header__nav-link">
          Dashboard
        </a>
        <a mat-button routerLink="/review-periods" routerLinkActive="header__nav-link--active" class="header__nav-link">
          Review Periods
        </a>
        <a mat-button routerLink="/achievements" routerLinkActive="header__nav-link--active" class="header__nav-link">
          Achievements
        </a>
        <a mat-button routerLink="/goals" routerLinkActive="header__nav-link--active" class="header__nav-link">
          Goals
        </a>
        <a mat-button routerLink="/feedbacks" routerLinkActive="header__nav-link--active" class="header__nav-link">
          Feedbacks
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
    }

    .header__title {
      font-size: 1.5rem;
      font-weight: 500;
    }

    .header__nav {
      display: flex;
      gap: 0.5rem;
    }

    .header__nav-link {
      color: white;
    }

    .header__nav-link--active {
      background-color: rgba(255, 255, 255, 0.2);
    }
  `]
})
export class Header {}
