import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class EventsService {
  private readonly apiUrl = '/api/events';

  constructor(private http: HttpClient) {}

  getEvents(familyId?: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}${familyId ? `?familyId=${familyId}` : ''}`);
  }

  getEventById(eventId: string): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${eventId}`);
  }

  createEvent(command: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, command);
  }

  updateEvent(eventId: string, command: any): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/${eventId}`, command);
  }

  deleteEvent(eventId: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${eventId}`);
  }
}
