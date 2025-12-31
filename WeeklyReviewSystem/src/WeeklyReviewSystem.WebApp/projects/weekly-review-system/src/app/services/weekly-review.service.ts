import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { WeeklyReview } from '../models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class WeeklyReviewService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;

  private _weeklyReviewsSubject = new BehaviorSubject<WeeklyReview[]>([]);
  public weeklyReviews$ = this._weeklyReviewsSubject.asObservable();

  private _currentWeeklyReviewSubject = new BehaviorSubject<WeeklyReview | null>(null);
  public currentWeeklyReview$ = this._currentWeeklyReviewSubject.asObservable();

  getAll(): Observable<WeeklyReview[]> {
    return this._http.get<WeeklyReview[]>(`${this._baseUrl}/api/weeklyreviews`).pipe(
      tap(reviews => this._weeklyReviewsSubject.next(reviews))
    );
  }

  getById(id: string): Observable<WeeklyReview> {
    return this._http.get<WeeklyReview>(`${this._baseUrl}/api/weeklyreviews/${id}`).pipe(
      tap(review => this._currentWeeklyReviewSubject.next(review))
    );
  }

  create(review: Partial<WeeklyReview>): Observable<WeeklyReview> {
    return this._http.post<WeeklyReview>(`${this._baseUrl}/api/weeklyreviews`, review).pipe(
      tap(newReview => {
        const current = this._weeklyReviewsSubject.value;
        this._weeklyReviewsSubject.next([...current, newReview]);
      })
    );
  }

  update(id: string, review: Partial<WeeklyReview>): Observable<void> {
    return this._http.put<void>(`${this._baseUrl}/api/weeklyreviews/${id}`, review).pipe(
      tap(() => {
        const current = this._weeklyReviewsSubject.value;
        const index = current.findIndex(r => r.weeklyReviewId === id);
        if (index !== -1) {
          current[index] = { ...current[index], ...review };
          this._weeklyReviewsSubject.next([...current]);
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/weeklyreviews/${id}`).pipe(
      tap(() => {
        const current = this._weeklyReviewsSubject.value;
        this._weeklyReviewsSubject.next(current.filter(r => r.weeklyReviewId !== id));
      })
    );
  }
}
