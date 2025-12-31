import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Maintenance, CreateMaintenance, UpdateMaintenance } from '../models';
import { environment } from '../environments';

@Injectable({
  providedIn: 'root'
})
export class MaintenanceService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = `${environment.baseUrl}/api/maintenance`;

  private maintenanceSubject = new BehaviorSubject<Maintenance[]>([]);
  public maintenance$ = this.maintenanceSubject.asObservable();

  getAll(userId?: string): Observable<Maintenance[]> {
    const url = userId ? `${this.apiUrl}?userId=${userId}` : this.apiUrl;
    return this.http.get<Maintenance[]>(url).pipe(
      tap(maintenance => this.maintenanceSubject.next(maintenance))
    );
  }

  getById(id: string): Observable<Maintenance> {
    return this.http.get<Maintenance>(`${this.apiUrl}/${id}`);
  }

  create(maintenance: CreateMaintenance): Observable<Maintenance> {
    return this.http.post<Maintenance>(this.apiUrl, maintenance).pipe(
      tap(() => this.refreshMaintenance())
    );
  }

  update(id: string, maintenance: UpdateMaintenance): Observable<Maintenance> {
    return this.http.put<Maintenance>(`${this.apiUrl}/${id}`, maintenance).pipe(
      tap(() => this.refreshMaintenance())
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => this.refreshMaintenance())
    );
  }

  private refreshMaintenance(): void {
    this.getAll().subscribe();
  }
}
