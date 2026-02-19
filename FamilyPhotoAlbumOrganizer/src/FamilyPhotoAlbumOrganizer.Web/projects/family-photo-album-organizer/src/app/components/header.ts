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
      <span class="header__title">Family Photo Album Organizer</span>
      <nav class="header__nav">
        <a mat-button routerLink="/albums" routerLinkActive="header__nav-link--active" class="header__nav-link">Albums</a>
        <a mat-button routerLink="/photos" routerLinkActive="header__nav-link--active" class="header__nav-link">Photos</a>
        <a mat-button routerLink="/tags" routerLinkActive="header__nav-link--active" class="header__nav-link">Tags</a>
        <a mat-button routerLink="/people" routerLinkActive="header__nav-link--active" class="header__nav-link">People</a>
      </nav>
    </mat-toolbar>
  `,
  styles: [`
    .header {
      display: flex;
      justify-content: space-between;
      align-items: center;

      &__title {
        font-size: 1.5rem;
        font-weight: 500;
      }

      &__nav {
        display: flex;
        gap: 0.5rem;

        &-link {
          color: white;

          &--active {
            background-color: rgba(255, 255, 255, 0.2);
          }
        }
      }
    }
  `]
})
export class Header {}
