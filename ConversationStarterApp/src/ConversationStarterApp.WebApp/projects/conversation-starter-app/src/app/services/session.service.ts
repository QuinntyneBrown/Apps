import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { Session, CreateSessionRequest, UpdateSessionRequest } from '../models';

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

  getByUserId(userId: string): Observable<Session[]> {
    const params = new HttpParams().set('userId', userId);
    return this.http.get<Session[]>(this.apiUrl, { params }).pipe(
      tap(sessions => this.sessionsSubject.next(sessions))
    );
  }

  getRecent(userId: string, count: number = 10): Observable<Session[]> {
    const params = new HttpParams()
      .set('userId', userId)
      .set('count', count.toString());
    return this.http.get<Session[]>(`${this.apiUrl}/recent`, { params }).pipe(
      tap(sessions => this.sessionsSubject.next(sessions))
    );
  }

  getById(id: string): Observable<Session> {
    return this.http.get<Session>(`${this.apiUrl}/${id}`).pipe(
      tap(session => this.currentSessionSubject.next(session))
    );
  }

  create(request: CreateSessionRequest): Observable<Session> {
    return this.http.post<Session>(this.apiUrl, request).pipe(
      tap(session => {
        const current = this.sessionsSubject.value;
        this.sessionsSubject.next([session, ...current]);
        this.currentSessionSubject.next(session);
      })
    );
  }

  update(id: string, request: UpdateSessionRequest): Observable<Session> {
    return this.http.put<Session>(`${this.apiUrl}/${id}`, request).pipe(
      tap(updated => {
        const current = this.sessionsSubject.value;
        const index = current.findIndex(s => s.sessionId === id);
        if (index !== -1) {
          const newSessions = [...current];
          newSessions[index] = updated;
          this.sessionsSubject.next(newSessions);
        }
        if (this.currentSessionSubject.value?.sessionId === id) {
          this.currentSessionSubject.next(updated);
        }
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

  endSession(id: string): Observable<Session> {
    return this.http.post<Session>(`${this.apiUrl}/${id}/end`, {}).pipe(
      tap(updated => {
        const current = this.sessionsSubject.value;
        const index = current.findIndex(s => s.sessionId === id);
        if (index !== -1) {
          const newSessions = [...current];
          newSessions[index] = updated;
          this.sessionsSubject.next(newSessions);
        }
        if (this.currentSessionSubject.value?.sessionId === id) {
          this.currentSessionSubject.next(updated);
        }
      })
    );
  }
}
