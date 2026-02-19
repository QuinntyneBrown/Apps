import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments';
import { Application, CreateApplication, UpdateApplication } from '../models';

@Injectable({
  providedIn: 'root'
})
export class ApplicationService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/applications`;

  private applicationsSubject = new BehaviorSubject<Application[]>([]);
  public applications$ = this.applicationsSubject.asObservable();

  getApplications(): Observable<Application[]> {
    return this.http.get<Application[]>(this.baseUrl).pipe(
      tap(applications => this.applicationsSubject.next(applications))
    );
  }

  getApplicationById(id: string): Observable<Application> {
    return this.http.get<Application>(`${this.baseUrl}/${id}`);
  }

  createApplication(application: CreateApplication): Observable<Application> {
    return this.http.post<Application>(this.baseUrl, application).pipe(
      tap(newApplication => {
        const current = this.applicationsSubject.value;
        this.applicationsSubject.next([...current, newApplication]);
      })
    );
  }

  updateApplication(application: UpdateApplication): Observable<Application> {
    return this.http.put<Application>(`${this.baseUrl}/${application.applicationId}`, application).pipe(
      tap(updatedApplication => {
        const current = this.applicationsSubject.value;
        const index = current.findIndex(a => a.applicationId === updatedApplication.applicationId);
        if (index !== -1) {
          current[index] = updatedApplication;
          this.applicationsSubject.next([...current]);
        }
      })
    );
  }

  deleteApplication(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const current = this.applicationsSubject.value;
        this.applicationsSubject.next(current.filter(a => a.applicationId !== id));
      })
    );
  }
}
