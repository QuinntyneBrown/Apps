import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments';
import { Holding, CreateHolding, UpdateHolding } from '../models';

@Injectable({ providedIn: 'root' })
export class HoldingService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;

  private _holdingsSubject = new BehaviorSubject<Holding[]>([]);
  holdings$ = this._holdingsSubject.asObservable();

  getAll(accountId?: string): Observable<Holding[]> {
    let params = new HttpParams();
    if (accountId) params = params.set('accountId', accountId);

    return this._http.get<Holding[]>(`${this._baseUrl}/api/Holding`, { params }).pipe(
      tap(holdings => this._holdingsSubject.next(holdings))
    );
  }

  getById(id: string): Observable<Holding> {
    return this._http.get<Holding>(`${this._baseUrl}/api/Holding/${id}`);
  }

  create(holding: CreateHolding): Observable<Holding> {
    return this._http.post<Holding>(`${this._baseUrl}/api/Holding`, holding).pipe(
      tap(created => {
        const current = this._holdingsSubject.value;
        this._holdingsSubject.next([...current, created]);
      })
    );
  }

  update(id: string, holding: UpdateHolding): Observable<Holding> {
    return this._http.put<Holding>(`${this._baseUrl}/api/Holding/${id}`, holding).pipe(
      tap(updated => {
        const current = this._holdingsSubject.value;
        const index = current.findIndex(h => h.holdingId === id);
        if (index !== -1) {
          current[index] = updated;
          this._holdingsSubject.next([...current]);
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/Holding/${id}`).pipe(
      tap(() => {
        const current = this._holdingsSubject.value;
        this._holdingsSubject.next(current.filter(h => h.holdingId !== id));
      })
    );
  }
}
