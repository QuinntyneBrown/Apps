import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments';
import { SessionAnalytics, GenerateAnalyticsCommand } from '../models';

@Injectable({
  providedIn: 'root'
})
export class SessionAnalyticsService {
  private readonly apiUrl = `${environment.baseUrl}/api/SessionAnalytics`;
  private analyticsSubject = new BehaviorSubject<SessionAnalytics[]>([]);
  public analytics$ = this.analyticsSubject.asObservable();

  constructor(private http: HttpClient) {}

  getAnalytics(userId?: string): Observable<SessionAnalytics[]> {
    let params = new HttpParams();
    if (userId) params = params.set('userId', userId);

    return this.http.get<SessionAnalytics[]>(this.apiUrl, { params }).pipe(
      tap(analytics => this.analyticsSubject.next(analytics))
    );
  }

  generateAnalytics(command: GenerateAnalyticsCommand): Observable<SessionAnalytics> {
    return this.http.post<SessionAnalytics>(`${this.apiUrl}/generate`, command).pipe(
      tap(analytics => {
        const analyticsArray = this.analyticsSubject.value;
        this.analyticsSubject.next([...analyticsArray, analytics]);
      })
    );
  }
}
