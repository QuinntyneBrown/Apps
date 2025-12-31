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
      <span class="header__title">Password Account Auditor</span>
      <nav class="header__nav">
        <a mat-button routerLink="/" routerLinkActive="header__nav-link--active" [routerLinkActiveOptions]="{exact: true}" class="header__nav-link">
          <mat-icon>dashboard</mat-icon>
          Dashboard
        </a>
        <a mat-button routerLink="/accounts" routerLinkActive="header__nav-link--active" class="header__nav-link">
          <mat-icon>account_circle</mat-icon>
          Accounts
        </a>
        <a mat-button routerLink="/breach-alerts" routerLinkActive="header__nav-link--active" class="header__nav-link">
          <mat-icon>warning</mat-icon>
          Breach Alerts
        </a>
        <a mat-button routerLink="/security-audits" routerLinkActive="header__nav-link--active" class="header__nav-link">
          <mat-icon>security</mat-icon>
          Security Audits
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
      position: sticky;
      top: 0;
      z-index: 1000;
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
      display: flex;
      align-items: center;
      gap: 0.5rem;
    }

    .header__nav-link--active {
      background-color: rgba(255, 255, 255, 0.1);
    }

    @media (max-width: 768px) {
      .header {
        flex-direction: column;
        align-items: flex-start;
      }

      .header__nav {
        width: 100%;
        flex-wrap: wrap;
      }
    }
  `]
})
export class Header {}
