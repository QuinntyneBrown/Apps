import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Certification } from '../models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CertificationService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;

  private _certificationsSubject = new BehaviorSubject<Certification[]>([]);
  public certifications$ = this._certificationsSubject.asObservable();

  getCertifications(): Observable<Certification[]> {
    return this._http.get<Certification[]>(`${this._baseUrl}/api/certifications`).pipe(
      tap(certifications => this._certificationsSubject.next(certifications))
    );
  }

  getCertificationById(id: string): Observable<Certification> {
    return this._http.get<Certification>(`${this._baseUrl}/api/certifications/${id}`);
  }

  createCertification(certification: Partial<Certification>): Observable<Certification> {
    return this._http.post<Certification>(`${this._baseUrl}/api/certifications`, certification).pipe(
      tap(() => this.getCertifications().subscribe())
    );
  }

  updateCertification(id: string, certification: Partial<Certification>): Observable<Certification> {
    return this._http.put<Certification>(`${this._baseUrl}/api/certifications/${id}`, certification).pipe(
      tap(() => this.getCertifications().subscribe())
    );
  }

  deleteCertification(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/certifications/${id}`).pipe(
      tap(() => this.getCertifications().subscribe())
    );
  }
}
