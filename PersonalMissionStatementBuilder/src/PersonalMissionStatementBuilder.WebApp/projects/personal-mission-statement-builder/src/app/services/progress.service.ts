import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments';
import { Progress, CreateProgress, UpdateProgress } from '../models';

@Injectable({
  providedIn: 'root'
})
export class ProgressService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/progresses`;

  private progressesSubject = new BehaviorSubject<Progress[]>([]);
  public progresses$ = this.progressesSubject.asObservable();

  private currentProgressSubject = new BehaviorSubject<Progress | null>(null);
  public currentProgress$ = this.currentProgressSubject.asObservable();

  getAll(): Observable<Progress[]> {
    return this.http.get<Progress[]>(this.baseUrl).pipe(
      tap(progresses => this.progressesSubject.next(progresses))
    );
  }

  getById(id: string): Observable<Progress> {
    return this.http.get<Progress>(`${this.baseUrl}/${id}`).pipe(
      tap(progress => this.currentProgressSubject.next(progress))
    );
  }

  create(progress: CreateProgress): Observable<Progress> {
    return this.http.post<Progress>(this.baseUrl, progress).pipe(
      tap(newProgress => {
        const current = this.progressesSubject.value;
        this.progressesSubject.next([...current, newProgress]);
      })
    );
  }

  update(progress: UpdateProgress): Observable<Progress> {
    return this.http.put<Progress>(`${this.baseUrl}/${progress.progressId}`, progress).pipe(
      tap(updated => {
        const current = this.progressesSubject.value;
        const index = current.findIndex(p => p.progressId === updated.progressId);
        if (index !== -1) {
          current[index] = updated;
          this.progressesSubject.next([...current]);
        }
        this.currentProgressSubject.next(updated);
      })
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const current = this.progressesSubject.value;
        this.progressesSubject.next(current.filter(p => p.progressId !== id));
        if (this.currentProgressSubject.value?.progressId === id) {
          this.currentProgressSubject.next(null);
        }
      })
    );
  }
}
