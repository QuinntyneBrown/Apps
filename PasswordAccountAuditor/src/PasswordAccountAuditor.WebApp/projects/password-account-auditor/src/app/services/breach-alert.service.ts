import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../../environments';
import { BreachAlert, CreateBreachAlert, UpdateBreachAlert } from '../models';

@Injectable({
  providedIn: 'root'
})
export class BreachAlertService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/breachalerts`;

  private breachAlertsSubject = new BehaviorSubject<BreachAlert[]>([]);
  public breachAlerts$ = this.breachAlertsSubject.asObservable();

  getBreachAlerts(): Observable<BreachAlert[]> {
    return this.http.get<BreachAlert[]>(this.baseUrl).pipe(
      tap(alerts => this.breachAlertsSubject.next(alerts))
    );
  }

  getBreachAlertById(id: string): Observable<BreachAlert> {
    return this.http.get<BreachAlert>(`${this.baseUrl}/${id}`);
  }

  createBreachAlert(alert: CreateBreachAlert): Observable<BreachAlert> {
    return this.http.post<BreachAlert>(this.baseUrl, alert).pipe(
      tap(() => this.getBreachAlerts().subscribe())
    );
  }

  updateBreachAlert(alert: UpdateBreachAlert): Observable<BreachAlert> {
    return this.http.put<BreachAlert>(`${this.baseUrl}/${alert.breachAlertId}`, alert).pipe(
      tap(() => this.getBreachAlerts().subscribe())
    );
  }

  deleteBreachAlert(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => this.getBreachAlerts().subscribe())
    );
  }
}
