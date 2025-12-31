import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Income } from '../models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class IncomeService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;

  private _incomes$ = new BehaviorSubject<Income[]>([]);
  public incomes$ = this._incomes$.asObservable();

  getAll(): Observable<Income[]> {
    return this._http.get<Income[]>(`${this._baseUrl}/api/incomes`).pipe(
      tap(incomes => this._incomes$.next(incomes))
    );
  }

  getById(id: string): Observable<Income> {
    return this._http.get<Income>(`${this._baseUrl}/api/incomes/${id}`);
  }

  create(income: Partial<Income>): Observable<Income> {
    return this._http.post<Income>(`${this._baseUrl}/api/incomes`, income).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  update(id: string, income: Partial<Income>): Observable<Income> {
    return this._http.put<Income>(`${this._baseUrl}/api/incomes/${id}`, income).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  delete(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/incomes/${id}`).pipe(
      tap(() => this.getAll().subscribe())
    );
  }
}
