import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments';
import { RecoveryExercise, CreateRecoveryExercise, UpdateRecoveryExercise } from '../models';

@Injectable({ providedIn: 'root' })
export class RecoveryExerciseService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;

  private _exercisesSubject = new BehaviorSubject<RecoveryExercise[]>([]);
  exercises$ = this._exercisesSubject.asObservable();

  getAll(injuryId?: string): Observable<RecoveryExercise[]> {
    let params = new HttpParams();
    if (injuryId) params = params.set('injuryId', injuryId);

    return this._http.get<RecoveryExercise[]>(`${this._baseUrl}/api/RecoveryExercises`, { params }).pipe(
      tap(exercises => this._exercisesSubject.next(exercises))
    );
  }

  getById(id: string): Observable<RecoveryExercise> {
    return this._http.get<RecoveryExercise>(`${this._baseUrl}/api/RecoveryExercises/${id}`);
  }

  create(exercise: CreateRecoveryExercise): Observable<RecoveryExercise> {
    return this._http.post<RecoveryExercise>(`${this._baseUrl}/api/RecoveryExercises`, exercise).pipe(
      tap(created => {
        const current = this._exercisesSubject.value;
        this._exercisesSubject.next([...current, created]);
      })
    );
  }

  update(id: string, exercise: UpdateRecoveryExercise): Observable<RecoveryExercise> {
    return this._http.put<RecoveryExercise>(`${this._baseUrl}/api/RecoveryExercises/${id}`, exercise).pipe(
      tap(updated => {
        const current = this._exercisesSubject.value;
        const index = current.findIndex(e => e.recoveryExerciseId === id);
        if (index !== -1) {
          current[index] = updated;
          this._exercisesSubject.next([...current]);
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/RecoveryExercises/${id}`).pipe(
      tap(() => {
        const current = this._exercisesSubject.value;
        this._exercisesSubject.next(current.filter(e => e.recoveryExerciseId !== id));
      })
    );
  }
}
