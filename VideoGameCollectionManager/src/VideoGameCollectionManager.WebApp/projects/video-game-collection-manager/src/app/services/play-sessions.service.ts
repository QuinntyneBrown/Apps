import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { PlaySession } from '../models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PlaySessionsService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;
  private _playSessionsSubject = new BehaviorSubject<PlaySession[]>([]);

  public playSessions$ = this._playSessionsSubject.asObservable();

  getAll(): Observable<PlaySession[]> {
    return this._http.get<PlaySession[]>(`${this._baseUrl}/api/playsessions`).pipe(
      tap(sessions => this._playSessionsSubject.next(sessions))
    );
  }

  getById(id: string): Observable<PlaySession> {
    return this._http.get<PlaySession>(`${this._baseUrl}/api/playsessions/${id}`);
  }

  create(session: Partial<PlaySession>): Observable<PlaySession> {
    return this._http.post<PlaySession>(`${this._baseUrl}/api/playsessions`, session).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  update(id: string, session: Partial<PlaySession>): Observable<PlaySession> {
    return this._http.put<PlaySession>(`${this._baseUrl}/api/playsessions/${id}`, session).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  delete(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/playsessions/${id}`).pipe(
      tap(() => this.getAll().subscribe())
    );
  }
}
