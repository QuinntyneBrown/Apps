import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { HealthTrend, CreateHealthTrend, UpdateHealthTrend } from '../models';
import { environment } from '../environments';

@Injectable({
  providedIn: 'root'
})
export class HealthTrendService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/health-trends`;

  private healthTrendsSubject = new BehaviorSubject<HealthTrend[]>([]);
  public healthTrends$ = this.healthTrendsSubject.asObservable();

  private selectedHealthTrendSubject = new BehaviorSubject<HealthTrend | null>(null);
  public selectedHealthTrend$ = this.selectedHealthTrendSubject.asObservable();

  getAll(): Observable<HealthTrend[]> {
    return this.http.get<HealthTrend[]>(this.baseUrl).pipe(
      tap(healthTrends => this.healthTrendsSubject.next(healthTrends))
    );
  }

  getById(id: string): Observable<HealthTrend> {
    return this.http.get<HealthTrend>(`${this.baseUrl}/${id}`).pipe(
      tap(healthTrend => this.selectedHealthTrendSubject.next(healthTrend))
    );
  }

  create(healthTrend: CreateHealthTrend): Observable<HealthTrend> {
    return this.http.post<HealthTrend>(this.baseUrl, healthTrend).pipe(
      tap(newHealthTrend => {
        const current = this.healthTrendsSubject.value;
        this.healthTrendsSubject.next([...current, newHealthTrend]);
      })
    );
  }

  update(healthTrend: UpdateHealthTrend): Observable<HealthTrend> {
    return this.http.put<HealthTrend>(`${this.baseUrl}/${healthTrend.healthTrendId}`, healthTrend).pipe(
      tap(updatedHealthTrend => {
        const current = this.healthTrendsSubject.value;
        const index = current.findIndex(ht => ht.healthTrendId === updatedHealthTrend.healthTrendId);
        if (index !== -1) {
          current[index] = updatedHealthTrend;
          this.healthTrendsSubject.next([...current]);
        }
        this.selectedHealthTrendSubject.next(updatedHealthTrend);
      })
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const current = this.healthTrendsSubject.value;
        this.healthTrendsSubject.next(current.filter(ht => ht.healthTrendId !== id));
        if (this.selectedHealthTrendSubject.value?.healthTrendId === id) {
          this.selectedHealthTrendSubject.next(null);
        }
      })
    );
  }

  clearSelected(): void {
    this.selectedHealthTrendSubject.next(null);
  }
}
