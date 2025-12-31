import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments';
import { Budget, CreateBudget, UpdateBudget } from '../models';

@Injectable({ providedIn: 'root' })
export class BudgetService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;

  private _budgetsSubject = new BehaviorSubject<Budget[]>([]);
  budgets$ = this._budgetsSubject.asObservable();

  getAll(): Observable<Budget[]> {
    return this._http.get<Budget[]>(`${this._baseUrl}/api/Budgets`).pipe(
      tap(budgets => this._budgetsSubject.next(budgets))
    );
  }

  getById(id: string): Observable<Budget> {
    return this._http.get<Budget>(`${this._baseUrl}/api/Budgets/${id}`);
  }

  create(budget: CreateBudget): Observable<Budget> {
    return this._http.post<Budget>(`${this._baseUrl}/api/Budgets`, budget).pipe(
      tap(created => {
        const current = this._budgetsSubject.value;
        this._budgetsSubject.next([...current, created]);
      })
    );
  }

  update(id: string, budget: UpdateBudget): Observable<Budget> {
    return this._http.put<Budget>(`${this._baseUrl}/api/Budgets/${id}`, budget).pipe(
      tap(updated => {
        const current = this._budgetsSubject.value;
        const index = current.findIndex(b => b.budgetId === id);
        if (index !== -1) {
          current[index] = updated;
          this._budgetsSubject.next([...current]);
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/Budgets/${id}`).pipe(
      tap(() => {
        const current = this._budgetsSubject.value;
        this._budgetsSubject.next(current.filter(b => b.budgetId !== id));
      })
    );
  }
}
