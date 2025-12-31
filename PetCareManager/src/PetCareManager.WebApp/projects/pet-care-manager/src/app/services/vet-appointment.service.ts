import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { VetAppointment, CreateVetAppointmentDto, UpdateVetAppointmentDto } from '../models';

@Injectable({
  providedIn: 'root'
})
export class VetAppointmentService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/vetappointments`;

  private appointmentsSubject = new BehaviorSubject<VetAppointment[]>([]);
  public appointments$ = this.appointmentsSubject.asObservable();

  getAppointments(petId?: string): Observable<VetAppointment[]> {
    let params = new HttpParams();
    if (petId) {
      params = params.set('petId', petId);
    }

    return this.http.get<VetAppointment[]>(this.baseUrl, { params }).pipe(
      tap(appointments => this.appointmentsSubject.next(appointments))
    );
  }

  getAppointmentById(appointmentId: string): Observable<VetAppointment> {
    return this.http.get<VetAppointment>(`${this.baseUrl}/${appointmentId}`);
  }

  createAppointment(appointment: CreateVetAppointmentDto): Observable<VetAppointment> {
    return this.http.post<VetAppointment>(this.baseUrl, appointment).pipe(
      tap(newAppointment => {
        const currentAppointments = this.appointmentsSubject.value;
        this.appointmentsSubject.next([...currentAppointments, newAppointment]);
      })
    );
  }

  updateAppointment(appointmentId: string, appointment: UpdateVetAppointmentDto): Observable<VetAppointment> {
    return this.http.put<VetAppointment>(`${this.baseUrl}/${appointmentId}`, appointment).pipe(
      tap(updatedAppointment => {
        const currentAppointments = this.appointmentsSubject.value;
        const index = currentAppointments.findIndex(a => a.vetAppointmentId === appointmentId);
        if (index !== -1) {
          const newAppointments = [...currentAppointments];
          newAppointments[index] = updatedAppointment;
          this.appointmentsSubject.next(newAppointments);
        }
      })
    );
  }

  deleteAppointment(appointmentId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${appointmentId}`).pipe(
      tap(() => {
        const currentAppointments = this.appointmentsSubject.value;
        this.appointmentsSubject.next(currentAppointments.filter(a => a.vetAppointmentId !== appointmentId));
      })
    );
  }
}
