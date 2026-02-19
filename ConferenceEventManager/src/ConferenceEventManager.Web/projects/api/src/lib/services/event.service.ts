import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { Event, CreateEventCommand, UpdateEventCommand } from '../models';

@Injectable({
  providedIn: 'root'
})
export class EventService {
  private readonly apiUrl = `${environment.baseUrl}/api/events`;
  private eventsSubject = new BehaviorSubject<Event[]>([]);
  public events$ = this.eventsSubject.asObservable();

  private currentEventSubject = new BehaviorSubject<Event | null>(null);
  public currentEvent$ = this.currentEventSubject.asObservable();

  constructor(private http: HttpClient) {}

  getAll(userId?: string): Observable<Event[]> {
    const url = userId ? `${this.apiUrl}?userId=${userId}` : this.apiUrl;
    return this.http.get<Event[]>(url).pipe(
      tap(events => this.eventsSubject.next(events))
    );
  }

  getById(id: string): Observable<Event> {
    return this.http.get<Event>(`${this.apiUrl}/${id}`).pipe(
      tap(event => this.currentEventSubject.next(event))
    );
  }

  create(command: CreateEventCommand): Observable<Event> {
    return this.http.post<Event>(this.apiUrl, command).pipe(
      tap(event => {
        const current = this.eventsSubject.value;
        this.eventsSubject.next([...current, event]);
      })
    );
  }

  update(id: string, command: UpdateEventCommand): Observable<Event> {
    return this.http.put<Event>(`${this.apiUrl}/${id}`, command).pipe(
      tap(updatedEvent => {
        const current = this.eventsSubject.value;
        const index = current.findIndex(e => e.eventId === id);
        if (index !== -1) {
          current[index] = updatedEvent;
          this.eventsSubject.next([...current]);
        }
        this.currentEventSubject.next(updatedEvent);
      })
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => {
        const current = this.eventsSubject.value;
        this.eventsSubject.next(current.filter(e => e.eventId !== id));
        if (this.currentEventSubject.value?.eventId === id) {
          this.currentEventSubject.next(null);
        }
      })
    );
  }
}
