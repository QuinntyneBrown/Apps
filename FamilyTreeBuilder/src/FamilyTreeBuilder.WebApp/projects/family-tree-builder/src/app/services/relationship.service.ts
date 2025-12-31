import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { Relationship, CreateRelationshipRequest } from '../models';

@Injectable({
  providedIn: 'root'
})
export class RelationshipService {
  private readonly apiUrl = `${environment.baseUrl}/api/relationships`;
  private relationshipsSubject = new BehaviorSubject<Relationship[]>([]);
  public relationships$ = this.relationshipsSubject.asObservable();

  constructor(private http: HttpClient) {}

  getRelationships(personId?: string): Observable<Relationship[]> {
    const params = personId ? { personId } : {};
    return this.http.get<Relationship[]>(this.apiUrl, { params }).pipe(
      tap(relationships => this.relationshipsSubject.next(relationships))
    );
  }

  getRelationshipById(id: string): Observable<Relationship> {
    return this.http.get<Relationship>(`${this.apiUrl}/${id}`);
  }

  createRelationship(request: CreateRelationshipRequest): Observable<Relationship> {
    return this.http.post<Relationship>(this.apiUrl, request).pipe(
      tap(() => this.refreshRelationships())
    );
  }

  deleteRelationship(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => this.refreshRelationships())
    );
  }

  private refreshRelationships(): void {
    this.getRelationships().subscribe();
  }
}
