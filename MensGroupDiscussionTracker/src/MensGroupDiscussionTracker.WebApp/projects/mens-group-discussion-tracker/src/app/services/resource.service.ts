import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../../environments';
import { Resource, CreateResource, UpdateResource } from '../models';

@Injectable({
  providedIn: 'root'
})
export class ResourceService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/resources`;

  private resourcesSubject = new BehaviorSubject<Resource[]>([]);
  public resources$ = this.resourcesSubject.asObservable();

  getAll(): Observable<Resource[]> {
    return this.http.get<Resource[]>(this.baseUrl).pipe(
      tap(resources => this.resourcesSubject.next(resources))
    );
  }

  getById(id: string): Observable<Resource> {
    return this.http.get<Resource>(`${this.baseUrl}/${id}`);
  }

  getByGroupId(groupId: string): Observable<Resource[]> {
    return this.http.get<Resource[]>(`${this.baseUrl}/group/${groupId}`);
  }

  create(resource: CreateResource): Observable<Resource> {
    return this.http.post<Resource>(this.baseUrl, resource).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  update(resource: UpdateResource): Observable<Resource> {
    return this.http.put<Resource>(`${this.baseUrl}/${resource.resourceId}`, resource).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => this.getAll().subscribe())
    );
  }
}
