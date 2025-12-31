import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Accomplishment } from '../models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AccomplishmentService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;

  private _accomplishmentsSubject = new BehaviorSubject<Accomplishment[]>([]);
  public accomplishments$ = this._accomplishmentsSubject.asObservable();

  getAll(): Observable<Accomplishment[]> {
    return this._http.get<Accomplishment[]>(`${this._baseUrl}/api/accomplishments`).pipe(
      tap(accomplishments => this._accomplishmentsSubject.next(accomplishments))
    );
  }

  getById(id: string): Observable<Accomplishment> {
    return this._http.get<Accomplishment>(`${this._baseUrl}/api/accomplishments/${id}`);
  }

  create(accomplishment: Partial<Accomplishment>): Observable<Accomplishment> {
    return this._http.post<Accomplishment>(`${this._baseUrl}/api/accomplishments`, accomplishment).pipe(
      tap(newAccomplishment => {
        const current = this._accomplishmentsSubject.value;
        this._accomplishmentsSubject.next([...current, newAccomplishment]);
      })
    );
  }

  update(id: string, accomplishment: Partial<Accomplishment>): Observable<void> {
    return this._http.put<void>(`${this._baseUrl}/api/accomplishments/${id}`, accomplishment).pipe(
      tap(() => {
        const current = this._accomplishmentsSubject.value;
        const index = current.findIndex(a => a.accomplishmentId === id);
        if (index !== -1) {
          current[index] = { ...current[index], ...accomplishment };
          this._accomplishmentsSubject.next([...current]);
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/accomplishments/${id}`).pipe(
      tap(() => {
        const current = this._accomplishmentsSubject.value;
        this._accomplishmentsSubject.next(current.filter(a => a.accomplishmentId !== id));
      })
    );
  }
}
