import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Expense } from '../models';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ExpensesService {
  private readonly _http = inject(HttpClient);
  private readonly _baseUrl = `${environment.baseUrl}/api/expenses`;
  private readonly _expensesSubject = new BehaviorSubject<Expense[]>([]);

  public readonly expenses$ = this._expensesSubject.asObservable();

  public getAll(budgetId?: string): Observable<Expense[]> {
    const url = budgetId ? `${this._baseUrl}?budgetId=${budgetId}` : this._baseUrl;
    return this._http.get<Expense[]>(url).pipe(
      tap((expenses) => this._expensesSubject.next(expenses))
    );
  }

  public getById(id: string): Observable<Expense> {
    return this._http.get<Expense>(`${this._baseUrl}/${id}`);
  }

  public create(expense: Partial<Expense>): Observable<Expense> {
    return this._http.post<Expense>(this._baseUrl, expense).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  public update(id: string, expense: Partial<Expense>): Observable<Expense> {
    return this._http.put<Expense>(`${this._baseUrl}/${id}`, { ...expense, expenseId: id }).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  public delete(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/${id}`).pipe(
      tap(() => this.getAll().subscribe())
    );
  }
}
