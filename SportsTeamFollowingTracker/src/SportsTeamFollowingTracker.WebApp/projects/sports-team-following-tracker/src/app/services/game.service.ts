import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Game, CreateGameRequest, UpdateGameRequest } from '../models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GameService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;

  private _gamesSubject = new BehaviorSubject<Game[]>([]);
  public games$ = this._gamesSubject.asObservable();

  getGames(teamId?: string): Observable<Game[]> {
    const url = teamId
      ? `${this._baseUrl}/api/games?teamId=${teamId}`
      : `${this._baseUrl}/api/games`;
    return this._http.get<Game[]>(url).pipe(
      tap(games => this._gamesSubject.next(games))
    );
  }

  getGameById(id: string): Observable<Game> {
    return this._http.get<Game>(`${this._baseUrl}/api/games/${id}`);
  }

  createGame(request: CreateGameRequest): Observable<Game> {
    return this._http.post<Game>(`${this._baseUrl}/api/games`, request).pipe(
      tap(game => {
        const currentGames = this._gamesSubject.value;
        this._gamesSubject.next([...currentGames, game]);
      })
    );
  }

  updateGame(request: UpdateGameRequest): Observable<Game> {
    return this._http.put<Game>(`${this._baseUrl}/api/games/${request.gameId}`, request).pipe(
      tap(updatedGame => {
        const currentGames = this._gamesSubject.value;
        const index = currentGames.findIndex(g => g.gameId === updatedGame.gameId);
        if (index !== -1) {
          currentGames[index] = updatedGame;
          this._gamesSubject.next([...currentGames]);
        }
      })
    );
  }

  deleteGame(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/games/${id}`).pipe(
      tap(() => {
        const currentGames = this._gamesSubject.value;
        this._gamesSubject.next(currentGames.filter(g => g.gameId !== id));
      })
    );
  }
}
