import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { Organization, CreateOrganizationCommand, UpdateOrganizationCommand } from '../models';

@Injectable({
  providedIn: 'root'
})
export class OrganizationService {
  private readonly apiUrl = `${environment.baseUrl}/api/Organizations`;
  private organizationsSubject = new BehaviorSubject<Organization[]>([]);
  public organizations$ = this.organizationsSubject.asObservable();

  constructor(private http: HttpClient) {}

  getAll(): Observable<Organization[]> {
    return this.http.get<Organization[]>(this.apiUrl).pipe(
      tap(organizations => this.organizationsSubject.next(organizations))
    );
  }

  getById(id: string): Observable<Organization> {
    return this.http.get<Organization>(`${this.apiUrl}/${id}`);
  }

  create(command: CreateOrganizationCommand): Observable<Organization> {
    return this.http.post<Organization>(this.apiUrl, command).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  update(id: string, command: UpdateOrganizationCommand): Observable<Organization> {
    return this.http.put<Organization>(`${this.apiUrl}/${id}`, command).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => this.getAll().subscribe())
    );
  }
}
