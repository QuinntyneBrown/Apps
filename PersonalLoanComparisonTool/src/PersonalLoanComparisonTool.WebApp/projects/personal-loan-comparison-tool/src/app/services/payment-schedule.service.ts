import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments';
import { PaymentSchedule, CreatePaymentScheduleCommand, UpdatePaymentScheduleCommand } from '../models';

@Injectable({
  providedIn: 'root'
})
export class PaymentScheduleService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/paymentschedules`;

  private paymentSchedulesSubject = new BehaviorSubject<PaymentSchedule[]>([]);
  public paymentSchedules$ = this.paymentSchedulesSubject.asObservable();

  private selectedPaymentScheduleSubject = new BehaviorSubject<PaymentSchedule | null>(null);
  public selectedPaymentSchedule$ = this.selectedPaymentScheduleSubject.asObservable();

  getAll(): Observable<PaymentSchedule[]> {
    return this.http.get<PaymentSchedule[]>(this.baseUrl).pipe(
      tap(paymentSchedules => this.paymentSchedulesSubject.next(paymentSchedules))
    );
  }

  getById(id: string): Observable<PaymentSchedule> {
    return this.http.get<PaymentSchedule>(`${this.baseUrl}/${id}`).pipe(
      tap(paymentSchedule => this.selectedPaymentScheduleSubject.next(paymentSchedule))
    );
  }

  create(command: CreatePaymentScheduleCommand): Observable<PaymentSchedule> {
    return this.http.post<PaymentSchedule>(this.baseUrl, command).pipe(
      tap(paymentSchedule => {
        const currentPaymentSchedules = this.paymentSchedulesSubject.value;
        this.paymentSchedulesSubject.next([...currentPaymentSchedules, paymentSchedule]);
      })
    );
  }

  update(command: UpdatePaymentScheduleCommand): Observable<PaymentSchedule> {
    return this.http.put<PaymentSchedule>(`${this.baseUrl}/${command.paymentScheduleId}`, command).pipe(
      tap(updatedPaymentSchedule => {
        const currentPaymentSchedules = this.paymentSchedulesSubject.value;
        const index = currentPaymentSchedules.findIndex(ps => ps.paymentScheduleId === updatedPaymentSchedule.paymentScheduleId);
        if (index !== -1) {
          currentPaymentSchedules[index] = updatedPaymentSchedule;
          this.paymentSchedulesSubject.next([...currentPaymentSchedules]);
        }
        this.selectedPaymentScheduleSubject.next(updatedPaymentSchedule);
      })
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const currentPaymentSchedules = this.paymentSchedulesSubject.value;
        this.paymentSchedulesSubject.next(currentPaymentSchedules.filter(ps => ps.paymentScheduleId !== id));
        if (this.selectedPaymentScheduleSubject.value?.paymentScheduleId === id) {
          this.selectedPaymentScheduleSubject.next(null);
        }
      })
    );
  }

  clearSelection(): void {
    this.selectedPaymentScheduleSubject.next(null);
  }
}
