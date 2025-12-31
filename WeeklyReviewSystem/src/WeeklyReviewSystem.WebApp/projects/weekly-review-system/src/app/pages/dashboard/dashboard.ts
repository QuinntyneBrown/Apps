import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { WeeklyReviewService } from '../../services';
import { map } from 'rxjs';

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
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard {
  private _weeklyReviewService = inject(WeeklyReviewService);

  viewModel$ = this._weeklyReviewService.weeklyReviews$.pipe(
    map(reviews => ({
      totalReviews: reviews.length,
      completedReviews: reviews.filter(r => r.isCompleted).length,
      inProgressReviews: reviews.filter(r => !r.isCompleted).length,
      averageRating: reviews.length > 0
        ? reviews.filter(r => r.overallRating).reduce((sum, r) => sum + (r.overallRating || 0), 0) / reviews.filter(r => r.overallRating).length
        : 0,
      recentReviews: reviews.slice(0, 3)
    }))
  );

  ngOnInit(): void {
    this._weeklyReviewService.getAll().subscribe();
  }
}
