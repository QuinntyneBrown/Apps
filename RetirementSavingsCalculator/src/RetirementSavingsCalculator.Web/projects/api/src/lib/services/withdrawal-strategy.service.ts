import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments';
import { WithdrawalStrategy, CreateWithdrawalStrategy, UpdateWithdrawalStrategy } from '../models';

@Injectable({
  providedIn: 'root'
})
export class WithdrawalStrategyService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/withdrawal-strategies`;

  private strategiesSubject = new BehaviorSubject<WithdrawalStrategy[]>([]);
  public strategies$ = this.strategiesSubject.asObservable();

  private selectedStrategySubject = new BehaviorSubject<WithdrawalStrategy | null>(null);
  public selectedStrategy$ = this.selectedStrategySubject.asObservable();

  loadStrategies(retirementScenarioId?: string): Observable<WithdrawalStrategy[]> {
    const url = retirementScenarioId
      ? `${this.baseUrl}?retirementScenarioId=${retirementScenarioId}`
      : this.baseUrl;
    return this.http.get<WithdrawalStrategy[]>(url).pipe(
      tap(strategies => this.strategiesSubject.next(strategies))
    );
  }

  getStrategy(id: string): Observable<WithdrawalStrategy> {
    return this.http.get<WithdrawalStrategy>(`${this.baseUrl}/${id}`).pipe(
      tap(strategy => this.selectedStrategySubject.next(strategy))
    );
  }

  createStrategy(strategy: CreateWithdrawalStrategy): Observable<WithdrawalStrategy> {
    return this.http.post<WithdrawalStrategy>(this.baseUrl, strategy).pipe(
      tap(newStrategy => {
        const current = this.strategiesSubject.value;
        this.strategiesSubject.next([...current, newStrategy]);
      })
    );
  }

  updateStrategy(strategy: UpdateWithdrawalStrategy): Observable<WithdrawalStrategy> {
    return this.http.put<WithdrawalStrategy>(`${this.baseUrl}/${strategy.withdrawalStrategyId}`, strategy).pipe(
      tap(updatedStrategy => {
        const current = this.strategiesSubject.value;
        const index = current.findIndex(s => s.withdrawalStrategyId === updatedStrategy.withdrawalStrategyId);
        if (index !== -1) {
          current[index] = updatedStrategy;
          this.strategiesSubject.next([...current]);
        }
        this.selectedStrategySubject.next(updatedStrategy);
      })
    );
  }

  deleteStrategy(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const current = this.strategiesSubject.value;
        this.strategiesSubject.next(current.filter(s => s.withdrawalStrategyId !== id));
        if (this.selectedStrategySubject.value?.withdrawalStrategyId === id) {
          this.selectedStrategySubject.next(null);
        }
      })
    );
  }
}
