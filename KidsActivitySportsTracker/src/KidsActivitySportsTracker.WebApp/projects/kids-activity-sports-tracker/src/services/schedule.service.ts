import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Schedule, CreateSchedule, UpdateSchedule } from '../models';
import { environment } from '../environments';

@Injectable({
  providedIn: 'root'
})
export class ScheduleService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/schedules`;

  private schedulesSubject = new BehaviorSubject<Schedule[]>([]);
  public schedules$ = this.schedulesSubject.asObservable();

  loadSchedules(): Observable<Schedule[]> {
    return this.http.get<Schedule[]>(this.baseUrl).pipe(
      tap(schedules => this.schedulesSubject.next(schedules))
    );
  }

  getScheduleById(id: string): Observable<Schedule> {
    return this.http.get<Schedule>(`${this.baseUrl}/${id}`);
  }

  createSchedule(schedule: CreateSchedule): Observable<Schedule> {
    return this.http.post<Schedule>(this.baseUrl, schedule).pipe(
      tap(() => this.loadSchedules().subscribe())
    );
  }

  updateSchedule(schedule: UpdateSchedule): Observable<Schedule> {
    return this.http.put<Schedule>(`${this.baseUrl}/${schedule.scheduleId}`, schedule).pipe(
      tap(() => this.loadSchedules().subscribe())
    );
  }

  deleteSchedule(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => this.loadSchedules().subscribe())
    );
  }
}
