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
      <span class="header__title">Personal Loan Comparison Tool</span>
      <nav class="header__nav">
        <a mat-button routerLink="/dashboard" routerLinkActive="header__nav-link--active" class="header__nav-link">
          Dashboard
        </a>
        <a mat-button routerLink="/loans" routerLinkActive="header__nav-link--active" class="header__nav-link">
          Loans
        </a>
        <a mat-button routerLink="/offers" routerLinkActive="header__nav-link--active" class="header__nav-link">
          Offers
        </a>
        <a mat-button routerLink="/payment-schedules" routerLinkActive="header__nav-link--active" class="header__nav-link">
          Payment Schedules
        </a>
      </nav>
    </mat-toolbar>
  `,
  styles: [`
    .header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      padding: 0 16px;
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
      background-color: rgba(255, 255, 255, 0.2);
    }
  `]
})
export class Header { }
