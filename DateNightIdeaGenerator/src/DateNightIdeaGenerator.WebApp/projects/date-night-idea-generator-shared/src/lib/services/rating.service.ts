import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments/environment';
import { Rating, CreateRating, UpdateRating } from '../models';

@Injectable({
  providedIn: 'root'
})
export class RatingService {
  private readonly apiUrl = `${environment.baseUrl}/api/Ratings`;
  private ratingsSubject = new BehaviorSubject<Rating[]>([]);
  public ratings$ = this.ratingsSubject.asObservable();

  private selectedRatingSubject = new BehaviorSubject<Rating | null>(null);
  public selectedRating$ = this.selectedRatingSubject.asObservable();

  constructor(private http: HttpClient) {}

  getAll(dateIdeaId?: string, experienceId?: string, userId?: string): Observable<Rating[]> {
    let params = new HttpParams();
    if (dateIdeaId) params = params.set('dateIdeaId', dateIdeaId);
    if (experienceId) params = params.set('experienceId', experienceId);
    if (userId) params = params.set('userId', userId);

    return this.http.get<Rating[]>(this.apiUrl, { params }).pipe(
      tap(ratings => this.ratingsSubject.next(ratings))
    );
  }

  getById(id: string): Observable<Rating> {
    return this.http.get<Rating>(`${this.apiUrl}/${id}`).pipe(
      tap(rating => this.selectedRatingSubject.next(rating))
    );
  }

  create(rating: CreateRating): Observable<Rating> {
    return this.http.post<Rating>(this.apiUrl, rating).pipe(
      tap(newRating => {
        const current = this.ratingsSubject.value;
        this.ratingsSubject.next([...current, newRating]);
      })
    );
  }

  update(id: string, rating: UpdateRating): Observable<Rating> {
    return this.http.put<Rating>(`${this.apiUrl}/${id}`, rating).pipe(
      tap(updatedRating => {
        const current = this.ratingsSubject.value;
        const index = current.findIndex(r => r.ratingId === id);
        if (index !== -1) {
          current[index] = updatedRating;
          this.ratingsSubject.next([...current]);
        }
        this.selectedRatingSubject.next(updatedRating);
      })
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => {
        const current = this.ratingsSubject.value;
        this.ratingsSubject.next(current.filter(r => r.ratingId !== id));
        if (this.selectedRatingSubject.value?.ratingId === id) {
          this.selectedRatingSubject.next(null);
        }
      })
    );
  }
}
