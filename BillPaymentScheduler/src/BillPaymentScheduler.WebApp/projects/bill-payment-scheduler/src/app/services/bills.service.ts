import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Bill } from '../models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class BillsService {
  private readonly _http = inject(HttpClient);
  private readonly _baseUrl = `${environment.baseUrl}/api/bills`;
  private readonly _billsSubject = new BehaviorSubject<Bill[]>([]);

  public readonly bills$ = this._billsSubject.asObservable();

  public getAll(): Observable<Bill[]> {
    return this._http.get<Bill[]>(this._baseUrl).pipe(
      tap((bills) => this._billsSubject.next(bills))
    );
  }

  public getById(id: string): Observable<Bill> {
    return this._http.get<Bill>(`${this._baseUrl}/${id}`);
  }

  public create(bill: Partial<Bill>): Observable<Bill> {
    return this._http.post<Bill>(this._baseUrl, bill).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  public update(id: string, bill: Partial<Bill>): Observable<Bill> {
    return this._http.put<Bill>(`${this._baseUrl}/${id}`, { ...bill, billId: id }).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  public delete(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/${id}`).pipe(
      tap(() => this.getAll().subscribe())
    );
  }
}
