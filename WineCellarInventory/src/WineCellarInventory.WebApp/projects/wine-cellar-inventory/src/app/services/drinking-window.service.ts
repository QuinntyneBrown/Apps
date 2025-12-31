import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { DrinkingWindow } from '../models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DrinkingWindowService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;
  private _drinkingWindowsSubject = new BehaviorSubject<DrinkingWindow[]>([]);

  drinkingWindows$ = this._drinkingWindowsSubject.asObservable();

  getDrinkingWindows(): Observable<DrinkingWindow[]> {
    return this._http.get<DrinkingWindow[]>(`${this._baseUrl}/api/drinkingwindows`).pipe(
      tap(windows => this._drinkingWindowsSubject.next(windows))
    );
  }

  getDrinkingWindowById(drinkingWindowId: string): Observable<DrinkingWindow> {
    return this._http.get<DrinkingWindow>(`${this._baseUrl}/api/drinkingwindows/${drinkingWindowId}`);
  }

  createDrinkingWindow(window: Partial<DrinkingWindow>): Observable<DrinkingWindow> {
    return this._http.post<DrinkingWindow>(`${this._baseUrl}/api/drinkingwindows`, window).pipe(
      tap(newWindow => {
        const currentWindows = this._drinkingWindowsSubject.value;
        this._drinkingWindowsSubject.next([...currentWindows, newWindow]);
      })
    );
  }

  updateDrinkingWindow(drinkingWindowId: string, window: Partial<DrinkingWindow>): Observable<DrinkingWindow> {
    return this._http.put<DrinkingWindow>(`${this._baseUrl}/api/drinkingwindows/${drinkingWindowId}`, window).pipe(
      tap(updatedWindow => {
        const currentWindows = this._drinkingWindowsSubject.value;
        const index = currentWindows.findIndex(w => w.drinkingWindowId === drinkingWindowId);
        if (index !== -1) {
          const updatedWindows = [...currentWindows];
          updatedWindows[index] = updatedWindow;
          this._drinkingWindowsSubject.next(updatedWindows);
        }
      })
    );
  }

  deleteDrinkingWindow(drinkingWindowId: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/drinkingwindows/${drinkingWindowId}`).pipe(
      tap(() => {
        const currentWindows = this._drinkingWindowsSubject.value;
        this._drinkingWindowsSubject.next(currentWindows.filter(w => w.drinkingWindowId !== drinkingWindowId));
      })
    );
  }
}
