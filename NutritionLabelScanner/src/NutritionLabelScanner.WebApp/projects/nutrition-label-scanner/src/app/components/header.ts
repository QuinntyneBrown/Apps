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
        <a routerLink="/" class="header__brand">
          <mat-icon class="header__icon">receipt_long</mat-icon>
          <span class="header__title">Nutrition Label Scanner</span>
        </a>
        <nav class="header__nav">
          <a mat-button routerLink="/" routerLinkActive="header__nav-link--active" [routerLinkActiveOptions]="{exact: true}" class="header__nav-link">
            Dashboard
          </a>
          <a mat-button routerLink="/products" routerLinkActive="header__nav-link--active" class="header__nav-link">
            Products
          </a>
          <a mat-button routerLink="/nutrition-infos" routerLinkActive="header__nav-link--active" class="header__nav-link">
            Nutrition Info
          </a>
          <a mat-button routerLink="/comparisons" routerLinkActive="header__nav-link--active" class="header__nav-link">
            Comparisons
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
    }

    .header__brand {
      display: flex;
      align-items: center;
      gap: 0.5rem;
      text-decoration: none;
      color: inherit;
    }

    .header__icon {
      font-size: 28px;
      width: 28px;
      height: 28px;
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
      color: rgba(255, 255, 255, 0.87);
    }

    .header__nav-link--active {
      background-color: rgba(255, 255, 255, 0.1);
    }
  `]
})
export class Header {}
