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
      <span class="header__title">Focus Session Tracker</span>
      <nav class="header__nav">
        <a mat-button routerLink="/dashboard" routerLinkActive="header__nav-link--active" class="header__nav-link">
          <mat-icon>dashboard</mat-icon>
          Dashboard
        </a>
        <a mat-button routerLink="/sessions" routerLinkActive="header__nav-link--active" class="header__nav-link">
          <mat-icon>timer</mat-icon>
          Sessions
        </a>
        <a mat-button routerLink="/analytics" routerLinkActive="header__nav-link--active" class="header__nav-link">
          <mat-icon>analytics</mat-icon>
          Analytics
        </a>
        <a mat-button routerLink="/distractions" routerLinkActive="header__nav-link--active" class="header__nav-link">
          <mat-icon>notifications_off</mat-icon>
          Distractions
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
      box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
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
      display: flex;
      align-items: center;
      gap: 4px;
    }

    .header__nav-link--active {
      background-color: rgba(255, 255, 255, 0.1);
    }

    .header__nav-link mat-icon {
      font-size: 20px;
      height: 20px;
      width: 20px;
    }
  `]
})
export class Header {}
