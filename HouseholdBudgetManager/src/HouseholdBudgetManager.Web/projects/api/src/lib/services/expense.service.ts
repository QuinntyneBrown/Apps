import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments';
import { Expense, CreateExpense, UpdateExpense } from '../models';

@Injectable({ providedIn: 'root' })
export class ExpenseService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;

  private _expensesSubject = new BehaviorSubject<Expense[]>([]);
  expenses$ = this._expensesSubject.asObservable();

  getAll(budgetId?: string): Observable<Expense[]> {
    let params = new HttpParams();
    if (budgetId) params = params.set('budgetId', budgetId);

    return this._http.get<Expense[]>(`${this._baseUrl}/api/Expenses`, { params }).pipe(
      tap(expenses => this._expensesSubject.next(expenses))
    );
  }

  getById(id: string): Observable<Expense> {
    return this._http.get<Expense>(`${this._baseUrl}/api/Expenses/${id}`);
  }

  create(expense: CreateExpense): Observable<Expense> {
    return this._http.post<Expense>(`${this._baseUrl}/api/Expenses`, expense).pipe(
      tap(created => {
        const current = this._expensesSubject.value;
        this._expensesSubject.next([...current, created]);
      })
    );
  }

  update(id: string, expense: UpdateExpense): Observable<Expense> {
    return this._http.put<Expense>(`${this._baseUrl}/api/Expenses/${id}`, expense).pipe(
      tap(updated => {
        const current = this._expensesSubject.value;
        const index = current.findIndex(e => e.expenseId === id);
        if (index !== -1) {
          current[index] = updated;
          this._expensesSubject.next([...current]);
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/Expenses/${id}`).pipe(
      tap(() => {
        const current = this._expensesSubject.value;
        this._expensesSubject.next(current.filter(e => e.expenseId !== id));
      })
    );
  }
}
