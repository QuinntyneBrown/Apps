import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { CashFlow, CreateCashFlow, UpdateCashFlow } from '../models';
import { environment } from '../environments';

@Injectable({
  providedIn: 'root'
})
export class CashFlowService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/cashflows`;

  private cashFlowsSubject = new BehaviorSubject<CashFlow[]>([]);
  public cashFlows$ = this.cashFlowsSubject.asObservable();

  getCashFlows(): Observable<CashFlow[]> {
    return this.http.get<CashFlow[]>(this.baseUrl).pipe(
      tap(cashFlows => this.cashFlowsSubject.next(cashFlows))
    );
  }

  getCashFlow(id: string): Observable<CashFlow> {
    return this.http.get<CashFlow>(`${this.baseUrl}/${id}`);
  }

  createCashFlow(cashFlow: CreateCashFlow): Observable<CashFlow> {
    return this.http.post<CashFlow>(this.baseUrl, cashFlow).pipe(
      tap(() => this.getCashFlows().subscribe())
    );
  }

  updateCashFlow(cashFlow: UpdateCashFlow): Observable<CashFlow> {
    return this.http.put<CashFlow>(`${this.baseUrl}/${cashFlow.cashFlowId}`, cashFlow).pipe(
      tap(() => this.getCashFlows().subscribe())
    );
  }

  deleteCashFlow(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => this.getCashFlows().subscribe())
    );
  }
}
