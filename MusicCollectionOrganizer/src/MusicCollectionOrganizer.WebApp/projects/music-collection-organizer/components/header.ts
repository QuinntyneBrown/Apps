import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterLink, MatToolbarModule, MatButtonModule],
  template: `
    <mat-toolbar color="primary" class="header">
      <span class="header__title">Music Collection Organizer</span>
      <nav class="header__nav">
        <a mat-button routerLink="/" routerLinkActive="header__nav-link--active" [routerLinkActiveOptions]="{exact: true}" class="header__nav-link">
          Dashboard
        </a>
        <a mat-button routerLink="/albums" routerLinkActive="header__nav-link--active" class="header__nav-link">
          Albums
        </a>
        <a mat-button routerLink="/artists" routerLinkActive="header__nav-link--active" class="header__nav-link">
          Artists
        </a>
        <a mat-button routerLink="/listening-logs" routerLinkActive="header__nav-link--active" class="header__nav-link">
          Listening Logs
        </a>
      </nav>
    </mat-toolbar>
  `,
  styles: [`
    .header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      gap: 1rem;
    }

    .header__title {
      font-size: 1.25rem;
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
      background-color: rgba(255, 255, 255, 0.1);
    }
  `]
})
export class Header {}
