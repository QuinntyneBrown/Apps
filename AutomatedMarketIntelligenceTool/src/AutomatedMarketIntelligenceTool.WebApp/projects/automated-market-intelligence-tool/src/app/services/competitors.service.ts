import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Competitor, CreateCompetitorRequest, UpdateCompetitorRequest } from '../models';
import { environment } from '../environments/environment';

@Injectable({ providedIn: 'root' })
export class CompetitorsService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/competitors`;

  private readonly competitorsSubject = new BehaviorSubject<Competitor[]>([]);
  competitors$ = this.competitorsSubject.asObservable();

  loadCompetitors(): Observable<Competitor[]> {
    return this.http.get<Competitor[]>(this.baseUrl).pipe(
      tap(competitors => this.competitorsSubject.next(competitors))
    );
  }

  getCompetitorById(id: string): Observable<Competitor> {
    return this.http.get<Competitor>(`${this.baseUrl}/${id}`);
  }

  createCompetitor(request: CreateCompetitorRequest): Observable<Competitor> {
    return this.http.post<Competitor>(this.baseUrl, request).pipe(
      tap(competitor => {
        const current = this.competitorsSubject.value;
        this.competitorsSubject.next([...current, competitor]);
      })
    );
  }

  updateCompetitor(request: UpdateCompetitorRequest): Observable<Competitor> {
    return this.http.put<Competitor>(`${this.baseUrl}/${request.competitorId}`, request).pipe(
      tap(updated => {
        const current = this.competitorsSubject.value;
        const index = current.findIndex(c => c.competitorId === updated.competitorId);
        if (index !== -1) {
          current[index] = updated;
          this.competitorsSubject.next([...current]);
        }
      })
    );
  }

  deleteCompetitor(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const current = this.competitorsSubject.value;
        this.competitorsSubject.next(current.filter(c => c.competitorId !== id));
      })
    );
  }
}
