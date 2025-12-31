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
      <span class="header__title">Home Maintenance Schedule</span>
      <nav class="header__nav">
        <a mat-button routerLink="/dashboard" routerLinkActive="active">
          <mat-icon>dashboard</mat-icon>
          Dashboard
        </a>
        <a mat-button routerLink="/tasks" routerLinkActive="active">
          <mat-icon>task</mat-icon>
          Tasks
        </a>
        <a mat-button routerLink="/contractors" routerLinkActive="active">
          <mat-icon>engineering</mat-icon>
          Contractors
        </a>
        <a mat-button routerLink="/service-logs" routerLinkActive="active">
          <mat-icon>history</mat-icon>
          Service Logs
        </a>
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
      margin-right: 2rem;
    }
    .header__nav {
      display: flex;
      gap: 0.5rem;
    }
    .header__nav a.active {
      background-color: rgba(255, 255, 255, 0.1);
    }
  `]
})
export class Header {}
