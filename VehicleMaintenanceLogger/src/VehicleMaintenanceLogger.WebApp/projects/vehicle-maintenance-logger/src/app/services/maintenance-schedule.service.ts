import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../environments';
import { MaintenanceSchedule, CreateMaintenanceScheduleRequest, UpdateMaintenanceScheduleRequest } from '../models';

@Injectable({
  providedIn: 'root'
})
export class MaintenanceScheduleService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/maintenanceschedules`;

  private schedulesSubject = new BehaviorSubject<MaintenanceSchedule[]>([]);
  public schedules$ = this.schedulesSubject.asObservable();

  getSchedules(vehicleId?: string): Observable<MaintenanceSchedule[]> {
    const url = vehicleId ? `${this.baseUrl}?vehicleId=${vehicleId}` : this.baseUrl;
    return this.http.get<MaintenanceSchedule[]>(url).pipe(
      tap(schedules => this.schedulesSubject.next(schedules))
    );
  }

  getScheduleById(maintenanceScheduleId: string): Observable<MaintenanceSchedule> {
    return this.http.get<MaintenanceSchedule>(`${this.baseUrl}/${maintenanceScheduleId}`);
  }

  createSchedule(request: CreateMaintenanceScheduleRequest): Observable<MaintenanceSchedule> {
    return this.http.post<MaintenanceSchedule>(this.baseUrl, request).pipe(
      tap(schedule => {
        const currentSchedules = this.schedulesSubject.value;
        this.schedulesSubject.next([...currentSchedules, schedule]);
      })
    );
  }

  updateSchedule(maintenanceScheduleId: string, request: UpdateMaintenanceScheduleRequest): Observable<MaintenanceSchedule> {
    return this.http.put<MaintenanceSchedule>(`${this.baseUrl}/${maintenanceScheduleId}`, request).pipe(
      tap(updatedSchedule => {
        const currentSchedules = this.schedulesSubject.value;
        const index = currentSchedules.findIndex(s => s.maintenanceScheduleId === maintenanceScheduleId);
        if (index !== -1) {
          const updated = [...currentSchedules];
          updated[index] = updatedSchedule;
          this.schedulesSubject.next(updated);
        }
      })
    );
  }

  deleteSchedule(maintenanceScheduleId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${maintenanceScheduleId}`).pipe(
      tap(() => {
        const currentSchedules = this.schedulesSubject.value;
        this.schedulesSubject.next(currentSchedules.filter(s => s.maintenanceScheduleId !== maintenanceScheduleId));
      })
    );
  }
}
