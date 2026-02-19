import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments';
import { Income, CreateIncome, UpdateIncome } from '../models';

@Injectable({ providedIn: 'root' })
export class IncomeService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;

  private _incomesSubject = new BehaviorSubject<Income[]>([]);
  incomes$ = this._incomesSubject.asObservable();

  getAll(budgetId?: string): Observable<Income[]> {
    let params = new HttpParams();
    if (budgetId) params = params.set('budgetId', budgetId);

    return this._http.get<Income[]>(`${this._baseUrl}/api/Incomes`, { params }).pipe(
      tap(incomes => this._incomesSubject.next(incomes))
    );
  }

  getById(id: string): Observable<Income> {
    return this._http.get<Income>(`${this._baseUrl}/api/Incomes/${id}`);
  }

  create(income: CreateIncome): Observable<Income> {
    return this._http.post<Income>(`${this._baseUrl}/api/Incomes`, income).pipe(
      tap(created => {
        const current = this._incomesSubject.value;
        this._incomesSubject.next([...current, created]);
      })
    );
  }

  update(id: string, income: UpdateIncome): Observable<Income> {
    return this._http.put<Income>(`${this._baseUrl}/api/Incomes/${id}`, income).pipe(
      tap(updated => {
        const current = this._incomesSubject.value;
        const index = current.findIndex(i => i.incomeId === id);
        if (index !== -1) {
          current[index] = updated;
          this._incomesSubject.next([...current]);
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/Incomes/${id}`).pipe(
      tap(() => {
        const current = this._incomesSubject.value;
        this._incomesSubject.next(current.filter(i => i.incomeId !== id));
      })
    );
  }
}
