import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Installation, CreateInstallationCommand, UpdateInstallationCommand } from '../models';
import { environment } from '../environments';

@Injectable({
  providedIn: 'root'
})
export class InstallationsService {
  private readonly apiUrl = `${environment.baseUrl}/api/installations`;
  private installationsSubject = new BehaviorSubject<Installation[]>([]);
  public installations$ = this.installationsSubject.asObservable();

  constructor(private http: HttpClient) {}

  getAll(): Observable<Installation[]> {
    return this.http.get<Installation[]>(this.apiUrl).pipe(
      tap(installations => this.installationsSubject.next(installations))
    );
  }

  getById(id: string): Observable<Installation> {
    return this.http.get<Installation>(`${this.apiUrl}/${id}`);
  }

  getByModificationId(modificationId: string): Observable<Installation[]> {
    return this.http.get<Installation[]>(`${this.apiUrl}/modification/${modificationId}`).pipe(
      tap(installations => this.installationsSubject.next(installations))
    );
  }

  create(command: CreateInstallationCommand): Observable<Installation> {
    return this.http.post<Installation>(this.apiUrl, command).pipe(
      tap(installation => {
        const current = this.installationsSubject.value;
        this.installationsSubject.next([...current, installation]);
      })
    );
  }

  update(id: string, command: UpdateInstallationCommand): Observable<Installation> {
    return this.http.put<Installation>(`${this.apiUrl}/${id}`, command).pipe(
      tap(updatedInstallation => {
        const current = this.installationsSubject.value;
        const index = current.findIndex(i => i.installationId === id);
        if (index !== -1) {
          current[index] = updatedInstallation;
          this.installationsSubject.next([...current]);
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => {
        const current = this.installationsSubject.value;
        this.installationsSubject.next(current.filter(i => i.installationId !== id));
      })
    );
  }
}
