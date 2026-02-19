import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { ViewingRecord, ShowProgress, MarkMovieWatchedRequest, MarkEpisodeWatchedRequest } from '../models';
import { environment } from '../environments';

@Injectable({
  providedIn: 'root'
})
export class ViewingService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = `${environment.baseUrl}/api/viewing`;

  private viewingHistorySubject = new BehaviorSubject<ViewingRecord[]>([]);
  public viewingHistory$ = this.viewingHistorySubject.asObservable();

  getHistory(): Observable<ViewingRecord[]> {
    return this.http.get<ViewingRecord[]>(`${this.apiUrl}/history`).pipe(
      tap(history => this.viewingHistorySubject.next(history))
    );
  }

  getShowProgress(tvShowId: string): Observable<ShowProgress> {
    return this.http.get<ShowProgress>(`${this.apiUrl}/show-progress/${tvShowId}`);
  }

  markMovieWatched(movieId: string, request: MarkMovieWatchedRequest): Observable<ViewingRecord> {
    return this.http.post<ViewingRecord>(`${this.apiUrl}/movie/${movieId}/watched`, request).pipe(
      tap(() => this.refreshHistory())
    );
  }

  markEpisodeWatched(tvShowId: string, seasonNumber: number, episodeNumber: number, request: MarkEpisodeWatchedRequest): Observable<ViewingRecord> {
    return this.http.post<ViewingRecord>(
      `${this.apiUrl}/tvshow/${tvShowId}/season/${seasonNumber}/episode/${episodeNumber}/watched`,
      request
    ).pipe(
      tap(() => this.refreshHistory())
    );
  }

  abandonContent(contentId: string): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/abandon/${contentId}`, {}).pipe(
      tap(() => this.refreshHistory())
    );
  }

  private refreshHistory(): void {
    this.getHistory().subscribe();
  }
}
