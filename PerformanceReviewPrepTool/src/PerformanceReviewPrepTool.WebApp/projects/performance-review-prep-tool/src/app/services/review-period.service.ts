import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../environments';
import { ReviewPeriod, CreateReviewPeriod, UpdateReviewPeriod } from '../models';

@Injectable({
  providedIn: 'root'
})
export class ReviewPeriodService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/review-periods`;

  private reviewPeriodsSubject = new BehaviorSubject<ReviewPeriod[]>([]);
  public reviewPeriods$ = this.reviewPeriodsSubject.asObservable();

  getAll(): Observable<ReviewPeriod[]> {
    return this.http.get<ReviewPeriod[]>(this.baseUrl).pipe(
      tap(reviewPeriods => this.reviewPeriodsSubject.next(reviewPeriods))
    );
  }

  getById(id: string): Observable<ReviewPeriod> {
    return this.http.get<ReviewPeriod>(`${this.baseUrl}/${id}`);
  }

  create(reviewPeriod: CreateReviewPeriod): Observable<ReviewPeriod> {
    return this.http.post<ReviewPeriod>(this.baseUrl, reviewPeriod).pipe(
      tap(newReviewPeriod => {
        const current = this.reviewPeriodsSubject.value;
        this.reviewPeriodsSubject.next([...current, newReviewPeriod]);
      })
    );
  }

  update(reviewPeriod: UpdateReviewPeriod): Observable<ReviewPeriod> {
    return this.http.put<ReviewPeriod>(`${this.baseUrl}/${reviewPeriod.reviewPeriodId}`, reviewPeriod).pipe(
      tap(updatedReviewPeriod => {
        const current = this.reviewPeriodsSubject.value;
        const index = current.findIndex(rp => rp.reviewPeriodId === updatedReviewPeriod.reviewPeriodId);
        if (index !== -1) {
          current[index] = updatedReviewPeriod;
          this.reviewPeriodsSubject.next([...current]);
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const current = this.reviewPeriodsSubject.value;
        this.reviewPeriodsSubject.next(current.filter(rp => rp.reviewPeriodId !== id));
      })
    );
  }
}
