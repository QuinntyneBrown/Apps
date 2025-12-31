import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Ride, CreateRide, UpdateRide } from '../models';
import { environment } from '../environments';

@Injectable({
  providedIn: 'root'
})
export class RideService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = `${environment.baseUrl}/api/rides`;

  private ridesSubject = new BehaviorSubject<Ride[]>([]);
  public rides$ = this.ridesSubject.asObservable();

  getAll(userId?: string): Observable<Ride[]> {
    const url = userId ? `${this.apiUrl}?userId=${userId}` : this.apiUrl;
    return this.http.get<Ride[]>(url).pipe(
      tap(rides => this.ridesSubject.next(rides))
    );
  }

  getById(id: string): Observable<Ride> {
    return this.http.get<Ride>(`${this.apiUrl}/${id}`);
  }

  create(ride: CreateRide): Observable<Ride> {
    return this.http.post<Ride>(this.apiUrl, ride).pipe(
      tap(() => this.refreshRides())
    );
  }

  update(id: string, ride: UpdateRide): Observable<Ride> {
    return this.http.put<Ride>(`${this.apiUrl}/${id}`, ride).pipe(
      tap(() => this.refreshRides())
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => this.refreshRides())
    );
  }

  private refreshRides(): void {
    this.getAll().subscribe();
  }
}
