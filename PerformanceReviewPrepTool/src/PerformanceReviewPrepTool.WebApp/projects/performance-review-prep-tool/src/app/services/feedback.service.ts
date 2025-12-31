import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../environments';
import { Feedback, CreateFeedback, UpdateFeedback } from '../models';

@Injectable({
  providedIn: 'root'
})
export class FeedbackService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/feedbacks`;

  private feedbacksSubject = new BehaviorSubject<Feedback[]>([]);
  public feedbacks$ = this.feedbacksSubject.asObservable();

  getAll(): Observable<Feedback[]> {
    return this.http.get<Feedback[]>(this.baseUrl).pipe(
      tap(feedbacks => this.feedbacksSubject.next(feedbacks))
    );
  }

  getById(id: string): Observable<Feedback> {
    return this.http.get<Feedback>(`${this.baseUrl}/${id}`);
  }

  getByReviewPeriod(reviewPeriodId: string): Observable<Feedback[]> {
    return this.http.get<Feedback[]>(`${this.baseUrl}/review-period/${reviewPeriodId}`).pipe(
      tap(feedbacks => this.feedbacksSubject.next(feedbacks))
    );
  }

  create(feedback: CreateFeedback): Observable<Feedback> {
    return this.http.post<Feedback>(this.baseUrl, feedback).pipe(
      tap(newFeedback => {
        const current = this.feedbacksSubject.value;
        this.feedbacksSubject.next([...current, newFeedback]);
      })
    );
  }

  update(feedback: UpdateFeedback): Observable<Feedback> {
    return this.http.put<Feedback>(`${this.baseUrl}/${feedback.feedbackId}`, feedback).pipe(
      tap(updatedFeedback => {
        const current = this.feedbacksSubject.value;
        const index = current.findIndex(f => f.feedbackId === updatedFeedback.feedbackId);
        if (index !== -1) {
          current[index] = updatedFeedback;
          this.feedbacksSubject.next([...current]);
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const current = this.feedbacksSubject.value;
        this.feedbacksSubject.next(current.filter(f => f.feedbackId !== id));
      })
    );
  }
}
