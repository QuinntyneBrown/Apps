import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { BookService, ReadingLogService, ReviewService, WishlistService } from '../../services';
import { map, combineLatest } from 'rxjs';

@Component({
  selector: 'app-dashboard',
  imports: [CommonModule, RouterModule, MatCardModule, MatButtonModule, MatIconModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard {
  private readonly bookService = inject(BookService);
  private readonly readingLogService = inject(ReadingLogService);
  private readonly reviewService = inject(ReviewService);
  private readonly wishlistService = inject(WishlistService);

  stats$ = combineLatest([
    this.bookService.books$,
    this.readingLogService.readingLogs$,
    this.reviewService.reviews$,
    this.wishlistService.wishlists$
  ]).pipe(
    map(([books, logs, reviews, wishlists]) => ({
      totalBooks: books.length,
      currentlyReading: books.filter(b => b.status === 'CurrentlyReading').length,
      completed: books.filter(b => b.status === 'Completed').length,
      totalLogs: logs.length,
      totalReviews: reviews.length,
      wishlistItems: wishlists.filter(w => !w.isAcquired).length
    }))
  );
}
