import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { AdminTaskService, DeadlineService, RenewalService } from '../services';

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
            <mat-card-title class="dashboard__card-title">
              <mat-icon class="dashboard__card-icon">task</mat-icon>
              Admin Tasks
            </mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__card-stat">
              <span class="dashboard__card-number">{{ totalTasks }}</span>
              <span class="dashboard__card-label">Total Tasks</span>
            </div>
            <div class="dashboard__card-stat">
              <span class="dashboard__card-number">{{ activeTasks }}</span>
              <span class="dashboard__card-label">Active</span>
            </div>
            <div class="dashboard__card-stat">
              <span class="dashboard__card-number">{{ completedTasks }}</span>
              <span class="dashboard__card-label">Completed</span>
            </div>
          </mat-card-content>
          <mat-card-actions>
            <a mat-raised-button color="primary" routerLink="/admin-tasks">View All Tasks</a>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-card-title class="dashboard__card-title">
              <mat-icon class="dashboard__card-icon">event</mat-icon>
              Deadlines
            </mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__card-stat">
              <span class="dashboard__card-number">{{ totalDeadlines }}</span>
              <span class="dashboard__card-label">Total Deadlines</span>
            </div>
            <div class="dashboard__card-stat">
              <span class="dashboard__card-number">{{ activeDeadlines }}</span>
              <span class="dashboard__card-label">Active</span>
            </div>
            <div class="dashboard__card-stat">
              <span class="dashboard__card-number">{{ completedDeadlines }}</span>
              <span class="dashboard__card-label">Completed</span>
            </div>
          </mat-card-content>
          <mat-card-actions>
            <a mat-raised-button color="primary" routerLink="/deadlines">View All Deadlines</a>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-card-title class="dashboard__card-title">
              <mat-icon class="dashboard__card-icon">autorenew</mat-icon>
              Renewals
            </mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__card-stat">
              <span class="dashboard__card-number">{{ totalRenewals }}</span>
              <span class="dashboard__card-label">Total Renewals</span>
            </div>
            <div class="dashboard__card-stat">
              <span class="dashboard__card-number">{{ activeRenewals }}</span>
              <span class="dashboard__card-label">Active</span>
            </div>
            <div class="dashboard__card-stat">
              <span class="dashboard__card-number">{{ inactiveRenewals }}</span>
              <span class="dashboard__card-label">Inactive</span>
            </div>
          </mat-card-content>
          <mat-card-actions>
            <a mat-raised-button color="primary" routerLink="/renewals">View All Renewals</a>
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
        font-weight: 500;
      }

      &__cards {
        display: grid;
        grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
        gap: 2rem;
      }

      &__card {
        display: flex;
        flex-direction: column;
      }

      &__card-title {
        display: flex;
        align-items: center;
        gap: 0.5rem;
      }

      &__card-icon {
        font-size: 1.5rem;
        width: 1.5rem;
        height: 1.5rem;
      }

      &__card-stat {
        display: flex;
        flex-direction: column;
        margin-bottom: 1rem;

        &:last-child {
          margin-bottom: 0;
        }
      }

      &__card-number {
        font-size: 2rem;
        font-weight: 500;
        color: #3f51b5;
      }

      &__card-label {
        font-size: 0.875rem;
        color: rgba(0, 0, 0, 0.6);
      }
    }
  `]
})
export class Dashboard implements OnInit {
  private adminTaskService = inject(AdminTaskService);
  private deadlineService = inject(DeadlineService);
  private renewalService = inject(RenewalService);

  totalTasks = 0;
  activeTasks = 0;
  completedTasks = 0;

  totalDeadlines = 0;
  activeDeadlines = 0;
  completedDeadlines = 0;

  totalRenewals = 0;
  activeRenewals = 0;
  inactiveRenewals = 0;

  ngOnInit(): void {
    this.loadData();
  }

  private loadData(): void {
    this.adminTaskService.getAll().subscribe(tasks => {
      this.totalTasks = tasks.length;
      this.activeTasks = tasks.filter(t => !t.isCompleted).length;
      this.completedTasks = tasks.filter(t => t.isCompleted).length;
    });

    this.deadlineService.getAll().subscribe(deadlines => {
      this.totalDeadlines = deadlines.length;
      this.activeDeadlines = deadlines.filter(d => !d.isCompleted).length;
      this.completedDeadlines = deadlines.filter(d => d.isCompleted).length;
    });

    this.renewalService.getAll().subscribe(renewals => {
      this.totalRenewals = renewals.length;
      this.activeRenewals = renewals.filter(r => r.isActive).length;
      this.inactiveRenewals = renewals.filter(r => !r.isActive).length;
    });
  }
}
