import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatChipsModule } from '@angular/material/chips';
import { MatIconModule } from '@angular/material/icon';
import { WatchlistItem } from '../../models';

@Component({
  selector: 'app-watchlist-card',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatChipsModule, MatIconModule],
  templateUrl: './watchlist-card.html',
  styleUrl: './watchlist-card.scss'
})
export class WatchlistCard {
  @Input({ required: true }) item!: WatchlistItem;
  @Output() watch = new EventEmitter<WatchlistItem>();
  @Output() remove = new EventEmitter<string>();

  get isMovie(): boolean {
    return this.item.contentType === 'movie';
  }

  get formattedDuration(): string {
    if (this.item.runtime) {
      return `${this.item.runtime} min`;
    }
    if (this.item.seasons) {
      return `${this.item.seasons} Seasons`;
    }
    return '';
  }

  get addedAgo(): string {
    const now = new Date();
    const added = new Date(this.item.addedDate);
    const diffMs = now.getTime() - added.getTime();
    const diffDays = Math.floor(diffMs / (1000 * 60 * 60 * 24));

    if (diffDays === 0) return 'Added today';
    if (diffDays === 1) return 'Added yesterday';
    if (diffDays < 7) return `Added ${diffDays} days ago`;
    if (diffDays < 30) return `Added ${Math.floor(diffDays / 7)} week${Math.floor(diffDays / 7) > 1 ? 's' : ''} ago`;
    if (diffDays < 365) return `Added ${Math.floor(diffDays / 30)} month${Math.floor(diffDays / 30) > 1 ? 's' : ''} ago`;
    return `Added ${Math.floor(diffDays / 365)} year${Math.floor(diffDays / 365) > 1 ? 's' : ''} ago`;
  }

  get priorityClass(): string {
    return `watchlist-card__priority--${this.item.priority}`;
  }

  onWatch(): void {
    this.watch.emit(this.item);
  }

  onRemove(): void {
    this.remove.emit(this.item.watchlistItemId);
  }

  formatGenre(genre: string): string {
    return genre.charAt(0).toUpperCase() + genre.slice(1);
  }
}
