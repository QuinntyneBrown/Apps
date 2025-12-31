import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { ReviewService } from '../../services';

@Component({
  selector: 'app-reviews',
  imports: [CommonModule, MatButtonModule, MatIconModule, MatCardModule],
  templateUrl: './reviews.html',
  styleUrl: './reviews.scss'
})
export class Reviews implements OnInit {
  private readonly reviewService = inject(ReviewService);

  reviews$ = this.reviewService.reviews$;

  ngOnInit(): void {
    this.reviewService.getReviews().subscribe();
  }

  onDeleteReview(review: any): void {
    if (confirm('Are you sure you want to delete this review?')) {
      this.reviewService.deleteReview(review.reviewId).subscribe();
    }
  }

  getStarArray(rating: number): boolean[] {
    return Array(5).fill(false).map((_, i) => i < rating);
  }
}
