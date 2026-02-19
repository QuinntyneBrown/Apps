import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Source, CreateSource, UpdateSource } from '../models';
import { environment } from '../environments';

@Injectable({
  providedIn: 'root'
})
export class SourceService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/sources`;

  private sourcesSubject = new BehaviorSubject<Source[]>([]);
  public sources$ = this.sourcesSubject.asObservable();

  private loadingSubject = new BehaviorSubject<boolean>(false);
  public loading$ = this.loadingSubject.asObservable();

  getSources(): Observable<Source[]> {
    this.loadingSubject.next(true);
    return this.http.get<Source[]>(this.baseUrl).pipe(
      tap(sources => {
        this.sourcesSubject.next(sources);
        this.loadingSubject.next(false);
      })
    );
  }

  getSource(id: string): Observable<Source> {
    return this.http.get<Source>(`${this.baseUrl}/${id}`);
  }

  createSource(source: CreateSource): Observable<Source> {
    return this.http.post<Source>(this.baseUrl, source).pipe(
      tap(newSource => {
        const sources = this.sourcesSubject.value;
        this.sourcesSubject.next([...sources, newSource]);
      })
    );
  }

  updateSource(source: UpdateSource): Observable<Source> {
    return this.http.put<Source>(`${this.baseUrl}/${source.sourceId}`, source).pipe(
      tap(updatedSource => {
        const sources = this.sourcesSubject.value;
        const index = sources.findIndex(s => s.sourceId === updatedSource.sourceId);
        if (index !== -1) {
          sources[index] = updatedSource;
          this.sourcesSubject.next([...sources]);
        }
      })
    );
  }

  deleteSource(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const sources = this.sourcesSubject.value;
        this.sourcesSubject.next(sources.filter(s => s.sourceId !== id));
      })
    );
  }
}
