import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../../environments';
import { Account, CreateAccount, UpdateAccount } from '../models';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/accounts`;

  private accountsSubject = new BehaviorSubject<Account[]>([]);
  public accounts$ = this.accountsSubject.asObservable();

  getAccounts(): Observable<Account[]> {
    return this.http.get<Account[]>(this.baseUrl).pipe(
      tap(accounts => this.accountsSubject.next(accounts))
    );
  }

  getAccountById(id: string): Observable<Account> {
    return this.http.get<Account>(`${this.baseUrl}/${id}`);
  }

  createAccount(account: CreateAccount): Observable<Account> {
    return this.http.post<Account>(this.baseUrl, account).pipe(
      tap(() => this.getAccounts().subscribe())
    );
  }

  updateAccount(account: UpdateAccount): Observable<Account> {
    return this.http.put<Account>(`${this.baseUrl}/${account.accountId}`, account).pipe(
      tap(() => this.getAccounts().subscribe())
    );
  }

  deleteAccount(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => this.getAccounts().subscribe())
    );
  }
}
