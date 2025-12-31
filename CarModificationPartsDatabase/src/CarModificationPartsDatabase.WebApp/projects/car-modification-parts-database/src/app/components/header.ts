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
      <span class="header__title">Car Modification Parts Database</span>
      <nav class="header__nav">
        <a mat-button routerLink="/" routerLinkActive="header__nav-link--active" [routerLinkActiveOptions]="{exact: true}">Dashboard</a>
        <a mat-button routerLink="/parts" routerLinkActive="header__nav-link--active">Parts</a>
        <a mat-button routerLink="/modifications" routerLinkActive="header__nav-link--active">Modifications</a>
        <a mat-button routerLink="/installations" routerLinkActive="header__nav-link--active">Installations</a>
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

        &-link--active {
          background-color: rgba(255, 255, 255, 0.1);
        }
      }
    }
  `]
})
export class Header {}
