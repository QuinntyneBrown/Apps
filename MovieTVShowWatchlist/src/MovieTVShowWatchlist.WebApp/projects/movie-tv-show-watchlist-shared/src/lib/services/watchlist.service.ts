import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, of, map, tap, catchError } from 'rxjs';
import { WatchlistItem, FilterState, ContentType, Genre, Mood, Priority } from '../models';
import { API_CONFIG } from './api-config';

@Injectable({
  providedIn: 'root'
})
export class WatchlistService {
  private _http = inject(HttpClient);
  private _config = inject(API_CONFIG, { optional: true });

  private _watchlistSubject = new BehaviorSubject<WatchlistItem[]>([]);
  private _filterSubject = new BehaviorSubject<FilterState>({
    contentTypes: ['movie', 'tvshow'],
    genres: [],
    moods: [],
    availableNow: false,
    comingSoon: false,
    unavailable: false
  });
  private _sortSubject = new BehaviorSubject<string>('priority');
  private _initialized = false;

  watchlist$ = this._watchlistSubject.asObservable();
  filter$ = this._filterSubject.asObservable();
  sort$ = this._sortSubject.asObservable();

  filteredWatchlist$: Observable<WatchlistItem[]> = this._watchlistSubject.pipe(
    map(items => this.applyFiltersAndSort(items))
  );

  private get baseUrl(): string {
    return this._config?.baseUrl ?? 'http://localhost:5000';
  }

  loadWatchlist(): Observable<WatchlistItem[]> {
    if (this._initialized) {
      return this.watchlist$;
    }

    return this._http.get<WatchlistItem[]>(`${this.baseUrl}/api/watchlist`).pipe(
      tap(items => {
        this._watchlistSubject.next(items);
        this._initialized = true;
      }),
      catchError(() => {
        const mockData = this.getMockData();
        this._watchlistSubject.next(mockData);
        this._initialized = true;
        return of(mockData);
      })
    );
  }

  addItem(item: Omit<WatchlistItem, 'watchlistItemId' | 'addedDate'>): Observable<WatchlistItem> {
    const newItem: WatchlistItem = {
      ...item,
      watchlistItemId: crypto.randomUUID(),
      addedDate: new Date()
    };

    return this._http.post<WatchlistItem>(`${this.baseUrl}/api/watchlist`, newItem).pipe(
      tap(created => {
        const current = this._watchlistSubject.value;
        this._watchlistSubject.next([...current, created]);
      }),
      catchError(() => {
        const current = this._watchlistSubject.value;
        this._watchlistSubject.next([...current, newItem]);
        return of(newItem);
      })
    );
  }

  removeItem(watchlistItemId: string): Observable<void> {
    return this._http.delete<void>(`${this.baseUrl}/api/watchlist/${watchlistItemId}`).pipe(
      tap(() => {
        const current = this._watchlistSubject.value;
        this._watchlistSubject.next(current.filter(i => i.watchlistItemId !== watchlistItemId));
      }),
      catchError(() => {
        const current = this._watchlistSubject.value;
        this._watchlistSubject.next(current.filter(i => i.watchlistItemId !== watchlistItemId));
        return of(void 0);
      })
    );
  }

  updatePriority(watchlistItemId: string, priority: Priority): Observable<WatchlistItem> {
    return this._http.patch<WatchlistItem>(`${this.baseUrl}/api/watchlist/${watchlistItemId}`, { priority }).pipe(
      tap(updated => {
        const current = this._watchlistSubject.value;
        const index = current.findIndex(i => i.watchlistItemId === watchlistItemId);
        if (index > -1) {
          current[index] = { ...current[index], priority };
          this._watchlistSubject.next([...current]);
        }
      }),
      catchError(() => {
        const current = this._watchlistSubject.value;
        const index = current.findIndex(i => i.watchlistItemId === watchlistItemId);
        if (index > -1) {
          current[index] = { ...current[index], priority };
          this._watchlistSubject.next([...current]);
        }
        return of(current[index]);
      })
    );
  }

  setFilter(filter: Partial<FilterState>): void {
    const current = this._filterSubject.value;
    this._filterSubject.next({ ...current, ...filter });
  }

  clearFilters(): void {
    this._filterSubject.next({
      contentTypes: ['movie', 'tvshow'],
      genres: [],
      moods: [],
      availableNow: false,
      comingSoon: false,
      unavailable: false
    });
  }

  setSort(sort: string): void {
    this._sortSubject.next(sort);
  }

  getItemCount(): number {
    return this._watchlistSubject.value.length;
  }

  private applyFiltersAndSort(items: WatchlistItem[]): WatchlistItem[] {
    const filter = this._filterSubject.value;
    const sort = this._sortSubject.value;

    let filtered = items.filter(item => {
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

    return this.sortItems(filtered, sort);
  }

  private sortItems(items: WatchlistItem[], sort: string): WatchlistItem[] {
    const priorityOrder: Record<Priority, number> = { high: 0, medium: 1, low: 2 };

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

  private getMockData(): WatchlistItem[] {
    return [
      {
        watchlistItemId: '1',
        title: 'Inception',
        contentType: 'movie',
        releaseYear: 2010,
        genres: ['sci-fi', 'thriller'],
        priority: 'high',
        platform: 'Netflix',
        runtime: 148,
        addedDate: new Date(Date.now() - 2 * 24 * 60 * 60 * 1000),
        mood: 'thought-provoking'
      },
      {
        watchlistItemId: '2',
        title: 'Parasite',
        contentType: 'movie',
        releaseYear: 2019,
        genres: ['drama', 'thriller'],
        priority: 'medium',
        platform: 'Criterion',
        runtime: 132,
        addedDate: new Date(Date.now() - 7 * 24 * 60 * 60 * 1000),
        mood: 'thought-provoking'
      },
      {
        watchlistItemId: '3',
        title: 'The Wire',
        contentType: 'tvshow',
        releaseYear: 2002,
        genres: ['drama', 'crime'],
        priority: 'high',
        platform: 'HBO Max',
        seasons: 5,
        addedDate: new Date(Date.now() - 3 * 24 * 60 * 60 * 1000),
        mood: 'intense'
      },
      {
        watchlistItemId: '4',
        title: 'Dune',
        contentType: 'movie',
        releaseYear: 2021,
        genres: ['sci-fi', 'adventure'],
        priority: 'low',
        platform: 'HBO Max',
        runtime: 155,
        addedDate: new Date(Date.now() - 14 * 24 * 60 * 60 * 1000),
        mood: 'action-packed'
      },
      {
        watchlistItemId: '5',
        title: 'The Godfather',
        contentType: 'movie',
        releaseYear: 1972,
        genres: ['crime', 'drama'],
        priority: 'medium',
        platform: 'Prime',
        runtime: 175,
        addedDate: new Date(Date.now() - 30 * 24 * 60 * 60 * 1000),
        mood: 'intense'
      },
      {
        watchlistItemId: '6',
        title: 'The Crown',
        contentType: 'tvshow',
        releaseYear: 2016,
        genres: ['drama', 'history'],
        priority: 'low',
        platform: 'Netflix',
        seasons: 6,
        addedDate: new Date(Date.now() - 60 * 24 * 60 * 60 * 1000),
        mood: 'relaxing'
      }
    ];
  }
}
