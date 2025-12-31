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
      <span class="header__title">Document Vault Organizer</span>
      <span class="header__spacer"></span>
      <nav class="header__nav">
        <a mat-button routerLink="/documents" routerLinkActive="header__nav-link--active" class="header__nav-link">
          Documents
        </a>
        <a mat-button routerLink="/categories" routerLinkActive="header__nav-link--active" class="header__nav-link">
          Categories
        </a>
        <a mat-button routerLink="/alerts" routerLinkActive="header__nav-link--active" class="header__nav-link">
          Alerts
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
        &--active {
          background-color: rgba(255, 255, 255, 0.1);
        }
      }
    }
  `]
})
export class Header {}
