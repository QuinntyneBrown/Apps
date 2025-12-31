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
      <span class="header__title">Professional Network CRM</span>
      <nav class="header__nav">
        <a mat-button routerLink="/" routerLinkActive="header__nav-link--active" [routerLinkActiveOptions]="{exact: true}" class="header__nav-link">
          Dashboard
        </a>
        <a mat-button routerLink="/contacts" routerLinkActive="header__nav-link--active" class="header__nav-link">
          Contacts
        </a>
        <a mat-button routerLink="/follow-ups" routerLinkActive="header__nav-link--active" class="header__nav-link">
          Follow-ups
        </a>
        <a mat-button routerLink="/interactions" routerLinkActive="header__nav-link--active" class="header__nav-link">
          Interactions
        </a>
      </nav>
    </mat-toolbar>
  `,
  styles: [`
    .header {
      display: flex;
      justify-content: space-between;
      align-items: center;
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
      background-color: rgba(255, 255, 255, 0.1);
    }
  `]
})
export class Header { }
