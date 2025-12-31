import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { PageRevision, CreatePageRevision, UpdatePageRevision } from '../models';
import { environment } from '../environments';

@Injectable({
  providedIn: 'root'
})
export class PageRevisionService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/page-revisions`;

  private pageRevisionsSubject = new BehaviorSubject<PageRevision[]>([]);
  public pageRevisions$ = this.pageRevisionsSubject.asObservable();

  getAll(): Observable<PageRevision[]> {
    return this.http.get<PageRevision[]>(this.baseUrl).pipe(
      tap(revisions => this.pageRevisionsSubject.next(revisions))
    );
  }

  getById(id: string): Observable<PageRevision> {
    return this.http.get<PageRevision>(`${this.baseUrl}/${id}`);
  }

  create(pageRevision: CreatePageRevision): Observable<PageRevision> {
    return this.http.post<PageRevision>(this.baseUrl, pageRevision).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  update(pageRevision: UpdatePageRevision): Observable<PageRevision> {
    return this.http.put<PageRevision>(`${this.baseUrl}/${pageRevision.pageRevisionId}`, pageRevision).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => this.getAll().subscribe())
    );
  }
}
