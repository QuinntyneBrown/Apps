import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments';
import { Injury, CreateInjury, UpdateInjury } from '../models';

@Injectable({ providedIn: 'root' })
export class InjuryService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;

  private _injuriesSubject = new BehaviorSubject<Injury[]>([]);
  injuries$ = this._injuriesSubject.asObservable();

  getAll(userId?: string): Observable<Injury[]> {
    let params = new HttpParams();
    if (userId) params = params.set('userId', userId);

    return this._http.get<Injury[]>(`${this._baseUrl}/api/Injuries`, { params }).pipe(
      tap(injuries => this._injuriesSubject.next(injuries))
    );
  }

  getById(id: string): Observable<Injury> {
    return this._http.get<Injury>(`${this._baseUrl}/api/Injuries/${id}`);
  }

  create(injury: CreateInjury): Observable<Injury> {
    return this._http.post<Injury>(`${this._baseUrl}/api/Injuries`, injury).pipe(
      tap(created => {
        const current = this._injuriesSubject.value;
        this._injuriesSubject.next([...current, created]);
      })
    );
  }

  update(id: string, injury: UpdateInjury): Observable<Injury> {
    return this._http.put<Injury>(`${this._baseUrl}/api/Injuries/${id}`, injury).pipe(
      tap(updated => {
        const current = this._injuriesSubject.value;
        const index = current.findIndex(i => i.injuryId === id);
        if (index !== -1) {
          current[index] = updated;
          this._injuriesSubject.next([...current]);
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/Injuries/${id}`).pipe(
      tap(() => {
        const current = this._injuriesSubject.value;
        this._injuriesSubject.next(current.filter(i => i.injuryId !== id));
      })
    );
  }
}
