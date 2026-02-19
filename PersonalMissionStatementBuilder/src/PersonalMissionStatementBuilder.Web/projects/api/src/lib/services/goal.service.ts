import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments';
import { Goal, CreateGoal, UpdateGoal } from '../models';

@Injectable({
  providedIn: 'root'
})
export class GoalService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/goals`;

  private goalsSubject = new BehaviorSubject<Goal[]>([]);
  public goals$ = this.goalsSubject.asObservable();

  private currentGoalSubject = new BehaviorSubject<Goal | null>(null);
  public currentGoal$ = this.currentGoalSubject.asObservable();

  getAll(): Observable<Goal[]> {
    return this.http.get<Goal[]>(this.baseUrl).pipe(
      tap(goals => this.goalsSubject.next(goals))
    );
  }

  getById(id: string): Observable<Goal> {
    return this.http.get<Goal>(`${this.baseUrl}/${id}`).pipe(
      tap(goal => this.currentGoalSubject.next(goal))
    );
  }

  create(goal: CreateGoal): Observable<Goal> {
    return this.http.post<Goal>(this.baseUrl, goal).pipe(
      tap(newGoal => {
        const current = this.goalsSubject.value;
        this.goalsSubject.next([...current, newGoal]);
      })
    );
  }

  update(goal: UpdateGoal): Observable<Goal> {
    return this.http.put<Goal>(`${this.baseUrl}/${goal.goalId}`, goal).pipe(
      tap(updated => {
        const current = this.goalsSubject.value;
        const index = current.findIndex(g => g.goalId === updated.goalId);
        if (index !== -1) {
          current[index] = updated;
          this.goalsSubject.next([...current]);
        }
        this.currentGoalSubject.next(updated);
      })
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const current = this.goalsSubject.value;
        this.goalsSubject.next(current.filter(g => g.goalId !== id));
        if (this.currentGoalSubject.value?.goalId === id) {
          this.currentGoalSubject.next(null);
        }
      })
    );
  }
}
