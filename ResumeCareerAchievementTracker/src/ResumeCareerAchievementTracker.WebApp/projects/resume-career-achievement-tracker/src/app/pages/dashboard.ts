import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { AchievementService, ProjectService, SkillService } from '../services';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule
  ],
  template: `
    <div class="dashboard">
      <div class="dashboard__header">
        <h1 class="dashboard__title">Dashboard</h1>
        <p class="dashboard__subtitle">Overview of your career achievements, projects, and skills</p>
      </div>

      <div class="dashboard__cards">
        <mat-card class="dashboard__card dashboard__card--achievements">
          <mat-card-header class="dashboard__card-header">
            <mat-icon class="dashboard__card-icon">emoji_events</mat-icon>
            <mat-card-title>Achievements</mat-card-title>
          </mat-card-header>
          <mat-card-content class="dashboard__card-content">
            <div class="dashboard__card-stat">
              <span class="dashboard__card-number">{{ (achievements$ | async)?.length || 0 }}</span>
              <span class="dashboard__card-label">Total Achievements</span>
            </div>
            <div class="dashboard__card-stat">
              <span class="dashboard__card-number">{{ getFeaturedCount(achievements$ | async) }}</span>
              <span class="dashboard__card-label">Featured</span>
            </div>
          </mat-card-content>
          <mat-card-actions class="dashboard__card-actions">
            <a mat-button routerLink="/achievements" color="primary">
              View All
            </a>
            <a mat-raised-button routerLink="/achievements/new" color="primary">
              Add New
            </a>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card dashboard__card--projects">
          <mat-card-header class="dashboard__card-header">
            <mat-icon class="dashboard__card-icon">work</mat-icon>
            <mat-card-title>Projects</mat-card-title>
          </mat-card-header>
          <mat-card-content class="dashboard__card-content">
            <div class="dashboard__card-stat">
              <span class="dashboard__card-number">{{ (projects$ | async)?.length || 0 }}</span>
              <span class="dashboard__card-label">Total Projects</span>
            </div>
            <div class="dashboard__card-stat">
              <span class="dashboard__card-number">{{ getFeaturedCount(projects$ | async) }}</span>
              <span class="dashboard__card-label">Featured</span>
            </div>
          </mat-card-content>
          <mat-card-actions class="dashboard__card-actions">
            <a mat-button routerLink="/projects" color="primary">
              View All
            </a>
            <a mat-raised-button routerLink="/projects/new" color="primary">
              Add New
            </a>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card dashboard__card--skills">
          <mat-card-header class="dashboard__card-header">
            <mat-icon class="dashboard__card-icon">psychology</mat-icon>
            <mat-card-title>Skills</mat-card-title>
          </mat-card-header>
          <mat-card-content class="dashboard__card-content">
            <div class="dashboard__card-stat">
              <span class="dashboard__card-number">{{ (skills$ | async)?.length || 0 }}</span>
              <span class="dashboard__card-label">Total Skills</span>
            </div>
            <div class="dashboard__card-stat">
              <span class="dashboard__card-number">{{ getFeaturedCount(skills$ | async) }}</span>
              <span class="dashboard__card-label">Featured</span>
            </div>
          </mat-card-content>
          <mat-card-actions class="dashboard__card-actions">
            <a mat-button routerLink="/skills" color="primary">
              View All
            </a>
            <a mat-raised-button routerLink="/skills/new" color="primary">
              Add New
            </a>
          </mat-card-actions>
        </mat-card>
      </div>
    </div>
  `,
  styles: [`
    .dashboard {
      padding: 2rem;
      max-width: 1400px;
      margin: 0 auto;
    }

    .dashboard__header {
      margin-bottom: 2rem;
    }

    .dashboard__title {
      font-size: 2rem;
      font-weight: 500;
      margin: 0 0 0.5rem 0;
      color: rgba(0, 0, 0, 0.87);
    }

    .dashboard__subtitle {
      font-size: 1rem;
      margin: 0;
      color: rgba(0, 0, 0, 0.6);
    }

    .dashboard__cards {
      display: grid;
      grid-template-columns: repeat(auto-fit, minmax(320px, 1fr));
      gap: 1.5rem;
    }

    .dashboard__card {
      display: flex;
      flex-direction: column;
    }

    .dashboard__card-header {
      display: flex;
      align-items: center;
      gap: 1rem;
      margin-bottom: 1rem;
    }

    .dashboard__card-icon {
      width: 48px;
      height: 48px;
      font-size: 48px;
      color: #1976d2;
    }

    .dashboard__card-content {
      flex: 1;
      display: flex;
      gap: 2rem;
      padding: 1rem 0;
    }

    .dashboard__card-stat {
      display: flex;
      flex-direction: column;
      gap: 0.5rem;
    }

    .dashboard__card-number {
      font-size: 2.5rem;
      font-weight: 500;
      color: rgba(0, 0, 0, 0.87);
    }

    .dashboard__card-label {
      font-size: 0.875rem;
      color: rgba(0, 0, 0, 0.6);
    }

    .dashboard__card-actions {
      display: flex;
      justify-content: flex-end;
      gap: 0.5rem;
      padding: 0 1rem 1rem;
    }

    @media (max-width: 768px) {
      .dashboard {
        padding: 1rem;
      }

      .dashboard__cards {
        grid-template-columns: 1fr;
      }
    }
  `]
})
export class Dashboard implements OnInit {
  private achievementService = inject(AchievementService);
  private projectService = inject(ProjectService);
  private skillService = inject(SkillService);

  achievements$ = this.achievementService.achievements$;
  projects$ = this.projectService.projects$;
  skills$ = this.skillService.skills$;

  ngOnInit(): void {
    this.achievementService.getAchievements().subscribe();
    this.projectService.getProjects().subscribe();
    this.skillService.getSkills().subscribe();
  }

  getFeaturedCount(items: any[] | null): number {
    return items?.filter(item => item.isFeatured).length || 0;
  }
}
