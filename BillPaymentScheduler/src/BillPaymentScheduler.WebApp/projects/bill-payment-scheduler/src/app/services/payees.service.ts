import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Payee } from '../models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class PayeesService {
  private readonly _http = inject(HttpClient);
  private readonly _baseUrl = `${environment.baseUrl}/api/payees`;
  private readonly _payeesSubject = new BehaviorSubject<Payee[]>([]);

  public readonly payees$ = this._payeesSubject.asObservable();

  public getAll(): Observable<Payee[]> {
    return this._http.get<Payee[]>(this._baseUrl).pipe(
      tap((payees) => this._payeesSubject.next(payees))
    );
  }

  public getById(id: string): Observable<Payee> {
    return this._http.get<Payee>(`${this._baseUrl}/${id}`);
  }

  public create(payee: Partial<Payee>): Observable<Payee> {
    return this._http.post<Payee>(this._baseUrl, payee).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  public update(id: string, payee: Partial<Payee>): Observable<Payee> {
    return this._http.put<Payee>(`${this._baseUrl}/${id}`, { ...payee, payeeId: id }).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  public delete(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/${id}`).pipe(
      tap(() => this.getAll().subscribe())
    );
  }
}
