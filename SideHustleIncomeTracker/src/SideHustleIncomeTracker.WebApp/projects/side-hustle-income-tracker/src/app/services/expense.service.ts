import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Expense } from '../models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ExpenseService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;

  private _expenses$ = new BehaviorSubject<Expense[]>([]);
  public expenses$ = this._expenses$.asObservable();

  getAll(): Observable<Expense[]> {
    return this._http.get<Expense[]>(`${this._baseUrl}/api/expenses`).pipe(
      tap(expenses => this._expenses$.next(expenses))
    );
  }

  getById(id: string): Observable<Expense> {
    return this._http.get<Expense>(`${this._baseUrl}/api/expenses/${id}`);
  }

  create(expense: Partial<Expense>): Observable<Expense> {
    return this._http.post<Expense>(`${this._baseUrl}/api/expenses`, expense).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  update(id: string, expense: Partial<Expense>): Observable<Expense> {
    return this._http.put<Expense>(`${this._baseUrl}/api/expenses/${id}`, expense).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  delete(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/expenses/${id}`).pipe(
      tap(() => this.getAll().subscribe())
    );
  }
}
