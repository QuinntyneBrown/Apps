import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Deduction, CreateDeduction, UpdateDeduction } from '../models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DeductionService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;

  private _deductionsSubject = new BehaviorSubject<Deduction[]>([]);
  public deductions$ = this._deductionsSubject.asObservable();

  getAll(): Observable<Deduction[]> {
    return this._http.get<Deduction[]>(`${this._baseUrl}/api/deductions`).pipe(
      tap(deductions => this._deductionsSubject.next(deductions))
    );
  }

  getById(id: string): Observable<Deduction> {
    return this._http.get<Deduction>(`${this._baseUrl}/api/deductions/${id}`);
  }

  getByTaxYear(taxYearId: string): Observable<Deduction[]> {
    return this._http.get<Deduction[]>(`${this._baseUrl}/api/deductions?taxYearId=${taxYearId}`).pipe(
      tap(deductions => this._deductionsSubject.next(deductions))
    );
  }

  create(deduction: CreateDeduction): Observable<Deduction> {
    return this._http.post<Deduction>(`${this._baseUrl}/api/deductions`, deduction).pipe(
      tap(newDeduction => {
        const current = this._deductionsSubject.value;
        this._deductionsSubject.next([...current, newDeduction]);
      })
    );
  }

  update(deduction: UpdateDeduction): Observable<Deduction> {
    return this._http.put<Deduction>(`${this._baseUrl}/api/deductions/${deduction.deductionId}`, deduction).pipe(
      tap(updated => {
        const current = this._deductionsSubject.value;
        const index = current.findIndex(d => d.deductionId === updated.deductionId);
        if (index !== -1) {
          current[index] = updated;
          this._deductionsSubject.next([...current]);
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/deductions/${id}`).pipe(
      tap(() => {
        const current = this._deductionsSubject.value;
        this._deductionsSubject.next(current.filter(d => d.deductionId !== id));
      })
    );
  }
}
