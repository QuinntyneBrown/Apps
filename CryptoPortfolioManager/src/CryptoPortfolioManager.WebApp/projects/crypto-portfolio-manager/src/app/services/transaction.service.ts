import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { Transaction, CreateTransactionRequest, UpdateTransactionRequest } from '../models';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {
  private readonly apiUrl = `${environment.baseUrl}/api/transactions`;
  private transactionsSubject = new BehaviorSubject<Transaction[]>([]);
  public transactions$ = this.transactionsSubject.asObservable();

  private selectedTransactionSubject = new BehaviorSubject<Transaction | null>(null);
  public selectedTransaction$ = this.selectedTransactionSubject.asObservable();

  constructor(private http: HttpClient) {}

  getTransactions(walletId?: string): Observable<Transaction[]> {
    const url = walletId ? `${this.apiUrl}?walletId=${walletId}` : this.apiUrl;
    return this.http.get<Transaction[]>(url).pipe(
      tap(transactions => this.transactionsSubject.next(transactions))
    );
  }

  getTransactionById(id: string): Observable<Transaction> {
    return this.http.get<Transaction>(`${this.apiUrl}/${id}`).pipe(
      tap(transaction => this.selectedTransactionSubject.next(transaction))
    );
  }

  createTransaction(request: CreateTransactionRequest): Observable<Transaction> {
    return this.http.post<Transaction>(this.apiUrl, request).pipe(
      tap(transaction => {
        const currentTransactions = this.transactionsSubject.value;
        this.transactionsSubject.next([...currentTransactions, transaction]);
      })
    );
  }

  updateTransaction(id: string, request: UpdateTransactionRequest): Observable<Transaction> {
    return this.http.put<Transaction>(`${this.apiUrl}/${id}`, request).pipe(
      tap(transaction => {
        const currentTransactions = this.transactionsSubject.value;
        const index = currentTransactions.findIndex(t => t.transactionId === id);
        if (index !== -1) {
          currentTransactions[index] = transaction;
          this.transactionsSubject.next([...currentTransactions]);
        }
        if (this.selectedTransactionSubject.value?.transactionId === id) {
          this.selectedTransactionSubject.next(transaction);
        }
      })
    );
  }

  deleteTransaction(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => {
        const currentTransactions = this.transactionsSubject.value;
        this.transactionsSubject.next(currentTransactions.filter(t => t.transactionId !== id));
        if (this.selectedTransactionSubject.value?.transactionId === id) {
          this.selectedTransactionSubject.next(null);
        }
      })
    );
  }
}
