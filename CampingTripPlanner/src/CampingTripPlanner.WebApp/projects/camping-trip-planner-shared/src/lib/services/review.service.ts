import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { Review, CreateReview, UpdateReview } from '../models';

@Injectable({
  providedIn: 'root'
})
export class ReviewService {
  private baseUrl = `${environment.baseUrl}/api/reviews`;
  private reviewsSubject = new BehaviorSubject<Review[]>([]);
  public reviews$ = this.reviewsSubject.asObservable();

  private selectedReviewSubject = new BehaviorSubject<Review | null>(null);
  public selectedReview$ = this.selectedReviewSubject.asObservable();

  constructor(private http: HttpClient) {}

  getReviews(userId?: string, campsiteId?: string): Observable<Review[]> {
    let params = new HttpParams();
    if (userId) params = params.set('userId', userId);
    if (campsiteId) params = params.set('campsiteId', campsiteId);

    return this.http.get<Review[]>(this.baseUrl, { params }).pipe(
      tap(reviews => this.reviewsSubject.next(reviews))
    );
  }

  getReviewById(reviewId: string): Observable<Review> {
    return this.http.get<Review>(`${this.baseUrl}/${reviewId}`).pipe(
      tap(review => this.selectedReviewSubject.next(review))
    );
  }

  createReview(review: CreateReview): Observable<Review> {
    return this.http.post<Review>(this.baseUrl, review).pipe(
      tap(newReview => {
        const current = this.reviewsSubject.value;
        this.reviewsSubject.next([...current, newReview]);
      })
    );
  }

  updateReview(reviewId: string, review: UpdateReview): Observable<Review> {
    return this.http.put<Review>(`${this.baseUrl}/${reviewId}`, review).pipe(
      tap(updatedReview => {
        const current = this.reviewsSubject.value;
        const index = current.findIndex(r => r.reviewId === reviewId);
        if (index !== -1) {
          current[index] = updatedReview;
          this.reviewsSubject.next([...current]);
        }
      })
    );
  }

  deleteReview(reviewId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${reviewId}`).pipe(
      tap(() => {
        const current = this.reviewsSubject.value;
        this.reviewsSubject.next(current.filter(r => r.reviewId !== reviewId));
      })
    );
  }

  clearSelectedReview(): void {
    this.selectedReviewSubject.next(null);
  }
}
