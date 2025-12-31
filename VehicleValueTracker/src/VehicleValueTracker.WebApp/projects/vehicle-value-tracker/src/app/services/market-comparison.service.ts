import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { MarketComparison } from '../models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MarketComparisonService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;
  private _comparisonsSubject = new BehaviorSubject<MarketComparison[]>([]);

  comparisons$ = this._comparisonsSubject.asObservable();

  getMarketComparisons(): Observable<MarketComparison[]> {
    return this._http.get<MarketComparison[]>(`${this._baseUrl}/api/marketcomparisons`).pipe(
      tap(comparisons => this._comparisonsSubject.next(comparisons))
    );
  }

  getMarketComparisonById(id: string): Observable<MarketComparison> {
    return this._http.get<MarketComparison>(`${this._baseUrl}/api/marketcomparisons/${id}`);
  }

  createMarketComparison(comparison: Partial<MarketComparison>): Observable<MarketComparison> {
    return this._http.post<MarketComparison>(`${this._baseUrl}/api/marketcomparisons`, comparison).pipe(
      tap(() => this.getMarketComparisons().subscribe())
    );
  }

  updateMarketComparison(id: string, comparison: Partial<MarketComparison>): Observable<MarketComparison> {
    return this._http.put<MarketComparison>(`${this._baseUrl}/api/marketcomparisons/${id}`, comparison).pipe(
      tap(() => this.getMarketComparisons().subscribe())
    );
  }

  deleteMarketComparison(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/marketcomparisons/${id}`).pipe(
      tap(() => this.getMarketComparisons().subscribe())
    );
  }

  getMarketComparisonsByVehicle(vehicleId: string): Observable<MarketComparison[]> {
    return this._http.get<MarketComparison[]>(`${this._baseUrl}/api/marketcomparisons?vehicleId=${vehicleId}`);
  }
}
