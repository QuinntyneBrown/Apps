import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Budget } from '../models';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class BudgetsService {
  private readonly _http = inject(HttpClient);
  private readonly _baseUrl = `${environment.baseUrl}/api/budgets`;
  private readonly _budgetsSubject = new BehaviorSubject<Budget[]>([]);

  public readonly budgets$ = this._budgetsSubject.asObservable();

  public getAll(): Observable<Budget[]> {
    return this._http.get<Budget[]>(this._baseUrl).pipe(
      tap((budgets) => this._budgetsSubject.next(budgets))
    );
  }

  public getById(id: string): Observable<Budget> {
    return this._http.get<Budget>(`${this._baseUrl}/${id}`);
  }

  public create(budget: Partial<Budget>): Observable<Budget> {
    return this._http.post<Budget>(this._baseUrl, budget).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  public update(id: string, budget: Partial<Budget>): Observable<Budget> {
    return this._http.put<Budget>(`${this._baseUrl}/${id}`, { ...budget, budgetId: id }).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  public delete(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/${id}`).pipe(
      tap(() => this.getAll().subscribe())
    );
  }
}
