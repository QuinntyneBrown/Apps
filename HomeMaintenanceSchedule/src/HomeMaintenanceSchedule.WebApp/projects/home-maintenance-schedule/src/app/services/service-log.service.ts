import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments';
import { ServiceLog, CreateServiceLog, UpdateServiceLog } from '../models';

@Injectable({ providedIn: 'root' })
export class ServiceLogService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;

  private _logsSubject = new BehaviorSubject<ServiceLog[]>([]);
  logs$ = this._logsSubject.asObservable();

  getAll(maintenanceTaskId?: string, contractorId?: string): Observable<ServiceLog[]> {
    let params = new HttpParams();
    if (maintenanceTaskId) params = params.set('maintenanceTaskId', maintenanceTaskId);
    if (contractorId) params = params.set('contractorId', contractorId);

    return this._http.get<ServiceLog[]>(`${this._baseUrl}/api/ServiceLogs`, { params }).pipe(
      tap(logs => this._logsSubject.next(logs))
    );
  }

  getById(id: string): Observable<ServiceLog> {
    return this._http.get<ServiceLog>(`${this._baseUrl}/api/ServiceLogs/${id}`);
  }

  create(log: CreateServiceLog): Observable<ServiceLog> {
    return this._http.post<ServiceLog>(`${this._baseUrl}/api/ServiceLogs`, log).pipe(
      tap(created => {
        const current = this._logsSubject.value;
        this._logsSubject.next([...current, created]);
      })
    );
  }

  update(id: string, log: UpdateServiceLog): Observable<ServiceLog> {
    return this._http.put<ServiceLog>(`${this._baseUrl}/api/ServiceLogs/${id}`, log).pipe(
      tap(updated => {
        const current = this._logsSubject.value;
        const index = current.findIndex(l => l.serviceLogId === id);
        if (index !== -1) {
          current[index] = updated;
          this._logsSubject.next([...current]);
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/ServiceLogs/${id}`).pipe(
      tap(() => {
        const current = this._logsSubject.value;
        this._logsSubject.next(current.filter(l => l.serviceLogId !== id));
      })
    );
  }
}
