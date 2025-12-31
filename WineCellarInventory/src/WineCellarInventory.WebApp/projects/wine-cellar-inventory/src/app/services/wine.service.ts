import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Wine } from '../models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class WineService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;
  private _winesSubject = new BehaviorSubject<Wine[]>([]);

  wines$ = this._winesSubject.asObservable();

  getWines(): Observable<Wine[]> {
    return this._http.get<Wine[]>(`${this._baseUrl}/api/wines`).pipe(
      tap(wines => this._winesSubject.next(wines))
    );
  }

  getWineById(wineId: string): Observable<Wine> {
    return this._http.get<Wine>(`${this._baseUrl}/api/wines/${wineId}`);
  }

  createWine(wine: Partial<Wine>): Observable<Wine> {
    return this._http.post<Wine>(`${this._baseUrl}/api/wines`, wine).pipe(
      tap(newWine => {
        const currentWines = this._winesSubject.value;
        this._winesSubject.next([...currentWines, newWine]);
      })
    );
  }

  updateWine(wineId: string, wine: Partial<Wine>): Observable<Wine> {
    return this._http.put<Wine>(`${this._baseUrl}/api/wines/${wineId}`, wine).pipe(
      tap(updatedWine => {
        const currentWines = this._winesSubject.value;
        const index = currentWines.findIndex(w => w.wineId === wineId);
        if (index !== -1) {
          const updatedWines = [...currentWines];
          updatedWines[index] = updatedWine;
          this._winesSubject.next(updatedWines);
        }
      })
    );
  }

  deleteWine(wineId: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/wines/${wineId}`).pipe(
      tap(() => {
        const currentWines = this._winesSubject.value;
        this._winesSubject.next(currentWines.filter(w => w.wineId !== wineId));
      })
    );
  }
}
