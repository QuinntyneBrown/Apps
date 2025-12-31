import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Vital, CreateVital, UpdateVital } from '../models';
import { environment } from '../environments';

@Injectable({
  providedIn: 'root'
})
export class VitalService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/vitals`;

  private vitalsSubject = new BehaviorSubject<Vital[]>([]);
  public vitals$ = this.vitalsSubject.asObservable();

  private selectedVitalSubject = new BehaviorSubject<Vital | null>(null);
  public selectedVital$ = this.selectedVitalSubject.asObservable();

  getAll(): Observable<Vital[]> {
    return this.http.get<Vital[]>(this.baseUrl).pipe(
      tap(vitals => this.vitalsSubject.next(vitals))
    );
  }

  getById(id: string): Observable<Vital> {
    return this.http.get<Vital>(`${this.baseUrl}/${id}`).pipe(
      tap(vital => this.selectedVitalSubject.next(vital))
    );
  }

  create(vital: CreateVital): Observable<Vital> {
    return this.http.post<Vital>(this.baseUrl, vital).pipe(
      tap(newVital => {
        const current = this.vitalsSubject.value;
        this.vitalsSubject.next([...current, newVital]);
      })
    );
  }

  update(vital: UpdateVital): Observable<Vital> {
    return this.http.put<Vital>(`${this.baseUrl}/${vital.vitalId}`, vital).pipe(
      tap(updatedVital => {
        const current = this.vitalsSubject.value;
        const index = current.findIndex(v => v.vitalId === updatedVital.vitalId);
        if (index !== -1) {
          current[index] = updatedVital;
          this.vitalsSubject.next([...current]);
        }
        this.selectedVitalSubject.next(updatedVital);
      })
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const current = this.vitalsSubject.value;
        this.vitalsSubject.next(current.filter(v => v.vitalId !== id));
        if (this.selectedVitalSubject.value?.vitalId === id) {
          this.selectedVitalSubject.next(null);
        }
      })
    );
  }

  clearSelected(): void {
    this.selectedVitalSubject.next(null);
  }
}
