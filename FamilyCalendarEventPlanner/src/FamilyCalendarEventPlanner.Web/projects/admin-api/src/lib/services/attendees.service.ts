import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class AttendeesService {
  private readonly apiUrl = '/api/attendees';

  constructor(private http: HttpClient) {}

  getAttendees(eventId: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}?eventId=${eventId}`);
  }

  addAttendee(command: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, command);
  }

  respondToEvent(attendeeId: string, command: any): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/${attendeeId}/respond`, command);
  }
}
