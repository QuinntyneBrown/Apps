import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Season, CreateSeasonRequest, UpdateSeasonRequest } from '../models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SeasonService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;

  private _seasonsSubject = new BehaviorSubject<Season[]>([]);
  public seasons$ = this._seasonsSubject.asObservable();

  getSeasons(teamId?: string): Observable<Season[]> {
    const url = teamId
      ? `${this._baseUrl}/api/seasons?teamId=${teamId}`
      : `${this._baseUrl}/api/seasons`;
    return this._http.get<Season[]>(url).pipe(
      tap(seasons => this._seasonsSubject.next(seasons))
    );
  }

  getSeasonById(id: string): Observable<Season> {
    return this._http.get<Season>(`${this._baseUrl}/api/seasons/${id}`);
  }

  createSeason(request: CreateSeasonRequest): Observable<Season> {
    return this._http.post<Season>(`${this._baseUrl}/api/seasons`, request).pipe(
      tap(season => {
        const currentSeasons = this._seasonsSubject.value;
        this._seasonsSubject.next([...currentSeasons, season]);
      })
    );
  }

  updateSeason(request: UpdateSeasonRequest): Observable<Season> {
    return this._http.put<Season>(`${this._baseUrl}/api/seasons/${request.seasonId}`, request).pipe(
      tap(updatedSeason => {
        const currentSeasons = this._seasonsSubject.value;
        const index = currentSeasons.findIndex(s => s.seasonId === updatedSeason.seasonId);
        if (index !== -1) {
          currentSeasons[index] = updatedSeason;
          this._seasonsSubject.next([...currentSeasons]);
        }
      })
    );
  }

  deleteSeason(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/seasons/${id}`).pipe(
      tap(() => {
        const currentSeasons = this._seasonsSubject.value;
        this._seasonsSubject.next(currentSeasons.filter(s => s.seasonId !== id));
      })
    );
  }
}
