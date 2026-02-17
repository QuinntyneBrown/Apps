import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments';
import { Resource, CreateResourceCommand, UpdateResourceCommand } from '../models';

@Injectable({
  providedIn: 'root'
})
export class ResourceService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/resources`;

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

  create(command: CreateResourceCommand): Observable<Resource> {
    return this.http.post<Resource>(this.baseUrl, command).pipe(
      tap(resource => {
        const current = this.resourcesSubject.value;
        this.resourcesSubject.next([...current, resource]);
      })
    );
  }

  update(id: string, command: UpdateResourceCommand): Observable<Resource> {
    return this.http.put<Resource>(`${this.baseUrl}/${id}`, command).pipe(
      tap(updated => {
        const current = this.resourcesSubject.value;
        const index = current.findIndex(r => r.resourceId === id);
        if (index !== -1) {
          current[index] = updated;
          this.resourcesSubject.next([...current]);
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const current = this.resourcesSubject.value;
        this.resourcesSubject.next(current.filter(r => r.resourceId !== id));
      })
    );
  }
}
