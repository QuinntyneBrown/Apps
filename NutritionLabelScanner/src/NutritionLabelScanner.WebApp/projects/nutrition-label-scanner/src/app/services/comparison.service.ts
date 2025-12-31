import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../../environments';
import { Comparison, CreateComparison, UpdateComparison } from '../models';

@Injectable({
  providedIn: 'root'
})
export class ComparisonService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/comparisons`;

  private comparisonsSubject = new BehaviorSubject<Comparison[]>([]);
  public comparisons$ = this.comparisonsSubject.asObservable();

  private loadingSubject = new BehaviorSubject<boolean>(false);
  public loading$ = this.loadingSubject.asObservable();

  getAll(): Observable<Comparison[]> {
    this.loadingSubject.next(true);
    return this.http.get<Comparison[]>(this.baseUrl).pipe(
      tap(comparisons => {
        this.comparisonsSubject.next(comparisons);
        this.loadingSubject.next(false);
      })
    );
  }

  getById(id: string): Observable<Comparison> {
    return this.http.get<Comparison>(`${this.baseUrl}/${id}`);
  }

  create(comparison: CreateComparison): Observable<Comparison> {
    this.loadingSubject.next(true);
    return this.http.post<Comparison>(this.baseUrl, comparison).pipe(
      tap(() => {
        this.getAll().subscribe();
      })
    );
  }

  update(comparison: UpdateComparison): Observable<Comparison> {
    this.loadingSubject.next(true);
    return this.http.put<Comparison>(`${this.baseUrl}/${comparison.comparisonId}`, comparison).pipe(
      tap(() => {
        this.getAll().subscribe();
      })
    );
  }

  delete(id: string): Observable<void> {
    this.loadingSubject.next(true);
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        this.getAll().subscribe();
      })
    );
  }
}
