import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments';
import { Transaction, CreateTransaction, UpdateTransaction } from '../models';

@Injectable({ providedIn: 'root' })
export class TransactionService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;

  private _transactionsSubject = new BehaviorSubject<Transaction[]>([]);
  transactions$ = this._transactionsSubject.asObservable();

  getAll(accountId?: string): Observable<Transaction[]> {
    let params = new HttpParams();
    if (accountId) params = params.set('accountId', accountId);

    return this._http.get<Transaction[]>(`${this._baseUrl}/api/Transaction`, { params }).pipe(
      tap(transactions => this._transactionsSubject.next(transactions))
    );
  }

  getById(id: string): Observable<Transaction> {
    return this._http.get<Transaction>(`${this._baseUrl}/api/Transaction/${id}`);
  }

  create(transaction: CreateTransaction): Observable<Transaction> {
    return this._http.post<Transaction>(`${this._baseUrl}/api/Transaction`, transaction).pipe(
      tap(created => {
        const current = this._transactionsSubject.value;
        this._transactionsSubject.next([...current, created]);
      })
    );
  }

  update(id: string, transaction: UpdateTransaction): Observable<Transaction> {
    return this._http.put<Transaction>(`${this._baseUrl}/api/Transaction/${id}`, transaction).pipe(
      tap(updated => {
        const current = this._transactionsSubject.value;
        const index = current.findIndex(t => t.transactionId === id);
        if (index !== -1) {
          current[index] = updated;
          this._transactionsSubject.next([...current]);
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/Transaction/${id}`).pipe(
      tap(() => {
        const current = this._transactionsSubject.value;
        this._transactionsSubject.next(current.filter(t => t.transactionId !== id));
      })
    );
  }
}
