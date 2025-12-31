import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments';
import { Appliance, CreateApplianceRequest, UpdateApplianceRequest } from '../models';

@Injectable({
  providedIn: 'root',
})
export class ApplianceService {
  private readonly _http = inject(HttpClient);
  private readonly _baseUrl = `${environment.baseUrl}/api/appliances`;

  private readonly _appliances$ = new BehaviorSubject<Appliance[]>([]);
  public readonly appliances$ = this._appliances$.asObservable();

  getAppliances(userId?: string): Observable<Appliance[]> {
    const url = userId ? `${this._baseUrl}?userId=${userId}` : this._baseUrl;
    return this._http.get<Appliance[]>(url).pipe(
      tap((appliances) => this._appliances$.next(appliances))
    );
  }

  getApplianceById(id: string): Observable<Appliance> {
    return this._http.get<Appliance>(`${this._baseUrl}/${id}`);
  }

  createAppliance(request: CreateApplianceRequest): Observable<Appliance> {
    return this._http.post<Appliance>(this._baseUrl, request).pipe(
      tap((appliance) => {
        const current = this._appliances$.value;
        this._appliances$.next([...current, appliance]);
      })
    );
  }

  updateAppliance(id: string, request: UpdateApplianceRequest): Observable<Appliance> {
    return this._http.put<Appliance>(`${this._baseUrl}/${id}`, request).pipe(
      tap((updated) => {
        const current = this._appliances$.value;
        const index = current.findIndex((a) => a.applianceId === id);
        if (index !== -1) {
          current[index] = updated;
          this._appliances$.next([...current]);
        }
      })
    );
  }

  deleteAppliance(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/${id}`).pipe(
      tap(() => {
        const current = this._appliances$.value;
        this._appliances$.next(current.filter((a) => a.applianceId !== id));
      })
    );
  }
}
