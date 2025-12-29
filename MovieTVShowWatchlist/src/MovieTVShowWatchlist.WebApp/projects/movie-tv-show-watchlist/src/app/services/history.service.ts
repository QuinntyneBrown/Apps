import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, of, tap, catchError } from 'rxjs';
import { ViewingRecord } from '../models';
import { API_CONFIG } from './api-config';

@Injectable({
  providedIn: 'root'
})
export class HistoryService {
  private _http = inject(HttpClient);
  private _config = inject(API_CONFIG, { optional: true });

  private _historySubject = new BehaviorSubject<ViewingRecord[]>([]);

  history$ = this._historySubject.asObservable();

  private get baseUrl(): string {
    return this._config?.baseUrl ?? 'http://localhost:5000';
  }

  loadHistory(): Observable<ViewingRecord[]> {
    return this._http.get<ViewingRecord[]>(`${this.baseUrl}/api/history`).pipe(
      tap(records => this._historySubject.next(records)),
      catchError(() => {
        const mockData = this.getMockData();
        this._historySubject.next(mockData);
        return of(mockData);
      })
    );
  }

  markAsWatched(item: Omit<ViewingRecord, 'viewingRecordId'>): Observable<ViewingRecord> {
    const newRecord: ViewingRecord = {
      ...item,
      viewingRecordId: crypto.randomUUID()
    };

    return this._http.post<ViewingRecord>(`${this.baseUrl}/api/history`, newRecord).pipe(
      tap(record => {
        const current = this._historySubject.value;
        this._historySubject.next([record, ...current]);
      }),
      catchError(() => {
        const current = this._historySubject.value;
        this._historySubject.next([newRecord, ...current]);
        return of(newRecord);
      })
    );
  }

  private getMockData(): ViewingRecord[] {
    return [
      {
        viewingRecordId: '1',
        title: 'Oppenheimer',
        contentType: 'movie',
        watchDate: new Date(Date.now() - 1 * 24 * 60 * 60 * 1000),
        platform: 'Theater',
        rating: 5,
        isRewatch: false,
        runtime: 180
      },
      {
        viewingRecordId: '2',
        title: 'Breaking Bad',
        contentType: 'tvshow',
        watchDate: new Date(Date.now() - 2 * 24 * 60 * 60 * 1000),
        platform: 'Netflix',
        rating: 5,
        isRewatch: true,
        runtime: 47
      },
      {
        viewingRecordId: '3',
        title: 'The Dark Knight',
        contentType: 'movie',
        watchDate: new Date(Date.now() - 5 * 24 * 60 * 60 * 1000),
        platform: 'HBO Max',
        rating: 5,
        isRewatch: true,
        runtime: 152
      },
      {
        viewingRecordId: '4',
        title: 'Succession',
        contentType: 'tvshow',
        watchDate: new Date(Date.now() - 7 * 24 * 60 * 60 * 1000),
        platform: 'HBO Max',
        rating: 4,
        isRewatch: false,
        runtime: 60
      },
      {
        viewingRecordId: '5',
        title: 'Interstellar',
        contentType: 'movie',
        watchDate: new Date(Date.now() - 10 * 24 * 60 * 60 * 1000),
        platform: 'Prime',
        rating: 5,
        isRewatch: false,
        runtime: 169
      }
    ];
  }
}
