import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { Benefit } from '../models';

@Injectable({
  providedIn: 'root'
})
export class BenefitService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;

  private _benefits$ = new BehaviorSubject<Benefit[]>([]);
  public benefits$ = this._benefits$.asObservable();

  getBenefits(): Observable<Benefit[]> {
    return this._http.get<Benefit[]>(`${this._baseUrl}/api/benefits`).pipe(
      tap(benefits => this._benefits$.next(benefits))
    );
  }

  getBenefitById(id: string): Observable<Benefit> {
    return this._http.get<Benefit>(`${this._baseUrl}/api/benefits/${id}`);
  }

  createBenefit(benefit: Partial<Benefit>): Observable<Benefit> {
    return this._http.post<Benefit>(`${this._baseUrl}/api/benefits`, benefit).pipe(
      tap(() => this.getBenefits().subscribe())
    );
  }

  updateBenefit(id: string, benefit: Partial<Benefit>): Observable<Benefit> {
    return this._http.put<Benefit>(`${this._baseUrl}/api/benefits/${id}`, benefit).pipe(
      tap(() => this.getBenefits().subscribe())
    );
  }

  deleteBenefit(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/benefits/${id}`).pipe(
      tap(() => this.getBenefits().subscribe())
    );
  }
}
