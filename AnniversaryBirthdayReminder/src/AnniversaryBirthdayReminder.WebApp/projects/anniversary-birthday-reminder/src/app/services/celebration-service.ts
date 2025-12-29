import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Celebration, CelebrationStatus } from '../models';
import { apiBaseUrl } from './api-config';

@Injectable({
  providedIn: 'root'
})
export class CelebrationService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = apiBaseUrl;
  private readonly celebrationsSubject = new BehaviorSubject<Celebration[]>([]);

  celebrations$ = this.celebrationsSubject.asObservable();

  getCelebrations(): Observable<Celebration[]> {
    return this.http.get<Celebration[]>(`${this.baseUrl}/api/celebrations`).pipe(
      tap(celebrations => this.celebrationsSubject.next(celebrations))
    );
  }

  getCelebrationsByDate(dateId: string): Observable<Celebration[]> {
    return this.http.get<Celebration[]>(`${this.baseUrl}/api/dates/${dateId}/celebrations`);
  }

  getCelebration(celebrationId: string): Observable<Celebration> {
    return this.http.get<Celebration>(`${this.baseUrl}/api/celebrations/${celebrationId}`);
  }

  markAsCompleted(dateId: string, celebration: Partial<Celebration>): Observable<Celebration> {
    return this.http.post<Celebration>(`${this.baseUrl}/api/dates/${dateId}/celebrations`, {
      ...celebration,
      status: CelebrationStatus.Completed
    }).pipe(
      tap(newCelebration => {
        const current = this.celebrationsSubject.value;
        this.celebrationsSubject.next([...current, newCelebration]);
      })
    );
  }

  markAsSkipped(dateId: string, notes?: string): Observable<Celebration> {
    return this.http.post<Celebration>(`${this.baseUrl}/api/dates/${dateId}/celebrations`, {
      notes,
      status: CelebrationStatus.Skipped
    }).pipe(
      tap(newCelebration => {
        const current = this.celebrationsSubject.value;
        this.celebrationsSubject.next([...current, newCelebration]);
      })
    );
  }

  updateCelebration(celebrationId: string, celebration: Partial<Celebration>): Observable<Celebration> {
    return this.http.put<Celebration>(`${this.baseUrl}/api/celebrations/${celebrationId}`, celebration).pipe(
      tap(updatedCelebration => {
        const current = this.celebrationsSubject.value;
        const index = current.findIndex(c => c.celebrationId === celebrationId);
        if (index >= 0) {
          const updated = [...current];
          updated[index] = updatedCelebration;
          this.celebrationsSubject.next(updated);
        }
      })
    );
  }

  addPhotos(celebrationId: string, photos: string[]): Observable<Celebration> {
    return this.http.post<Celebration>(`${this.baseUrl}/api/celebrations/${celebrationId}/photos`, { photos });
  }

  getRatingStars(rating: number): string {
    return '★'.repeat(rating) + '☆'.repeat(5 - rating);
  }
}
