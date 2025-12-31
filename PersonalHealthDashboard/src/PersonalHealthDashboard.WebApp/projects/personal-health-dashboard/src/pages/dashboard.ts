import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { VitalService, HealthTrendService, WearableDataService } from '../services';
import { Observable, combineLatest, map } from 'rxjs';

interface DashboardStats {
  totalVitals: number;
  totalHealthTrends: number;
  totalWearableData: number;
  recentVitalsCount: number;
}

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
        <h1 class="dashboard__title">Personal Health Dashboard</h1>
        <p class="dashboard__subtitle">Monitor your health metrics and trends</p>
      </div>

      <div class="dashboard__stats" *ngIf="stats$ | async as stats; else loading">
        <mat-card class="dashboard__card dashboard__card--vitals">
          <mat-card-header>
            <mat-icon class="dashboard__card-icon">favorite</mat-icon>
            <mat-card-title>Vitals</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__card-value">{{ stats.totalVitals }}</div>
            <div class="dashboard__card-label">Total Measurements</div>
            <div class="dashboard__card-info">{{ stats.recentVitalsCount }} this week</div>
          </mat-card-content>
          <mat-card-actions>
            <a mat-button routerLink="/vitals" color="primary">View All</a>
            <a mat-raised-button routerLink="/vitals/new" color="primary">Add New</a>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card dashboard__card--trends">
          <mat-card-header>
            <mat-icon class="dashboard__card-icon">trending_up</mat-icon>
            <mat-card-title>Health Trends</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__card-value">{{ stats.totalHealthTrends }}</div>
            <div class="dashboard__card-label">Active Trends</div>
            <div class="dashboard__card-info">Track your progress</div>
          </mat-card-content>
          <mat-card-actions>
            <a mat-button routerLink="/health-trends" color="primary">View All</a>
            <a mat-raised-button routerLink="/health-trends/new" color="primary">Add New</a>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card dashboard__card--wearable">
          <mat-card-header>
            <mat-icon class="dashboard__card-icon">watch</mat-icon>
            <mat-card-title>Wearable Data</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__card-value">{{ stats.totalWearableData }}</div>
            <div class="dashboard__card-label">Synced Records</div>
            <div class="dashboard__card-info">From connected devices</div>
          </mat-card-content>
          <mat-card-actions>
            <a mat-button routerLink="/wearable-data" color="primary">View All</a>
            <a mat-raised-button routerLink="/wearable-data/new" color="primary">Add New</a>
          </mat-card-actions>
        </mat-card>
      </div>

      <ng-template #loading>
        <div class="dashboard__loading">
          <mat-spinner></mat-spinner>
        </div>
      </ng-template>
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
      font-size: 1.125rem;
      color: rgba(0, 0, 0, 0.6);
      margin: 0;
    }

    .dashboard__stats {
      display: grid;
      grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
      gap: 1.5rem;
    }

    .dashboard__card {
      display: flex;
      flex-direction: column;
    }

    .dashboard__card mat-card-header {
      display: flex;
      align-items: center;
      gap: 0.75rem;
      margin-bottom: 1rem;
    }

    .dashboard__card-icon {
      font-size: 2rem;
      width: 2rem;
      height: 2rem;
      color: #3f51b5;
    }

    .dashboard__card--vitals .dashboard__card-icon {
      color: #e91e63;
    }

    .dashboard__card--trends .dashboard__card-icon {
      color: #4caf50;
    }

    .dashboard__card--wearable .dashboard__card-icon {
      color: #ff9800;
    }

    .dashboard__card mat-card-title {
      font-size: 1.25rem;
      margin: 0;
    }

    .dashboard__card-value {
      font-size: 3rem;
      font-weight: 500;
      color: rgba(0, 0, 0, 0.87);
      margin-bottom: 0.5rem;
    }

    .dashboard__card-label {
      font-size: 1rem;
      color: rgba(0, 0, 0, 0.6);
      margin-bottom: 0.25rem;
    }

    .dashboard__card-info {
      font-size: 0.875rem;
      color: rgba(0, 0, 0, 0.5);
    }

    .dashboard__card mat-card-actions {
      display: flex;
      gap: 0.5rem;
      margin-top: auto;
      padding-top: 1rem;
    }

    .dashboard__loading {
      display: flex;
      justify-content: center;
      align-items: center;
      min-height: 300px;
    }

    @media (max-width: 768px) {
      .dashboard {
        padding: 1rem;
      }

      .dashboard__title {
        font-size: 1.5rem;
      }

      .dashboard__subtitle {
        font-size: 1rem;
      }

      .dashboard__stats {
        grid-template-columns: 1fr;
      }

      .dashboard__card-value {
        font-size: 2.5rem;
      }
    }
  `]
})
export class Dashboard implements OnInit {
  private vitalService = inject(VitalService);
  private healthTrendService = inject(HealthTrendService);
  private wearableDataService = inject(WearableDataService);

  stats$!: Observable<DashboardStats>;

  ngOnInit(): void {
    this.vitalService.getAll().subscribe();
    this.healthTrendService.getAll().subscribe();
    this.wearableDataService.getAll().subscribe();

    this.stats$ = combineLatest([
      this.vitalService.vitals$,
      this.healthTrendService.healthTrends$,
      this.wearableDataService.wearableData$
    ]).pipe(
      map(([vitals, healthTrends, wearableData]) => {
        const oneWeekAgo = new Date();
        oneWeekAgo.setDate(oneWeekAgo.getDate() - 7);

        return {
          totalVitals: vitals.length,
          totalHealthTrends: healthTrends.length,
          totalWearableData: wearableData.length,
          recentVitalsCount: vitals.filter(v => new Date(v.measuredAt) >= oneWeekAgo).length
        };
      })
    );
  }
}
