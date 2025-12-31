import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { Race, CreateRaceRequest, UpdateRaceRequest } from '../models';

@Injectable({
  providedIn: 'root'
})
export class RaceService {
  private readonly _http = inject(HttpClient);
  private readonly _baseUrl = environment.baseUrl;

  private readonly _racesSubject = new BehaviorSubject<Race[]>([]);
  public readonly races$ = this._racesSubject.asObservable();

  getRaces(): Observable<Race[]> {
    return this._http.get<Race[]>(`${this._baseUrl}/api/race`).pipe(
      tap(races => this._racesSubject.next(races))
    );
  }

  getRaceById(id: string): Observable<Race> {
    return this._http.get<Race>(`${this._baseUrl}/api/race/${id}`);
  }

  createRace(request: CreateRaceRequest): Observable<Race> {
    return this._http.post<Race>(`${this._baseUrl}/api/race`, request).pipe(
      tap(newRace => {
        const currentRaces = this._racesSubject.value;
        this._racesSubject.next([...currentRaces, newRace]);
      })
    );
  }

  updateRace(id: string, request: UpdateRaceRequest): Observable<Race> {
    return this._http.put<Race>(`${this._baseUrl}/api/race/${id}`, request).pipe(
      tap(updatedRace => {
        const currentRaces = this._racesSubject.value;
        const index = currentRaces.findIndex(r => r.raceId === id);
        if (index !== -1) {
          currentRaces[index] = updatedRace;
          this._racesSubject.next([...currentRaces]);
        }
      })
    );
  }

  deleteRace(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/race/${id}`).pipe(
      tap(() => {
        const currentRaces = this._racesSubject.value;
        this._racesSubject.next(currentRaces.filter(r => r.raceId !== id));
      })
    );
  }
}
