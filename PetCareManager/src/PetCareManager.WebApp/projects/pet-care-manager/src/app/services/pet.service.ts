import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { Pet, CreatePetDto, UpdatePetDto, PetType } from '../models';

@Injectable({
  providedIn: 'root'
})
export class PetService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/pets`;

  private petsSubject = new BehaviorSubject<Pet[]>([]);
  public pets$ = this.petsSubject.asObservable();

  getPets(userId?: string, petType?: PetType): Observable<Pet[]> {
    let params = new HttpParams();
    if (userId) {
      params = params.set('userId', userId);
    }
    if (petType) {
      params = params.set('petType', petType);
    }

    return this.http.get<Pet[]>(this.baseUrl, { params }).pipe(
      tap(pets => this.petsSubject.next(pets))
    );
  }

  getPetById(petId: string): Observable<Pet> {
    return this.http.get<Pet>(`${this.baseUrl}/${petId}`);
  }

  createPet(pet: CreatePetDto): Observable<Pet> {
    return this.http.post<Pet>(this.baseUrl, pet).pipe(
      tap(newPet => {
        const currentPets = this.petsSubject.value;
        this.petsSubject.next([...currentPets, newPet]);
      })
    );
  }

  updatePet(petId: string, pet: UpdatePetDto): Observable<Pet> {
    return this.http.put<Pet>(`${this.baseUrl}/${petId}`, pet).pipe(
      tap(updatedPet => {
        const currentPets = this.petsSubject.value;
        const index = currentPets.findIndex(p => p.petId === petId);
        if (index !== -1) {
          const newPets = [...currentPets];
          newPets[index] = updatedPet;
          this.petsSubject.next(newPets);
        }
      })
    );
  }

  deletePet(petId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${petId}`).pipe(
      tap(() => {
        const currentPets = this.petsSubject.value;
        this.petsSubject.next(currentPets.filter(p => p.petId !== petId));
      })
    );
  }
}
