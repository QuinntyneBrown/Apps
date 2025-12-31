import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { ExpirationAlert, CreateExpirationAlertCommand } from '../models';

@Injectable({
  providedIn: 'root'
})
export class ExpirationAlertService {
  private readonly apiUrl = `${environment.baseUrl}/api/expirationalerts`;
  private alertsSubject = new BehaviorSubject<ExpirationAlert[]>([]);
  public alerts$ = this.alertsSubject.asObservable();

  constructor(private http: HttpClient) {}

  getExpirationAlerts(onlyUnacknowledged?: boolean): Observable<ExpirationAlert[]> {
    const params = onlyUnacknowledged !== undefined ? { onlyUnacknowledged: onlyUnacknowledged.toString() } : {};
    return this.http.get<ExpirationAlert[]>(this.apiUrl, { params }).pipe(
      tap(alerts => this.alertsSubject.next(alerts))
    );
  }

  getExpirationAlertById(id: string): Observable<ExpirationAlert> {
    return this.http.get<ExpirationAlert>(`${this.apiUrl}/${id}`);
  }

  createExpirationAlert(command: CreateExpirationAlertCommand): Observable<ExpirationAlert> {
    return this.http.post<ExpirationAlert>(this.apiUrl, command).pipe(
      tap(() => this.getExpirationAlerts().subscribe())
    );
  }

  acknowledgeExpirationAlert(id: string): Observable<ExpirationAlert> {
    return this.http.put<ExpirationAlert>(`${this.apiUrl}/${id}/acknowledge`, {}).pipe(
      tap(() => this.getExpirationAlerts().subscribe())
    );
  }

  deleteExpirationAlert(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => this.getExpirationAlerts().subscribe())
    );
  }
}
