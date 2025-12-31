import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments';
import { Account, CreateAccount, UpdateAccount } from '../models';

@Injectable({ providedIn: 'root' })
export class AccountService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;

  private _accountsSubject = new BehaviorSubject<Account[]>([]);
  accounts$ = this._accountsSubject.asObservable();

  getAll(): Observable<Account[]> {
    return this._http.get<Account[]>(`${this._baseUrl}/api/Account`).pipe(
      tap(accounts => this._accountsSubject.next(accounts))
    );
  }

  getById(id: string): Observable<Account> {
    return this._http.get<Account>(`${this._baseUrl}/api/Account/${id}`);
  }

  create(account: CreateAccount): Observable<Account> {
    return this._http.post<Account>(`${this._baseUrl}/api/Account`, account).pipe(
      tap(created => {
        const current = this._accountsSubject.value;
        this._accountsSubject.next([...current, created]);
      })
    );
  }

  update(id: string, account: UpdateAccount): Observable<Account> {
    return this._http.put<Account>(`${this._baseUrl}/api/Account/${id}`, account).pipe(
      tap(updated => {
        const current = this._accountsSubject.value;
        const index = current.findIndex(a => a.accountId === id);
        if (index !== -1) {
          current[index] = updated;
          this._accountsSubject.next([...current]);
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/Account/${id}`).pipe(
      tap(() => {
        const current = this._accountsSubject.value;
        this._accountsSubject.next(current.filter(a => a.accountId !== id));
      })
    );
  }
}
