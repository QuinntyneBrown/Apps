import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { VacationBudget, CreateVacationBudgetCommand, UpdateVacationBudgetCommand } from '../models';

@Injectable({
  providedIn: 'root'
})
export class VacationBudgetService {
  private readonly apiUrl = `${environment.baseUrl}/api/vacationbudgets`;
  private budgetsSubject = new BehaviorSubject<VacationBudget[]>([]);
  public budgets$ = this.budgetsSubject.asObservable();

  constructor(private http: HttpClient) {}

  getBudgets(tripId?: string): Observable<VacationBudget[]> {
    const params = tripId ? { tripId } : {};
    return this.http.get<VacationBudget[]>(this.apiUrl, { params }).pipe(
      tap(budgets => this.budgetsSubject.next(budgets))
    );
  }

  getBudgetById(vacationBudgetId: string): Observable<VacationBudget> {
    return this.http.get<VacationBudget>(`${this.apiUrl}/${vacationBudgetId}`);
  }

  createBudget(command: CreateVacationBudgetCommand): Observable<VacationBudget> {
    return this.http.post<VacationBudget>(this.apiUrl, command).pipe(
      tap(budget => {
        const currentBudgets = this.budgetsSubject.value;
        this.budgetsSubject.next([...currentBudgets, budget]);
      })
    );
  }

  updateBudget(vacationBudgetId: string, command: UpdateVacationBudgetCommand): Observable<VacationBudget> {
    return this.http.put<VacationBudget>(`${this.apiUrl}/${vacationBudgetId}`, command).pipe(
      tap(updatedBudget => {
        const currentBudgets = this.budgetsSubject.value;
        const index = currentBudgets.findIndex(b => b.vacationBudgetId === vacationBudgetId);
        if (index !== -1) {
          currentBudgets[index] = updatedBudget;
          this.budgetsSubject.next([...currentBudgets]);
        }
      })
    );
  }

  deleteBudget(vacationBudgetId: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${vacationBudgetId}`).pipe(
      tap(() => {
        const currentBudgets = this.budgetsSubject.value;
        this.budgetsSubject.next(currentBudgets.filter(b => b.vacationBudgetId !== vacationBudgetId));
      })
    );
  }
}
