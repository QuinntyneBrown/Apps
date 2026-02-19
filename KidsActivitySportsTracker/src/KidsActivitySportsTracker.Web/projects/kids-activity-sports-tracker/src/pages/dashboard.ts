import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { ActivityService, CarpoolService, ScheduleService } from '../services';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule],
  template: `
    <div class="dashboard">
      <h1 class="dashboard__title">Dashboard</h1>
      <div class="dashboard__cards">
        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-card-title class="dashboard__card-title">
              <mat-icon class="dashboard__card-icon">sports</mat-icon>
              Activities
            </mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <p class="dashboard__card-count">{{ (activityService.activities$ | async)?.length || 0 }}</p>
            <p class="dashboard__card-description">Track your kids' activities and sports</p>
          </mat-card-content>
          <mat-card-actions>
            <button mat-raised-button color="primary" (click)="navigateTo('/activities')">
              View Activities
            </button>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-card-title class="dashboard__card-title">
              <mat-icon class="dashboard__card-icon">directions_car</mat-icon>
              Carpools
            </mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <p class="dashboard__card-count">{{ (carpoolService.carpools$ | async)?.length || 0 }}</p>
            <p class="dashboard__card-description">Manage carpool arrangements</p>
          </mat-card-content>
          <mat-card-actions>
            <button mat-raised-button color="primary" (click)="navigateTo('/carpools')">
              View Carpools
            </button>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-card-title class="dashboard__card-title">
              <mat-icon class="dashboard__card-icon">event</mat-icon>
              Schedules
            </mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <p class="dashboard__card-count">{{ (scheduleService.schedules$ | async)?.length || 0 }}</p>
            <p class="dashboard__card-description">View upcoming events and practices</p>
          </mat-card-content>
          <mat-card-actions>
            <button mat-raised-button color="primary" (click)="navigateTo('/schedules')">
              View Schedules
            </button>
          </mat-card-actions>
        </mat-card>
      </div>
    </div>
  `,
  styles: [`
    .dashboard {
      padding: 24px;
    }

    .dashboard__title {
      margin: 0 0 24px 0;
      font-size: 32px;
      font-weight: 500;
    }

    .dashboard__cards {
      display: grid;
      grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
      gap: 24px;
    }

    .dashboard__card {
      height: 100%;
    }

    .dashboard__card-title {
      display: flex;
      align-items: center;
      gap: 8px;
    }

    .dashboard__card-icon {
      color: #1976d2;
    }

    .dashboard__card-count {
      font-size: 48px;
      font-weight: 700;
      margin: 16px 0;
      color: #1976d2;
    }

    .dashboard__card-description {
      color: rgba(0, 0, 0, 0.6);
      margin: 0 0 16px 0;
    }
  `]
})
export class Dashboard implements OnInit {
  readonly activityService = inject(ActivityService);
  readonly carpoolService = inject(CarpoolService);
  readonly scheduleService = inject(ScheduleService);
  private readonly router = inject(Router);

  ngOnInit(): void {
    this.activityService.loadActivities().subscribe();
    this.carpoolService.loadCarpools().subscribe();
    this.scheduleService.loadSchedules().subscribe();
  }

  navigateTo(path: string): void {
    this.router.navigate([path]);
  }
}
