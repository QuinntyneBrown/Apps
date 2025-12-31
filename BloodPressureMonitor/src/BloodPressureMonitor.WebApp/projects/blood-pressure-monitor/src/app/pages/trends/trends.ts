import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Subject, takeUntil } from 'rxjs';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { TrendService } from '../../services';
import { Trend } from '../../models';

@Component({
  selector: 'app-trends',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatChipsModule,
    MatProgressSpinnerModule
  ],
  templateUrl: './trends.html',
  styleUrl: './trends.scss'
})
export class Trends implements OnInit, OnDestroy {
  private destroy$ = new Subject<void>();

  trends: Trend[] = [];
  loading = false;
  error: string | null = null;

  // Hardcoded user ID for demo purposes
  private userId = '00000000-0000-0000-0000-000000000001';

  constructor(private trendService: TrendService) {}

  ngOnInit(): void {
    this.loadTrends();

    this.trendService.trends$
      .pipe(takeUntil(this.destroy$))
      .subscribe(trends => this.trends = trends);

    this.trendService.loading$
      .pipe(takeUntil(this.destroy$))
      .subscribe(loading => this.loading = loading);

    this.trendService.error$
      .pipe(takeUntil(this.destroy$))
      .subscribe(error => this.error = error);
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  loadTrends(): void {
    this.trendService.getTrendsByUserId(this.userId).subscribe();
  }

  onCalculateTrend(days: number = 30): void {
    const endDate = new Date();
    const startDate = new Date();
    startDate.setDate(startDate.getDate() - days);

    this.trendService.calculateTrend({
      userId: this.userId,
      startDate: startDate.toISOString(),
      endDate: endDate.toISOString()
    }).subscribe();
  }

  onDeleteTrend(trendId: string): void {
    if (confirm('Are you sure you want to delete this trend analysis?')) {
      this.trendService.deleteTrend(trendId).subscribe();
    }
  }

  getTrendDirectionClass(direction: string): string {
    switch (direction.toLowerCase()) {
      case 'improving':
        return 'trends__direction--improving';
      case 'worsening':
        return 'trends__direction--worsening';
      case 'stable':
        return 'trends__direction--stable';
      default:
        return '';
    }
  }

  getTrendDirectionIcon(direction: string): string {
    switch (direction.toLowerCase()) {
      case 'improving':
        return 'trending_down';
      case 'worsening':
        return 'trending_up';
      case 'stable':
        return 'trending_flat';
      default:
        return 'trending_flat';
    }
  }
}
