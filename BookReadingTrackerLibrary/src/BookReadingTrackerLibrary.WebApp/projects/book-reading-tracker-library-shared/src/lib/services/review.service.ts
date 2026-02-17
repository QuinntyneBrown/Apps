import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Review } from '../models';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ReviewService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/reviews`;

  private reviewsSubject = new BehaviorSubject<Review[]>([]);
  public reviews$ = this.reviewsSubject.asObservable();

  getReviews(userId?: string, bookId?: string): Observable<Review[]> {
    let url = this.baseUrl;
    const params: string[] = [];

    if (userId) params.push(`userId=${userId}`);
    if (bookId) params.push(`bookId=${bookId}`);

    if (params.length > 0) {
      url += `?${params.join('&')}`;
    }

    return this.http.get<Review[]>(url).pipe(
      tap(reviews => this.reviewsSubject.next(reviews))
    );
  }

  getReviewById(reviewId: string): Observable<Review> {
    return this.http.get<Review>(`${this.baseUrl}/${reviewId}`);
  }

  createReview(review: Partial<Review>): Observable<Review> {
    return this.http.post<Review>(this.baseUrl, review).pipe(
      tap(newReview => {
        const currentReviews = this.reviewsSubject.value;
        this.reviewsSubject.next([...currentReviews, newReview]);
      })
    );
  }

  updateReview(reviewId: string, review: Partial<Review>): Observable<Review> {
    return this.http.put<Review>(`${this.baseUrl}/${reviewId}`, { ...review, reviewId }).pipe(
      tap(updatedReview => {
        const currentReviews = this.reviewsSubject.value;
        const index = currentReviews.findIndex(r => r.reviewId === reviewId);
        if (index !== -1) {
          const newReviews = [...currentReviews];
          newReviews[index] = updatedReview;
          this.reviewsSubject.next(newReviews);
        }
      })
    );
  }

  deleteReview(reviewId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${reviewId}`).pipe(
      tap(() => {
        const currentReviews = this.reviewsSubject.value;
        this.reviewsSubject.next(currentReviews.filter(r => r.reviewId !== reviewId));
      })
    );
  }
}
