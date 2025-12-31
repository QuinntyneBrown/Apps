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
      <span class="header__title">Fishing Log Spot Tracker</span>
      <nav class="header__nav">
        <a mat-button routerLink="/dashboard" routerLinkActive="header__nav-link--active" class="header__nav-link">
          <mat-icon>dashboard</mat-icon>
          Dashboard
        </a>
        <a mat-button routerLink="/trips" routerLinkActive="header__nav-link--active" class="header__nav-link">
          <mat-icon>directions_boat</mat-icon>
          Trips
        </a>
        <a mat-button routerLink="/spots" routerLinkActive="header__nav-link--active" class="header__nav-link">
          <mat-icon>place</mat-icon>
          Spots
        </a>
        <a mat-button routerLink="/catches" routerLinkActive="header__nav-link--active" class="header__nav-link">
          <mat-icon>phishing</mat-icon>
          Catches
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
      display: flex;
      align-items: center;
      gap: 4px;
    }

    .header__nav-link--active {
      background-color: rgba(255, 255, 255, 0.1);
    }
  `]
})
export class Header {}
