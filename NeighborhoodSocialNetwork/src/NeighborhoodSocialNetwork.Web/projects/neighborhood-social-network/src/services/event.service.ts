import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, tap } from 'rxjs';
import { environment } from '../environments';
import { Event, CreateEvent, UpdateEvent } from '../models';

@Injectable({
  providedIn: 'root'
})
export class EventService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/events`;

  private eventsSubject = new BehaviorSubject<Event[]>([]);
  events$ = this.eventsSubject.asObservable();

  private selectedEventSubject = new BehaviorSubject<Event | null>(null);
  selectedEvent$ = this.selectedEventSubject.asObservable();

  getAll() {
    return this.http.get<Event[]>(this.baseUrl).pipe(
      tap(events => this.eventsSubject.next(events))
    );
  }

  getById(id: string) {
    return this.http.get<Event>(`${this.baseUrl}/${id}`).pipe(
      tap(event => this.selectedEventSubject.next(event))
    );
  }

  create(event: CreateEvent) {
    return this.http.post<Event>(this.baseUrl, event).pipe(
      tap(newEvent => {
        const events = this.eventsSubject.value;
        this.eventsSubject.next([...events, newEvent]);
      })
    );
  }

  update(event: UpdateEvent) {
    return this.http.put<Event>(`${this.baseUrl}/${event.eventId}`, event).pipe(
      tap(updatedEvent => {
        const events = this.eventsSubject.value.map(e =>
          e.eventId === updatedEvent.eventId ? updatedEvent : e
        );
        this.eventsSubject.next(events);
        this.selectedEventSubject.next(updatedEvent);
      })
    );
  }

  delete(id: string) {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const events = this.eventsSubject.value.filter(e => e.eventId !== id);
        this.eventsSubject.next(events);
        if (this.selectedEventSubject.value?.eventId === id) {
          this.selectedEventSubject.next(null);
        }
      })
    );
  }
}
