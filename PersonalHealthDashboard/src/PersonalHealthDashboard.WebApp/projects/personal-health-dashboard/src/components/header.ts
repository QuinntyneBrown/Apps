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
      <div class="header__container">
        <a routerLink="/" class="header__logo">
          <mat-icon class="header__logo-icon">favorite</mat-icon>
          <span class="header__logo-text">Personal Health Dashboard</span>
        </a>
        <nav class="header__nav">
          <a
            mat-button
            routerLink="/"
            routerLinkActive="header__nav-link--active"
            [routerLinkActiveOptions]="{exact: true}"
            class="header__nav-link">
            Dashboard
          </a>
          <a
            mat-button
            routerLink="/vitals"
            routerLinkActive="header__nav-link--active"
            class="header__nav-link">
            Vitals
          </a>
          <a
            mat-button
            routerLink="/health-trends"
            routerLinkActive="header__nav-link--active"
            class="header__nav-link">
            Health Trends
          </a>
          <a
            mat-button
            routerLink="/wearable-data"
            routerLinkActive="header__nav-link--active"
            class="header__nav-link">
            Wearable Data
          </a>
        </nav>
      </div>
    </mat-toolbar>
  `,
  styles: [`
    .header {
      position: sticky;
      top: 0;
      z-index: 1000;
    }

    .header__container {
      display: flex;
      align-items: center;
      justify-content: space-between;
      width: 100%;
      max-width: 1400px;
      margin: 0 auto;
      padding: 0 1rem;
    }

    .header__logo {
      display: flex;
      align-items: center;
      gap: 0.5rem;
      text-decoration: none;
      color: inherit;
      font-size: 1.25rem;
      font-weight: 500;
    }

    .header__logo-icon {
      font-size: 1.5rem;
      width: 1.5rem;
      height: 1.5rem;
    }

    .header__logo-text {
      white-space: nowrap;
    }

    .header__nav {
      display: flex;
      gap: 0.5rem;
    }

    .header__nav-link {
      color: rgba(255, 255, 255, 0.9);
    }

    .header__nav-link--active {
      background-color: rgba(255, 255, 255, 0.1);
      color: white;
    }

    @media (max-width: 768px) {
      .header__container {
        flex-direction: column;
        align-items: flex-start;
        gap: 1rem;
        padding: 0.5rem 1rem;
      }

      .header__nav {
        width: 100%;
        flex-wrap: wrap;
      }

      .header__logo-text {
        font-size: 1rem;
      }
    }
  `]
})
export class Header {}
