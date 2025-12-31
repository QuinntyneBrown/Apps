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
      <span class="header__title">Mortgage Payoff Optimizer</span>
      <nav class="header__nav">
        <a mat-button routerLink="/" routerLinkActive="header__nav-item--active" [routerLinkActiveOptions]="{exact: true}" class="header__nav-item">
          Dashboard
        </a>
        <a mat-button routerLink="/mortgages" routerLinkActive="header__nav-item--active" class="header__nav-item">
          Mortgages
        </a>
        <a mat-button routerLink="/payments" routerLinkActive="header__nav-item--active" class="header__nav-item">
          Payments
        </a>
        <a mat-button routerLink="/refinance-scenarios" routerLinkActive="header__nav-item--active" class="header__nav-item">
          Refinance Scenarios
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

    .header__nav-item {
      color: white;
    }

    .header__nav-item--active {
      background-color: rgba(255, 255, 255, 0.1);
    }
  `]
})
export class Header { }
