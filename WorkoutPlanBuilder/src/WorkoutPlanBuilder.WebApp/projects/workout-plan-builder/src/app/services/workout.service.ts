import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { Workout } from '../models';

@Injectable({
  providedIn: 'root'
})
export class WorkoutService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/workouts`;

  private workoutsSubject = new BehaviorSubject<Workout[]>([]);
  public workouts$ = this.workoutsSubject.asObservable();

  getAll(): Observable<Workout[]> {
    return this.http.get<Workout[]>(this.baseUrl).pipe(
      tap(workouts => this.workoutsSubject.next(workouts))
    );
  }

  getById(id: string): Observable<Workout> {
    return this.http.get<Workout>(`${this.baseUrl}/${id}`);
  }

  create(workout: Partial<Workout>): Observable<Workout> {
    return this.http.post<Workout>(this.baseUrl, workout).pipe(
      tap(newWorkout => {
        const currentWorkouts = this.workoutsSubject.value;
        this.workoutsSubject.next([...currentWorkouts, newWorkout]);
      })
    );
  }

  update(id: string, workout: Partial<Workout>): Observable<Workout> {
    return this.http.put<Workout>(`${this.baseUrl}/${id}`, workout).pipe(
      tap(updatedWorkout => {
        const currentWorkouts = this.workoutsSubject.value;
        const index = currentWorkouts.findIndex(w => w.workoutId === id);
        if (index !== -1) {
          currentWorkouts[index] = updatedWorkout;
          this.workoutsSubject.next([...currentWorkouts]);
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const currentWorkouts = this.workoutsSubject.value;
        this.workoutsSubject.next(currentWorkouts.filter(w => w.workoutId !== id));
      })
    );
  }
}
