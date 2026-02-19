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
      <span class="header__title">Friend Group Event Coordinator</span>
      <nav class="header__nav">
        <a mat-button routerLink="/groups" routerLinkActive="header__nav-link--active" class="header__nav-link">Groups</a>
        <a mat-button routerLink="/events" routerLinkActive="header__nav-link--active" class="header__nav-link">Events</a>
      </nav>
    </mat-toolbar>
  `,
  styles: [`
    .header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      padding: 0 1rem;
    }

    .header__title {
      font-size: 1.25rem;
      font-weight: 500;
    }

    .header__nav {
      display: flex;
      gap: 0.5rem;
    }

    .header__nav-link--active {
      background-color: rgba(255, 255, 255, 0.1);
    }
  `]
})
export class Header {}
