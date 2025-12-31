import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { Vaccination, CreateVaccinationDto, UpdateVaccinationDto } from '../models';

@Injectable({
  providedIn: 'root'
})
export class VaccinationService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/vaccinations`;

  private vaccinationsSubject = new BehaviorSubject<Vaccination[]>([]);
  public vaccinations$ = this.vaccinationsSubject.asObservable();

  getVaccinations(petId?: string): Observable<Vaccination[]> {
    let params = new HttpParams();
    if (petId) {
      params = params.set('petId', petId);
    }

    return this.http.get<Vaccination[]>(this.baseUrl, { params }).pipe(
      tap(vaccinations => this.vaccinationsSubject.next(vaccinations))
    );
  }

  getVaccinationById(vaccinationId: string): Observable<Vaccination> {
    return this.http.get<Vaccination>(`${this.baseUrl}/${vaccinationId}`);
  }

  createVaccination(vaccination: CreateVaccinationDto): Observable<Vaccination> {
    return this.http.post<Vaccination>(this.baseUrl, vaccination).pipe(
      tap(newVaccination => {
        const currentVaccinations = this.vaccinationsSubject.value;
        this.vaccinationsSubject.next([...currentVaccinations, newVaccination]);
      })
    );
  }

  updateVaccination(vaccinationId: string, vaccination: UpdateVaccinationDto): Observable<Vaccination> {
    return this.http.put<Vaccination>(`${this.baseUrl}/${vaccinationId}`, vaccination).pipe(
      tap(updatedVaccination => {
        const currentVaccinations = this.vaccinationsSubject.value;
        const index = currentVaccinations.findIndex(v => v.vaccinationId === vaccinationId);
        if (index !== -1) {
          const newVaccinations = [...currentVaccinations];
          newVaccinations[index] = updatedVaccination;
          this.vaccinationsSubject.next(newVaccinations);
        }
      })
    );
  }

  deleteVaccination(vaccinationId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${vaccinationId}`).pipe(
      tap(() => {
        const currentVaccinations = this.vaccinationsSubject.value;
        this.vaccinationsSubject.next(currentVaccinations.filter(v => v.vaccinationId !== vaccinationId));
      })
    );
  }
}
