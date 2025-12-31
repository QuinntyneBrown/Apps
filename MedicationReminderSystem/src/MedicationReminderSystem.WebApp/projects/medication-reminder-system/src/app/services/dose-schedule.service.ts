import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap, catchError, throwError } from 'rxjs';
import { environment } from '../../environments/environment';
import { DoseSchedule, CreateDoseScheduleCommand, UpdateDoseScheduleCommand } from '../models';

@Injectable({
  providedIn: 'root'
})
export class DoseScheduleService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/doseschedules`;

  private doseSchedulesSubject = new BehaviorSubject<DoseSchedule[]>([]);
  private loadingSubject = new BehaviorSubject<boolean>(false);
  private errorSubject = new BehaviorSubject<string | null>(null);

  doseSchedules$ = this.doseSchedulesSubject.asObservable();
  loading$ = this.loadingSubject.asObservable();
  error$ = this.errorSubject.asObservable();

  getAll(): Observable<DoseSchedule[]> {
    this.loadingSubject.next(true);
    this.errorSubject.next(null);

    return this.http.get<DoseSchedule[]>(this.baseUrl).pipe(
      tap(schedules => {
        this.doseSchedulesSubject.next(schedules);
        this.loadingSubject.next(false);
      }),
      catchError(error => {
        this.errorSubject.next(error.message);
        this.loadingSubject.next(false);
        return throwError(() => error);
      })
    );
  }

  getById(id: string): Observable<DoseSchedule> {
    this.loadingSubject.next(true);
    this.errorSubject.next(null);

    return this.http.get<DoseSchedule>(`${this.baseUrl}/${id}`).pipe(
      tap(() => this.loadingSubject.next(false)),
      catchError(error => {
        this.errorSubject.next(error.message);
        this.loadingSubject.next(false);
        return throwError(() => error);
      })
    );
  }

  create(command: CreateDoseScheduleCommand): Observable<DoseSchedule> {
    this.loadingSubject.next(true);
    this.errorSubject.next(null);

    return this.http.post<DoseSchedule>(this.baseUrl, command).pipe(
      tap(schedule => {
        const current = this.doseSchedulesSubject.value;
        this.doseSchedulesSubject.next([...current, schedule]);
        this.loadingSubject.next(false);
      }),
      catchError(error => {
        this.errorSubject.next(error.message);
        this.loadingSubject.next(false);
        return throwError(() => error);
      })
    );
  }

  update(id: string, command: UpdateDoseScheduleCommand): Observable<DoseSchedule> {
    this.loadingSubject.next(true);
    this.errorSubject.next(null);

    return this.http.put<DoseSchedule>(`${this.baseUrl}/${id}`, command).pipe(
      tap(schedule => {
        const current = this.doseSchedulesSubject.value;
        const index = current.findIndex(s => s.doseScheduleId === id);
        if (index !== -1) {
          const updated = [...current];
          updated[index] = schedule;
          this.doseSchedulesSubject.next(updated);
        }
        this.loadingSubject.next(false);
      }),
      catchError(error => {
        this.errorSubject.next(error.message);
        this.loadingSubject.next(false);
        return throwError(() => error);
      })
    );
  }

  delete(id: string): Observable<void> {
    this.loadingSubject.next(true);
    this.errorSubject.next(null);

    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const current = this.doseSchedulesSubject.value;
        this.doseSchedulesSubject.next(current.filter(s => s.doseScheduleId !== id));
        this.loadingSubject.next(false);
      }),
      catchError(error => {
        this.errorSubject.next(error.message);
        this.loadingSubject.next(false);
        return throwError(() => error);
      })
    );
  }
}
