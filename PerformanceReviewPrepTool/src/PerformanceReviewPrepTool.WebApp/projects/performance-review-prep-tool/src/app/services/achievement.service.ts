import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../environments';
import { Achievement, CreateAchievement, UpdateAchievement } from '../models';

@Injectable({
  providedIn: 'root'
})
export class AchievementService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/achievements`;

  private achievementsSubject = new BehaviorSubject<Achievement[]>([]);
  public achievements$ = this.achievementsSubject.asObservable();

  getAll(): Observable<Achievement[]> {
    return this.http.get<Achievement[]>(this.baseUrl).pipe(
      tap(achievements => this.achievementsSubject.next(achievements))
    );
  }

  getById(id: string): Observable<Achievement> {
    return this.http.get<Achievement>(`${this.baseUrl}/${id}`);
  }

  getByReviewPeriod(reviewPeriodId: string): Observable<Achievement[]> {
    return this.http.get<Achievement[]>(`${this.baseUrl}/review-period/${reviewPeriodId}`).pipe(
      tap(achievements => this.achievementsSubject.next(achievements))
    );
  }

  create(achievement: CreateAchievement): Observable<Achievement> {
    return this.http.post<Achievement>(this.baseUrl, achievement).pipe(
      tap(newAchievement => {
        const current = this.achievementsSubject.value;
        this.achievementsSubject.next([...current, newAchievement]);
      })
    );
  }

  update(achievement: UpdateAchievement): Observable<Achievement> {
    return this.http.put<Achievement>(`${this.baseUrl}/${achievement.achievementId}`, achievement).pipe(
      tap(updatedAchievement => {
        const current = this.achievementsSubject.value;
        const index = current.findIndex(a => a.achievementId === updatedAchievement.achievementId);
        if (index !== -1) {
          current[index] = updatedAchievement;
          this.achievementsSubject.next([...current]);
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const current = this.achievementsSubject.value;
        this.achievementsSubject.next(current.filter(a => a.achievementId !== id));
      })
    );
  }
}
