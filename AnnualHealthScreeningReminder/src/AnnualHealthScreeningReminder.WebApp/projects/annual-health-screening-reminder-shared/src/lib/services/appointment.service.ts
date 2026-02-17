import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Appointment } from '../models';
import { environment } from '../environments';

@Injectable({
  providedIn: 'root'
})
export class AppointmentService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = `${environment.baseUrl}/api/appointments`;

  private appointmentsSubject = new BehaviorSubject<Appointment[]>([]);
  public appointments$ = this.appointmentsSubject.asObservable();

  getAll(userId?: string, screeningId?: string): Observable<Appointment[]> {
    let url = this.apiUrl;
    const params: string[] = [];
    if (userId) params.push(`userId=${userId}`);
    if (screeningId) params.push(`screeningId=${screeningId}`);
    if (params.length > 0) url += `?${params.join('&')}`;

    return this.http.get<Appointment[]>(url).pipe(
      tap(appointments => this.appointmentsSubject.next(appointments))
    );
  }

  getById(id: string): Observable<Appointment> {
    return this.http.get<Appointment>(`${this.apiUrl}/${id}`);
  }

  create(appointment: Partial<Appointment>): Observable<Appointment> {
    return this.http.post<Appointment>(this.apiUrl, appointment).pipe(
      tap(() => this.refreshAppointments())
    );
  }

  update(id: string, appointment: Partial<Appointment>): Observable<Appointment> {
    return this.http.put<Appointment>(`${this.apiUrl}/${id}`, appointment).pipe(
      tap(() => this.refreshAppointments())
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => this.refreshAppointments())
    );
  }

  private refreshAppointments(): void {
    this.getAll().subscribe();
  }
}
