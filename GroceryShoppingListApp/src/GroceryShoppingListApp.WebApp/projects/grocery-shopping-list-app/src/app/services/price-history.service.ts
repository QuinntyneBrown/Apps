import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { PriceHistory, CreatePriceHistoryRequest, UpdatePriceHistoryRequest } from '../models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PriceHistoryService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.apiUrl;

  private _priceHistoriesSubject = new BehaviorSubject<PriceHistory[]>([]);
  priceHistories$ = this._priceHistoriesSubject.asObservable();

  getAll(): Observable<PriceHistory[]> {
    return this._http.get<PriceHistory[]>(`${this._baseUrl}/api/PriceHistories`).pipe(
      tap(histories => this._priceHistoriesSubject.next(histories))
    );
  }

  getById(id: string): Observable<PriceHistory> {
    return this._http.get<PriceHistory>(`${this._baseUrl}/api/PriceHistories/${id}`);
  }

  create(request: CreatePriceHistoryRequest): Observable<PriceHistory> {
    return this._http.post<PriceHistory>(`${this._baseUrl}/api/PriceHistories`, request).pipe(
      tap(newHistory => {
        const currentHistories = this._priceHistoriesSubject.value;
        this._priceHistoriesSubject.next([...currentHistories, newHistory]);
      })
    );
  }

  update(request: UpdatePriceHistoryRequest): Observable<PriceHistory> {
    return this._http.put<PriceHistory>(`${this._baseUrl}/api/PriceHistories/${request.priceHistoryId}`, request).pipe(
      tap(updatedHistory => {
        const currentHistories = this._priceHistoriesSubject.value;
        const index = currentHistories.findIndex(h => h.priceHistoryId === updatedHistory.priceHistoryId);
        if (index !== -1) {
          currentHistories[index] = updatedHistory;
          this._priceHistoriesSubject.next([...currentHistories]);
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/PriceHistories/${id}`).pipe(
      tap(() => {
        const currentHistories = this._priceHistoriesSubject.value;
        this._priceHistoriesSubject.next(currentHistories.filter(h => h.priceHistoryId !== id));
      })
    );
  }
}
