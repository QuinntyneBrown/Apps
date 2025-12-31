import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Team, CreateTeamRequest, UpdateTeamRequest } from '../models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TeamService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;

  private _teamsSubject = new BehaviorSubject<Team[]>([]);
  public teams$ = this._teamsSubject.asObservable();

  getTeams(): Observable<Team[]> {
    return this._http.get<Team[]>(`${this._baseUrl}/api/teams`).pipe(
      tap(teams => this._teamsSubject.next(teams))
    );
  }

  getTeamById(id: string): Observable<Team> {
    return this._http.get<Team>(`${this._baseUrl}/api/teams/${id}`);
  }

  createTeam(request: CreateTeamRequest): Observable<Team> {
    return this._http.post<Team>(`${this._baseUrl}/api/teams`, request).pipe(
      tap(team => {
        const currentTeams = this._teamsSubject.value;
        this._teamsSubject.next([...currentTeams, team]);
      })
    );
  }

  updateTeam(request: UpdateTeamRequest): Observable<Team> {
    return this._http.put<Team>(`${this._baseUrl}/api/teams/${request.teamId}`, request).pipe(
      tap(updatedTeam => {
        const currentTeams = this._teamsSubject.value;
        const index = currentTeams.findIndex(t => t.teamId === updatedTeam.teamId);
        if (index !== -1) {
          currentTeams[index] = updatedTeam;
          this._teamsSubject.next([...currentTeams]);
        }
      })
    );
  }

  deleteTeam(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/teams/${id}`).pipe(
      tap(() => {
        const currentTeams = this._teamsSubject.value;
        this._teamsSubject.next(currentTeams.filter(t => t.teamId !== id));
      })
    );
  }
}
