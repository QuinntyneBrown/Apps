import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { Exercise } from '../models';

@Injectable({
  providedIn: 'root'
})
export class ExerciseService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/exercises`;

  private exercisesSubject = new BehaviorSubject<Exercise[]>([]);
  public exercises$ = this.exercisesSubject.asObservable();

  getAll(): Observable<Exercise[]> {
    return this.http.get<Exercise[]>(this.baseUrl).pipe(
      tap(exercises => this.exercisesSubject.next(exercises))
    );
  }

  getById(id: string): Observable<Exercise> {
    return this.http.get<Exercise>(`${this.baseUrl}/${id}`);
  }

  create(exercise: Partial<Exercise>): Observable<Exercise> {
    return this.http.post<Exercise>(this.baseUrl, exercise).pipe(
      tap(newExercise => {
        const currentExercises = this.exercisesSubject.value;
        this.exercisesSubject.next([...currentExercises, newExercise]);
      })
    );
  }

  update(id: string, exercise: Partial<Exercise>): Observable<Exercise> {
    return this.http.put<Exercise>(`${this.baseUrl}/${id}`, exercise).pipe(
      tap(updatedExercise => {
        const currentExercises = this.exercisesSubject.value;
        const index = currentExercises.findIndex(e => e.exerciseId === id);
        if (index !== -1) {
          currentExercises[index] = updatedExercise;
          this.exercisesSubject.next([...currentExercises]);
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const currentExercises = this.exercisesSubject.value;
        this.exercisesSubject.next(currentExercises.filter(e => e.exerciseId !== id));
      })
    );
  }
}
