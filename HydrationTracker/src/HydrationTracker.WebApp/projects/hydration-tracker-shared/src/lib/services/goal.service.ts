import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments/environment';
import { Goal, CreateGoalCommand, UpdateGoalCommand } from '../models';

@Injectable({
  providedIn: 'root'
})
export class GoalService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.baseUrl}/api/goal`;

  private goalsSubject = new BehaviorSubject<Goal[]>([]);
  public goals$ = this.goalsSubject.asObservable();

  private loadingSubject = new BehaviorSubject<boolean>(false);
  public loading$ = this.loadingSubject.asObservable();

  getGoals(): Observable<Goal[]> {
    this.loadingSubject.next(true);
    return this.http.get<Goal[]>(this.apiUrl).pipe(
      tap(goals => {
        this.goalsSubject.next(goals);
        this.loadingSubject.next(false);
      })
    );
  }

  getGoalById(id: string): Observable<Goal> {
    return this.http.get<Goal>(`${this.apiUrl}/${id}`);
  }

  createGoal(command: CreateGoalCommand): Observable<Goal> {
    return this.http.post<Goal>(this.apiUrl, command).pipe(
      tap(goal => {
        const currentGoals = this.goalsSubject.value;
        this.goalsSubject.next([...currentGoals, goal]);
      })
    );
  }

  updateGoal(id: string, command: UpdateGoalCommand): Observable<Goal> {
    return this.http.put<Goal>(`${this.apiUrl}/${id}`, command).pipe(
      tap(updatedGoal => {
        const currentGoals = this.goalsSubject.value;
        const index = currentGoals.findIndex(g => g.goalId === id);
        if (index !== -1) {
          currentGoals[index] = updatedGoal;
          this.goalsSubject.next([...currentGoals]);
        }
      })
    );
  }

  deleteGoal(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => {
        const currentGoals = this.goalsSubject.value;
        this.goalsSubject.next(currentGoals.filter(g => g.goalId !== id));
      })
    );
  }
}
