import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments';
import { Dividend, CreateDividend, UpdateDividend } from '../models';

@Injectable({ providedIn: 'root' })
export class DividendService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;

  private _dividendsSubject = new BehaviorSubject<Dividend[]>([]);
  dividends$ = this._dividendsSubject.asObservable();

  getAll(holdingId?: string): Observable<Dividend[]> {
    let params = new HttpParams();
    if (holdingId) params = params.set('holdingId', holdingId);

    return this._http.get<Dividend[]>(`${this._baseUrl}/api/Dividend`, { params }).pipe(
      tap(dividends => this._dividendsSubject.next(dividends))
    );
  }

  getById(id: string): Observable<Dividend> {
    return this._http.get<Dividend>(`${this._baseUrl}/api/Dividend/${id}`);
  }

  create(dividend: CreateDividend): Observable<Dividend> {
    return this._http.post<Dividend>(`${this._baseUrl}/api/Dividend`, dividend).pipe(
      tap(created => {
        const current = this._dividendsSubject.value;
        this._dividendsSubject.next([...current, created]);
      })
    );
  }

  update(id: string, dividend: UpdateDividend): Observable<Dividend> {
    return this._http.put<Dividend>(`${this._baseUrl}/api/Dividend/${id}`, dividend).pipe(
      tap(updated => {
        const current = this._dividendsSubject.value;
        const index = current.findIndex(d => d.dividendId === id);
        if (index !== -1) {
          current[index] = updated;
          this._dividendsSubject.next([...current]);
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/Dividend/${id}`).pipe(
      tap(() => {
        const current = this._dividendsSubject.value;
        this._dividendsSubject.next(current.filter(d => d.dividendId !== id));
      })
    );
  }
}
