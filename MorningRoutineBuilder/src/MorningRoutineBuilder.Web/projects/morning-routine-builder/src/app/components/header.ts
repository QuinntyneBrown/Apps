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
      <span class="header__title">Morning Routine Builder</span>
      <nav class="header__nav">
        <a mat-button routerLink="/dashboard" routerLinkActive="header__nav-link--active" class="header__nav-link">
          Dashboard
        </a>
        <a mat-button routerLink="/routines" routerLinkActive="header__nav-link--active" class="header__nav-link">
          Routines
        </a>
        <a mat-button routerLink="/tasks" routerLinkActive="header__nav-link--active" class="header__nav-link">
          Tasks
        </a>
        <a mat-button routerLink="/completion-logs" routerLinkActive="header__nav-link--active" class="header__nav-link">
          Completion Logs
        </a>
        <a mat-button routerLink="/streaks" routerLinkActive="header__nav-link--active" class="header__nav-link">
          Streaks
        </a>
      </nav>
    </mat-toolbar>
  `,
  styles: [`
    .header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      padding: 0 1rem;

      &__title {
        font-size: 1.5rem;
        font-weight: 500;
      }

      &__nav {
        display: flex;
        gap: 0.5rem;

        &-link {
          &--active {
            background-color: rgba(255, 255, 255, 0.1);
          }
        }
      }
    }
  `]
})
export class Header {}
