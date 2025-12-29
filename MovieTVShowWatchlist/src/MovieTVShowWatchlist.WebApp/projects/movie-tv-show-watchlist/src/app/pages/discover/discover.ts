import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatChipsModule } from '@angular/material/chips';
import { MatIconModule } from '@angular/material/icon';
import { map, startWith, switchMap } from 'rxjs';
import { RecommendationService, WatchlistService } from '../../services';
import { Recommendation } from '../../models';

@Component({
  selector: 'app-discover',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatChipsModule, MatIconModule],
  templateUrl: './discover.html',
  styleUrl: './discover.scss'
})
export class Discover {
  private _recommendationService = inject(RecommendationService);
  private _watchlistService = inject(WatchlistService);

  viewModel$ = this._recommendationService.loadRecommendations().pipe(
    switchMap(() => this._recommendationService.recommendations$),
    map(recommendations => ({ recommendations })),
    startWith({ recommendations: [] as Recommendation[] })
  );

  getSourceIcon(source: string): string {
    switch (source) {
      case 'system': return '🤖';
      case 'friend': return '👥';
      case 'critic': return '📝';
      default: return '💡';
    }
  }

  onAddToWatchlist(recommendation: Recommendation): void {
    this._watchlistService.addItem({
      title: recommendation.title,
      contentType: recommendation.contentType,
      releaseYear: recommendation.releaseYear,
      genres: recommendation.genres,
      priority: 'medium',
      platform: 'Unknown'
    }).subscribe();
  }

  onDismiss(recommendationId: string): void {
    this._recommendationService.dismissRecommendation(recommendationId).subscribe();
  }

  formatGenre(genre: string): string {
    return genre.charAt(0).toUpperCase() + genre.slice(1);
  }
}
