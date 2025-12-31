import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { Trip, CreateTrip, UpdateTrip } from '../models';

@Injectable({
  providedIn: 'root'
})
export class TripService {
  private baseUrl = `${environment.baseUrl}/api/trips`;
  private tripsSubject = new BehaviorSubject<Trip[]>([]);
  public trips$ = this.tripsSubject.asObservable();

  private selectedTripSubject = new BehaviorSubject<Trip | null>(null);
  public selectedTrip$ = this.selectedTripSubject.asObservable();

  constructor(private http: HttpClient) {}

  getTrips(userId?: string, campsiteId?: string): Observable<Trip[]> {
    let params = new HttpParams();
    if (userId) params = params.set('userId', userId);
    if (campsiteId) params = params.set('campsiteId', campsiteId);

    return this.http.get<Trip[]>(this.baseUrl, { params }).pipe(
      tap(trips => this.tripsSubject.next(trips))
    );
  }

  getTripById(tripId: string): Observable<Trip> {
    return this.http.get<Trip>(`${this.baseUrl}/${tripId}`).pipe(
      tap(trip => this.selectedTripSubject.next(trip))
    );
  }

  createTrip(trip: CreateTrip): Observable<Trip> {
    return this.http.post<Trip>(this.baseUrl, trip).pipe(
      tap(newTrip => {
        const current = this.tripsSubject.value;
        this.tripsSubject.next([...current, newTrip]);
      })
    );
  }

  updateTrip(tripId: string, trip: UpdateTrip): Observable<Trip> {
    return this.http.put<Trip>(`${this.baseUrl}/${tripId}`, trip).pipe(
      tap(updatedTrip => {
        const current = this.tripsSubject.value;
        const index = current.findIndex(t => t.tripId === tripId);
        if (index !== -1) {
          current[index] = updatedTrip;
          this.tripsSubject.next([...current]);
        }
      })
    );
  }

  deleteTrip(tripId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${tripId}`).pipe(
      tap(() => {
        const current = this.tripsSubject.value;
        this.tripsSubject.next(current.filter(t => t.tripId !== tripId));
      })
    );
  }

  clearSelectedTrip(): void {
    this.selectedTripSubject.next(null);
  }
}
