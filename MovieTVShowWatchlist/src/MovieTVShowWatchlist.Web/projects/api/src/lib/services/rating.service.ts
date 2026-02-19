import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Rating, CreateRatingRequest, UpdateRatingRequest } from '../models';
import { environment } from '../environments';

@Injectable({
  providedIn: 'root'
})
export class RatingService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = `${environment.baseUrl}/api/ratings`;

  private ratingsSubject = new BehaviorSubject<Rating[]>([]);
  public ratings$ = this.ratingsSubject.asObservable();

  getAll(): Observable<Rating[]> {
    return this.http.get<Rating[]>(this.apiUrl).pipe(
      tap(ratings => this.ratingsSubject.next(ratings))
    );
  }

  getById(id: string): Observable<Rating> {
    return this.http.get<Rating>(`${this.apiUrl}/${id}`);
  }

  getByContent(contentId: string): Observable<Rating[]> {
    return this.http.get<Rating[]>(`${this.apiUrl}/content/${contentId}`);
  }

  create(contentId: string, request: CreateRatingRequest): Observable<Rating> {
    return this.http.post<Rating>(`${this.apiUrl}/${contentId}`, request).pipe(
      tap(() => this.refreshRatings())
    );
  }

  update(id: string, request: UpdateRatingRequest): Observable<Rating> {
    return this.http.put<Rating>(`${this.apiUrl}/${id}`, request).pipe(
      tap(() => this.refreshRatings())
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => this.refreshRatings())
    );
  }

  private refreshRatings(): void {
    this.getAll().subscribe();
  }
}
