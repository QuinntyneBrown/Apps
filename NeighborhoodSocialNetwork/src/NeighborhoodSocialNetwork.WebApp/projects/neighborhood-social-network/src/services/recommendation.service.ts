import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, tap } from 'rxjs';
import { environment } from '../environments';
import { Recommendation, CreateRecommendation, UpdateRecommendation } from '../models';

@Injectable({
  providedIn: 'root'
})
export class RecommendationService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/recommendations`;

  private recommendationsSubject = new BehaviorSubject<Recommendation[]>([]);
  recommendations$ = this.recommendationsSubject.asObservable();

  private selectedRecommendationSubject = new BehaviorSubject<Recommendation | null>(null);
  selectedRecommendation$ = this.selectedRecommendationSubject.asObservable();

  getAll() {
    return this.http.get<Recommendation[]>(this.baseUrl).pipe(
      tap(recommendations => this.recommendationsSubject.next(recommendations))
    );
  }

  getById(id: string) {
    return this.http.get<Recommendation>(`${this.baseUrl}/${id}`).pipe(
      tap(recommendation => this.selectedRecommendationSubject.next(recommendation))
    );
  }

  create(recommendation: CreateRecommendation) {
    return this.http.post<Recommendation>(this.baseUrl, recommendation).pipe(
      tap(newRecommendation => {
        const recommendations = this.recommendationsSubject.value;
        this.recommendationsSubject.next([...recommendations, newRecommendation]);
      })
    );
  }

  update(recommendation: UpdateRecommendation) {
    return this.http.put<Recommendation>(`${this.baseUrl}/${recommendation.recommendationId}`, recommendation).pipe(
      tap(updatedRecommendation => {
        const recommendations = this.recommendationsSubject.value.map(r =>
          r.recommendationId === updatedRecommendation.recommendationId ? updatedRecommendation : r
        );
        this.recommendationsSubject.next(recommendations);
        this.selectedRecommendationSubject.next(updatedRecommendation);
      })
    );
  }

  delete(id: string) {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const recommendations = this.recommendationsSubject.value.filter(r => r.recommendationId !== id);
        this.recommendationsSubject.next(recommendations);
        if (this.selectedRecommendationSubject.value?.recommendationId === id) {
          this.selectedRecommendationSubject.next(null);
        }
      })
    );
  }
}
