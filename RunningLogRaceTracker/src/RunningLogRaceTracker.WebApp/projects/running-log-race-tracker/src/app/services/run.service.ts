import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { Run, CreateRunRequest, UpdateRunRequest } from '../models';

@Injectable({
  providedIn: 'root'
})
export class RunService {
  private readonly _http = inject(HttpClient);
  private readonly _baseUrl = environment.baseUrl;

  private readonly _runsSubject = new BehaviorSubject<Run[]>([]);
  public readonly runs$ = this._runsSubject.asObservable();

  getRuns(): Observable<Run[]> {
    return this._http.get<Run[]>(`${this._baseUrl}/api/run`).pipe(
      tap(runs => this._runsSubject.next(runs))
    );
  }

  getRunById(id: string): Observable<Run> {
    return this._http.get<Run>(`${this._baseUrl}/api/run/${id}`);
  }

  createRun(request: CreateRunRequest): Observable<Run> {
    return this._http.post<Run>(`${this._baseUrl}/api/run`, request).pipe(
      tap(newRun => {
        const currentRuns = this._runsSubject.value;
        this._runsSubject.next([...currentRuns, newRun]);
      })
    );
  }

  updateRun(id: string, request: UpdateRunRequest): Observable<Run> {
    return this._http.put<Run>(`${this._baseUrl}/api/run/${id}`, request).pipe(
      tap(updatedRun => {
        const currentRuns = this._runsSubject.value;
        const index = currentRuns.findIndex(r => r.runId === id);
        if (index !== -1) {
          currentRuns[index] = updatedRun;
          this._runsSubject.next([...currentRuns]);
        }
      })
    );
  }

  deleteRun(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/run/${id}`).pipe(
      tap(() => {
        const currentRuns = this._runsSubject.value;
        this._runsSubject.next(currentRuns.filter(r => r.runId !== id));
      })
    );
  }
}
