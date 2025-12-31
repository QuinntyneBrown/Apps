import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { AuditReport, CreateAuditReportRequest, UpdateAuditReportRequest } from '../models';

@Injectable({
  providedIn: 'root'
})
export class AuditReportService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;
  private _auditReportsSubject = new BehaviorSubject<AuditReport[]>([]);

  auditReports$ = this._auditReportsSubject.asObservable();

  getAll(): Observable<AuditReport[]> {
    return this._http.get<AuditReport[]>(`${this._baseUrl}/api/auditreports`).pipe(
      tap(reports => this._auditReportsSubject.next(reports))
    );
  }

  getById(id: string): Observable<AuditReport> {
    return this._http.get<AuditReport>(`${this._baseUrl}/api/auditreports/${id}`);
  }

  create(request: CreateAuditReportRequest): Observable<AuditReport> {
    return this._http.post<AuditReport>(`${this._baseUrl}/api/auditreports`, request).pipe(
      tap(report => {
        const current = this._auditReportsSubject.value;
        this._auditReportsSubject.next([...current, report]);
      })
    );
  }

  update(id: string, request: UpdateAuditReportRequest): Observable<void> {
    return this._http.put<void>(`${this._baseUrl}/api/auditreports/${id}`, request).pipe(
      tap(() => {
        const current = this._auditReportsSubject.value;
        const index = current.findIndex(r => r.auditReportId === id);
        if (index !== -1) {
          this.getAll().subscribe();
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/auditreports/${id}`).pipe(
      tap(() => {
        const current = this._auditReportsSubject.value;
        this._auditReportsSubject.next(current.filter(r => r.auditReportId !== id));
      })
    );
  }
}
