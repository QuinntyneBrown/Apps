import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Income } from '../models';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class IncomesService {
  private readonly _http = inject(HttpClient);
  private readonly _baseUrl = `${environment.baseUrl}/api/incomes`;
  private readonly _incomesSubject = new BehaviorSubject<Income[]>([]);

  public readonly incomes$ = this._incomesSubject.asObservable();

  public getAll(budgetId?: string): Observable<Income[]> {
    const url = budgetId ? `${this._baseUrl}?budgetId=${budgetId}` : this._baseUrl;
    return this._http.get<Income[]>(url).pipe(
      tap((incomes) => this._incomesSubject.next(incomes))
    );
  }

  public getById(id: string): Observable<Income> {
    return this._http.get<Income>(`${this._baseUrl}/${id}`);
  }

  public create(income: Partial<Income>): Observable<Income> {
    return this._http.post<Income>(this._baseUrl, income).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  public update(id: string, income: Partial<Income>): Observable<Income> {
    return this._http.put<Income>(`${this._baseUrl}/${id}`, { ...income, incomeId: id }).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  public delete(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/${id}`).pipe(
      tap(() => this.getAll().subscribe())
    );
  }
}
