import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { TaxYear, CreateTaxYear, UpdateTaxYear } from '../models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TaxYearService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;

  private _taxYearsSubject = new BehaviorSubject<TaxYear[]>([]);
  public taxYears$ = this._taxYearsSubject.asObservable();

  getAll(): Observable<TaxYear[]> {
    return this._http.get<TaxYear[]>(`${this._baseUrl}/api/taxyears`).pipe(
      tap(taxYears => this._taxYearsSubject.next(taxYears))
    );
  }

  getById(id: string): Observable<TaxYear> {
    return this._http.get<TaxYear>(`${this._baseUrl}/api/taxyears/${id}`);
  }

  create(taxYear: CreateTaxYear): Observable<TaxYear> {
    return this._http.post<TaxYear>(`${this._baseUrl}/api/taxyears`, taxYear).pipe(
      tap(newTaxYear => {
        const current = this._taxYearsSubject.value;
        this._taxYearsSubject.next([...current, newTaxYear]);
      })
    );
  }

  update(taxYear: UpdateTaxYear): Observable<TaxYear> {
    return this._http.put<TaxYear>(`${this._baseUrl}/api/taxyears/${taxYear.taxYearId}`, taxYear).pipe(
      tap(updated => {
        const current = this._taxYearsSubject.value;
        const index = current.findIndex(t => t.taxYearId === updated.taxYearId);
        if (index !== -1) {
          current[index] = updated;
          this._taxYearsSubject.next([...current]);
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/taxyears/${id}`).pipe(
      tap(() => {
        const current = this._taxYearsSubject.value;
        this._taxYearsSubject.next(current.filter(t => t.taxYearId !== id));
      })
    );
  }
}
