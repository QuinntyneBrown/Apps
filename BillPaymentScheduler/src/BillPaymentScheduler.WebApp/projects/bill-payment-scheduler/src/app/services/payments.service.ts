import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Payment } from '../models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class PaymentsService {
  private readonly _http = inject(HttpClient);
  private readonly _baseUrl = `${environment.baseUrl}/api/payments`;
  private readonly _paymentsSubject = new BehaviorSubject<Payment[]>([]);

  public readonly payments$ = this._paymentsSubject.asObservable();

  public getAll(): Observable<Payment[]> {
    return this._http.get<Payment[]>(this._baseUrl).pipe(
      tap((payments) => this._paymentsSubject.next(payments))
    );
  }

  public getById(id: string): Observable<Payment> {
    return this._http.get<Payment>(`${this._baseUrl}/${id}`);
  }

  public create(payment: Partial<Payment>): Observable<Payment> {
    return this._http.post<Payment>(this._baseUrl, payment).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  public update(id: string, payment: Partial<Payment>): Observable<Payment> {
    return this._http.put<Payment>(`${this._baseUrl}/${id}`, { ...payment, paymentId: id }).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  public delete(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/${id}`).pipe(
      tap(() => this.getAll().subscribe())
    );
  }
}
