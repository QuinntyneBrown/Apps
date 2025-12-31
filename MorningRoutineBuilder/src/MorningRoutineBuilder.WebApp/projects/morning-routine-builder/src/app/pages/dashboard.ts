import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { RouterLink } from '@angular/router';
import { RoutineService, RoutineTaskService, CompletionLogService, StreakService } from '../services';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule, RouterLink],
  template: `
    <div class="dashboard">
      <h1 class="dashboard__title">Dashboard</h1>

      <div class="dashboard__cards">
        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-card-title class="dashboard__card-title">
              <mat-icon class="dashboard__card-icon">event_note</mat-icon>
              Routines
            </mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__card-stat">{{ (routines$ | async)?.length || 0 }}</div>
            <div class="dashboard__card-label">Total Routines</div>
          </mat-card-content>
          <mat-card-actions>
            <button mat-button color="primary" routerLink="/routines">View All</button>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-card-title class="dashboard__card-title">
              <mat-icon class="dashboard__card-icon">task_alt</mat-icon>
              Tasks
            </mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__card-stat">{{ (tasks$ | async)?.length || 0 }}</div>
            <div class="dashboard__card-label">Total Tasks</div>
          </mat-card-content>
          <mat-card-actions>
            <button mat-button color="primary" routerLink="/tasks">View All</button>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-card-title class="dashboard__card-title">
              <mat-icon class="dashboard__card-icon">check_circle</mat-icon>
              Completion Logs
            </mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__card-stat">{{ (logs$ | async)?.length || 0 }}</div>
            <div class="dashboard__card-label">Total Completions</div>
          </mat-card-content>
          <mat-card-actions>
            <button mat-button color="primary" routerLink="/completion-logs">View All</button>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-card-title class="dashboard__card-title">
              <mat-icon class="dashboard__card-icon">local_fire_department</mat-icon>
              Streaks
            </mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__card-stat">{{ (streaks$ | async)?.length || 0 }}</div>
            <div class="dashboard__card-label">Active Streaks</div>
          </mat-card-content>
          <mat-card-actions>
            <button mat-button color="primary" routerLink="/streaks">View All</button>
          </mat-card-actions>
        </mat-card>
      </div>
    </div>
  `,
  styles: [`
    .dashboard {
      padding: 2rem;

      &__title {
        margin: 0 0 2rem 0;
        font-size: 2rem;
        font-weight: 400;
      }

      &__cards {
        display: grid;
        grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
        gap: 1.5rem;
      }

      &__card {
        &-title {
          display: flex;
          align-items: center;
          gap: 0.5rem;
        }

        &-icon {
          color: #1976d2;
        }

        &-stat {
          font-size: 3rem;
          font-weight: 300;
          text-align: center;
          margin: 1rem 0;
          color: #1976d2;
        }

        &-label {
          text-align: center;
          color: rgba(0, 0, 0, 0.6);
          font-size: 0.875rem;
        }
      }
    }
  `]
})
export class Dashboard implements OnInit {
  private routineService = inject(RoutineService);
  private taskService = inject(RoutineTaskService);
  private logService = inject(CompletionLogService);
  private streakService = inject(StreakService);

  routines$ = this.routineService.routines$;
  tasks$ = this.taskService.tasks$;
  logs$ = this.logService.logs$;
  streaks$ = this.streakService.streaks$;

  ngOnInit() {
    this.routineService.getAll().subscribe();
    this.taskService.getAll().subscribe();
    this.logService.getAll().subscribe();
    this.streakService.getAll().subscribe();
  }
}
