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
      <span class="header__title">Investment Portfolio Tracker</span>
      <nav class="header__nav">
        <a mat-button routerLink="/dashboard" routerLinkActive="active">
          <mat-icon>dashboard</mat-icon>
          Dashboard
        </a>
        <a mat-button routerLink="/accounts" routerLinkActive="active">
          <mat-icon>account_balance</mat-icon>
          Accounts
        </a>
        <a mat-button routerLink="/holdings" routerLinkActive="active">
          <mat-icon>pie_chart</mat-icon>
          Holdings
        </a>
        <a mat-button routerLink="/transactions" routerLinkActive="active">
          <mat-icon>receipt_long</mat-icon>
          Transactions
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
      margin-right: 2rem;
    }
    .header__nav {
      display: flex;
      gap: 0.5rem;
    }
    .header__nav a.active {
      background-color: rgba(255, 255, 255, 0.1);
    }
  `]
})
export class Header {}
