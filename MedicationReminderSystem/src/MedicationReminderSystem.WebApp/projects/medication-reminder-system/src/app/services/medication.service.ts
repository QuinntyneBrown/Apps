import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap, catchError, throwError } from 'rxjs';
import { environment } from '../../environments/environment';
import { Medication, CreateMedicationCommand, UpdateMedicationCommand } from '../models';

@Injectable({
  providedIn: 'root'
})
export class MedicationService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/medications`;

  private medicationsSubject = new BehaviorSubject<Medication[]>([]);
  private loadingSubject = new BehaviorSubject<boolean>(false);
  private errorSubject = new BehaviorSubject<string | null>(null);

  medications$ = this.medicationsSubject.asObservable();
  loading$ = this.loadingSubject.asObservable();
  error$ = this.errorSubject.asObservable();

  getAll(): Observable<Medication[]> {
    this.loadingSubject.next(true);
    this.errorSubject.next(null);

    return this.http.get<Medication[]>(this.baseUrl).pipe(
      tap(medications => {
        this.medicationsSubject.next(medications);
        this.loadingSubject.next(false);
      }),
      catchError(error => {
        this.errorSubject.next(error.message);
        this.loadingSubject.next(false);
        return throwError(() => error);
      })
    );
  }

  getById(id: string): Observable<Medication> {
    this.loadingSubject.next(true);
    this.errorSubject.next(null);

    return this.http.get<Medication>(`${this.baseUrl}/${id}`).pipe(
      tap(() => this.loadingSubject.next(false)),
      catchError(error => {
        this.errorSubject.next(error.message);
        this.loadingSubject.next(false);
        return throwError(() => error);
      })
    );
  }

  create(command: CreateMedicationCommand): Observable<Medication> {
    this.loadingSubject.next(true);
    this.errorSubject.next(null);

    return this.http.post<Medication>(this.baseUrl, command).pipe(
      tap(medication => {
        const current = this.medicationsSubject.value;
        this.medicationsSubject.next([...current, medication]);
        this.loadingSubject.next(false);
      }),
      catchError(error => {
        this.errorSubject.next(error.message);
        this.loadingSubject.next(false);
        return throwError(() => error);
      })
    );
  }

  update(id: string, command: UpdateMedicationCommand): Observable<Medication> {
    this.loadingSubject.next(true);
    this.errorSubject.next(null);

    return this.http.put<Medication>(`${this.baseUrl}/${id}`, command).pipe(
      tap(medication => {
        const current = this.medicationsSubject.value;
        const index = current.findIndex(m => m.medicationId === id);
        if (index !== -1) {
          const updated = [...current];
          updated[index] = medication;
          this.medicationsSubject.next(updated);
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
        const current = this.medicationsSubject.value;
        this.medicationsSubject.next(current.filter(m => m.medicationId !== id));
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
