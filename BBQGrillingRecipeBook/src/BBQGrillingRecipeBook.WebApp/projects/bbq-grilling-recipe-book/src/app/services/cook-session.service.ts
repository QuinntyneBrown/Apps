import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { CookSession, CreateCookSession, UpdateCookSession } from '../models';
import { environment } from '../../environments';

@Injectable({
  providedIn: 'root'
})
export class CookSessionService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/cooksessions`;

  private cookSessionsSubject = new BehaviorSubject<CookSession[]>([]);
  public cookSessions$ = this.cookSessionsSubject.asObservable();

  getCookSessions(): Observable<CookSession[]> {
    return this.http.get<CookSession[]>(this.baseUrl).pipe(
      tap(sessions => this.cookSessionsSubject.next(sessions))
    );
  }

  getCookSessionById(id: string): Observable<CookSession> {
    return this.http.get<CookSession>(`${this.baseUrl}/${id}`);
  }

  createCookSession(session: CreateCookSession): Observable<CookSession> {
    return this.http.post<CookSession>(this.baseUrl, session).pipe(
      tap(newSession => {
        const current = this.cookSessionsSubject.value;
        this.cookSessionsSubject.next([...current, newSession]);
      })
    );
  }

  updateCookSession(session: UpdateCookSession): Observable<CookSession> {
    return this.http.put<CookSession>(`${this.baseUrl}/${session.cookSessionId}`, session).pipe(
      tap(updated => {
        const current = this.cookSessionsSubject.value;
        const index = current.findIndex(s => s.cookSessionId === updated.cookSessionId);
        if (index !== -1) {
          current[index] = updated;
          this.cookSessionsSubject.next([...current]);
        }
      })
    );
  }

  deleteCookSession(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const current = this.cookSessionsSubject.value;
        this.cookSessionsSubject.next(current.filter(s => s.cookSessionId !== id));
      })
    );
  }
}
