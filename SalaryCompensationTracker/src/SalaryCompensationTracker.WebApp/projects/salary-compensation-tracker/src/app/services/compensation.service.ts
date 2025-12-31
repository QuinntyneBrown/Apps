import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { Compensation } from '../models';

@Injectable({
  providedIn: 'root'
})
export class CompensationService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;

  private _compensations$ = new BehaviorSubject<Compensation[]>([]);
  public compensations$ = this._compensations$.asObservable();

  getCompensations(): Observable<Compensation[]> {
    return this._http.get<Compensation[]>(`${this._baseUrl}/api/compensations`).pipe(
      tap(compensations => this._compensations$.next(compensations))
    );
  }

  getCompensationById(id: string): Observable<Compensation> {
    return this._http.get<Compensation>(`${this._baseUrl}/api/compensations/${id}`);
  }

  createCompensation(compensation: Partial<Compensation>): Observable<Compensation> {
    return this._http.post<Compensation>(`${this._baseUrl}/api/compensations`, compensation).pipe(
      tap(() => this.getCompensations().subscribe())
    );
  }

  updateCompensation(id: string, compensation: Partial<Compensation>): Observable<Compensation> {
    return this._http.put<Compensation>(`${this._baseUrl}/api/compensations/${id}`, compensation).pipe(
      tap(() => this.getCompensations().subscribe())
    );
  }

  deleteCompensation(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/compensations/${id}`).pipe(
      tap(() => this.getCompensations().subscribe())
    );
  }
}
