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
      <div class="header__container">
        <a routerLink="/" class="header__logo">
          <mat-icon class="header__logo-icon">work</mat-icon>
          <span class="header__logo-text">Resume & Career Tracker</span>
        </a>
        <nav class="header__nav">
          <a
            mat-button
            routerLink="/dashboard"
            routerLinkActive="header__nav-link--active"
            class="header__nav-link">
            Dashboard
          </a>
          <a
            mat-button
            routerLink="/achievements"
            routerLinkActive="header__nav-link--active"
            class="header__nav-link">
            Achievements
          </a>
          <a
            mat-button
            routerLink="/projects"
            routerLinkActive="header__nav-link--active"
            class="header__nav-link">
            Projects
          </a>
          <a
            mat-button
            routerLink="/skills"
            routerLinkActive="header__nav-link--active"
            class="header__nav-link">
            Skills
          </a>
        </nav>
      </div>
    </mat-toolbar>
  `,
  styles: [`
    .header {
      position: sticky;
      top: 0;
      z-index: 1000;
    }

    .header__container {
      display: flex;
      align-items: center;
      justify-content: space-between;
      width: 100%;
      max-width: 1400px;
      margin: 0 auto;
    }

    .header__logo {
      display: flex;
      align-items: center;
      gap: 0.5rem;
      text-decoration: none;
      color: inherit;
      font-size: 1.25rem;
      font-weight: 500;
    }

    .header__logo-icon {
      width: 28px;
      height: 28px;
      font-size: 28px;
    }

    .header__logo-text {
      white-space: nowrap;
    }

    .header__nav {
      display: flex;
      gap: 0.5rem;
    }

    .header__nav-link {
      color: rgba(255, 255, 255, 0.87);
    }

    .header__nav-link--active {
      background-color: rgba(255, 255, 255, 0.1);
    }

    @media (max-width: 768px) {
      .header__logo-text {
        display: none;
      }

      .header__nav {
        gap: 0.25rem;
      }
    }
  `]
})
export class Header {}
