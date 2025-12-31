import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments/environment';
import { EfficiencyReport, GenerateEfficiencyReportRequest } from '../models';

@Injectable({
  providedIn: 'root'
})
export class EfficiencyReportsService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/EfficiencyReports`;

  private reportsSubject = new BehaviorSubject<EfficiencyReport[]>([]);
  public reports$ = this.reportsSubject.asObservable();

  private currentReportSubject = new BehaviorSubject<EfficiencyReport | null>(null);
  public currentReport$ = this.currentReportSubject.asObservable();

  getAll(vehicleId?: string): Observable<EfficiencyReport[]> {
    const params = vehicleId ? { vehicleId } : {};
    return this.http.get<EfficiencyReport[]>(this.baseUrl, { params }).pipe(
      tap(reports => this.reportsSubject.next(reports))
    );
  }

  getById(id: string): Observable<EfficiencyReport> {
    return this.http.get<EfficiencyReport>(`${this.baseUrl}/${id}`).pipe(
      tap(report => this.currentReportSubject.next(report))
    );
  }

  generate(request: GenerateEfficiencyReportRequest): Observable<EfficiencyReport> {
    return this.http.post<EfficiencyReport>(this.baseUrl, request).pipe(
      tap(() => this.getAll(request.vehicleId).subscribe())
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => this.getAll().subscribe())
    );
  }
}
