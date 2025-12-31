import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { DigitalAccount, CreateDigitalAccountCommand, UpdateDigitalAccountCommand, AccountType } from '../models';

@Injectable({
  providedIn: 'root'
})
export class DigitalAccountsService {
  private readonly baseUrl = `${environment.baseUrl}/api/DigitalAccounts`;
  private accountsSubject = new BehaviorSubject<DigitalAccount[]>([]);
  public accounts$ = this.accountsSubject.asObservable();

  constructor(private http: HttpClient) {}

  getAll(userId?: string, accountType?: AccountType): Observable<DigitalAccount[]> {
    let url = this.baseUrl;
    const params: string[] = [];

    if (userId) params.push(`userId=${userId}`);
    if (accountType !== undefined) params.push(`accountType=${accountType}`);

    if (params.length > 0) {
      url += `?${params.join('&')}`;
    }

    return this.http.get<DigitalAccount[]>(url).pipe(
      tap(accounts => this.accountsSubject.next(accounts))
    );
  }

  getById(id: string): Observable<DigitalAccount> {
    return this.http.get<DigitalAccount>(`${this.baseUrl}/${id}`);
  }

  create(command: CreateDigitalAccountCommand): Observable<DigitalAccount> {
    return this.http.post<DigitalAccount>(this.baseUrl, command).pipe(
      tap(account => {
        const current = this.accountsSubject.value;
        this.accountsSubject.next([...current, account]);
      })
    );
  }

  update(id: string, command: UpdateDigitalAccountCommand): Observable<DigitalAccount> {
    return this.http.put<DigitalAccount>(`${this.baseUrl}/${id}`, command).pipe(
      tap(updated => {
        const current = this.accountsSubject.value;
        const index = current.findIndex(a => a.digitalAccountId === id);
        if (index !== -1) {
          current[index] = updated;
          this.accountsSubject.next([...current]);
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const current = this.accountsSubject.value;
        this.accountsSubject.next(current.filter(a => a.digitalAccountId !== id));
      })
    );
  }
}
