import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { ExpenseClaim } from '../models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ExpenseClaimsService {
  private readonly _http = inject(HttpClient);
  private readonly _baseUrl = `${environment.baseUrl}/api/expense-claims`;
  private readonly _expenseClaimsSubject = new BehaviorSubject<ExpenseClaim[]>([]);

  public readonly expenseClaims$ = this._expenseClaimsSubject.asObservable();

  public getAll(): Observable<ExpenseClaim[]> {
    return this._http.get<ExpenseClaim[]>(this._baseUrl).pipe(
      tap((expenseClaims) => this._expenseClaimsSubject.next(expenseClaims))
    );
  }

  public getById(id: string): Observable<ExpenseClaim> {
    return this._http.get<ExpenseClaim>(`${this._baseUrl}/${id}`);
  }

  public create(expenseClaim: Partial<ExpenseClaim>): Observable<ExpenseClaim> {
    return this._http.post<ExpenseClaim>(this._baseUrl, expenseClaim).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  public update(id: string, expenseClaim: Partial<ExpenseClaim>): Observable<ExpenseClaim> {
    return this._http.put<ExpenseClaim>(`${this._baseUrl}/${id}`, { ...expenseClaim, expenseClaimId: id }).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  public delete(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/${id}`).pipe(
      tap(() => this.getAll().subscribe())
    );
  }
}
