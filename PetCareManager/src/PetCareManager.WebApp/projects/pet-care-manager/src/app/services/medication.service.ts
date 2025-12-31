import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { Medication, CreateMedicationDto, UpdateMedicationDto } from '../models';

@Injectable({
  providedIn: 'root'
})
export class MedicationService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/medications`;

  private medicationsSubject = new BehaviorSubject<Medication[]>([]);
  public medications$ = this.medicationsSubject.asObservable();

  getMedications(petId?: string): Observable<Medication[]> {
    let params = new HttpParams();
    if (petId) {
      params = params.set('petId', petId);
    }

    return this.http.get<Medication[]>(this.baseUrl, { params }).pipe(
      tap(medications => this.medicationsSubject.next(medications))
    );
  }

  getMedicationById(medicationId: string): Observable<Medication> {
    return this.http.get<Medication>(`${this.baseUrl}/${medicationId}`);
  }

  createMedication(medication: CreateMedicationDto): Observable<Medication> {
    return this.http.post<Medication>(this.baseUrl, medication).pipe(
      tap(newMedication => {
        const currentMedications = this.medicationsSubject.value;
        this.medicationsSubject.next([...currentMedications, newMedication]);
      })
    );
  }

  updateMedication(medicationId: string, medication: UpdateMedicationDto): Observable<Medication> {
    return this.http.put<Medication>(`${this.baseUrl}/${medicationId}`, medication).pipe(
      tap(updatedMedication => {
        const currentMedications = this.medicationsSubject.value;
        const index = currentMedications.findIndex(m => m.medicationId === medicationId);
        if (index !== -1) {
          const newMedications = [...currentMedications];
          newMedications[index] = updatedMedication;
          this.medicationsSubject.next(newMedications);
        }
      })
    );
  }

  deleteMedication(medicationId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${medicationId}`).pipe(
      tap(() => {
        const currentMedications = this.medicationsSubject.value;
        this.medicationsSubject.next(currentMedications.filter(m => m.medicationId !== medicationId));
      })
    );
  }
}
