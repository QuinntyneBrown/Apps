import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments/environment';
import { Vehicle, CreateVehicleRequest, UpdateVehicleRequest } from '../models';

@Injectable({
  providedIn: 'root'
})
export class VehiclesService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/Vehicles`;

  private vehiclesSubject = new BehaviorSubject<Vehicle[]>([]);
  public vehicles$ = this.vehiclesSubject.asObservable();

  private currentVehicleSubject = new BehaviorSubject<Vehicle | null>(null);
  public currentVehicle$ = this.currentVehicleSubject.asObservable();

  getAll(isActive?: boolean): Observable<Vehicle[]> {
    const params = isActive !== undefined ? { isActive: isActive.toString() } : {};
    return this.http.get<Vehicle[]>(this.baseUrl, { params }).pipe(
      tap(vehicles => this.vehiclesSubject.next(vehicles))
    );
  }

  getById(id: string): Observable<Vehicle> {
    return this.http.get<Vehicle>(`${this.baseUrl}/${id}`).pipe(
      tap(vehicle => this.currentVehicleSubject.next(vehicle))
    );
  }

  create(request: CreateVehicleRequest): Observable<Vehicle> {
    return this.http.post<Vehicle>(this.baseUrl, request).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  update(id: string, request: UpdateVehicleRequest): Observable<Vehicle> {
    return this.http.put<Vehicle>(`${this.baseUrl}/${id}`, request).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => this.getAll().subscribe())
    );
  }
}
