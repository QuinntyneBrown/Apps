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
      <span class="header__title">Freelance Project Manager</span>
      <nav class="header__nav">
        <a mat-button routerLink="/clients" routerLinkActive="header__nav-link--active" class="header__nav-link">
          <mat-icon>people</mat-icon>
          <span>Clients</span>
        </a>
        <a mat-button routerLink="/projects" routerLinkActive="header__nav-link--active" class="header__nav-link">
          <mat-icon>work</mat-icon>
          <span>Projects</span>
        </a>
        <a mat-button routerLink="/invoices" routerLinkActive="header__nav-link--active" class="header__nav-link">
          <mat-icon>receipt</mat-icon>
          <span>Invoices</span>
        </a>
        <a mat-button routerLink="/time-entries" routerLinkActive="header__nav-link--active" class="header__nav-link">
          <mat-icon>schedule</mat-icon>
          <span>Time Tracking</span>
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
      box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
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
      gap: 0.25rem;
    }

    .header__nav-link--active {
      background-color: rgba(255, 255, 255, 0.1);
    }
  `]
})
export class Header {}
