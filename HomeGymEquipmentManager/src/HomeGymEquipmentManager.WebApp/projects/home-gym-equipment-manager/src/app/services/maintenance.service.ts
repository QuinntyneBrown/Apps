import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Maintenance } from '../models';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MaintenanceService {
  private readonly apiUrl = `${environment.baseUrl}/api/Maintenance`;
  private maintenanceListSubject = new BehaviorSubject<Maintenance[]>([]);
  private selectedMaintenanceSubject = new BehaviorSubject<Maintenance | null>(null);

  maintenanceList$ = this.maintenanceListSubject.asObservable();
  selectedMaintenance$ = this.selectedMaintenanceSubject.asObservable();

  constructor(private http: HttpClient) {}

  getAll(userId?: string, equipmentId?: string): Observable<Maintenance[]> {
    let params = new HttpParams();
    if (userId) params = params.set('userId', userId);
    if (equipmentId) params = params.set('equipmentId', equipmentId);

    return this.http.get<Maintenance[]>(this.apiUrl, { params }).pipe(
      tap(maintenance => this.maintenanceListSubject.next(maintenance))
    );
  }

  getById(id: string): Observable<Maintenance> {
    return this.http.get<Maintenance>(`${this.apiUrl}/${id}`).pipe(
      tap(maintenance => this.selectedMaintenanceSubject.next(maintenance))
    );
  }

  create(maintenance: Maintenance): Observable<Maintenance> {
    return this.http.post<Maintenance>(this.apiUrl, maintenance).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  update(id: string, maintenance: Maintenance): Observable<Maintenance> {
    return this.http.put<Maintenance>(`${this.apiUrl}/${id}`, maintenance).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => this.getAll().subscribe())
    );
  }
}
