import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { Trend, CalculateTrendRequest } from '../models';

@Injectable({
  providedIn: 'root'
})
export class TrendService {
  private baseUrl = `${environment.baseUrl}/api`;

  private trendsSubject = new BehaviorSubject<Trend[]>([]);
  public trends$ = this.trendsSubject.asObservable();

  private loadingSubject = new BehaviorSubject<boolean>(false);
  public loading$ = this.loadingSubject.asObservable();

  private errorSubject = new BehaviorSubject<string | null>(null);
  public error$ = this.errorSubject.asObservable();

  constructor(private http: HttpClient) {}

  getTrendById(id: string): Observable<Trend> {
    return this.http.get<Trend>(`${this.baseUrl}/trends/${id}`);
  }

  getTrendsByUserId(userId: string): Observable<Trend[]> {
    this.loadingSubject.next(true);
    this.errorSubject.next(null);

    const params = new HttpParams().set('userId', userId);

    return this.http.get<Trend[]>(`${this.baseUrl}/trends`, { params }).pipe(
      tap({
        next: (trends) => {
          this.trendsSubject.next(trends);
          this.loadingSubject.next(false);
        },
        error: (error) => {
          this.errorSubject.next(error.message);
          this.loadingSubject.next(false);
        }
      })
    );
  }

  calculateTrend(request: CalculateTrendRequest): Observable<Trend> {
    this.loadingSubject.next(true);
    this.errorSubject.next(null);

    return this.http.post<Trend>(`${this.baseUrl}/trends/calculate`, request).pipe(
      tap({
        next: (newTrend) => {
          const currentTrends = this.trendsSubject.value;
          this.trendsSubject.next([newTrend, ...currentTrends]);
          this.loadingSubject.next(false);
        },
        error: (error) => {
          this.errorSubject.next(error.message);
          this.loadingSubject.next(false);
        }
      })
    );
  }

  deleteTrend(id: string): Observable<void> {
    this.loadingSubject.next(true);
    this.errorSubject.next(null);

    return this.http.delete<void>(`${this.baseUrl}/trends/${id}`).pipe(
      tap({
        next: () => {
          const currentTrends = this.trendsSubject.value;
          this.trendsSubject.next(currentTrends.filter(t => t.trendId !== id));
          this.loadingSubject.next(false);
        },
        error: (error) => {
          this.errorSubject.next(error.message);
          this.loadingSubject.next(false);
        }
      })
    );
  }
}
