import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Policy } from '../models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PolicyService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;
  private _policiesSubject = new BehaviorSubject<Policy[]>([]);

  public policies$ = this._policiesSubject.asObservable();

  getPolicies(): Observable<Policy[]> {
    return this._http.get<Policy[]>(`${this._baseUrl}/api/policies`).pipe(
      tap(policies => this._policiesSubject.next(policies))
    );
  }

  getPolicy(id: string): Observable<Policy> {
    return this._http.get<Policy>(`${this._baseUrl}/api/policies/${id}`);
  }

  createPolicy(policy: Partial<Policy>): Observable<Policy> {
    return this._http.post<Policy>(`${this._baseUrl}/api/policies`, policy).pipe(
      tap(() => this.getPolicies().subscribe())
    );
  }

  updatePolicy(id: string, policy: Partial<Policy>): Observable<Policy> {
    return this._http.put<Policy>(`${this._baseUrl}/api/policies/${id}`, policy).pipe(
      tap(() => this.getPolicies().subscribe())
    );
  }

  deletePolicy(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/policies/${id}`).pipe(
      tap(() => this.getPolicies().subscribe())
    );
  }
}
