import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { SessionService, PhotoService, GearService, ProjectService } from '../services';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink, MatCardModule, MatButtonModule, MatIconModule],
  template: `
    <div class="dashboard">
      <h1 class="dashboard__title">Dashboard</h1>

      <div class="dashboard__cards">
        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-icon class="dashboard__card-icon">camera</mat-icon>
            <mat-card-title>Sessions</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__card-count">{{ (sessions$ | async)?.length || 0 }}</div>
            <p class="dashboard__card-description">Total photography sessions</p>
          </mat-card-content>
          <mat-card-actions>
            <a mat-button color="primary" routerLink="/sessions">View All</a>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-icon class="dashboard__card-icon">photo_library</mat-icon>
            <mat-card-title>Photos</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__card-count">{{ (photos$ | async)?.length || 0 }}</div>
            <p class="dashboard__card-description">Total photos captured</p>
          </mat-card-content>
          <mat-card-actions>
            <a mat-button color="primary" routerLink="/photos">View All</a>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-icon class="dashboard__card-icon">photo_camera</mat-icon>
            <mat-card-title>Gear</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__card-count">{{ (gears$ | async)?.length || 0 }}</div>
            <p class="dashboard__card-description">Photography equipment</p>
          </mat-card-content>
          <mat-card-actions>
            <a mat-button color="primary" routerLink="/gears">View All</a>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-icon class="dashboard__card-icon">folder</mat-icon>
            <mat-card-title>Projects</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__card-count">{{ (projects$ | async)?.length || 0 }}</div>
            <p class="dashboard__card-description">Active projects</p>
          </mat-card-content>
          <mat-card-actions>
            <a mat-button color="primary" routerLink="/projects">View All</a>
          </mat-card-actions>
        </mat-card>
      </div>
    </div>
  `,
  styles: [`
    .dashboard {
      padding: 2rem;
    }

    .dashboard__title {
      margin-bottom: 2rem;
      font-size: 2rem;
      font-weight: 500;
    }

    .dashboard__cards {
      display: grid;
      grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
      gap: 1.5rem;
    }

    .dashboard__card {
      display: flex;
      flex-direction: column;
    }

    .dashboard__card-icon {
      margin-right: 0.5rem;
      color: #1976d2;
    }

    .dashboard__card-count {
      font-size: 3rem;
      font-weight: bold;
      color: #1976d2;
      text-align: center;
      margin: 1rem 0;
    }

    .dashboard__card-description {
      text-align: center;
      color: #666;
      margin: 0;
    }
  `]
})
export class Dashboard implements OnInit {
  private readonly sessionService = inject(SessionService);
  private readonly photoService = inject(PhotoService);
  private readonly gearService = inject(GearService);
  private readonly projectService = inject(ProjectService);

  sessions$ = this.sessionService.sessions$;
  photos$ = this.photoService.photos$;
  gears$ = this.gearService.gears$;
  projects$ = this.projectService.projects$;

  ngOnInit(): void {
    this.sessionService.getAll().subscribe();
    this.photoService.getAll().subscribe();
    this.gearService.getAll().subscribe();
    this.projectService.getAll().subscribe();
  }
}
