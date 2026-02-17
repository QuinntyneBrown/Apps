import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, of, tap, catchError } from 'rxjs';
import { Recommendation } from '../models';
import { API_CONFIG } from './api-config';

@Injectable({
  providedIn: 'root'
})
export class RecommendationService {
  private _http = inject(HttpClient);
  private _config = inject(API_CONFIG, { optional: true });

  private _recommendationsSubject = new BehaviorSubject<Recommendation[]>([]);

  recommendations$ = this._recommendationsSubject.asObservable();

  private get baseUrl(): string {
    return this._config?.baseUrl ?? 'http://localhost:5000';
  }

  loadRecommendations(): Observable<Recommendation[]> {
    return this._http.get<Recommendation[]>(`${this.baseUrl}/api/recommendations`).pipe(
      tap(recs => this._recommendationsSubject.next(recs)),
      catchError(() => {
        const mockData = this.getMockData();
        this._recommendationsSubject.next(mockData);
        return of(mockData);
      })
    );
  }

  dismissRecommendation(recommendationId: string): Observable<void> {
    return this._http.delete<void>(`${this.baseUrl}/api/recommendations/${recommendationId}`).pipe(
      tap(() => {
        const current = this._recommendationsSubject.value;
        this._recommendationsSubject.next(current.filter(r => r.recommendationId !== recommendationId));
      }),
      catchError(() => {
        const current = this._recommendationsSubject.value;
        this._recommendationsSubject.next(current.filter(r => r.recommendationId !== recommendationId));
        return of(void 0);
      })
    );
  }

  private getMockData(): Recommendation[] {
    return [
      {
        recommendationId: '1',
        title: 'Blade Runner 2049',
        contentType: 'movie',
        releaseYear: 2017,
        genres: ['sci-fi', 'drama'],
        matchScore: 95,
        reason: 'Based on your love of Inception and Dune',
        source: 'system'
      },
      {
        recommendationId: '2',
        title: 'True Detective',
        contentType: 'tvshow',
        releaseYear: 2014,
        genres: ['crime', 'drama', 'mystery'],
        matchScore: 92,
        reason: 'Similar to The Wire which you have in your watchlist',
        source: 'system'
      },
      {
        recommendationId: '3',
        title: 'Arrival',
        contentType: 'movie',
        releaseYear: 2016,
        genres: ['sci-fi', 'drama'],
        matchScore: 90,
        reason: 'From the director of Dune',
        source: 'critic'
      },
      {
        recommendationId: '4',
        title: 'The Prestige',
        contentType: 'movie',
        releaseYear: 2006,
        genres: ['drama', 'mystery', 'thriller'],
        matchScore: 88,
        reason: 'Another Nolan masterpiece like Inception',
        source: 'friend'
      },
      {
        recommendationId: '5',
        title: 'Mindhunter',
        contentType: 'tvshow',
        releaseYear: 2017,
        genres: ['crime', 'drama', 'thriller'],
        matchScore: 85,
        reason: 'If you enjoyed crime dramas',
        source: 'system'
      },
      {
        recommendationId: '6',
        title: 'Ex Machina',
        contentType: 'movie',
        releaseYear: 2014,
        genres: ['sci-fi', 'drama', 'thriller'],
        matchScore: 83,
        reason: 'Thought-provoking sci-fi you will enjoy',
        source: 'system'
      }
    ];
  }
}
