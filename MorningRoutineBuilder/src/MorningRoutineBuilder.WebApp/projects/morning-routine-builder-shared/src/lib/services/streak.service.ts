import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Streak, CreateStreakRequest, UpdateStreakRequest } from '../models';
import { environment } from '../environments';

@Injectable({
  providedIn: 'root'
})
export class StreakService {
  private http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/streaks`;

  private streaksSubject = new BehaviorSubject<Streak[]>([]);
  public streaks$ = this.streaksSubject.asObservable();

  getAll(): Observable<Streak[]> {
    return this.http.get<Streak[]>(this.baseUrl).pipe(
      tap(streaks => this.streaksSubject.next(streaks))
    );
  }

  getById(id: string): Observable<Streak> {
    return this.http.get<Streak>(`${this.baseUrl}/${id}`);
  }

  getByRoutineId(routineId: string): Observable<Streak> {
    return this.http.get<Streak>(`${this.baseUrl}/routine/${routineId}`);
  }

  create(request: CreateStreakRequest): Observable<Streak> {
    return this.http.post<Streak>(this.baseUrl, request).pipe(
      tap(streak => {
        const current = this.streaksSubject.value;
        this.streaksSubject.next([...current, streak]);
      })
    );
  }

  update(id: string, request: UpdateStreakRequest): Observable<Streak> {
    return this.http.put<Streak>(`${this.baseUrl}/${id}`, request).pipe(
      tap(updated => {
        const current = this.streaksSubject.value;
        const index = current.findIndex(s => s.streakId === id);
        if (index !== -1) {
          current[index] = updated;
          this.streaksSubject.next([...current]);
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const current = this.streaksSubject.value;
        this.streaksSubject.next(current.filter(s => s.streakId !== id));
      })
    );
  }
}
