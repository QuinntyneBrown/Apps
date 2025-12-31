import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Lease, CreateLease, UpdateLease } from '../models';
import { environment } from '../environments';

@Injectable({
  providedIn: 'root'
})
export class LeaseService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/leases`;

  private leasesSubject = new BehaviorSubject<Lease[]>([]);
  public leases$ = this.leasesSubject.asObservable();

  getLeases(): Observable<Lease[]> {
    return this.http.get<Lease[]>(this.baseUrl).pipe(
      tap(leases => this.leasesSubject.next(leases))
    );
  }

  getLease(id: string): Observable<Lease> {
    return this.http.get<Lease>(`${this.baseUrl}/${id}`);
  }

  createLease(lease: CreateLease): Observable<Lease> {
    return this.http.post<Lease>(this.baseUrl, lease).pipe(
      tap(() => this.getLeases().subscribe())
    );
  }

  updateLease(lease: UpdateLease): Observable<Lease> {
    return this.http.put<Lease>(`${this.baseUrl}/${lease.leaseId}`, lease).pipe(
      tap(() => this.getLeases().subscribe())
    );
  }

  deleteLease(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => this.getLeases().subscribe())
    );
  }

  terminateLease(id: string): Observable<Lease> {
    return this.http.post<Lease>(`${this.baseUrl}/${id}/terminate`, {}).pipe(
      tap(() => this.getLeases().subscribe())
    );
  }
}
