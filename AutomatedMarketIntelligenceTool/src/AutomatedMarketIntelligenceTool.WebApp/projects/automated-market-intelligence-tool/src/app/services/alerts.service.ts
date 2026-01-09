import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Alert, CreateAlertRequest, UpdateAlertRequest } from '../models';
import { environment } from '../environments/environment';

@Injectable({ providedIn: 'root' })
export class AlertsService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/alerts`;

  private readonly alertsSubject = new BehaviorSubject<Alert[]>([]);
  alerts$ = this.alertsSubject.asObservable();

  loadAlerts(): Observable<Alert[]> {
    return this.http.get<Alert[]>(this.baseUrl).pipe(
      tap(alerts => this.alertsSubject.next(alerts))
    );
  }

  getAlertById(id: string): Observable<Alert> {
    return this.http.get<Alert>(`${this.baseUrl}/${id}`);
  }

  createAlert(request: CreateAlertRequest): Observable<Alert> {
    return this.http.post<Alert>(this.baseUrl, request).pipe(
      tap(alert => {
        const current = this.alertsSubject.value;
        this.alertsSubject.next([...current, alert]);
      })
    );
  }

  updateAlert(request: UpdateAlertRequest): Observable<Alert> {
    return this.http.put<Alert>(`${this.baseUrl}/${request.alertId}`, request).pipe(
      tap(updated => {
        const current = this.alertsSubject.value;
        const index = current.findIndex(a => a.alertId === updated.alertId);
        if (index !== -1) {
          current[index] = updated;
          this.alertsSubject.next([...current]);
        }
      })
    );
  }

  deleteAlert(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const current = this.alertsSubject.value;
        this.alertsSubject.next(current.filter(a => a.alertId !== id));
      })
    );
  }

  toggleAlert(id: string, isActive: boolean): Observable<Alert> {
    return this.http.patch<Alert>(`${this.baseUrl}/${id}/toggle`, { isActive }).pipe(
      tap(updated => {
        const current = this.alertsSubject.value;
        const index = current.findIndex(a => a.alertId === updated.alertId);
        if (index !== -1) {
          current[index] = updated;
          this.alertsSubject.next([...current]);
        }
      })
    );
  }
}
