import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { Goal, CreateGoalRequest, UpdateGoalRequest } from '../models';

@Injectable({
  providedIn: 'root'
})
export class GoalService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;
  private _goalsSubject = new BehaviorSubject<Goal[]>([]);

  goals$ = this._goalsSubject.asObservable();

  getAll(): Observable<Goal[]> {
    return this._http.get<Goal[]>(`${this._baseUrl}/api/goals`).pipe(
      tap(goals => this._goalsSubject.next(goals))
    );
  }

  getById(id: string): Observable<Goal> {
    return this._http.get<Goal>(`${this._baseUrl}/api/goals/${id}`);
  }

  create(request: CreateGoalRequest): Observable<Goal> {
    return this._http.post<Goal>(`${this._baseUrl}/api/goals`, request).pipe(
      tap(goal => {
        const current = this._goalsSubject.value;
        this._goalsSubject.next([...current, goal]);
      })
    );
  }

  update(id: string, request: UpdateGoalRequest): Observable<void> {
    return this._http.put<void>(`${this._baseUrl}/api/goals/${id}`, request).pipe(
      tap(() => {
        const current = this._goalsSubject.value;
        const index = current.findIndex(g => g.goalId === id);
        if (index !== -1) {
          this.getAll().subscribe();
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/goals/${id}`).pipe(
      tap(() => {
        const current = this._goalsSubject.value;
        this._goalsSubject.next(current.filter(g => g.goalId !== id));
      })
    );
  }
}
