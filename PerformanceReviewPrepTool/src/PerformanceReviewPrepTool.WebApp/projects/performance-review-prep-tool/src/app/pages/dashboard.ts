import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { ReviewPeriodService, AchievementService, GoalService, FeedbackService } from '../services';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink, MatCardModule, MatButtonModule, MatIconModule],
  template: `
    <div class="dashboard">
      <h1 class="dashboard__title">Performance Review Dashboard</h1>

      <div class="dashboard__cards">
        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-card-title class="dashboard__card-title">
              <mat-icon class="dashboard__card-icon">event</mat-icon>
              Review Periods
            </mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <p class="dashboard__card-count">{{ (reviewPeriods$ | async)?.length || 0 }}</p>
            <p class="dashboard__card-description">Active review periods</p>
          </mat-card-content>
          <mat-card-actions>
            <a mat-raised-button color="primary" routerLink="/review-periods">Manage</a>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-card-title class="dashboard__card-title">
              <mat-icon class="dashboard__card-icon">emoji_events</mat-icon>
              Achievements
            </mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <p class="dashboard__card-count">{{ (achievements$ | async)?.length || 0 }}</p>
            <p class="dashboard__card-description">Logged achievements</p>
          </mat-card-content>
          <mat-card-actions>
            <a mat-raised-button color="primary" routerLink="/achievements">Manage</a>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-card-title class="dashboard__card-title">
              <mat-icon class="dashboard__card-icon">flag</mat-icon>
              Goals
            </mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <p class="dashboard__card-count">{{ (goals$ | async)?.length || 0 }}</p>
            <p class="dashboard__card-description">Active goals</p>
          </mat-card-content>
          <mat-card-actions>
            <a mat-raised-button color="primary" routerLink="/goals">Manage</a>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-card-title class="dashboard__card-title">
              <mat-icon class="dashboard__card-icon">feedback</mat-icon>
              Feedbacks
            </mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <p class="dashboard__card-count">{{ (feedbacks$ | async)?.length || 0 }}</p>
            <p class="dashboard__card-description">Received feedbacks</p>
          </mat-card-content>
          <mat-card-actions>
            <a mat-raised-button color="primary" routerLink="/feedbacks">Manage</a>
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
      margin: 0 0 2rem 0;
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

    .dashboard__card-title {
      display: flex;
      align-items: center;
      gap: 0.5rem;
    }

    .dashboard__card-icon {
      font-size: 1.5rem;
      width: 1.5rem;
      height: 1.5rem;
    }

    .dashboard__card-count {
      font-size: 3rem;
      font-weight: bold;
      margin: 1rem 0 0.5rem 0;
      text-align: center;
    }

    .dashboard__card-description {
      text-align: center;
      color: rgba(0, 0, 0, 0.6);
      margin: 0 0 1rem 0;
    }
  `]
})
export class Dashboard implements OnInit {
  private reviewPeriodService = inject(ReviewPeriodService);
  private achievementService = inject(AchievementService);
  private goalService = inject(GoalService);
  private feedbackService = inject(FeedbackService);

  reviewPeriods$ = this.reviewPeriodService.reviewPeriods$;
  achievements$ = this.achievementService.achievements$;
  goals$ = this.goalService.goals$;
  feedbacks$ = this.feedbackService.feedbacks$;

  ngOnInit(): void {
    this.reviewPeriodService.getAll().subscribe();
    this.achievementService.getAll().subscribe();
    this.goalService.getAll().subscribe();
    this.feedbackService.getAll().subscribe();
  }
}
