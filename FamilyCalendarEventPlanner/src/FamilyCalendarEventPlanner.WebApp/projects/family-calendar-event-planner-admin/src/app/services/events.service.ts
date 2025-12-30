// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments';
import { CalendarEventDto, CreateEventCommand, UpdateEventCommand } from '../models/calendar-event-dto';

@Injectable({ providedIn: 'root' })
export class EventsService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = environment.apiBaseUrl;

  getEvents(familyId?: string): Observable<CalendarEventDto[]> {
    const params = familyId ? `?familyId=${familyId}` : '';
    return this.http.get<CalendarEventDto[]>(`${this.baseUrl}/api/events${params}`);
  }

  getEventById(eventId: string): Observable<CalendarEventDto> {
    return this.http.get<CalendarEventDto>(`${this.baseUrl}/api/events/${eventId}`);
  }

  createEvent(command: CreateEventCommand): Observable<CalendarEventDto> {
    return this.http.post<CalendarEventDto>(`${this.baseUrl}/api/events`, command);
  }

  updateEvent(eventId: string, command: UpdateEventCommand): Observable<CalendarEventDto> {
    return this.http.put<CalendarEventDto>(`${this.baseUrl}/api/events/${eventId}`, command);
  }

  deleteEvent(eventId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/api/events/${eventId}`);
  }

  cancelEvent(eventId: string): Observable<CalendarEventDto> {
    return this.http.post<CalendarEventDto>(`${this.baseUrl}/api/events/${eventId}/cancel`, {});
  }

  completeEvent(eventId: string): Observable<CalendarEventDto> {
    return this.http.post<CalendarEventDto>(`${this.baseUrl}/api/events/${eventId}/complete`, {});
  }
}
