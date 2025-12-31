import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Property, CreateProperty, UpdateProperty } from '../models';
import { environment } from '../environments';

@Injectable({
  providedIn: 'root'
})
export class PropertyService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/properties`;

  private propertiesSubject = new BehaviorSubject<Property[]>([]);
  public properties$ = this.propertiesSubject.asObservable();

  getProperties(): Observable<Property[]> {
    return this.http.get<Property[]>(this.baseUrl).pipe(
      tap(properties => this.propertiesSubject.next(properties))
    );
  }

  getProperty(id: string): Observable<Property> {
    return this.http.get<Property>(`${this.baseUrl}/${id}`);
  }

  createProperty(property: CreateProperty): Observable<Property> {
    return this.http.post<Property>(this.baseUrl, property).pipe(
      tap(() => this.getProperties().subscribe())
    );
  }

  updateProperty(property: UpdateProperty): Observable<Property> {
    return this.http.put<Property>(`${this.baseUrl}/${property.propertyId}`, property).pipe(
      tap(() => this.getProperties().subscribe())
    );
  }

  deleteProperty(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => this.getProperties().subscribe())
    );
  }
}
