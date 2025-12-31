import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { Registry, RegistryType } from '../models';

export interface CreateRegistryRequest {
  userId: string;
  name: string;
  description?: string;
  type: RegistryType;
  eventDate: Date;
}

export interface UpdateRegistryRequest {
  registryId: string;
  name: string;
  description?: string;
  type: RegistryType;
  eventDate: Date;
  isActive: boolean;
}

@Injectable({
  providedIn: 'root'
})
export class RegistryService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.apiUrl}/api/registries`;

  private registriesSubject = new BehaviorSubject<Registry[]>([]);
  private loadingSubject = new BehaviorSubject<boolean>(false);
  private selectedRegistrySubject = new BehaviorSubject<Registry | null>(null);

  registries$ = this.registriesSubject.asObservable();
  loading$ = this.loadingSubject.asObservable();
  selectedRegistry$ = this.selectedRegistrySubject.asObservable();

  getRegistries(userId?: string, isActive?: boolean): Observable<Registry[]> {
    this.loadingSubject.next(true);

    let params = new HttpParams();
    if (userId) params = params.set('userId', userId);
    if (isActive !== undefined) params = params.set('isActive', isActive.toString());

    return this.http.get<Registry[]>(this.baseUrl, { params }).pipe(
      tap(registries => {
        this.registriesSubject.next(registries);
        this.loadingSubject.next(false);
      })
    );
  }

  getRegistryById(registryId: string): Observable<Registry> {
    this.loadingSubject.next(true);

    return this.http.get<Registry>(`${this.baseUrl}/${registryId}`).pipe(
      tap(registry => {
        this.selectedRegistrySubject.next(registry);
        this.loadingSubject.next(false);
      })
    );
  }

  createRegistry(request: CreateRegistryRequest): Observable<Registry> {
    this.loadingSubject.next(true);

    return this.http.post<Registry>(this.baseUrl, request).pipe(
      tap(registry => {
        const currentRegistries = this.registriesSubject.value;
        this.registriesSubject.next([registry, ...currentRegistries]);
        this.loadingSubject.next(false);
      })
    );
  }

  updateRegistry(registryId: string, request: UpdateRegistryRequest): Observable<Registry> {
    this.loadingSubject.next(true);

    return this.http.put<Registry>(`${this.baseUrl}/${registryId}`, request).pipe(
      tap(updatedRegistry => {
        const currentRegistries = this.registriesSubject.value;
        const index = currentRegistries.findIndex(r => r.registryId === registryId);
        if (index !== -1) {
          currentRegistries[index] = updatedRegistry;
          this.registriesSubject.next([...currentRegistries]);
        }
        if (this.selectedRegistrySubject.value?.registryId === registryId) {
          this.selectedRegistrySubject.next(updatedRegistry);
        }
        this.loadingSubject.next(false);
      })
    );
  }

  deleteRegistry(registryId: string): Observable<void> {
    this.loadingSubject.next(true);

    return this.http.delete<void>(`${this.baseUrl}/${registryId}`).pipe(
      tap(() => {
        const currentRegistries = this.registriesSubject.value;
        this.registriesSubject.next(currentRegistries.filter(r => r.registryId !== registryId));
        if (this.selectedRegistrySubject.value?.registryId === registryId) {
          this.selectedRegistrySubject.next(null);
        }
        this.loadingSubject.next(false);
      })
    );
  }

  setSelectedRegistry(registry: Registry | null): void {
    this.selectedRegistrySubject.next(registry);
  }
}
