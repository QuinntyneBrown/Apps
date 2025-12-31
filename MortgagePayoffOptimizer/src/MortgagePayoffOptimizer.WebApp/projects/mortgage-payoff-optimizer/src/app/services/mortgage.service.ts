import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments';
import { Mortgage, CreateMortgage, UpdateMortgage } from '../models';

@Injectable({
  providedIn: 'root'
})
export class MortgageService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/Mortgage`;

  private mortgagesSubject = new BehaviorSubject<Mortgage[]>([]);
  public mortgages$ = this.mortgagesSubject.asObservable();

  private currentMortgageSubject = new BehaviorSubject<Mortgage | null>(null);
  public currentMortgage$ = this.currentMortgageSubject.asObservable();

  getMortgages(): Observable<Mortgage[]> {
    return this.http.get<Mortgage[]>(this.baseUrl).pipe(
      tap(mortgages => this.mortgagesSubject.next(mortgages))
    );
  }

  getMortgageById(id: string): Observable<Mortgage> {
    return this.http.get<Mortgage>(`${this.baseUrl}/${id}`).pipe(
      tap(mortgage => this.currentMortgageSubject.next(mortgage))
    );
  }

  createMortgage(mortgage: CreateMortgage): Observable<Mortgage> {
    return this.http.post<Mortgage>(this.baseUrl, mortgage).pipe(
      tap(() => this.getMortgages().subscribe())
    );
  }

  updateMortgage(mortgage: UpdateMortgage): Observable<Mortgage> {
    return this.http.put<Mortgage>(`${this.baseUrl}/${mortgage.mortgageId}`, mortgage).pipe(
      tap(() => this.getMortgages().subscribe())
    );
  }

  deleteMortgage(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => this.getMortgages().subscribe())
    );
  }
}
