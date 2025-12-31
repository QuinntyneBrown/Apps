import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Game } from '../models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GamesService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;
  private _gamesSubject = new BehaviorSubject<Game[]>([]);

  public games$ = this._gamesSubject.asObservable();

  getAll(): Observable<Game[]> {
    return this._http.get<Game[]>(`${this._baseUrl}/api/games`).pipe(
      tap(games => this._gamesSubject.next(games))
    );
  }

  getById(id: string): Observable<Game> {
    return this._http.get<Game>(`${this._baseUrl}/api/games/${id}`);
  }

  create(game: Partial<Game>): Observable<Game> {
    return this._http.post<Game>(`${this._baseUrl}/api/games`, game).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  update(id: string, game: Partial<Game>): Observable<Game> {
    return this._http.put<Game>(`${this._baseUrl}/api/games/${id}`, game).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  delete(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/games/${id}`).pipe(
      tap(() => this.getAll().subscribe())
    );
  }
}
