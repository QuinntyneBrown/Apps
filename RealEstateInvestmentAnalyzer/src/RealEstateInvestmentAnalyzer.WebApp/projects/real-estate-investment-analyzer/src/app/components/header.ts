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
      <span class="header__title">Real Estate Investment Analyzer</span>
      <span class="header__spacer"></span>
      <nav class="header__nav">
        <a mat-button routerLink="/dashboard" routerLinkActive="header__nav-link--active" class="header__nav-link">
          <mat-icon>dashboard</mat-icon>
          Dashboard
        </a>
        <a mat-button routerLink="/properties" routerLinkActive="header__nav-link--active" class="header__nav-link">
          <mat-icon>home</mat-icon>
          Properties
        </a>
        <a mat-button routerLink="/leases" routerLinkActive="header__nav-link--active" class="header__nav-link">
          <mat-icon>assignment</mat-icon>
          Leases
        </a>
        <a mat-button routerLink="/expenses" routerLinkActive="header__nav-link--active" class="header__nav-link">
          <mat-icon>receipt</mat-icon>
          Expenses
        </a>
        <a mat-button routerLink="/cash-flows" routerLinkActive="header__nav-link--active" class="header__nav-link">
          <mat-icon>attach_money</mat-icon>
          Cash Flows
        </a>
      </nav>
    </mat-toolbar>
  `,
  styles: [`
    .header {
      position: sticky;
      top: 0;
      z-index: 1000;
    }

    .header__title {
      font-size: 1.25rem;
      font-weight: 500;
    }

    .header__spacer {
      flex: 1 1 auto;
    }

    .header__nav {
      display: flex;
      gap: 0.5rem;
    }

    .header__nav-link {
      display: flex;
      align-items: center;
      gap: 0.25rem;
    }

    .header__nav-link--active {
      background-color: rgba(255, 255, 255, 0.1);
    }
  `]
})
export class Header {}
