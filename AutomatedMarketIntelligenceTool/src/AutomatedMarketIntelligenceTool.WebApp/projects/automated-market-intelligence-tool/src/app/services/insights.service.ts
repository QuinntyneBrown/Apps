import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Insight, CreateInsightRequest, UpdateInsightRequest } from '../models';
import { environment } from '../environments/environment';

@Injectable({ providedIn: 'root' })
export class InsightsService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/insights`;

  private readonly insightsSubject = new BehaviorSubject<Insight[]>([]);
  insights$ = this.insightsSubject.asObservable();

  loadInsights(): Observable<Insight[]> {
    return this.http.get<Insight[]>(this.baseUrl).pipe(
      tap(insights => this.insightsSubject.next(insights))
    );
  }

  getInsightById(id: string): Observable<Insight> {
    return this.http.get<Insight>(`${this.baseUrl}/${id}`);
  }

  createInsight(request: CreateInsightRequest): Observable<Insight> {
    return this.http.post<Insight>(this.baseUrl, request).pipe(
      tap(insight => {
        const current = this.insightsSubject.value;
        this.insightsSubject.next([...current, insight]);
      })
    );
  }

  updateInsight(request: UpdateInsightRequest): Observable<Insight> {
    return this.http.put<Insight>(`${this.baseUrl}/${request.insightId}`, request).pipe(
      tap(updated => {
        const current = this.insightsSubject.value;
        const index = current.findIndex(i => i.insightId === updated.insightId);
        if (index !== -1) {
          current[index] = updated;
          this.insightsSubject.next([...current]);
        }
      })
    );
  }

  deleteInsight(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const current = this.insightsSubject.value;
        this.insightsSubject.next(current.filter(i => i.insightId !== id));
      })
    );
  }
}
