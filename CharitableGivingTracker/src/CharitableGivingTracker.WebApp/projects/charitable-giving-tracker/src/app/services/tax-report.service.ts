import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { TaxReport, CreateTaxReportCommand, UpdateTaxReportCommand } from '../models';

@Injectable({
  providedIn: 'root'
})
export class TaxReportService {
  private readonly apiUrl = `${environment.baseUrl}/api/TaxReports`;
  private taxReportsSubject = new BehaviorSubject<TaxReport[]>([]);
  public taxReports$ = this.taxReportsSubject.asObservable();

  constructor(private http: HttpClient) {}

  getAll(): Observable<TaxReport[]> {
    return this.http.get<TaxReport[]>(this.apiUrl).pipe(
      tap(taxReports => this.taxReportsSubject.next(taxReports))
    );
  }

  getById(id: string): Observable<TaxReport> {
    return this.http.get<TaxReport>(`${this.apiUrl}/${id}`);
  }

  getByYear(year: number): Observable<TaxReport> {
    return this.http.get<TaxReport>(`${this.apiUrl}/year/${year}`);
  }

  create(command: CreateTaxReportCommand): Observable<TaxReport> {
    return this.http.post<TaxReport>(this.apiUrl, command).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  update(id: string, command: UpdateTaxReportCommand): Observable<TaxReport> {
    return this.http.put<TaxReport>(`${this.apiUrl}/${id}`, command).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => this.getAll().subscribe())
    );
  }
}
