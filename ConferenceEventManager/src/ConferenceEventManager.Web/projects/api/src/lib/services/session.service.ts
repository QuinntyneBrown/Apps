import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { Session, CreateSessionCommand, UpdateSessionCommand } from '../models';

@Injectable({
  providedIn: 'root'
})
export class SessionService {
  private readonly apiUrl = `${environment.baseUrl}/api/sessions`;
  private sessionsSubject = new BehaviorSubject<Session[]>([]);
  public sessions$ = this.sessionsSubject.asObservable();

  private currentSessionSubject = new BehaviorSubject<Session | null>(null);
  public currentSession$ = this.currentSessionSubject.asObservable();

  constructor(private http: HttpClient) {}

  getAll(eventId?: string, userId?: string): Observable<Session[]> {
    let url = this.apiUrl;
    const params: string[] = [];
    if (eventId) params.push(`eventId=${eventId}`);
    if (userId) params.push(`userId=${userId}`);
    if (params.length > 0) url += '?' + params.join('&');

    return this.http.get<Session[]>(url).pipe(
      tap(sessions => this.sessionsSubject.next(sessions))
    );
  }

  getById(id: string): Observable<Session> {
    return this.http.get<Session>(`${this.apiUrl}/${id}`).pipe(
      tap(session => this.currentSessionSubject.next(session))
    );
  }

  create(command: CreateSessionCommand): Observable<Session> {
    return this.http.post<Session>(this.apiUrl, command).pipe(
      tap(session => {
        const current = this.sessionsSubject.value;
        this.sessionsSubject.next([...current, session]);
      })
    );
  }

  update(id: string, command: UpdateSessionCommand): Observable<Session> {
    return this.http.put<Session>(`${this.apiUrl}/${id}`, command).pipe(
      tap(updatedSession => {
        const current = this.sessionsSubject.value;
        const index = current.findIndex(s => s.sessionId === id);
        if (index !== -1) {
          current[index] = updatedSession;
          this.sessionsSubject.next([...current]);
        }
        this.currentSessionSubject.next(updatedSession);
      })
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => {
        const current = this.sessionsSubject.value;
        this.sessionsSubject.next(current.filter(s => s.sessionId !== id));
        if (this.currentSessionSubject.value?.sessionId === id) {
          this.currentSessionSubject.next(null);
        }
      })
    );
  }
}
