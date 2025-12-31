import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments';
import { SearchQuery, CreateSearchQueryCommand, UpdateSearchQueryCommand } from '../models';

@Injectable({
  providedIn: 'root'
})
export class SearchQueryService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/searchqueries`;

  private searchQueriesSubject = new BehaviorSubject<SearchQuery[]>([]);
  public searchQueries$ = this.searchQueriesSubject.asObservable();

  getSearchQueries(): Observable<SearchQuery[]> {
    return this.http.get<SearchQuery[]>(this.baseUrl).pipe(
      tap(searchQueries => this.searchQueriesSubject.next(searchQueries))
    );
  }

  getSearchQueryById(id: string): Observable<SearchQuery> {
    return this.http.get<SearchQuery>(`${this.baseUrl}/${id}`);
  }

  createSearchQuery(command: CreateSearchQueryCommand): Observable<SearchQuery> {
    return this.http.post<SearchQuery>(this.baseUrl, command).pipe(
      tap(searchQuery => {
        const searchQueries = this.searchQueriesSubject.value;
        this.searchQueriesSubject.next([...searchQueries, searchQuery]);
      })
    );
  }

  updateSearchQuery(command: UpdateSearchQueryCommand): Observable<SearchQuery> {
    return this.http.put<SearchQuery>(`${this.baseUrl}/${command.searchQueryId}`, command).pipe(
      tap(updatedSearchQuery => {
        const searchQueries = this.searchQueriesSubject.value.map(searchQuery =>
          searchQuery.searchQueryId === updatedSearchQuery.searchQueryId ? updatedSearchQuery : searchQuery
        );
        this.searchQueriesSubject.next(searchQueries);
      })
    );
  }

  deleteSearchQuery(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const searchQueries = this.searchQueriesSubject.value.filter(searchQuery => searchQuery.searchQueryId !== id);
        this.searchQueriesSubject.next(searchQueries);
      })
    );
  }
}
