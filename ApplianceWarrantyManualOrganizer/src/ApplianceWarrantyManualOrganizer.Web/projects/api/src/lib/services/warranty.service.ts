import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments';
import { Warranty, CreateWarrantyRequest, UpdateWarrantyRequest } from '../models';

@Injectable({
  providedIn: 'root',
})
export class WarrantyService {
  private readonly _http = inject(HttpClient);
  private readonly _baseUrl = `${environment.baseUrl}/api/warranties`;

  private readonly _warranties$ = new BehaviorSubject<Warranty[]>([]);
  public readonly warranties$ = this._warranties$.asObservable();

  getWarrantiesByAppliance(applianceId: string): Observable<Warranty[]> {
    return this._http.get<Warranty[]>(`${this._baseUrl}/appliance/${applianceId}`).pipe(
      tap((warranties) => this._warranties$.next(warranties))
    );
  }

  getWarrantyById(id: string): Observable<Warranty> {
    return this._http.get<Warranty>(`${this._baseUrl}/${id}`);
  }

  createWarranty(request: CreateWarrantyRequest): Observable<Warranty> {
    return this._http.post<Warranty>(this._baseUrl, request).pipe(
      tap((warranty) => {
        const current = this._warranties$.value;
        this._warranties$.next([...current, warranty]);
      })
    );
  }

  updateWarranty(id: string, request: UpdateWarrantyRequest): Observable<Warranty> {
    return this._http.put<Warranty>(`${this._baseUrl}/${id}`, request).pipe(
      tap((updated) => {
        const current = this._warranties$.value;
        const index = current.findIndex((w) => w.warrantyId === id);
        if (index !== -1) {
          current[index] = updated;
          this._warranties$.next([...current]);
        }
      })
    );
  }

  deleteWarranty(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/${id}`).pipe(
      tap(() => {
        const current = this._warranties$.value;
        this._warranties$.next(current.filter((w) => w.warrantyId !== id));
      })
    );
  }
}
