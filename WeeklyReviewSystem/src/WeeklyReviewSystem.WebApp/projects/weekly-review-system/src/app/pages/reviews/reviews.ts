import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { WeeklyReviewService } from '../../services';
import { ReviewCard, ReviewFormDialog } from '../../components';
import { WeeklyReview } from '../../models';

@Component({
  selector: 'app-reviews',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatDialogModule,
    MatProgressSpinnerModule,
    MatSnackBarModule,
    ReviewCard
  ],
  templateUrl: './reviews.html',
  styleUrl: './reviews.scss'
})
export class Reviews {
  private _weeklyReviewService = inject(WeeklyReviewService);
  private _dialog = inject(MatDialog);
  private _snackBar = inject(MatSnackBar);

  reviews$ = this._weeklyReviewService.weeklyReviews$;

  ngOnInit(): void {
    this._weeklyReviewService.getAll().subscribe();
  }

  onCreateReview(): void {
    const dialogRef = this._dialog.open(ReviewFormDialog, {
      width: '600px',
      data: { review: null }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._weeklyReviewService.create({
          ...result,
          userId: '00000000-0000-0000-0000-000000000000'
        }).subscribe({
          next: () => {
            this._snackBar.open('Review created successfully', 'Close', { duration: 3000 });
          },
          error: (error) => {
            this._snackBar.open('Error creating review', 'Close', { duration: 3000 });
            console.error('Error creating review:', error);
          }
        });
      }
    });
  }

  onEditReview(review: WeeklyReview): void {
    const dialogRef = this._dialog.open(ReviewFormDialog, {
      width: '600px',
      data: { review }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._weeklyReviewService.update(review.weeklyReviewId, result).subscribe({
          next: () => {
            this._snackBar.open('Review updated successfully', 'Close', { duration: 3000 });
          },
          error: (error) => {
            this._snackBar.open('Error updating review', 'Close', { duration: 3000 });
            console.error('Error updating review:', error);
          }
        });
      }
    });
  }

  onDeleteReview(id: string): void {
    if (confirm('Are you sure you want to delete this review?')) {
      this._weeklyReviewService.delete(id).subscribe({
        next: () => {
          this._snackBar.open('Review deleted successfully', 'Close', { duration: 3000 });
        },
        error: (error) => {
          this._snackBar.open('Error deleting review', 'Close', { duration: 3000 });
          console.error('Error deleting review:', error);
        }
      });
    }
  }

  onViewReview(review: WeeklyReview): void {
    // Navigate to review detail page or show in dialog
    console.log('View review:', review);
  }
}
