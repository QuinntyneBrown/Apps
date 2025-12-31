import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap, catchError, throwError } from 'rxjs';
import { environment } from '../../environments/environment';
import { Refill, CreateRefillCommand, UpdateRefillCommand } from '../models';

@Injectable({
  providedIn: 'root'
})
export class RefillService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/refills`;

  private refillsSubject = new BehaviorSubject<Refill[]>([]);
  private loadingSubject = new BehaviorSubject<boolean>(false);
  private errorSubject = new BehaviorSubject<string | null>(null);

  refills$ = this.refillsSubject.asObservable();
  loading$ = this.loadingSubject.asObservable();
  error$ = this.errorSubject.asObservable();

  getAll(): Observable<Refill[]> {
    this.loadingSubject.next(true);
    this.errorSubject.next(null);

    return this.http.get<Refill[]>(this.baseUrl).pipe(
      tap(refills => {
        this.refillsSubject.next(refills);
        this.loadingSubject.next(false);
      }),
      catchError(error => {
        this.errorSubject.next(error.message);
        this.loadingSubject.next(false);
        return throwError(() => error);
      })
    );
  }

  getById(id: string): Observable<Refill> {
    this.loadingSubject.next(true);
    this.errorSubject.next(null);

    return this.http.get<Refill>(`${this.baseUrl}/${id}`).pipe(
      tap(() => this.loadingSubject.next(false)),
      catchError(error => {
        this.errorSubject.next(error.message);
        this.loadingSubject.next(false);
        return throwError(() => error);
      })
    );
  }

  create(command: CreateRefillCommand): Observable<Refill> {
    this.loadingSubject.next(true);
    this.errorSubject.next(null);

    return this.http.post<Refill>(this.baseUrl, command).pipe(
      tap(refill => {
        const current = this.refillsSubject.value;
        this.refillsSubject.next([...current, refill]);
        this.loadingSubject.next(false);
      }),
      catchError(error => {
        this.errorSubject.next(error.message);
        this.loadingSubject.next(false);
        return throwError(() => error);
      })
    );
  }

  update(id: string, command: UpdateRefillCommand): Observable<Refill> {
    this.loadingSubject.next(true);
    this.errorSubject.next(null);

    return this.http.put<Refill>(`${this.baseUrl}/${id}`, command).pipe(
      tap(refill => {
        const current = this.refillsSubject.value;
        const index = current.findIndex(r => r.refillId === id);
        if (index !== -1) {
          const updated = [...current];
          updated[index] = refill;
          this.refillsSubject.next(updated);
        }
        this.loadingSubject.next(false);
      }),
      catchError(error => {
        this.errorSubject.next(error.message);
        this.loadingSubject.next(false);
        return throwError(() => error);
      })
    );
  }

  delete(id: string): Observable<void> {
    this.loadingSubject.next(true);
    this.errorSubject.next(null);

    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const current = this.refillsSubject.value;
        this.refillsSubject.next(current.filter(r => r.refillId !== id));
        this.loadingSubject.next(false);
      }),
      catchError(error => {
        this.errorSubject.next(error.message);
        this.loadingSubject.next(false);
        return throwError(() => error);
      })
    );
  }
}
