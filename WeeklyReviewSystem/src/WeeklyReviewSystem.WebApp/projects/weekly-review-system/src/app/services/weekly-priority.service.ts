import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { WeeklyPriority } from '../models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class WeeklyPriorityService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;

  private _prioritiesSubject = new BehaviorSubject<WeeklyPriority[]>([]);
  public priorities$ = this._prioritiesSubject.asObservable();

  getAll(): Observable<WeeklyPriority[]> {
    return this._http.get<WeeklyPriority[]>(`${this._baseUrl}/api/priorities`).pipe(
      tap(priorities => this._prioritiesSubject.next(priorities))
    );
  }

  getById(id: string): Observable<WeeklyPriority> {
    return this._http.get<WeeklyPriority>(`${this._baseUrl}/api/priorities/${id}`);
  }

  create(priority: Partial<WeeklyPriority>): Observable<WeeklyPriority> {
    return this._http.post<WeeklyPriority>(`${this._baseUrl}/api/priorities`, priority).pipe(
      tap(newPriority => {
        const current = this._prioritiesSubject.value;
        this._prioritiesSubject.next([...current, newPriority]);
      })
    );
  }

  update(id: string, priority: Partial<WeeklyPriority>): Observable<void> {
    return this._http.put<void>(`${this._baseUrl}/api/priorities/${id}`, priority).pipe(
      tap(() => {
        const current = this._prioritiesSubject.value;
        const index = current.findIndex(p => p.weeklyPriorityId === id);
        if (index !== -1) {
          current[index] = { ...current[index], ...priority };
          this._prioritiesSubject.next([...current]);
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/priorities/${id}`).pipe(
      tap(() => {
        const current = this._prioritiesSubject.value;
        this._prioritiesSubject.next(current.filter(p => p.weeklyPriorityId !== id));
      })
    );
  }
}
