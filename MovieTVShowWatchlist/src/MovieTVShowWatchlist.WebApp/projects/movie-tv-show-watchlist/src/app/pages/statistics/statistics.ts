import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatCardModule } from '@angular/material/card';
import { map, startWith, switchMap } from 'rxjs';
import { StatsService } from '../../services';
import { StatCard } from '../../components/stat-card';

@Component({
  selector: 'app-statistics',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatSelectModule,
    MatFormFieldModule,
    MatCardModule,
    StatCard
  ],
  templateUrl: './statistics.html',
  styleUrl: './statistics.scss'
})
export class Statistics {
  private _statsService = inject(StatsService);

  periodOptions = [
    { value: 'this-year', label: 'This Year' },
    { value: 'this-month', label: 'This Month' },
    { value: 'this-week', label: 'This Week' },
    { value: 'all-time', label: 'All Time' }
  ];

  viewModel$ = this._statsService.loadStats().pipe(
    switchMap(() => this._statsService.stats$),
    map(stats => ({
      stats,
      period: 'this-year'
    })),
    startWith({ stats: null, period: 'this-year' })
  );

  onPeriodChange(period: string): void {
    this._statsService.setPeriod(period);
  }

  getMaxCount(monthlyData: { month: string; count: number }[]): number {
    return Math.max(...monthlyData.map(d => d.count));
  }

  getBarHeight(count: number, maxCount: number): string {
    return `${(count / maxCount) * 100}%`;
  }
}
