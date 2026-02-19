import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { Reading, CreateReadingRequest, UpdateReadingRequest } from '../models';

@Injectable({
  providedIn: 'root'
})
export class ReadingService {
  private baseUrl = `${environment.baseUrl}/api`;

  private readingsSubject = new BehaviorSubject<Reading[]>([]);
  public readings$ = this.readingsSubject.asObservable();

  private loadingSubject = new BehaviorSubject<boolean>(false);
  public loading$ = this.loadingSubject.asObservable();

  private errorSubject = new BehaviorSubject<string | null>(null);
  public error$ = this.errorSubject.asObservable();

  constructor(private http: HttpClient) {}

  getReadingById(id: string): Observable<Reading> {
    return this.http.get<Reading>(`${this.baseUrl}/readings/${id}`);
  }

  getReadingsByUserId(userId: string, startDate?: string, endDate?: string): Observable<Reading[]> {
    this.loadingSubject.next(true);
    this.errorSubject.next(null);

    let params = new HttpParams().set('userId', userId);
    if (startDate) {
      params = params.set('startDate', startDate);
    }
    if (endDate) {
      params = params.set('endDate', endDate);
    }

    return this.http.get<Reading[]>(`${this.baseUrl}/readings`, { params }).pipe(
      tap({
        next: (readings) => {
          this.readingsSubject.next(readings);
          this.loadingSubject.next(false);
        },
        error: (error) => {
          this.errorSubject.next(error.message);
          this.loadingSubject.next(false);
        }
      })
    );
  }

  getCriticalReadings(userId: string, daysBack: number = 30): Observable<Reading[]> {
    this.loadingSubject.next(true);
    this.errorSubject.next(null);

    const params = new HttpParams()
      .set('userId', userId)
      .set('daysBack', daysBack.toString());

    return this.http.get<Reading[]>(`${this.baseUrl}/readings/critical`, { params }).pipe(
      tap({
        next: () => this.loadingSubject.next(false),
        error: (error) => {
          this.errorSubject.next(error.message);
          this.loadingSubject.next(false);
        }
      })
    );
  }

  createReading(request: CreateReadingRequest): Observable<Reading> {
    this.loadingSubject.next(true);
    this.errorSubject.next(null);

    return this.http.post<Reading>(`${this.baseUrl}/readings`, request).pipe(
      tap({
        next: (newReading) => {
          const currentReadings = this.readingsSubject.value;
          this.readingsSubject.next([newReading, ...currentReadings]);
          this.loadingSubject.next(false);
        },
        error: (error) => {
          this.errorSubject.next(error.message);
          this.loadingSubject.next(false);
        }
      })
    );
  }

  updateReading(id: string, request: UpdateReadingRequest): Observable<Reading> {
    this.loadingSubject.next(true);
    this.errorSubject.next(null);

    return this.http.put<Reading>(`${this.baseUrl}/readings/${id}`, request).pipe(
      tap({
        next: (updatedReading) => {
          const currentReadings = this.readingsSubject.value;
          const index = currentReadings.findIndex(r => r.readingId === id);
          if (index !== -1) {
            currentReadings[index] = updatedReading;
            this.readingsSubject.next([...currentReadings]);
          }
          this.loadingSubject.next(false);
        },
        error: (error) => {
          this.errorSubject.next(error.message);
          this.loadingSubject.next(false);
        }
      })
    );
  }

  deleteReading(id: string): Observable<void> {
    this.loadingSubject.next(true);
    this.errorSubject.next(null);

    return this.http.delete<void>(`${this.baseUrl}/readings/${id}`).pipe(
      tap({
        next: () => {
          const currentReadings = this.readingsSubject.value;
          this.readingsSubject.next(currentReadings.filter(r => r.readingId !== id));
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
