import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments';
import { ServiceRecord, CreateServiceRecordRequest, UpdateServiceRecordRequest } from '../models';

@Injectable({
  providedIn: 'root',
})
export class ServiceRecordService {
  private readonly _http = inject(HttpClient);
  private readonly _baseUrl = `${environment.baseUrl}/api/servicerecords`;

  private readonly _serviceRecords$ = new BehaviorSubject<ServiceRecord[]>([]);
  public readonly serviceRecords$ = this._serviceRecords$.asObservable();

  getServiceRecordsByAppliance(applianceId: string): Observable<ServiceRecord[]> {
    return this._http.get<ServiceRecord[]>(`${this._baseUrl}/appliance/${applianceId}`).pipe(
      tap((records) => this._serviceRecords$.next(records))
    );
  }

  getServiceRecordById(id: string): Observable<ServiceRecord> {
    return this._http.get<ServiceRecord>(`${this._baseUrl}/${id}`);
  }

  createServiceRecord(request: CreateServiceRecordRequest): Observable<ServiceRecord> {
    return this._http.post<ServiceRecord>(this._baseUrl, request).pipe(
      tap((record) => {
        const current = this._serviceRecords$.value;
        this._serviceRecords$.next([...current, record]);
      })
    );
  }

  updateServiceRecord(id: string, request: UpdateServiceRecordRequest): Observable<ServiceRecord> {
    return this._http.put<ServiceRecord>(`${this._baseUrl}/${id}`, request).pipe(
      tap((updated) => {
        const current = this._serviceRecords$.value;
        const index = current.findIndex((s) => s.serviceRecordId === id);
        if (index !== -1) {
          current[index] = updated;
          this._serviceRecords$.next([...current]);
        }
      })
    );
  }

  deleteServiceRecord(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/${id}`).pipe(
      tap(() => {
        const current = this._serviceRecords$.value;
        this._serviceRecords$.next(current.filter((s) => s.serviceRecordId !== id));
      })
    );
  }
}
