import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments';
import { Payment, CreatePayment, UpdatePayment } from '../models';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/Payment`;

  private paymentsSubject = new BehaviorSubject<Payment[]>([]);
  public payments$ = this.paymentsSubject.asObservable();

  private currentPaymentSubject = new BehaviorSubject<Payment | null>(null);
  public currentPayment$ = this.currentPaymentSubject.asObservable();

  getPayments(): Observable<Payment[]> {
    return this.http.get<Payment[]>(this.baseUrl).pipe(
      tap(payments => this.paymentsSubject.next(payments))
    );
  }

  getPaymentById(id: string): Observable<Payment> {
    return this.http.get<Payment>(`${this.baseUrl}/${id}`).pipe(
      tap(payment => this.currentPaymentSubject.next(payment))
    );
  }

  createPayment(payment: CreatePayment): Observable<Payment> {
    return this.http.post<Payment>(this.baseUrl, payment).pipe(
      tap(() => this.getPayments().subscribe())
    );
  }

  updatePayment(payment: UpdatePayment): Observable<Payment> {
    return this.http.put<Payment>(`${this.baseUrl}/${payment.paymentId}`, payment).pipe(
      tap(() => this.getPayments().subscribe())
    );
  }

  deletePayment(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => this.getPayments().subscribe())
    );
  }
}
