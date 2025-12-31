import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Warranty } from '../models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class WarrantyService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;

  private _warranties$ = new BehaviorSubject<Warranty[]>([]);
  private _selectedWarranty$ = new BehaviorSubject<Warranty | null>(null);

  public readonly warranties$ = this._warranties$.asObservable();
  public readonly selectedWarranty$ = this._selectedWarranty$.asObservable();

  getAll(): Observable<Warranty[]> {
    return this._http.get<Warranty[]>(`${this._baseUrl}/api/warranties`).pipe(
      tap(warranties => this._warranties$.next(warranties))
    );
  }

  getById(id: string): Observable<Warranty> {
    return this._http.get<Warranty>(`${this._baseUrl}/api/warranties/${id}`).pipe(
      tap(warranty => this._selectedWarranty$.next(warranty))
    );
  }

  create(warranty: Partial<Warranty>): Observable<Warranty> {
    return this._http.post<Warranty>(`${this._baseUrl}/api/warranties`, warranty).pipe(
      tap(newWarranty => {
        const current = this._warranties$.value;
        this._warranties$.next([...current, newWarranty]);
      })
    );
  }

  update(id: string, warranty: Partial<Warranty>): Observable<Warranty> {
    return this._http.put<Warranty>(`${this._baseUrl}/api/warranties/${id}`, warranty).pipe(
      tap(updated => {
        const current = this._warranties$.value;
        const index = current.findIndex(w => w.warrantyId === id);
        if (index !== -1) {
          current[index] = updated;
          this._warranties$.next([...current]);
        }
        this._selectedWarranty$.next(updated);
      })
    );
  }

  delete(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/warranties/${id}`).pipe(
      tap(() => {
        const current = this._warranties$.value;
        this._warranties$.next(current.filter(w => w.warrantyId !== id));
        if (this._selectedWarranty$.value?.warrantyId === id) {
          this._selectedWarranty$.next(null);
        }
      })
    );
  }

  setSelectedWarranty(warranty: Warranty | null): void {
    this._selectedWarranty$.next(warranty);
  }
}
