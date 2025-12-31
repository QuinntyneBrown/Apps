import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments';
import {
  FocusSession,
  CreateFocusSessionCommand,
  UpdateFocusSessionCommand,
  CompleteFocusSessionCommand,
  SessionType
} from '../models';

@Injectable({
  providedIn: 'root'
})
export class FocusSessionsService {
  private readonly apiUrl = `${environment.baseUrl}/api/FocusSessions`;
  private sessionsSubject = new BehaviorSubject<FocusSession[]>([]);
  public sessions$ = this.sessionsSubject.asObservable();

  constructor(private http: HttpClient) {}

  getSessions(
    userId?: string,
    sessionType?: SessionType,
    startDate?: string,
    endDate?: string
  ): Observable<FocusSession[]> {
    let params = new HttpParams();
    if (userId) params = params.set('userId', userId);
    if (sessionType !== undefined) params = params.set('sessionType', sessionType.toString());
    if (startDate) params = params.set('startDate', startDate);
    if (endDate) params = params.set('endDate', endDate);

    return this.http.get<FocusSession[]>(this.apiUrl, { params }).pipe(
      tap(sessions => this.sessionsSubject.next(sessions))
    );
  }

  getSessionById(id: string): Observable<FocusSession> {
    return this.http.get<FocusSession>(`${this.apiUrl}/${id}`);
  }

  createSession(command: CreateFocusSessionCommand): Observable<FocusSession> {
    return this.http.post<FocusSession>(this.apiUrl, command).pipe(
      tap(session => {
        const sessions = this.sessionsSubject.value;
        this.sessionsSubject.next([...sessions, session]);
      })
    );
  }

  updateSession(id: string, command: UpdateFocusSessionCommand): Observable<FocusSession> {
    return this.http.put<FocusSession>(`${this.apiUrl}/${id}`, command).pipe(
      tap(updatedSession => {
        const sessions = this.sessionsSubject.value;
        const index = sessions.findIndex(s => s.focusSessionId === id);
        if (index !== -1) {
          sessions[index] = updatedSession;
          this.sessionsSubject.next([...sessions]);
        }
      })
    );
  }

  completeSession(id: string, command: CompleteFocusSessionCommand): Observable<FocusSession> {
    return this.http.post<FocusSession>(`${this.apiUrl}/${id}/complete`, command).pipe(
      tap(updatedSession => {
        const sessions = this.sessionsSubject.value;
        const index = sessions.findIndex(s => s.focusSessionId === id);
        if (index !== -1) {
          sessions[index] = updatedSession;
          this.sessionsSubject.next([...sessions]);
        }
      })
    );
  }

  deleteSession(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => {
        const sessions = this.sessionsSubject.value.filter(s => s.focusSessionId !== id);
        this.sessionsSubject.next(sessions);
      })
    );
  }
}
