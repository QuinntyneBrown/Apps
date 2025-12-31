import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../environments';
import { ServiceRecord, CreateServiceRecordRequest, UpdateServiceRecordRequest } from '../models';

@Injectable({
  providedIn: 'root'
})
export class ServiceRecordService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/servicerecords`;

  private serviceRecordsSubject = new BehaviorSubject<ServiceRecord[]>([]);
  public serviceRecords$ = this.serviceRecordsSubject.asObservable();

  getServiceRecords(vehicleId?: string): Observable<ServiceRecord[]> {
    const url = vehicleId ? `${this.baseUrl}?vehicleId=${vehicleId}` : this.baseUrl;
    return this.http.get<ServiceRecord[]>(url).pipe(
      tap(records => this.serviceRecordsSubject.next(records))
    );
  }

  getServiceRecordById(serviceRecordId: string): Observable<ServiceRecord> {
    return this.http.get<ServiceRecord>(`${this.baseUrl}/${serviceRecordId}`);
  }

  createServiceRecord(request: CreateServiceRecordRequest): Observable<ServiceRecord> {
    return this.http.post<ServiceRecord>(this.baseUrl, request).pipe(
      tap(record => {
        const currentRecords = this.serviceRecordsSubject.value;
        this.serviceRecordsSubject.next([...currentRecords, record]);
      })
    );
  }

  updateServiceRecord(serviceRecordId: string, request: UpdateServiceRecordRequest): Observable<ServiceRecord> {
    return this.http.put<ServiceRecord>(`${this.baseUrl}/${serviceRecordId}`, request).pipe(
      tap(updatedRecord => {
        const currentRecords = this.serviceRecordsSubject.value;
        const index = currentRecords.findIndex(r => r.serviceRecordId === serviceRecordId);
        if (index !== -1) {
          const updated = [...currentRecords];
          updated[index] = updatedRecord;
          this.serviceRecordsSubject.next(updated);
        }
      })
    );
  }

  deleteServiceRecord(serviceRecordId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${serviceRecordId}`).pipe(
      tap(() => {
        const currentRecords = this.serviceRecordsSubject.value;
        this.serviceRecordsSubject.next(currentRecords.filter(r => r.serviceRecordId !== serviceRecordId));
      })
    );
  }
}
