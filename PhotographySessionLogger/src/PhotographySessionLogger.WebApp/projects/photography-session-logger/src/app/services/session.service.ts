import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments';
import { Session, CreateSession, UpdateSession } from '../models';

@Injectable({
  providedIn: 'root'
})
export class SessionService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/sessions`;

  private sessionsSubject = new BehaviorSubject<Session[]>([]);
  public sessions$ = this.sessionsSubject.asObservable();

  getAll(): Observable<Session[]> {
    return this.http.get<Session[]>(this.baseUrl).pipe(
      tap(sessions => this.sessionsSubject.next(sessions))
    );
  }

  getById(id: string): Observable<Session> {
    return this.http.get<Session>(`${this.baseUrl}/${id}`);
  }

  create(session: CreateSession): Observable<Session> {
    return this.http.post<Session>(this.baseUrl, session).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  update(session: UpdateSession): Observable<Session> {
    return this.http.put<Session>(`${this.baseUrl}/${session.sessionId}`, session).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => this.getAll().subscribe())
    );
  }
}
