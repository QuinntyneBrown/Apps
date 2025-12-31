import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments';
import { EventAttendee, AddAttendeeRequest, RespondToEventRequest } from '../models';

@Injectable({
  providedIn: 'root'
})
export class AttendeesService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = environment.baseUrl;

  getAttendees(eventId: string): Observable<EventAttendee[]> {
    const params = new HttpParams().set('eventId', eventId);
    return this.http.get<EventAttendee[]>(`${this.baseUrl}/api/attendees`, { params });
  }

  addAttendee(request: AddAttendeeRequest): Observable<EventAttendee> {
    return this.http.post<EventAttendee>(`${this.baseUrl}/api/attendees`, request);
  }

  respondToEvent(request: RespondToEventRequest): Observable<EventAttendee> {
    return this.http.put<EventAttendee>(
      `${this.baseUrl}/api/attendees/${request.attendeeId}/respond`,
      request
    );
  }
}
