import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments';
import { DeliverySchedule, CreateDeliverySchedule, UpdateDeliverySchedule } from '../models';

@Injectable({
  providedIn: 'root'
})
export class DeliveryScheduleService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/delivery-schedules`;

  private deliverySchedulesSubject = new BehaviorSubject<DeliverySchedule[]>([]);
  public deliverySchedules$ = this.deliverySchedulesSubject.asObservable();

  private selectedDeliveryScheduleSubject = new BehaviorSubject<DeliverySchedule | null>(null);
  public selectedDeliverySchedule$ = this.selectedDeliveryScheduleSubject.asObservable();

  private loadingSubject = new BehaviorSubject<boolean>(false);
  public loading$ = this.loadingSubject.asObservable();

  getAll(): Observable<DeliverySchedule[]> {
    this.loadingSubject.next(true);
    return this.http.get<DeliverySchedule[]>(this.baseUrl).pipe(
      tap({
        next: (schedules) => {
          this.deliverySchedulesSubject.next(schedules);
          this.loadingSubject.next(false);
        },
        error: () => this.loadingSubject.next(false)
      })
    );
  }

  getById(id: string): Observable<DeliverySchedule> {
    this.loadingSubject.next(true);
    return this.http.get<DeliverySchedule>(`${this.baseUrl}/${id}`).pipe(
      tap({
        next: (schedule) => {
          this.selectedDeliveryScheduleSubject.next(schedule);
          this.loadingSubject.next(false);
        },
        error: () => this.loadingSubject.next(false)
      })
    );
  }

  getByLetterId(letterId: string): Observable<DeliverySchedule[]> {
    this.loadingSubject.next(true);
    return this.http.get<DeliverySchedule[]>(`${this.baseUrl}/letter/${letterId}`).pipe(
      tap({
        next: (schedules) => {
          this.loadingSubject.next(false);
        },
        error: () => this.loadingSubject.next(false)
      })
    );
  }

  create(schedule: CreateDeliverySchedule): Observable<DeliverySchedule> {
    this.loadingSubject.next(true);
    return this.http.post<DeliverySchedule>(this.baseUrl, schedule).pipe(
      tap({
        next: (newSchedule) => {
          const currentSchedules = this.deliverySchedulesSubject.value;
          this.deliverySchedulesSubject.next([...currentSchedules, newSchedule]);
          this.loadingSubject.next(false);
        },
        error: () => this.loadingSubject.next(false)
      })
    );
  }

  update(schedule: UpdateDeliverySchedule): Observable<DeliverySchedule> {
    this.loadingSubject.next(true);
    return this.http.put<DeliverySchedule>(`${this.baseUrl}/${schedule.deliveryScheduleId}`, schedule).pipe(
      tap({
        next: (updatedSchedule) => {
          const currentSchedules = this.deliverySchedulesSubject.value;
          const index = currentSchedules.findIndex(s => s.deliveryScheduleId === updatedSchedule.deliveryScheduleId);
          if (index !== -1) {
            currentSchedules[index] = updatedSchedule;
            this.deliverySchedulesSubject.next([...currentSchedules]);
          }
          this.selectedDeliveryScheduleSubject.next(updatedSchedule);
          this.loadingSubject.next(false);
        },
        error: () => this.loadingSubject.next(false)
      })
    );
  }

  delete(id: string): Observable<void> {
    this.loadingSubject.next(true);
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap({
        next: () => {
          const currentSchedules = this.deliverySchedulesSubject.value;
          this.deliverySchedulesSubject.next(currentSchedules.filter(s => s.deliveryScheduleId !== id));
          if (this.selectedDeliveryScheduleSubject.value?.deliveryScheduleId === id) {
            this.selectedDeliveryScheduleSubject.next(null);
          }
          this.loadingSubject.next(false);
        },
        error: () => this.loadingSubject.next(false)
      })
    );
  }

  clearSelectedDeliverySchedule(): void {
    this.selectedDeliveryScheduleSubject.next(null);
  }
}
