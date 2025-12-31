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
      <span class="header__title">Photography Session Logger</span>
      <nav class="header__nav">
        <a mat-button routerLink="/" routerLinkActive="header__nav-link--active" [routerLinkActiveOptions]="{exact: true}" class="header__nav-link">
          <mat-icon>dashboard</mat-icon>
          Dashboard
        </a>
        <a mat-button routerLink="/sessions" routerLinkActive="header__nav-link--active" class="header__nav-link">
          <mat-icon>camera</mat-icon>
          Sessions
        </a>
        <a mat-button routerLink="/photos" routerLinkActive="header__nav-link--active" class="header__nav-link">
          <mat-icon>photo_library</mat-icon>
          Photos
        </a>
        <a mat-button routerLink="/gears" routerLinkActive="header__nav-link--active" class="header__nav-link">
          <mat-icon>photo_camera</mat-icon>
          Gear
        </a>
        <a mat-button routerLink="/projects" routerLinkActive="header__nav-link--active" class="header__nav-link">
          <mat-icon>folder</mat-icon>
          Projects
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
    }

    .header__title {
      font-size: 1.25rem;
      font-weight: 500;
    }

    .header__nav {
      display: flex;
      gap: 0.5rem;
    }

    .header__nav-link {
      display: flex;
      align-items: center;
      gap: 0.5rem;
    }

    .header__nav-link--active {
      background-color: rgba(255, 255, 255, 0.1);
    }
  `]
})
export class Header {}
