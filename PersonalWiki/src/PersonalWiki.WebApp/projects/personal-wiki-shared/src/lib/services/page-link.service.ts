import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { PageLink, CreatePageLink, UpdatePageLink } from '../models';
import { environment } from '../environments';

@Injectable({
  providedIn: 'root'
})
export class PageLinkService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/page-links`;

  private pageLinksSubject = new BehaviorSubject<PageLink[]>([]);
  public pageLinks$ = this.pageLinksSubject.asObservable();

  getAll(): Observable<PageLink[]> {
    return this.http.get<PageLink[]>(this.baseUrl).pipe(
      tap(links => this.pageLinksSubject.next(links))
    );
  }

  getById(id: string): Observable<PageLink> {
    return this.http.get<PageLink>(`${this.baseUrl}/${id}`);
  }

  create(pageLink: CreatePageLink): Observable<PageLink> {
    return this.http.post<PageLink>(this.baseUrl, pageLink).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  update(pageLink: UpdatePageLink): Observable<PageLink> {
    return this.http.put<PageLink>(`${this.baseUrl}/${pageLink.pageLinkId}`, pageLink).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => this.getAll().subscribe())
    );
  }
}
