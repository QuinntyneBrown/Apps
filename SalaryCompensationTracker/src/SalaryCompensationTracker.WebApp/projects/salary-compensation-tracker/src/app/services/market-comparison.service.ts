import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { MarketComparison } from '../models';

@Injectable({
  providedIn: 'root'
})
export class MarketComparisonService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;

  private _marketComparisons$ = new BehaviorSubject<MarketComparison[]>([]);
  public marketComparisons$ = this._marketComparisons$.asObservable();

  getMarketComparisons(): Observable<MarketComparison[]> {
    return this._http.get<MarketComparison[]>(`${this._baseUrl}/api/marketcomparisons`).pipe(
      tap(marketComparisons => this._marketComparisons$.next(marketComparisons))
    );
  }

  getMarketComparisonById(id: string): Observable<MarketComparison> {
    return this._http.get<MarketComparison>(`${this._baseUrl}/api/marketcomparisons/${id}`);
  }

  createMarketComparison(marketComparison: Partial<MarketComparison>): Observable<MarketComparison> {
    return this._http.post<MarketComparison>(`${this._baseUrl}/api/marketcomparisons`, marketComparison).pipe(
      tap(() => this.getMarketComparisons().subscribe())
    );
  }

  updateMarketComparison(id: string, marketComparison: Partial<MarketComparison>): Observable<MarketComparison> {
    return this._http.put<MarketComparison>(`${this._baseUrl}/api/marketcomparisons/${id}`, marketComparison).pipe(
      tap(() => this.getMarketComparisons().subscribe())
    );
  }

  deleteMarketComparison(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/marketcomparisons/${id}`).pipe(
      tap(() => this.getMarketComparisons().subscribe())
    );
  }
}
