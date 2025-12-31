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
      <span class="header__title">Job Search Organizer</span>
      <nav class="header__nav">
        <a mat-button routerLink="/" routerLinkActive="header__nav-link--active" [routerLinkActiveOptions]="{exact: true}" class="header__nav-link">
          Dashboard
        </a>
        <a mat-button routerLink="/companies" routerLinkActive="header__nav-link--active" class="header__nav-link">
          Companies
        </a>
        <a mat-button routerLink="/applications" routerLinkActive="header__nav-link--active" class="header__nav-link">
          Applications
        </a>
        <a mat-button routerLink="/interviews" routerLinkActive="header__nav-link--active" class="header__nav-link">
          Interviews
        </a>
      </nav>
    </mat-toolbar>
  `,
  styles: [`
    .header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      padding: 0 24px;
    }

    .header__title {
      font-size: 20px;
      font-weight: 500;
    }

    .header__nav {
      display: flex;
      gap: 8px;
    }

    .header__nav-link {
      color: white;
    }

    .header__nav-link--active {
      background-color: rgba(255, 255, 255, 0.1);
    }
  `]
})
export class Header {}
