import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments';
import { Manual, CreateManualRequest, UpdateManualRequest } from '../models';

@Injectable({
  providedIn: 'root',
})
export class ManualService {
  private readonly _http = inject(HttpClient);
  private readonly _baseUrl = `${environment.baseUrl}/api/manuals`;

  private readonly _manuals$ = new BehaviorSubject<Manual[]>([]);
  public readonly manuals$ = this._manuals$.asObservable();

  getManualsByAppliance(applianceId: string): Observable<Manual[]> {
    return this._http.get<Manual[]>(`${this._baseUrl}/appliance/${applianceId}`).pipe(
      tap((manuals) => this._manuals$.next(manuals))
    );
  }

  getManualById(id: string): Observable<Manual> {
    return this._http.get<Manual>(`${this._baseUrl}/${id}`);
  }

  createManual(request: CreateManualRequest): Observable<Manual> {
    return this._http.post<Manual>(this._baseUrl, request).pipe(
      tap((manual) => {
        const current = this._manuals$.value;
        this._manuals$.next([...current, manual]);
      })
    );
  }

  updateManual(id: string, request: UpdateManualRequest): Observable<Manual> {
    return this._http.put<Manual>(`${this._baseUrl}/${id}`, request).pipe(
      tap((updated) => {
        const current = this._manuals$.value;
        const index = current.findIndex((m) => m.manualId === id);
        if (index !== -1) {
          current[index] = updated;
          this._manuals$.next([...current]);
        }
      })
    );
  }

  deleteManual(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/${id}`).pipe(
      tap(() => {
        const current = this._manuals$.value;
        this._manuals$.next(current.filter((m) => m.manualId !== id));
      })
    );
  }
}
