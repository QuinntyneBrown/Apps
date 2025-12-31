import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments/environment';
import { Trip, CreateTripRequest, UpdateTripRequest, CompleteTripRequest } from '../models';

@Injectable({
  providedIn: 'root'
})
export class TripsService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/Trips`;

  private tripsSubject = new BehaviorSubject<Trip[]>([]);
  public trips$ = this.tripsSubject.asObservable();

  private currentTripSubject = new BehaviorSubject<Trip | null>(null);
  public currentTrip$ = this.currentTripSubject.asObservable();

  getAll(vehicleId?: string): Observable<Trip[]> {
    const params = vehicleId ? { vehicleId } : {};
    return this.http.get<Trip[]>(this.baseUrl, { params }).pipe(
      tap(trips => this.tripsSubject.next(trips))
    );
  }

  getById(id: string): Observable<Trip> {
    return this.http.get<Trip>(`${this.baseUrl}/${id}`).pipe(
      tap(trip => this.currentTripSubject.next(trip))
    );
  }

  create(request: CreateTripRequest): Observable<Trip> {
    return this.http.post<Trip>(this.baseUrl, request).pipe(
      tap(() => this.getAll(request.vehicleId).subscribe())
    );
  }

  update(id: string, request: UpdateTripRequest): Observable<Trip> {
    return this.http.put<Trip>(`${this.baseUrl}/${id}`, request).pipe(
      tap(trip => this.getAll(trip.vehicleId).subscribe())
    );
  }

  complete(id: string, request: CompleteTripRequest): Observable<Trip> {
    return this.http.post<Trip>(`${this.baseUrl}/${id}/complete`, request).pipe(
      tap(trip => this.getAll(trip.vehicleId).subscribe())
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => this.getAll().subscribe())
    );
  }
}
