import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterLink, MatToolbarModule, MatButtonModule, MatIconModule],
  template: `
    <mat-toolbar color="primary" class="header">
      <span class="header__title" routerLink="/">Golf Score Tracker</span>
      <span class="header__spacer"></span>
      <nav class="header__nav">
        <a mat-button routerLink="/" class="header__nav-link">Home</a>
        <a mat-button routerLink="/courses" class="header__nav-link">Courses</a>
        <a mat-button routerLink="/rounds" class="header__nav-link">Rounds</a>
        <a mat-button routerLink="/handicaps" class="header__nav-link">Handicaps</a>
      </nav>
    </mat-toolbar>
  `,
  styles: [`
    .header {
      position: sticky;
      top: 0;
      z-index: 1000;
    }

    .header__title {
      cursor: pointer;
      font-size: 1.5rem;
      font-weight: 500;
    }

    .header__spacer {
      flex: 1 1 auto;
    }

    .header__nav {
      display: flex;
      gap: 0.5rem;
    }

    .header__nav-link {
      color: white;
    }
  `]
})
export class Header {}
