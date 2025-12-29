import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments';
import { CalendarEvent, CreateEventRequest, UpdateEventRequest } from './models';

@Injectable({
  providedIn: 'root'
})
export class EventsService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = environment.apiBaseUrl;

  getEvents(familyId?: string): Observable<CalendarEvent[]> {
    let params = new HttpParams();
    if (familyId) {
      params = params.set('familyId', familyId);
    }
    return this.http.get<CalendarEvent[]>(`${this.baseUrl}/api/events`, { params });
  }

  getEventById(eventId: string): Observable<CalendarEvent> {
    return this.http.get<CalendarEvent>(`${this.baseUrl}/api/events/${eventId}`);
  }

  createEvent(request: CreateEventRequest): Observable<CalendarEvent> {
    return this.http.post<CalendarEvent>(`${this.baseUrl}/api/events`, request);
  }

  updateEvent(request: UpdateEventRequest): Observable<CalendarEvent> {
    return this.http.put<CalendarEvent>(`${this.baseUrl}/api/events/${request.eventId}`, request);
  }

  cancelEvent(eventId: string): Observable<CalendarEvent> {
    return this.http.post<CalendarEvent>(`${this.baseUrl}/api/events/${eventId}/cancel`, {});
  }

  completeEvent(eventId: string): Observable<CalendarEvent> {
    return this.http.post<CalendarEvent>(`${this.baseUrl}/api/events/${eventId}/complete`, {});
  }
}
