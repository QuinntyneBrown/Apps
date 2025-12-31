import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../environments';
import { Vehicle, CreateVehicleRequest, UpdateVehicleRequest } from '../models';

@Injectable({
  providedIn: 'root'
})
export class VehicleService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/vehicles`;

  private vehiclesSubject = new BehaviorSubject<Vehicle[]>([]);
  public vehicles$ = this.vehiclesSubject.asObservable();

  getVehicles(): Observable<Vehicle[]> {
    return this.http.get<Vehicle[]>(this.baseUrl).pipe(
      tap(vehicles => this.vehiclesSubject.next(vehicles))
    );
  }

  getVehicleById(vehicleId: string): Observable<Vehicle> {
    return this.http.get<Vehicle>(`${this.baseUrl}/${vehicleId}`);
  }

  createVehicle(request: CreateVehicleRequest): Observable<Vehicle> {
    return this.http.post<Vehicle>(this.baseUrl, request).pipe(
      tap(vehicle => {
        const currentVehicles = this.vehiclesSubject.value;
        this.vehiclesSubject.next([...currentVehicles, vehicle]);
      })
    );
  }

  updateVehicle(vehicleId: string, request: UpdateVehicleRequest): Observable<Vehicle> {
    return this.http.put<Vehicle>(`${this.baseUrl}/${vehicleId}`, request).pipe(
      tap(updatedVehicle => {
        const currentVehicles = this.vehiclesSubject.value;
        const index = currentVehicles.findIndex(v => v.vehicleId === vehicleId);
        if (index !== -1) {
          const updated = [...currentVehicles];
          updated[index] = updatedVehicle;
          this.vehiclesSubject.next(updated);
        }
      })
    );
  }

  deleteVehicle(vehicleId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${vehicleId}`).pipe(
      tap(() => {
        const currentVehicles = this.vehiclesSubject.value;
        this.vehiclesSubject.next(currentVehicles.filter(v => v.vehicleId !== vehicleId));
      })
    );
  }
}
