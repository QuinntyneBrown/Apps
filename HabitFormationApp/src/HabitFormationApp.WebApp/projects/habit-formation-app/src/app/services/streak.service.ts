import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { Streak } from '../models';

@Injectable({
  providedIn: 'root'
})
export class StreakService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/streaks`;

  private streaksSubject = new BehaviorSubject<Streak[]>([]);
  public streaks$ = this.streaksSubject.asObservable();

  getStreaks(): Observable<Streak[]> {
    return this.http.get<Streak[]>(this.baseUrl).pipe(
      tap(streaks => this.streaksSubject.next(streaks))
    );
  }

  getStreakByHabitId(habitId: string): Observable<Streak> {
    return this.http.get<Streak>(`${this.baseUrl}/habit/${habitId}`);
  }

  incrementStreak(habitId: string): Observable<Streak> {
    return this.http.post<Streak>(`${this.baseUrl}/increment`, { habitId }).pipe(
      tap(updatedStreak => {
        const currentStreaks = this.streaksSubject.value;
        const index = currentStreaks.findIndex(s => s.habitId === habitId);
        if (index !== -1) {
          currentStreaks[index] = updatedStreak;
          this.streaksSubject.next([...currentStreaks]);
        } else {
          this.streaksSubject.next([...currentStreaks, updatedStreak]);
        }
      })
    );
  }
}
