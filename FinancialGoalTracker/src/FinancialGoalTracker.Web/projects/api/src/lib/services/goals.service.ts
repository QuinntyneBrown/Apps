import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { Goal, CreateGoalCommand, UpdateGoalCommand, GoalType, GoalStatus } from '../models';

@Injectable({
  providedIn: 'root'
})
export class GoalsService {
  private readonly baseUrl = `${environment.baseUrl}/api/goals`;
  private goalsSubject = new BehaviorSubject<Goal[]>([]);
  public goals$ = this.goalsSubject.asObservable();

  private selectedGoalSubject = new BehaviorSubject<Goal | null>(null);
  public selectedGoal$ = this.selectedGoalSubject.asObservable();

  constructor(private http: HttpClient) {}

  getGoals(goalType?: GoalType, status?: GoalStatus): Observable<Goal[]> {
    let params = new HttpParams();
    if (goalType !== undefined) {
      params = params.set('goalType', goalType.toString());
    }
    if (status !== undefined) {
      params = params.set('status', status.toString());
    }

    return this.http.get<Goal[]>(this.baseUrl, { params }).pipe(
      tap(goals => this.goalsSubject.next(goals))
    );
  }

  getGoalById(goalId: string): Observable<Goal> {
    return this.http.get<Goal>(`${this.baseUrl}/${goalId}`).pipe(
      tap(goal => this.selectedGoalSubject.next(goal))
    );
  }

  createGoal(command: CreateGoalCommand): Observable<Goal> {
    return this.http.post<Goal>(this.baseUrl, command).pipe(
      tap(goal => {
        const currentGoals = this.goalsSubject.value;
        this.goalsSubject.next([...currentGoals, goal]);
      })
    );
  }

  updateGoal(goalId: string, command: UpdateGoalCommand): Observable<Goal> {
    return this.http.put<Goal>(`${this.baseUrl}/${goalId}`, command).pipe(
      tap(updatedGoal => {
        const currentGoals = this.goalsSubject.value;
        const index = currentGoals.findIndex(g => g.goalId === goalId);
        if (index !== -1) {
          currentGoals[index] = updatedGoal;
          this.goalsSubject.next([...currentGoals]);
        }
        if (this.selectedGoalSubject.value?.goalId === goalId) {
          this.selectedGoalSubject.next(updatedGoal);
        }
      })
    );
  }

  deleteGoal(goalId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${goalId}`).pipe(
      tap(() => {
        const currentGoals = this.goalsSubject.value;
        this.goalsSubject.next(currentGoals.filter(g => g.goalId !== goalId));
        if (this.selectedGoalSubject.value?.goalId === goalId) {
          this.selectedGoalSubject.next(null);
        }
      })
    );
  }
}
