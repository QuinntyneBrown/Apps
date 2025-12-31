import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments';
import { Achievement, CreateAchievement, UpdateAchievement } from '../models';

@Injectable({
  providedIn: 'root'
})
export class AchievementService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/achievements`;

  private achievementsSubject = new BehaviorSubject<Achievement[]>([]);
  public achievements$ = this.achievementsSubject.asObservable();

  private loadingSubject = new BehaviorSubject<boolean>(false);
  public loading$ = this.loadingSubject.asObservable();

  getAchievements(): Observable<Achievement[]> {
    this.loadingSubject.next(true);
    return this.http.get<Achievement[]>(this.baseUrl).pipe(
      tap(achievements => {
        this.achievementsSubject.next(achievements);
        this.loadingSubject.next(false);
      })
    );
  }

  getAchievementById(id: string): Observable<Achievement> {
    return this.http.get<Achievement>(`${this.baseUrl}/${id}`);
  }

  createAchievement(achievement: CreateAchievement): Observable<Achievement> {
    return this.http.post<Achievement>(this.baseUrl, achievement).pipe(
      tap(newAchievement => {
        const currentAchievements = this.achievementsSubject.value;
        this.achievementsSubject.next([...currentAchievements, newAchievement]);
      })
    );
  }

  updateAchievement(achievement: UpdateAchievement): Observable<Achievement> {
    return this.http.put<Achievement>(`${this.baseUrl}/${achievement.achievementId}`, achievement).pipe(
      tap(updatedAchievement => {
        const currentAchievements = this.achievementsSubject.value;
        const index = currentAchievements.findIndex(a => a.achievementId === updatedAchievement.achievementId);
        if (index !== -1) {
          currentAchievements[index] = updatedAchievement;
          this.achievementsSubject.next([...currentAchievements]);
        }
      })
    );
  }

  deleteAchievement(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const currentAchievements = this.achievementsSubject.value;
        this.achievementsSubject.next(currentAchievements.filter(a => a.achievementId !== id));
      })
    );
  }

  toggleFeatured(id: string): Observable<Achievement> {
    return this.http.patch<Achievement>(`${this.baseUrl}/${id}/toggle-featured`, {}).pipe(
      tap(updatedAchievement => {
        const currentAchievements = this.achievementsSubject.value;
        const index = currentAchievements.findIndex(a => a.achievementId === updatedAchievement.achievementId);
        if (index !== -1) {
          currentAchievements[index] = updatedAchievement;
          this.achievementsSubject.next([...currentAchievements]);
        }
      })
    );
  }
}
