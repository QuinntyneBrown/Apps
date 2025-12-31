import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { Goal, CreateGoal, UpdateGoal } from '../models';

@Injectable({
  providedIn: 'root'
})
export class GoalService {
  private readonly apiUrl = `${environment.baseUrl}/api/goals`;
  private goalsSubject = new BehaviorSubject<Goal[]>([]);
  public goals$ = this.goalsSubject.asObservable();

  constructor(private http: HttpClient) {}

  getAll(userId?: string): Observable<Goal[]> {
    const url = userId ? `${this.apiUrl}?userId=${userId}` : this.apiUrl;
    return this.http.get<Goal[]>(url).pipe(
      tap(goals => this.goalsSubject.next(goals))
    );
  }

  getById(id: string): Observable<Goal> {
    return this.http.get<Goal>(`${this.apiUrl}/${id}`);
  }

  create(command: CreateGoal): Observable<Goal> {
    return this.http.post<Goal>(this.apiUrl, command).pipe(
      tap(goal => {
        const current = this.goalsSubject.value;
        this.goalsSubject.next([...current, goal]);
      })
    );
  }

  update(id: string, command: UpdateGoal): Observable<Goal> {
    return this.http.put<Goal>(`${this.apiUrl}/${id}`, command).pipe(
      tap(updatedGoal => {
        const current = this.goalsSubject.value;
        const index = current.findIndex(g => g.goalId === id);
        if (index !== -1) {
          current[index] = updatedGoal;
          this.goalsSubject.next([...current]);
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => {
        const current = this.goalsSubject.value;
        this.goalsSubject.next(current.filter(g => g.goalId !== id));
      })
    );
  }
}
