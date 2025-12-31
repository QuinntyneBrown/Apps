import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Review, CreateReviewRequest, UpdateReviewRequest } from '../models';
import { environment } from '../environments';

@Injectable({
  providedIn: 'root'
})
export class ReviewService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = `${environment.baseUrl}/api/reviews`;

  private reviewsSubject = new BehaviorSubject<Review[]>([]);
  public reviews$ = this.reviewsSubject.asObservable();

  getAll(): Observable<Review[]> {
    return this.http.get<Review[]>(this.apiUrl).pipe(
      tap(reviews => this.reviewsSubject.next(reviews))
    );
  }

  getById(id: string): Observable<Review> {
    return this.http.get<Review>(`${this.apiUrl}/${id}`);
  }

  getByContent(contentId: string): Observable<Review[]> {
    return this.http.get<Review[]>(`${this.apiUrl}/content/${contentId}`);
  }

  create(request: CreateReviewRequest): Observable<Review> {
    return this.http.post<Review>(this.apiUrl, request).pipe(
      tap(() => this.refreshReviews())
    );
  }

  update(id: string, request: UpdateReviewRequest): Observable<Review> {
    return this.http.put<Review>(`${this.apiUrl}/${id}`, request).pipe(
      tap(() => this.refreshReviews())
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => this.refreshReviews())
    );
  }

  private refreshReviews(): void {
    this.getAll().subscribe();
  }
}
