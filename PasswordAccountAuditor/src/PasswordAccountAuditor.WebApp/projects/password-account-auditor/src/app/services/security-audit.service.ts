import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../../environments';
import { SecurityAudit, CreateSecurityAudit, UpdateSecurityAudit } from '../models';

@Injectable({
  providedIn: 'root'
})
export class SecurityAuditService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/securityaudits`;

  private securityAuditsSubject = new BehaviorSubject<SecurityAudit[]>([]);
  public securityAudits$ = this.securityAuditsSubject.asObservable();

  getSecurityAudits(): Observable<SecurityAudit[]> {
    return this.http.get<SecurityAudit[]>(this.baseUrl).pipe(
      tap(audits => this.securityAuditsSubject.next(audits))
    );
  }

  getSecurityAuditById(id: string): Observable<SecurityAudit> {
    return this.http.get<SecurityAudit>(`${this.baseUrl}/${id}`);
  }

  createSecurityAudit(audit: CreateSecurityAudit): Observable<SecurityAudit> {
    return this.http.post<SecurityAudit>(this.baseUrl, audit).pipe(
      tap(() => this.getSecurityAudits().subscribe())
    );
  }

  updateSecurityAudit(audit: UpdateSecurityAudit): Observable<SecurityAudit> {
    return this.http.put<SecurityAudit>(`${this.baseUrl}/${audit.securityAuditId}`, audit).pipe(
      tap(() => this.getSecurityAudits().subscribe())
    );
  }

  deleteSecurityAudit(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => this.getSecurityAudits().subscribe())
    );
  }
}
