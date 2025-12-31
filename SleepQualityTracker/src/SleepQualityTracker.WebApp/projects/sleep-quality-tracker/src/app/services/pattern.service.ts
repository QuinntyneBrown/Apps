import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { Pattern, CreatePatternRequest, UpdatePatternRequest } from '../models';

@Injectable({
  providedIn: 'root'
})
export class PatternService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/patterns`;

  private patternsSubject = new BehaviorSubject<Pattern[]>([]);
  public patterns$ = this.patternsSubject.asObservable();

  getPatterns(
    userId?: string,
    patternType?: string,
    startDate?: string,
    endDate?: string,
    isHighConfidence?: boolean
  ): Observable<Pattern[]> {
    let params = new HttpParams();
    if (userId) params = params.set('userId', userId);
    if (patternType) params = params.set('patternType', patternType);
    if (startDate) params = params.set('startDate', startDate);
    if (endDate) params = params.set('endDate', endDate);
    if (isHighConfidence !== undefined) params = params.set('isHighConfidence', isHighConfidence.toString());

    return this.http.get<Pattern[]>(this.baseUrl, { params }).pipe(
      tap(patterns => this.patternsSubject.next(patterns))
    );
  }

  getPatternById(patternId: string): Observable<Pattern> {
    return this.http.get<Pattern>(`${this.baseUrl}/${patternId}`);
  }

  createPattern(request: CreatePatternRequest): Observable<Pattern> {
    return this.http.post<Pattern>(this.baseUrl, request).pipe(
      tap(pattern => {
        const current = this.patternsSubject.value;
        this.patternsSubject.next([...current, pattern]);
      })
    );
  }

  updatePattern(request: UpdatePatternRequest): Observable<Pattern> {
    return this.http.put<Pattern>(`${this.baseUrl}/${request.patternId}`, request).pipe(
      tap(pattern => {
        const current = this.patternsSubject.value;
        const index = current.findIndex(p => p.patternId === pattern.patternId);
        if (index !== -1) {
          current[index] = pattern;
          this.patternsSubject.next([...current]);
        }
      })
    );
  }

  deletePattern(patternId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${patternId}`).pipe(
      tap(() => {
        const current = this.patternsSubject.value;
        this.patternsSubject.next(current.filter(p => p.patternId !== patternId));
      })
    );
  }
}
