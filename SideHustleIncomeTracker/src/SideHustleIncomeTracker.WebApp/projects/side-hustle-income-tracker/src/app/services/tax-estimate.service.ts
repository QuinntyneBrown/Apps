import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { TaxEstimate } from '../models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TaxEstimateService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;

  private _taxEstimates$ = new BehaviorSubject<TaxEstimate[]>([]);
  public taxEstimates$ = this._taxEstimates$.asObservable();

  getAll(): Observable<TaxEstimate[]> {
    return this._http.get<TaxEstimate[]>(`${this._baseUrl}/api/taxestimates`).pipe(
      tap(taxEstimates => this._taxEstimates$.next(taxEstimates))
    );
  }

  getById(id: string): Observable<TaxEstimate> {
    return this._http.get<TaxEstimate>(`${this._baseUrl}/api/taxestimates/${id}`);
  }

  create(taxEstimate: Partial<TaxEstimate>): Observable<TaxEstimate> {
    return this._http.post<TaxEstimate>(`${this._baseUrl}/api/taxestimates`, taxEstimate).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  update(id: string, taxEstimate: Partial<TaxEstimate>): Observable<TaxEstimate> {
    return this._http.put<TaxEstimate>(`${this._baseUrl}/api/taxestimates/${id}`, taxEstimate).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  delete(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/taxestimates/${id}`).pipe(
      tap(() => this.getAll().subscribe())
    );
  }
}
