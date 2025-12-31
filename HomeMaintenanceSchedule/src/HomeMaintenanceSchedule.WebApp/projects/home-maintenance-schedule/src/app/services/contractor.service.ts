import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments';
import { Contractor, CreateContractor, UpdateContractor } from '../models';

@Injectable({ providedIn: 'root' })
export class ContractorService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;

  private _contractorsSubject = new BehaviorSubject<Contractor[]>([]);
  contractors$ = this._contractorsSubject.asObservable();

  getAll(userId?: string, specialty?: string, isActive?: boolean): Observable<Contractor[]> {
    let params = new HttpParams();
    if (userId) params = params.set('userId', userId);
    if (specialty) params = params.set('specialty', specialty);
    if (isActive !== undefined) params = params.set('isActive', isActive.toString());

    return this._http.get<Contractor[]>(`${this._baseUrl}/api/Contractors`, { params }).pipe(
      tap(contractors => this._contractorsSubject.next(contractors))
    );
  }

  getById(id: string): Observable<Contractor> {
    return this._http.get<Contractor>(`${this._baseUrl}/api/Contractors/${id}`);
  }

  create(contractor: CreateContractor): Observable<Contractor> {
    return this._http.post<Contractor>(`${this._baseUrl}/api/Contractors`, contractor).pipe(
      tap(created => {
        const current = this._contractorsSubject.value;
        this._contractorsSubject.next([...current, created]);
      })
    );
  }

  update(id: string, contractor: UpdateContractor): Observable<Contractor> {
    return this._http.put<Contractor>(`${this._baseUrl}/api/Contractors/${id}`, contractor).pipe(
      tap(updated => {
        const current = this._contractorsSubject.value;
        const index = current.findIndex(c => c.contractorId === id);
        if (index !== -1) {
          current[index] = updated;
          this._contractorsSubject.next([...current]);
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/Contractors/${id}`).pipe(
      tap(() => {
        const current = this._contractorsSubject.value;
        this._contractorsSubject.next(current.filter(c => c.contractorId !== id));
      })
    );
  }
}
