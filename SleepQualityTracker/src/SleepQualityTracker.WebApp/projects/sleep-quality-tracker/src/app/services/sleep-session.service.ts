import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { SleepSession, CreateSleepSessionRequest, UpdateSleepSessionRequest, SleepQuality } from '../models';

@Injectable({
  providedIn: 'root'
})
export class SleepSessionService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/sleepsessions`;

  private sleepSessionsSubject = new BehaviorSubject<SleepSession[]>([]);
  public sleepSessions$ = this.sleepSessionsSubject.asObservable();

  getSleepSessions(
    userId?: string,
    startDate?: string,
    endDate?: string,
    sleepQuality?: SleepQuality,
    meetsRecommendedDuration?: boolean
  ): Observable<SleepSession[]> {
    let params = new HttpParams();
    if (userId) params = params.set('userId', userId);
    if (startDate) params = params.set('startDate', startDate);
    if (endDate) params = params.set('endDate', endDate);
    if (sleepQuality !== undefined) params = params.set('sleepQuality', sleepQuality.toString());
    if (meetsRecommendedDuration !== undefined) params = params.set('meetsRecommendedDuration', meetsRecommendedDuration.toString());

    return this.http.get<SleepSession[]>(this.baseUrl, { params }).pipe(
      tap(sessions => this.sleepSessionsSubject.next(sessions))
    );
  }

  getSleepSessionById(sleepSessionId: string): Observable<SleepSession> {
    return this.http.get<SleepSession>(`${this.baseUrl}/${sleepSessionId}`);
  }

  createSleepSession(request: CreateSleepSessionRequest): Observable<SleepSession> {
    return this.http.post<SleepSession>(this.baseUrl, request).pipe(
      tap(session => {
        const current = this.sleepSessionsSubject.value;
        this.sleepSessionsSubject.next([...current, session]);
      })
    );
  }

  updateSleepSession(request: UpdateSleepSessionRequest): Observable<SleepSession> {
    return this.http.put<SleepSession>(`${this.baseUrl}/${request.sleepSessionId}`, request).pipe(
      tap(session => {
        const current = this.sleepSessionsSubject.value;
        const index = current.findIndex(s => s.sleepSessionId === session.sleepSessionId);
        if (index !== -1) {
          current[index] = session;
          this.sleepSessionsSubject.next([...current]);
        }
      })
    );
  }

  deleteSleepSession(sleepSessionId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${sleepSessionId}`).pipe(
      tap(() => {
        const current = this.sleepSessionsSubject.value;
        this.sleepSessionsSubject.next(current.filter(s => s.sleepSessionId !== sleepSessionId));
      })
    );
  }
}
