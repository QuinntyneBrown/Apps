import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, of, tap, catchError } from 'rxjs';
import { ViewingStats } from '../models';
import { API_CONFIG } from './api-config';

@Injectable({
  providedIn: 'root'
})
export class StatsService {
  private _http = inject(HttpClient);
  private _config = inject(API_CONFIG, { optional: true });

  private _statsSubject = new BehaviorSubject<ViewingStats | null>(null);
  private _periodSubject = new BehaviorSubject<string>('this-year');

  stats$ = this._statsSubject.asObservable();
  period$ = this._periodSubject.asObservable();

  private get baseUrl(): string {
    return this._config?.baseUrl ?? 'http://localhost:5000';
  }

  loadStats(period: string = 'this-year'): Observable<ViewingStats> {
    this._periodSubject.next(period);

    return this._http.get<ViewingStats>(`${this.baseUrl}/api/stats?period=${period}`).pipe(
      tap(stats => this._statsSubject.next(stats)),
      catchError(() => {
        const mockStats = this.getMockStats();
        this._statsSubject.next(mockStats);
        return of(mockStats);
      })
    );
  }

  setPeriod(period: string): void {
    this._periodSubject.next(period);
    this.loadStats(period).subscribe();
  }

  private getMockStats(): ViewingStats {
    return {
      moviesWatched: 142,
      showsWatched: 38,
      hoursWatched: 387,
      averageRating: 4.2,
      currentStreak: 7,
      longestStreak: 21,
      milestones: 5,
      nextMilestone: '150 movies',
      moviesChange: 12,
      showsChange: 5,
      hoursChange: 45,
      genreBreakdown: [
        { genre: 'Drama', percentage: 32 },
        { genre: 'Sci-Fi', percentage: 24 },
        { genre: 'Comedy', percentage: 18 },
        { genre: 'Action', percentage: 15 },
        { genre: 'Horror', percentage: 11 }
      ],
      monthlyData: [
        { month: 'Jan', count: 12 },
        { month: 'Feb', count: 15 },
        { month: 'Mar', count: 17 },
        { month: 'Apr', count: 14 },
        { month: 'May', count: 18 },
        { month: 'Jun', count: 13 },
        { month: 'Jul', count: 11 },
        { month: 'Aug', count: 16 },
        { month: 'Sep', count: 15 },
        { month: 'Oct', count: 17 },
        { month: 'Nov', count: 19 },
        { month: 'Dec', count: 18 }
      ]
    };
  }
}
