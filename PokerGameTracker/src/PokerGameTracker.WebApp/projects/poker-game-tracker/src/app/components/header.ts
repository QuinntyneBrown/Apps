import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterLink, MatToolbarModule, MatButtonModule],
  template: `
    <mat-toolbar color="primary" class="header">
      <div class="header__container">
        <a routerLink="/" class="header__title">Poker Game Tracker</a>
        <nav class="header__nav">
          <a mat-button routerLink="/" class="header__link">Dashboard</a>
          <a mat-button routerLink="/sessions" class="header__link">Sessions</a>
          <a mat-button routerLink="/hands" class="header__link">Hands</a>
          <a mat-button routerLink="/bankrolls" class="header__link">Bankrolls</a>
        </nav>
      </div>
    </mat-toolbar>
  `,
  styles: [`
    .header {
      &__container {
        display: flex;
        align-items: center;
        justify-content: space-between;
        width: 100%;
        max-width: 1400px;
        margin: 0 auto;
        padding: 0 16px;
      }

      &__title {
        color: white;
        text-decoration: none;
        font-size: 20px;
        font-weight: 500;
      }

      &__nav {
        display: flex;
        gap: 8px;
      }

      &__link {
        color: white;
      }
    }
  `]
})
export class Header {}
