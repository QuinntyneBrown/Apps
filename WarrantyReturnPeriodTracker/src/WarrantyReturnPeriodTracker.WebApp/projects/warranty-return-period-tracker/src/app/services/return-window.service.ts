import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { ReturnWindow } from '../models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ReturnWindowService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;

  private _returnWindows$ = new BehaviorSubject<ReturnWindow[]>([]);
  private _selectedReturnWindow$ = new BehaviorSubject<ReturnWindow | null>(null);

  public readonly returnWindows$ = this._returnWindows$.asObservable();
  public readonly selectedReturnWindow$ = this._selectedReturnWindow$.asObservable();

  getAll(): Observable<ReturnWindow[]> {
    return this._http.get<ReturnWindow[]>(`${this._baseUrl}/api/returnwindows`).pipe(
      tap(returnWindows => this._returnWindows$.next(returnWindows))
    );
  }

  getById(id: string): Observable<ReturnWindow> {
    return this._http.get<ReturnWindow>(`${this._baseUrl}/api/returnwindows/${id}`).pipe(
      tap(returnWindow => this._selectedReturnWindow$.next(returnWindow))
    );
  }

  create(returnWindow: Partial<ReturnWindow>): Observable<ReturnWindow> {
    return this._http.post<ReturnWindow>(`${this._baseUrl}/api/returnwindows`, returnWindow).pipe(
      tap(newReturnWindow => {
        const current = this._returnWindows$.value;
        this._returnWindows$.next([...current, newReturnWindow]);
      })
    );
  }

  update(id: string, returnWindow: Partial<ReturnWindow>): Observable<ReturnWindow> {
    return this._http.put<ReturnWindow>(`${this._baseUrl}/api/returnwindows/${id}`, returnWindow).pipe(
      tap(updated => {
        const current = this._returnWindows$.value;
        const index = current.findIndex(rw => rw.returnWindowId === id);
        if (index !== -1) {
          current[index] = updated;
          this._returnWindows$.next([...current]);
        }
        this._selectedReturnWindow$.next(updated);
      })
    );
  }

  delete(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/returnwindows/${id}`).pipe(
      tap(() => {
        const current = this._returnWindows$.value;
        this._returnWindows$.next(current.filter(rw => rw.returnWindowId !== id));
        if (this._selectedReturnWindow$.value?.returnWindowId === id) {
          this._selectedReturnWindow$.next(null);
        }
      })
    );
  }

  setSelectedReturnWindow(returnWindow: ReturnWindow | null): void {
    this._selectedReturnWindow$.next(returnWindow);
  }
}
