import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Expense, CreateExpense, UpdateExpense } from '../models';
import { environment } from '../environments';

@Injectable({
  providedIn: 'root'
})
export class ExpenseService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/expenses`;

  private expensesSubject = new BehaviorSubject<Expense[]>([]);
  public expenses$ = this.expensesSubject.asObservable();

  getExpenses(): Observable<Expense[]> {
    return this.http.get<Expense[]>(this.baseUrl).pipe(
      tap(expenses => this.expensesSubject.next(expenses))
    );
  }

  getExpense(id: string): Observable<Expense> {
    return this.http.get<Expense>(`${this.baseUrl}/${id}`);
  }

  createExpense(expense: CreateExpense): Observable<Expense> {
    return this.http.post<Expense>(this.baseUrl, expense).pipe(
      tap(() => this.getExpenses().subscribe())
    );
  }

  updateExpense(expense: UpdateExpense): Observable<Expense> {
    return this.http.put<Expense>(`${this.baseUrl}/${expense.expenseId}`, expense).pipe(
      tap(() => this.getExpenses().subscribe())
    );
  }

  deleteExpense(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => this.getExpenses().subscribe())
    );
  }
}
