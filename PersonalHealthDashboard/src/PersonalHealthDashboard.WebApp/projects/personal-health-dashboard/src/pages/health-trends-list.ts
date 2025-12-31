import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatChipsModule } from '@angular/material/chips';
import { HealthTrendService } from '../services';
import { HealthTrend } from '../models';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-health-trends-list',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatProgressSpinnerModule,
    MatChipsModule
  ],
  template: `
    <div class="health-trends-list">
      <mat-card class="health-trends-list__card">
        <mat-card-header class="health-trends-list__header">
          <mat-card-title class="health-trends-list__title">
            <mat-icon class="health-trends-list__title-icon">trending_up</mat-icon>
            <span>Health Trends</span>
          </mat-card-title>
          <button mat-raised-button color="primary" (click)="addHealthTrend()" class="health-trends-list__add-button">
            <mat-icon>add</mat-icon>
            Add Health Trend
          </button>
        </mat-card-header>

        <mat-card-content>
          <div *ngIf="healthTrends$ | async as healthTrends; else loading" class="health-trends-list__content">
            <div *ngIf="healthTrends.length === 0" class="health-trends-list__empty">
              <mat-icon class="health-trends-list__empty-icon">inbox</mat-icon>
              <p class="health-trends-list__empty-text">No health trends tracked yet</p>
              <button mat-raised-button color="primary" (click)="addHealthTrend()">Add Your First Health Trend</button>
            </div>

            <div *ngIf="healthTrends.length > 0" class="health-trends-list__table-container">
              <table mat-table [dataSource]="healthTrends" class="health-trends-list__table">
                <ng-container matColumnDef="metricName">
                  <th mat-header-cell *matHeaderCellDef>Metric</th>
                  <td mat-cell *matCellDef="let trend">
                    <strong>{{ trend.metricName }}</strong>
                  </td>
                </ng-container>

                <ng-container matColumnDef="period">
                  <th mat-header-cell *matHeaderCellDef>Period</th>
                  <td mat-cell *matCellDef="let trend">
                    {{ trend.startDate | date:'shortDate' }} - {{ trend.endDate | date:'shortDate' }}
                    <div class="health-trends-list__period-duration">({{ trend.periodDuration }} days)</div>
                  </td>
                </ng-container>

                <ng-container matColumnDef="averageValue">
                  <th mat-header-cell *matHeaderCellDef>Average</th>
                  <td mat-cell *matCellDef="let trend">{{ trend.averageValue | number:'1.2-2' }}</td>
                </ng-container>

                <ng-container matColumnDef="range">
                  <th mat-header-cell *matHeaderCellDef>Range</th>
                  <td mat-cell *matCellDef="let trend">
                    {{ trend.minValue | number:'1.2-2' }} - {{ trend.maxValue | number:'1.2-2' }}
                  </td>
                </ng-container>

                <ng-container matColumnDef="trendDirection">
                  <th mat-header-cell *matHeaderCellDef>Trend</th>
                  <td mat-cell *matCellDef="let trend">
                    <mat-chip [class.health-trends-list__chip--up]="trend.trendDirection === 'Increasing'"
                              [class.health-trends-list__chip--down]="trend.trendDirection === 'Decreasing'"
                              [class.health-trends-list__chip--stable]="trend.trendDirection === 'Stable'">
                      {{ trend.trendDirection }}
                      <span class="health-trends-list__percentage">({{ trend.percentageChange | number:'1.1-1' }}%)</span>
                    </mat-chip>
                  </td>
                </ng-container>

                <ng-container matColumnDef="insights">
                  <th mat-header-cell *matHeaderCellDef>Insights</th>
                  <td mat-cell *matCellDef="let trend" class="health-trends-list__insights">{{ trend.insights || '-' }}</td>
                </ng-container>

                <ng-container matColumnDef="actions">
                  <th mat-header-cell *matHeaderCellDef>Actions</th>
                  <td mat-cell *matCellDef="let trend">
                    <button mat-icon-button color="primary" (click)="editHealthTrend(trend.healthTrendId)" class="health-trends-list__action-button">
                      <mat-icon>edit</mat-icon>
                    </button>
                    <button mat-icon-button color="warn" (click)="deleteHealthTrend(trend.healthTrendId)" class="health-trends-list__action-button">
                      <mat-icon>delete</mat-icon>
                    </button>
                  </td>
                </ng-container>

                <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
                <tr mat-row *matRowDef="let row; columns: displayedColumns;" class="health-trends-list__row"></tr>
              </table>
            </div>
          </div>

          <ng-template #loading>
            <div class="health-trends-list__loading">
              <mat-spinner></mat-spinner>
            </div>
          </ng-template>
        </mat-card-content>
      </mat-card>
    </div>
  `,
  styles: [`
    .health-trends-list {
      padding: 2rem;
      max-width: 1400px;
      margin: 0 auto;
    }

    .health-trends-list__card {
      width: 100%;
    }

    .health-trends-list__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 1.5rem;
    }

    .health-trends-list__title {
      display: flex;
      align-items: center;
      gap: 0.5rem;
      font-size: 1.5rem;
      margin: 0;
    }

    .health-trends-list__title-icon {
      font-size: 1.75rem;
      width: 1.75rem;
      height: 1.75rem;
    }

    .health-trends-list__add-button {
      display: flex;
      align-items: center;
      gap: 0.5rem;
    }

    .health-trends-list__content {
      min-height: 200px;
    }

    .health-trends-list__empty {
      display: flex;
      flex-direction: column;
      align-items: center;
      justify-content: center;
      padding: 3rem 1rem;
      text-align: center;
    }

    .health-trends-list__empty-icon {
      font-size: 4rem;
      width: 4rem;
      height: 4rem;
      color: rgba(0, 0, 0, 0.3);
      margin-bottom: 1rem;
    }

    .health-trends-list__empty-text {
      font-size: 1.125rem;
      color: rgba(0, 0, 0, 0.6);
      margin-bottom: 1.5rem;
    }

    .health-trends-list__table-container {
      overflow-x: auto;
    }

    .health-trends-list__table {
      width: 100%;
    }

    .health-trends-list__row {
      cursor: pointer;
    }

    .health-trends-list__row:hover {
      background-color: rgba(0, 0, 0, 0.04);
    }

    .health-trends-list__period-duration {
      font-size: 0.75rem;
      color: rgba(0, 0, 0, 0.6);
    }

    .health-trends-list__chip {
      font-size: 0.875rem;
    }

    .health-trends-list__chip--up {
      background-color: #e8f5e9 !important;
      color: #2e7d32 !important;
    }

    .health-trends-list__chip--down {
      background-color: #ffebee !important;
      color: #c62828 !important;
    }

    .health-trends-list__chip--stable {
      background-color: #e3f2fd !important;
      color: #1565c0 !important;
    }

    .health-trends-list__percentage {
      margin-left: 0.25rem;
      font-weight: 600;
    }

    .health-trends-list__insights {
      max-width: 250px;
      overflow: hidden;
      text-overflow: ellipsis;
      white-space: nowrap;
    }

    .health-trends-list__action-button {
      margin-left: 0.25rem;
    }

    .health-trends-list__loading {
      display: flex;
      justify-content: center;
      align-items: center;
      min-height: 300px;
    }

    @media (max-width: 768px) {
      .health-trends-list {
        padding: 1rem;
      }

      .health-trends-list__header {
        flex-direction: column;
        align-items: flex-start;
        gap: 1rem;
      }

      .health-trends-list__add-button {
        width: 100%;
      }
    }
  `]
})
export class HealthTrendsList implements OnInit {
  private healthTrendService = inject(HealthTrendService);
  private router = inject(Router);

  healthTrends$!: Observable<HealthTrend[]>;
  displayedColumns: string[] = ['metricName', 'period', 'averageValue', 'range', 'trendDirection', 'insights', 'actions'];

  ngOnInit(): void {
    this.healthTrendService.getAll().subscribe();
    this.healthTrends$ = this.healthTrendService.healthTrends$;
  }

  addHealthTrend(): void {
    this.router.navigate(['/health-trends/new']);
  }

  editHealthTrend(id: string): void {
    this.router.navigate(['/health-trends/edit', id]);
  }

  deleteHealthTrend(id: string): void {
    if (confirm('Are you sure you want to delete this health trend?')) {
      this.healthTrendService.delete(id).subscribe();
    }
  }
}
