import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatDialog } from '@angular/material/dialog';
import { ReadingService, TrendService } from '../../services';
import { Reading, Trend } from '../../models';
import { ReadingCard } from '../../components/reading-card';
import { ReadingDialog, ReadingDialogData } from '../../components/reading-dialog';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatProgressSpinnerModule,
    ReadingCard
  ],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard implements OnInit, OnDestroy {
  private destroy$ = new Subject<void>();

  readings: Reading[] = [];
  recentReadings: Reading[] = [];
  criticalReadings: Reading[] = [];
  latestTrend: Trend | null = null;
  loading = false;
  error: string | null = null;

  // Hardcoded user ID for demo purposes
  private userId = '00000000-0000-0000-0000-000000000001';

  constructor(
    private readingService: ReadingService,
    private trendService: TrendService,
    private dialog: MatDialog,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadDashboardData();

    this.readingService.readings$
      .pipe(takeUntil(this.destroy$))
      .subscribe(readings => {
        this.readings = readings;
        this.recentReadings = readings.slice(0, 5);
      });

    this.readingService.loading$
      .pipe(takeUntil(this.destroy$))
      .subscribe(loading => this.loading = loading);

    this.readingService.error$
      .pipe(takeUntil(this.destroy$))
      .subscribe(error => this.error = error);

    this.trendService.trends$
      .pipe(takeUntil(this.destroy$))
      .subscribe(trends => {
        this.latestTrend = trends.length > 0 ? trends[0] : null;
      });
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  loadDashboardData(): void {
    this.readingService.getReadingsByUserId(this.userId).subscribe();
    this.readingService.getCriticalReadings(this.userId, 30).subscribe(
      readings => this.criticalReadings = readings
    );
    this.trendService.getTrendsByUserId(this.userId).subscribe();
  }

  onAddReading(): void {
    const dialogRef = this.dialog.open(ReadingDialog, {
      width: '600px',
      data: { userId: this.userId } as ReadingDialogData
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.readingService.createReading(result).subscribe({
          next: () => {
            this.loadDashboardData();
          }
        });
      }
    });
  }

  onEditReading(reading: Reading): void {
    const dialogRef = this.dialog.open(ReadingDialog, {
      width: '600px',
      data: { reading, userId: this.userId } as ReadingDialogData
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.readingService.updateReading(reading.readingId, result).subscribe({
          next: () => {
            this.loadDashboardData();
          }
        });
      }
    });
  }

  onDeleteReading(readingId: string): void {
    if (confirm('Are you sure you want to delete this reading?')) {
      this.readingService.deleteReading(readingId).subscribe({
        next: () => {
          this.loadDashboardData();
        }
      });
    }
  }

  onViewAllReadings(): void {
    this.router.navigate(['/readings']);
  }

  onViewTrends(): void {
    this.router.navigate(['/trends']);
  }

  onCalculateTrend(): void {
    const endDate = new Date();
    const startDate = new Date();
    startDate.setDate(startDate.getDate() - 30);

    this.trendService.calculateTrend({
      userId: this.userId,
      startDate: startDate.toISOString(),
      endDate: endDate.toISOString()
    }).subscribe({
      next: () => {
        this.loadDashboardData();
      }
    });
  }
}
