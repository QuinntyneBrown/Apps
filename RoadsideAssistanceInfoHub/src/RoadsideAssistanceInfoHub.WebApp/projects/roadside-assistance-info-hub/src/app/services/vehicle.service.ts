import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Vehicle } from '../models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class VehicleService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;
  private _vehiclesSubject = new BehaviorSubject<Vehicle[]>([]);

  public vehicles$ = this._vehiclesSubject.asObservable();

  getVehicles(): Observable<Vehicle[]> {
    return this._http.get<Vehicle[]>(`${this._baseUrl}/api/vehicles`).pipe(
      tap(vehicles => this._vehiclesSubject.next(vehicles))
    );
  }

  getVehicle(id: string): Observable<Vehicle> {
    return this._http.get<Vehicle>(`${this._baseUrl}/api/vehicles/${id}`);
  }

  createVehicle(vehicle: Partial<Vehicle>): Observable<Vehicle> {
    return this._http.post<Vehicle>(`${this._baseUrl}/api/vehicles`, vehicle).pipe(
      tap(() => this.getVehicles().subscribe())
    );
  }

  updateVehicle(id: string, vehicle: Partial<Vehicle>): Observable<Vehicle> {
    return this._http.put<Vehicle>(`${this._baseUrl}/api/vehicles/${id}`, vehicle).pipe(
      tap(() => this.getVehicles().subscribe())
    );
  }

  deleteVehicle(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/vehicles/${id}`).pipe(
      tap(() => this.getVehicles().subscribe())
    );
  }
}
