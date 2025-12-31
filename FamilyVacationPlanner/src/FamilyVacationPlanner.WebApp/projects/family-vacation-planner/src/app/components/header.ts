import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    RouterLink,
    RouterLinkActive,
    MatToolbarModule,
    MatButtonModule,
    MatIconModule
  ],
  template: `
    <mat-toolbar color="primary" class="header">
      <span class="header__title">Family Vacation Planner</span>
      <span class="header__spacer"></span>
      <nav class="header__nav">
        <a mat-button routerLink="/trips" routerLinkActive="header__nav-link--active" class="header__nav-link">
          <mat-icon>card_travel</mat-icon>
          Trips
        </a>
        <a mat-button routerLink="/bookings" routerLinkActive="header__nav-link--active" class="header__nav-link">
          <mat-icon>book_online</mat-icon>
          Bookings
        </a>
        <a mat-button routerLink="/itineraries" routerLinkActive="header__nav-link--active" class="header__nav-link">
          <mat-icon>event_note</mat-icon>
          Itineraries
        </a>
        <a mat-button routerLink="/packing-lists" routerLinkActive="header__nav-link--active" class="header__nav-link">
          <mat-icon>luggage</mat-icon>
          Packing Lists
        </a>
        <a mat-button routerLink="/budgets" routerLinkActive="header__nav-link--active" class="header__nav-link">
          <mat-icon>account_balance_wallet</mat-icon>
          Budgets
        </a>
      </nav>
    </mat-toolbar>
  `,
  styles: [`
    .header {
      &__title {
        font-size: 1.5rem;
        font-weight: 500;
      }

      &__spacer {
        flex: 1 1 auto;
      }

      &__nav {
        display: flex;
        gap: 0.5rem;
      }

      &__nav-link {
        display: flex;
        align-items: center;
        gap: 0.25rem;

        &--active {
          background-color: rgba(255, 255, 255, 0.2);
        }
      }
    }
  `]
})
export class Header {}
