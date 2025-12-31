import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatChipsModule } from '@angular/material/chips';
import { GoalsService } from '../services';
import { Goal, GoalStatus } from '../models';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule, MatProgressBarModule, MatChipsModule],
  template: `
    <div class="dashboard">
      <div class="dashboard__header">
        <h1 class="dashboard__title">Dashboard</h1>
        <button mat-raised-button color="primary" (click)="navigateToGoals()">
          <mat-icon>add</mat-icon>
          New Goal
        </button>
      </div>

      <div class="dashboard__stats">
        <mat-card class="dashboard__stat-card">
          <mat-card-content>
            <div class="dashboard__stat-icon dashboard__stat-icon--primary">
              <mat-icon>flag</mat-icon>
            </div>
            <div class="dashboard__stat-info">
              <div class="dashboard__stat-value">{{ (goals$ | async)?.length || 0 }}</div>
              <div class="dashboard__stat-label">Total Goals</div>
            </div>
          </mat-card-content>
        </mat-card>

        <mat-card class="dashboard__stat-card">
          <mat-card-content>
            <div class="dashboard__stat-icon dashboard__stat-icon--success">
              <mat-icon>trending_up</mat-icon>
            </div>
            <div class="dashboard__stat-info">
              <div class="dashboard__stat-value">{{ getActiveGoalsCount(goals$ | async) }}</div>
              <div class="dashboard__stat-label">Active Goals</div>
            </div>
          </mat-card-content>
        </mat-card>

        <mat-card class="dashboard__stat-card">
          <mat-card-content>
            <div class="dashboard__stat-icon dashboard__stat-icon--accent">
              <mat-icon>check_circle</mat-icon>
            </div>
            <div class="dashboard__stat-info">
              <div class="dashboard__stat-value">{{ getCompletedGoalsCount(goals$ | async) }}</div>
              <div class="dashboard__stat-label">Completed Goals</div>
            </div>
          </mat-card-content>
        </mat-card>
      </div>

      <div class="dashboard__goals">
        <h2 class="dashboard__section-title">Recent Goals</h2>
        <div class="dashboard__goals-grid" *ngIf="(goals$ | async)?.length; else noGoals">
          <mat-card *ngFor="let goal of (goals$ | async)?.slice(0, 6)" class="dashboard__goal-card" (click)="navigateToGoalDetails(goal.goalId)">
            <mat-card-header>
              <mat-card-title>{{ goal.name }}</mat-card-title>
              <mat-card-subtitle>{{ getGoalTypeLabel(goal.goalType) }}</mat-card-subtitle>
            </mat-card-header>
            <mat-card-content>
              <div class="dashboard__goal-progress">
                <div class="dashboard__goal-progress-info">
                  <span>\${{ goal.currentAmount | number:'1.2-2' }}</span>
                  <span>\${{ goal.targetAmount | number:'1.2-2' }}</span>
                </div>
                <mat-progress-bar mode="determinate" [value]="goal.progress"></mat-progress-bar>
                <div class="dashboard__goal-progress-label">{{ goal.progress }}% Complete</div>
              </div>
              <div class="dashboard__goal-meta">
                <mat-chip [class]="'dashboard__status-chip dashboard__status-chip--' + getStatusClass(goal.status)">
                  {{ getStatusLabel(goal.status) }}
                </mat-chip>
                <span class="dashboard__goal-date">Due: {{ goal.targetDate | date:'mediumDate' }}</span>
              </div>
            </mat-card-content>
          </mat-card>
        </div>
        <ng-template #noGoals>
          <div class="dashboard__empty">
            <mat-icon>inbox</mat-icon>
            <p>No goals yet. Create your first goal to get started!</p>
            <button mat-raised-button color="primary" (click)="navigateToGoals()">
              Create Goal
            </button>
          </div>
        </ng-template>
      </div>
    </div>
  `,
  styles: [`
    .dashboard {
      padding: 24px;
      max-width: 1400px;
      margin: 0 auto;

      &__header {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-bottom: 32px;
      }

      &__title {
        font-size: 32px;
        font-weight: 500;
        margin: 0;
      }

      &__stats {
        display: grid;
        grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
        gap: 24px;
        margin-bottom: 32px;
      }

      &__stat-card {
        mat-card-content {
          display: flex;
          align-items: center;
          gap: 16px;
        }
      }

      &__stat-icon {
        width: 56px;
        height: 56px;
        border-radius: 12px;
        display: flex;
        align-items: center;
        justify-content: center;

        mat-icon {
          font-size: 32px;
          width: 32px;
          height: 32px;
          color: white;
        }

        &--primary {
          background-color: #3f51b5;
        }

        &--success {
          background-color: #4caf50;
        }

        &--accent {
          background-color: #ff9800;
        }
      }

      &__stat-info {
        flex: 1;
      }

      &__stat-value {
        font-size: 32px;
        font-weight: 600;
        line-height: 1;
      }

      &__stat-label {
        font-size: 14px;
        color: rgba(0, 0, 0, 0.6);
        margin-top: 4px;
      }

      &__section-title {
        font-size: 24px;
        font-weight: 500;
        margin-bottom: 16px;
      }

      &__goals-grid {
        display: grid;
        grid-template-columns: repeat(auto-fill, minmax(350px, 1fr));
        gap: 24px;
      }

      &__goal-card {
        cursor: pointer;
        transition: transform 0.2s, box-shadow 0.2s;

        &:hover {
          transform: translateY(-4px);
          box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
        }
      }

      &__goal-progress {
        margin: 16px 0;

        &-info {
          display: flex;
          justify-content: space-between;
          font-weight: 500;
          margin-bottom: 8px;
        }

        &-label {
          font-size: 12px;
          color: rgba(0, 0, 0, 0.6);
          margin-top: 4px;
        }
      }

      &__goal-meta {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-top: 16px;
      }

      &__goal-date {
        font-size: 14px;
        color: rgba(0, 0, 0, 0.6);
      }

      &__status-chip {
        font-size: 12px;

        &--not-started {
          background-color: #9e9e9e;
          color: white;
        }

        &--in-progress {
          background-color: #2196f3;
          color: white;
        }

        &--completed {
          background-color: #4caf50;
          color: white;
        }

        &--paused {
          background-color: #ff9800;
          color: white;
        }

        &--cancelled {
          background-color: #f44336;
          color: white;
        }
      }

      &__empty {
        text-align: center;
        padding: 64px 24px;

        mat-icon {
          font-size: 64px;
          width: 64px;
          height: 64px;
          color: rgba(0, 0, 0, 0.3);
        }

        p {
          font-size: 18px;
          color: rgba(0, 0, 0, 0.6);
          margin: 16px 0 24px;
        }
      }
    }
  `]
})
export class Dashboard implements OnInit {
  goals$: Observable<Goal[]>;

  constructor(
    private goalsService: GoalsService,
    private router: Router
  ) {
    this.goals$ = this.goalsService.goals$;
  }

  ngOnInit(): void {
    this.goalsService.getGoals().subscribe();
  }

  getActiveGoalsCount(goals: Goal[] | null): number {
    if (!goals) return 0;
    return goals.filter(g => g.status === GoalStatus.InProgress).length;
  }

  getCompletedGoalsCount(goals: Goal[] | null): number {
    if (!goals) return 0;
    return goals.filter(g => g.status === GoalStatus.Completed).length;
  }

  getGoalTypeLabel(type: number): string {
    const labels: { [key: number]: string } = {
      0: 'Savings',
      1: 'Debt Payoff',
      2: 'Investment',
      3: 'Purchase',
      4: 'Emergency',
      5: 'Retirement'
    };
    return labels[type] || 'Unknown';
  }

  getStatusLabel(status: number): string {
    const labels: { [key: number]: string } = {
      0: 'Not Started',
      1: 'In Progress',
      2: 'Completed',
      3: 'Paused',
      4: 'Cancelled'
    };
    return labels[status] || 'Unknown';
  }

  getStatusClass(status: number): string {
    const classes: { [key: number]: string } = {
      0: 'not-started',
      1: 'in-progress',
      2: 'completed',
      3: 'paused',
      4: 'cancelled'
    };
    return classes[status] || 'not-started';
  }

  navigateToGoals(): void {
    this.router.navigate(['/goals']);
  }

  navigateToGoalDetails(goalId: string): void {
    this.router.navigate(['/goals', goalId]);
  }
}
