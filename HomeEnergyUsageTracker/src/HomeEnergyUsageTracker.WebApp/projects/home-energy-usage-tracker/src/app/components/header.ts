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
      <span class="header__title">Home Energy Usage Tracker</span>
      <nav class="header__nav">
        <a mat-button routerLink="/" routerLinkActive="header__nav-link--active" [routerLinkActiveOptions]="{exact: true}" class="header__nav-link">
          Dashboard
        </a>
        <a mat-button routerLink="/utility-bills" routerLinkActive="header__nav-link--active" class="header__nav-link">
          Utility Bills
        </a>
        <a mat-button routerLink="/usages" routerLinkActive="header__nav-link--active" class="header__nav-link">
          Usage Readings
        </a>
        <a mat-button routerLink="/savings-tips" routerLinkActive="header__nav-link--active" class="header__nav-link">
          Savings Tips
        </a>
      </nav>
    </mat-toolbar>
  `,
  styles: [`
    .header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 24px;

      &__title {
        font-size: 20px;
        font-weight: 500;
      }

      &__nav {
        display: flex;
        gap: 8px;
      }

      &__nav-link {
        color: white;

        &--active {
          background-color: rgba(255, 255, 255, 0.1);
        }
      }
    }
  `]
})
export class Header {}
