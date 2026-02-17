import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { Trip, CreateTripCommand, UpdateTripCommand } from '../models';

@Injectable({
  providedIn: 'root'
})
export class TripService {
  private readonly apiUrl = `${environment.baseUrl}/api/trips`;
  private tripsSubject = new BehaviorSubject<Trip[]>([]);
  public trips$ = this.tripsSubject.asObservable();

  constructor(private http: HttpClient) {}

  getTrips(userId?: string): Observable<Trip[]> {
    const params = userId ? { userId } : {};
    return this.http.get<Trip[]>(this.apiUrl, { params }).pipe(
      tap(trips => this.tripsSubject.next(trips))
    );
  }

  getTripById(tripId: string): Observable<Trip> {
    return this.http.get<Trip>(`${this.apiUrl}/${tripId}`);
  }

  createTrip(command: CreateTripCommand): Observable<Trip> {
    return this.http.post<Trip>(this.apiUrl, command).pipe(
      tap(trip => {
        const currentTrips = this.tripsSubject.value;
        this.tripsSubject.next([...currentTrips, trip]);
      })
    );
  }

  updateTrip(tripId: string, command: UpdateTripCommand): Observable<Trip> {
    return this.http.put<Trip>(`${this.apiUrl}/${tripId}`, command).pipe(
      tap(updatedTrip => {
        const currentTrips = this.tripsSubject.value;
        const index = currentTrips.findIndex(t => t.tripId === tripId);
        if (index !== -1) {
          currentTrips[index] = updatedTrip;
          this.tripsSubject.next([...currentTrips]);
        }
      })
    );
  }

  deleteTrip(tripId: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${tripId}`).pipe(
      tap(() => {
        const currentTrips = this.tripsSubject.value;
        this.tripsSubject.next(currentTrips.filter(t => t.tripId !== tripId));
      })
    );
  }
}
