import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Challenge } from '../models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ChallengeService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;

  private _challengesSubject = new BehaviorSubject<Challenge[]>([]);
  public challenges$ = this._challengesSubject.asObservable();

  getAll(): Observable<Challenge[]> {
    return this._http.get<Challenge[]>(`${this._baseUrl}/api/challenges`).pipe(
      tap(challenges => this._challengesSubject.next(challenges))
    );
  }

  getById(id: string): Observable<Challenge> {
    return this._http.get<Challenge>(`${this._baseUrl}/api/challenges/${id}`);
  }

  create(challenge: Partial<Challenge>): Observable<Challenge> {
    return this._http.post<Challenge>(`${this._baseUrl}/api/challenges`, challenge).pipe(
      tap(newChallenge => {
        const current = this._challengesSubject.value;
        this._challengesSubject.next([...current, newChallenge]);
      })
    );
  }

  update(id: string, challenge: Partial<Challenge>): Observable<void> {
    return this._http.put<void>(`${this._baseUrl}/api/challenges/${id}`, challenge).pipe(
      tap(() => {
        const current = this._challengesSubject.value;
        const index = current.findIndex(c => c.challengeId === id);
        if (index !== -1) {
          current[index] = { ...current[index], ...challenge };
          this._challengesSubject.next([...current]);
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/challenges/${id}`).pipe(
      tap(() => {
        const current = this._challengesSubject.value;
        this._challengesSubject.next(current.filter(c => c.challengeId !== id));
      })
    );
  }
}
