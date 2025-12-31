import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { WatchlistService, FavoriteService, RatingService, ReviewService, ViewingService } from '../services';
import { map, combineLatest } from 'rxjs';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink, MatCardModule, MatButtonModule, MatIconModule, MatChipsModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard implements OnInit {
  private readonly watchlistService = inject(WatchlistService);
  private readonly favoriteService = inject(FavoriteService);
  private readonly ratingService = inject(RatingService);
  private readonly reviewService = inject(ReviewService);
  private readonly viewingService = inject(ViewingService);

  viewModel$ = combineLatest({
    watchlist: this.watchlistService.watchlist$,
    favorites: this.favoriteService.favorites$,
    ratings: this.ratingService.ratings$,
    reviews: this.reviewService.reviews$,
    viewingHistory: this.viewingService.viewingHistory$
  }).pipe(
    map(({ watchlist, favorites, ratings, reviews, viewingHistory }) => ({
      highPriorityItems: watchlist.filter(w => w.priority === 'high'),
      recentFavorites: favorites.slice(0, 5),
      recentRatings: ratings.slice(0, 5),
      recentReviews: reviews.slice(0, 5),
      recentViewing: viewingHistory.slice(0, 5),
      totalWatchlist: watchlist.length,
      totalFavorites: favorites.length,
      totalRatings: ratings.length,
      totalReviews: reviews.length,
      totalViewing: viewingHistory.length
    }))
  );

  ngOnInit(): void {
    this.watchlistService.loadWatchlist().subscribe();
    this.favoriteService.getAll().subscribe();
    this.ratingService.getAll().subscribe();
    this.reviewService.getAll().subscribe();
    this.viewingService.getHistory().subscribe();
  }
}
