import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatCardModule } from '@angular/material/card';
import { ReviewService } from '../../services';

@Component({
  selector: 'app-reviews',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule, MatChipsModule, MatCardModule],
  templateUrl: './reviews.html',
  styleUrl: './reviews.scss'
})
export class Reviews implements OnInit {
  private readonly reviewService = inject(ReviewService);

  reviews$ = this.reviewService.reviews$;
  displayedColumns = ['contentType', 'reviewText', 'reviewDate', 'hasSpoilers', 'wouldRecommend', 'targetAudience', 'actions'];

  ngOnInit(): void {
    this.reviewService.getAll().subscribe();
  }

  deleteReview(id: string): void {
    if (confirm('Are you sure you want to delete this review?')) {
      this.reviewService.delete(id).subscribe();
    }
  }
}
