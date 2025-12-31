import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { Trip, CreateTripRequest, UpdateTripRequest } from '../models';

@Injectable({
  providedIn: 'root'
})
export class TripsService {
  private readonly apiUrl = `${environment.baseUrl}/api/Trips`;
  private tripsSubject = new BehaviorSubject<Trip[]>([]);
  public trips$ = this.tripsSubject.asObservable();

  constructor(private http: HttpClient) {}

  getTrips(userId?: string, spotId?: string): Observable<Trip[]> {
    let params = new HttpParams();
    if (userId) params = params.set('userId', userId);
    if (spotId) params = params.set('spotId', spotId);

    return this.http.get<Trip[]>(this.apiUrl, { params }).pipe(
      tap(trips => this.tripsSubject.next(trips))
    );
  }

  getTripById(id: string): Observable<Trip> {
    return this.http.get<Trip>(`${this.apiUrl}/${id}`);
  }

  createTrip(request: CreateTripRequest): Observable<Trip> {
    return this.http.post<Trip>(this.apiUrl, request).pipe(
      tap(() => this.refreshTrips())
    );
  }

  updateTrip(id: string, request: UpdateTripRequest): Observable<Trip> {
    return this.http.put<Trip>(`${this.apiUrl}/${id}`, request).pipe(
      tap(() => this.refreshTrips())
    );
  }

  deleteTrip(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => this.refreshTrips())
    );
  }

  private refreshTrips(): void {
    this.getTrips().subscribe();
  }
}
