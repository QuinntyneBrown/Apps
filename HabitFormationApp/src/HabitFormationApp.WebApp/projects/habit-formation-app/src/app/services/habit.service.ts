import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { Habit, CreateHabitRequest, UpdateHabitRequest } from '../models';

@Injectable({
  providedIn: 'root'
})
export class HabitService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/habits`;

  private habitsSubject = new BehaviorSubject<Habit[]>([]);
  public habits$ = this.habitsSubject.asObservable();

  getHabits(userId?: string, isActive?: boolean): Observable<Habit[]> {
    let url = this.baseUrl;
    const params: string[] = [];

    if (userId) {
      params.push(`userId=${userId}`);
    }
    if (isActive !== undefined) {
      params.push(`isActive=${isActive}`);
    }

    if (params.length > 0) {
      url += `?${params.join('&')}`;
    }

    return this.http.get<Habit[]>(url).pipe(
      tap(habits => this.habitsSubject.next(habits))
    );
  }

  getHabitById(habitId: string): Observable<Habit> {
    return this.http.get<Habit>(`${this.baseUrl}/${habitId}`);
  }

  createHabit(request: CreateHabitRequest): Observable<Habit> {
    return this.http.post<Habit>(this.baseUrl, request).pipe(
      tap(habit => {
        const currentHabits = this.habitsSubject.value;
        this.habitsSubject.next([...currentHabits, habit]);
      })
    );
  }

  updateHabit(habitId: string, request: UpdateHabitRequest): Observable<Habit> {
    return this.http.put<Habit>(`${this.baseUrl}/${habitId}`, request).pipe(
      tap(updatedHabit => {
        const currentHabits = this.habitsSubject.value;
        const index = currentHabits.findIndex(h => h.habitId === habitId);
        if (index !== -1) {
          currentHabits[index] = updatedHabit;
          this.habitsSubject.next([...currentHabits]);
        }
      })
    );
  }

  deleteHabit(habitId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${habitId}`).pipe(
      tap(() => {
        const currentHabits = this.habitsSubject.value;
        this.habitsSubject.next(currentHabits.filter(h => h.habitId !== habitId));
      })
    );
  }

  toggleActive(habitId: string): Observable<Habit> {
    return this.http.put<Habit>(`${this.baseUrl}/${habitId}/toggle-active`, {}).pipe(
      tap(updatedHabit => {
        const currentHabits = this.habitsSubject.value;
        const index = currentHabits.findIndex(h => h.habitId === habitId);
        if (index !== -1) {
          currentHabits[index] = updatedHabit;
          this.habitsSubject.next([...currentHabits]);
        }
      })
    );
  }
}
