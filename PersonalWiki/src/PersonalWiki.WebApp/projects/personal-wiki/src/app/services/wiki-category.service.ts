import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { WikiCategory, CreateWikiCategory, UpdateWikiCategory } from '../models';
import { environment } from '../environments';

@Injectable({
  providedIn: 'root'
})
export class WikiCategoryService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/wiki-categories`;

  private wikiCategoriesSubject = new BehaviorSubject<WikiCategory[]>([]);
  public wikiCategories$ = this.wikiCategoriesSubject.asObservable();

  getAll(): Observable<WikiCategory[]> {
    return this.http.get<WikiCategory[]>(this.baseUrl).pipe(
      tap(categories => this.wikiCategoriesSubject.next(categories))
    );
  }

  getById(id: string): Observable<WikiCategory> {
    return this.http.get<WikiCategory>(`${this.baseUrl}/${id}`);
  }

  create(wikiCategory: CreateWikiCategory): Observable<WikiCategory> {
    return this.http.post<WikiCategory>(this.baseUrl, wikiCategory).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  update(wikiCategory: UpdateWikiCategory): Observable<WikiCategory> {
    return this.http.put<WikiCategory>(`${this.baseUrl}/${wikiCategory.wikiCategoryId}`, wikiCategory).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => this.getAll().subscribe())
    );
  }
}
