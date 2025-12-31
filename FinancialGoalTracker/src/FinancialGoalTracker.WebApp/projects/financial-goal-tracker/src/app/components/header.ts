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
      <span class="header__title">Financial Goal Tracker</span>
      <nav class="header__nav">
        <a mat-button routerLink="/dashboard" routerLinkActive="header__nav-link--active" class="header__nav-link">
          <mat-icon>dashboard</mat-icon>
          Dashboard
        </a>
        <a mat-button routerLink="/goals" routerLinkActive="header__nav-link--active" class="header__nav-link">
          <mat-icon>flag</mat-icon>
          Goals
        </a>
        <a mat-button routerLink="/contributions" routerLinkActive="header__nav-link--active" class="header__nav-link">
          <mat-icon>payments</mat-icon>
          Contributions
        </a>
        <a mat-button routerLink="/milestones" routerLinkActive="header__nav-link--active" class="header__nav-link">
          <mat-icon>emoji_events</mat-icon>
          Milestones
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

      &__title {
        font-size: 20px;
        font-weight: 500;
      }

      &__nav {
        display: flex;
        gap: 8px;

        &-link {
          display: flex;
          align-items: center;
          gap: 8px;

          mat-icon {
            font-size: 20px;
            width: 20px;
            height: 20px;
          }

          &--active {
            background-color: rgba(255, 255, 255, 0.1);
          }
        }
      }
    }
  `]
})
export class Header { }
