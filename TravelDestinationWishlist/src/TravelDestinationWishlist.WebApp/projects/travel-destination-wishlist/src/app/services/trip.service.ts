import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { Trip } from '../models';

@Injectable({
  providedIn: 'root'
})
export class TripService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/trips`;

  private tripsSubject = new BehaviorSubject<Trip[]>([]);
  public trips$ = this.tripsSubject.asObservable();

  getTrips(userId?: string, destinationId?: string, startDate?: string, endDate?: string): Observable<Trip[]> {
    let url = this.baseUrl;
    const params: string[] = [];

    if (userId) params.push(`userId=${userId}`);
    if (destinationId) params.push(`destinationId=${destinationId}`);
    if (startDate) params.push(`startDate=${startDate}`);
    if (endDate) params.push(`endDate=${endDate}`);

    if (params.length > 0) {
      url += `?${params.join('&')}`;
    }

    return this.http.get<Trip[]>(url).pipe(
      tap(trips => this.tripsSubject.next(trips))
    );
  }

  getTripById(tripId: string): Observable<Trip> {
    return this.http.get<Trip>(`${this.baseUrl}/${tripId}`);
  }

  createTrip(trip: Partial<Trip>): Observable<Trip> {
    return this.http.post<Trip>(this.baseUrl, trip).pipe(
      tap(() => this.refreshTrips(trip.userId))
    );
  }

  updateTrip(tripId: string, trip: Partial<Trip>): Observable<Trip> {
    return this.http.put<Trip>(`${this.baseUrl}/${tripId}`, {
      ...trip,
      tripId
    }).pipe(
      tap(() => this.refreshTrips(trip.userId))
    );
  }

  deleteTrip(tripId: string, userId?: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${tripId}`).pipe(
      tap(() => this.refreshTrips(userId))
    );
  }

  private refreshTrips(userId?: string): void {
    if (userId) {
      this.getTrips(userId).subscribe();
    }
  }
}
