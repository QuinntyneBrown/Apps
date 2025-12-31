import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Budget } from '../models';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BudgetService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/budgets`;

  private budgetsSubject = new BehaviorSubject<Budget[]>([]);
  public budgets$ = this.budgetsSubject.asObservable();

  getByProjectId(projectId: string): Observable<Budget[]> {
    return this.http.get<Budget[]>(`${this.baseUrl}?projectId=${projectId}`).pipe(
      tap(budgets => this.budgetsSubject.next(budgets))
    );
  }

  create(budget: Partial<Budget>): Observable<Budget> {
    return this.http.post<Budget>(this.baseUrl, budget).pipe(
      tap(newBudget => {
        const current = this.budgetsSubject.value;
        this.budgetsSubject.next([...current, newBudget]);
      })
    );
  }
}
