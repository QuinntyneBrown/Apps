import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatSidenavModule } from '@angular/material/sidenav';
import { combineLatest, map, startWith, switchMap } from 'rxjs';
import { WatchlistService } from '../../services';
import { FilterState, WatchlistItem } from '../../models';
import { Sidebar } from '../../components/sidebar';
import { WatchlistCard } from '../../components/watchlist-card';

@Component({
  selector: 'app-watchlist',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatButtonModule,
    MatSelectModule,
    MatFormFieldModule,
    MatIconModule,
    MatSidenavModule,
    Sidebar,
    WatchlistCard
  ],
  templateUrl: './watchlist.html',
  styleUrl: './watchlist.scss'
})
export class Watchlist {
  private _watchlistService = inject(WatchlistService);

  sortOptions = [
    { value: 'priority', label: 'Sort by: Priority' },
    { value: 'recently-added', label: 'Sort by: Recently Added' },
    { value: 'title', label: 'Sort by: Title' },
    { value: 'release-year', label: 'Sort by: Release Year' }
  ];

  viewModel$ = this._watchlistService.loadWatchlist().pipe(
    switchMap(() =>
      combineLatest([
        this._watchlistService.filteredWatchlist$,
        this._watchlistService.filter$,
        this._watchlistService.sort$
      ]).pipe(
        map(([items, filter, sort]) => ({
          items: this.applySort(this.applyFilter(items, filter), sort),
          filter,
          sort,
          itemCount: items.length
        }))
      )
    ),
    startWith({ items: [], filter: this.getDefaultFilter(), sort: 'priority', itemCount: 0 })
  );

  onFilterChange(filter: Partial<FilterState>): void {
    this._watchlistService.setFilter(filter);
  }

  onClearFilters(): void {
    this._watchlistService.clearFilters();
  }

  onSortChange(sort: string): void {
    this._watchlistService.setSort(sort);
  }

  onWatch(item: WatchlistItem): void {
    console.log('Watch:', item);
  }

  onRemove(watchlistItemId: string): void {
    this._watchlistService.removeItem(watchlistItemId).subscribe();
  }

  private getDefaultFilter(): FilterState {
    return {
      contentTypes: ['movie', 'tvshow'],
      genres: [],
      moods: [],
      availableNow: false,
      comingSoon: false,
      unavailable: false
    };
  }

  private applyFilter(items: WatchlistItem[], filter: FilterState): WatchlistItem[] {
    return items.filter(item => {
      if (filter.contentTypes.length > 0 && !filter.contentTypes.includes(item.contentType)) {
        return false;
      }
      if (filter.genres.length > 0 && !item.genres.some(g => filter.genres.includes(g))) {
        return false;
      }
      if (filter.moods.length > 0 && item.mood && !filter.moods.includes(item.mood)) {
        return false;
      }
      return true;
    });
  }

  private applySort(items: WatchlistItem[], sort: string): WatchlistItem[] {
    const priorityOrder: Record<string, number> = { high: 0, medium: 1, low: 2 };

    return [...items].sort((a, b) => {
      switch (sort) {
        case 'priority':
          return priorityOrder[a.priority] - priorityOrder[b.priority];
        case 'recently-added':
          return new Date(b.addedDate).getTime() - new Date(a.addedDate).getTime();
        case 'title':
          return a.title.localeCompare(b.title);
        case 'release-year':
          return b.releaseYear - a.releaseYear;
        default:
          return 0;
      }
    });
  }
}
