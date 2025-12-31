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
      <span class="header__title">Personal Net Worth Dashboard</span>
      <nav class="header__nav">
        <a mat-button routerLink="/" routerLinkActive="header__link--active" [routerLinkActiveOptions]="{exact: true}" class="header__link">
          Dashboard
        </a>
        <a mat-button routerLink="/assets" routerLinkActive="header__link--active" class="header__link">
          Assets
        </a>
        <a mat-button routerLink="/liabilities" routerLinkActive="header__link--active" class="header__link">
          Liabilities
        </a>
        <a mat-button routerLink="/snapshots" routerLinkActive="header__link--active" class="header__link">
          Snapshots
        </a>
      </nav>
    </mat-toolbar>
  `,
  styles: [`
    .header {
      display: flex;
      justify-content: space-between;
      align-items: center;

      &__title {
        font-size: 1.25rem;
        font-weight: 500;
      }

      &__nav {
        display: flex;
        gap: 0.5rem;
      }

      &__link {
        color: white;

        &--active {
          background-color: rgba(255, 255, 255, 0.1);
        }
      }
    }
  `]
})
export class Header {}
