import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments';
import { Session, CreateSession, UpdateSession } from '../models';

@Injectable({
  providedIn: 'root'
})
export class SessionService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/sessions`;

  private sessionsSubject = new BehaviorSubject<Session[]>([]);
  public sessions$ = this.sessionsSubject.asObservable();

  getSessions(): Observable<Session[]> {
    return this.http.get<Session[]>(this.baseUrl).pipe(
      tap(sessions => this.sessionsSubject.next(sessions))
    );
  }

  getSessionById(id: string): Observable<Session> {
    return this.http.get<Session>(`${this.baseUrl}/${id}`);
  }

  createSession(session: CreateSession): Observable<Session> {
    return this.http.post<Session>(this.baseUrl, session).pipe(
      tap(() => this.getSessions().subscribe())
    );
  }

  updateSession(session: UpdateSession): Observable<Session> {
    return this.http.put<Session>(`${this.baseUrl}/${session.sessionId}`, session).pipe(
      tap(() => this.getSessions().subscribe())
    );
  }

  deleteSession(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => this.getSessions().subscribe())
    );
  }
}
