import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { WikiPage, CreateWikiPage, UpdateWikiPage } from '../models';
import { environment } from '../environments';

@Injectable({
  providedIn: 'root'
})
export class WikiPageService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/wiki-pages`;

  private wikiPagesSubject = new BehaviorSubject<WikiPage[]>([]);
  public wikiPages$ = this.wikiPagesSubject.asObservable();

  getAll(): Observable<WikiPage[]> {
    return this.http.get<WikiPage[]>(this.baseUrl).pipe(
      tap(pages => this.wikiPagesSubject.next(pages))
    );
  }

  getById(id: string): Observable<WikiPage> {
    return this.http.get<WikiPage>(`${this.baseUrl}/${id}`);
  }

  create(wikiPage: CreateWikiPage): Observable<WikiPage> {
    return this.http.post<WikiPage>(this.baseUrl, wikiPage).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  update(wikiPage: UpdateWikiPage): Observable<WikiPage> {
    return this.http.put<WikiPage>(`${this.baseUrl}/${wikiPage.wikiPageId}`, wikiPage).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => this.getAll().subscribe())
    );
  }
}
