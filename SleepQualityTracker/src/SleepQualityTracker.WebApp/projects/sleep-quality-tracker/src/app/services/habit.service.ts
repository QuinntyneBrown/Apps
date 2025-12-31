import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { Habit, CreateHabitRequest, UpdateHabitRequest } from '../models';

@Injectable({
  providedIn: 'root'
})
export class HabitService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/habits`;

  private habitsSubject = new BehaviorSubject<Habit[]>([]);
  public habits$ = this.habitsSubject.asObservable();

  getHabits(
    userId?: string,
    habitType?: string,
    isPositive?: boolean,
    isActive?: boolean,
    isHighImpact?: boolean
  ): Observable<Habit[]> {
    let params = new HttpParams();
    if (userId) params = params.set('userId', userId);
    if (habitType) params = params.set('habitType', habitType);
    if (isPositive !== undefined) params = params.set('isPositive', isPositive.toString());
    if (isActive !== undefined) params = params.set('isActive', isActive.toString());
    if (isHighImpact !== undefined) params = params.set('isHighImpact', isHighImpact.toString());

    return this.http.get<Habit[]>(this.baseUrl, { params }).pipe(
      tap(habits => this.habitsSubject.next(habits))
    );
  }

  getHabitById(habitId: string): Observable<Habit> {
    return this.http.get<Habit>(`${this.baseUrl}/${habitId}`);
  }

  createHabit(request: CreateHabitRequest): Observable<Habit> {
    return this.http.post<Habit>(this.baseUrl, request).pipe(
      tap(habit => {
        const current = this.habitsSubject.value;
        this.habitsSubject.next([...current, habit]);
      })
    );
  }

  updateHabit(request: UpdateHabitRequest): Observable<Habit> {
    return this.http.put<Habit>(`${this.baseUrl}/${request.habitId}`, request).pipe(
      tap(habit => {
        const current = this.habitsSubject.value;
        const index = current.findIndex(h => h.habitId === habit.habitId);
        if (index !== -1) {
          current[index] = habit;
          this.habitsSubject.next([...current]);
        }
      })
    );
  }

  deleteHabit(habitId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${habitId}`).pipe(
      tap(() => {
        const current = this.habitsSubject.value;
        this.habitsSubject.next(current.filter(h => h.habitId !== habitId));
      })
    );
  }
}
