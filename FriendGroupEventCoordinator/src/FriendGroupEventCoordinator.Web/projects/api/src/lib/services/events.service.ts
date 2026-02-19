import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { Event, CreateEvent, UpdateEvent } from '../models';

@Injectable({
  providedIn: 'root'
})
export class EventsService {
  private readonly apiUrl = `${environment.baseUrl}/api/events`;
  private eventsSubject = new BehaviorSubject<Event[]>([]);
  public events$ = this.eventsSubject.asObservable();

  constructor(private http: HttpClient) {}

  getEvents(): Observable<Event[]> {
    return this.http.get<Event[]>(this.apiUrl).pipe(
      tap(events => this.eventsSubject.next(events))
    );
  }

  getEvent(id: string): Observable<Event> {
    return this.http.get<Event>(`${this.apiUrl}/${id}`);
  }

  getEventsByGroup(groupId: string): Observable<Event[]> {
    return this.http.get<Event[]>(`${this.apiUrl}/group/${groupId}`);
  }

  createEvent(event: CreateEvent): Observable<Event> {
    return this.http.post<Event>(this.apiUrl, event).pipe(
      tap(() => this.getEvents().subscribe())
    );
  }

  updateEvent(id: string, event: UpdateEvent): Observable<Event> {
    return this.http.put<Event>(`${this.apiUrl}/${id}`, event).pipe(
      tap(() => this.getEvents().subscribe())
    );
  }

  cancelEvent(id: string): Observable<Event> {
    return this.http.post<Event>(`${this.apiUrl}/${id}/cancel`, {}).pipe(
      tap(() => this.getEvents().subscribe())
    );
  }

  deleteEvent(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => this.getEvents().subscribe())
    );
  }
}
