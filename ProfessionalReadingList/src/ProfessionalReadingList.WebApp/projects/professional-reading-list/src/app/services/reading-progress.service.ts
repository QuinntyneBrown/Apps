import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments';
import { ReadingProgress, CreateReadingProgressCommand, UpdateReadingProgressCommand } from '../models';

@Injectable({
  providedIn: 'root'
})
export class ReadingProgressService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/readingprogress`;

  private progressSubject = new BehaviorSubject<ReadingProgress[]>([]);
  public progress$ = this.progressSubject.asObservable();

  getAll(): Observable<ReadingProgress[]> {
    return this.http.get<ReadingProgress[]>(this.baseUrl).pipe(
      tap(progress => this.progressSubject.next(progress))
    );
  }

  getById(id: string): Observable<ReadingProgress> {
    return this.http.get<ReadingProgress>(`${this.baseUrl}/${id}`);
  }

  getByResourceId(resourceId: string): Observable<ReadingProgress> {
    return this.http.get<ReadingProgress>(`${this.baseUrl}/resource/${resourceId}`);
  }

  create(command: CreateReadingProgressCommand): Observable<ReadingProgress> {
    return this.http.post<ReadingProgress>(this.baseUrl, command).pipe(
      tap(progress => {
        const current = this.progressSubject.value;
        this.progressSubject.next([...current, progress]);
      })
    );
  }

  update(id: string, command: UpdateReadingProgressCommand): Observable<ReadingProgress> {
    return this.http.put<ReadingProgress>(`${this.baseUrl}/${id}`, command).pipe(
      tap(updated => {
        const current = this.progressSubject.value;
        const index = current.findIndex(p => p.readingProgressId === id);
        if (index !== -1) {
          current[index] = updated;
          this.progressSubject.next([...current]);
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const current = this.progressSubject.value;
        this.progressSubject.next(current.filter(p => p.readingProgressId !== id));
      })
    );
  }
}
