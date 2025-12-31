import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Business } from '../models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BusinessService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;

  private _businesses$ = new BehaviorSubject<Business[]>([]);
  public businesses$ = this._businesses$.asObservable();

  getAll(): Observable<Business[]> {
    return this._http.get<Business[]>(`${this._baseUrl}/api/businesses`).pipe(
      tap(businesses => this._businesses$.next(businesses))
    );
  }

  getById(id: string): Observable<Business> {
    return this._http.get<Business>(`${this._baseUrl}/api/businesses/${id}`);
  }

  create(business: Partial<Business>): Observable<Business> {
    return this._http.post<Business>(`${this._baseUrl}/api/businesses`, business).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  update(id: string, business: Partial<Business>): Observable<Business> {
    return this._http.put<Business>(`${this._baseUrl}/api/businesses/${id}`, business).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  delete(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/businesses/${id}`).pipe(
      tap(() => this.getAll().subscribe())
    );
  }
}
