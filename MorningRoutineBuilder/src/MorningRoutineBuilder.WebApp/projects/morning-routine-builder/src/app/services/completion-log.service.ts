import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { CompletionLog, CreateCompletionLogRequest, UpdateCompletionLogRequest } from '../models';
import { environment } from '../environments';

@Injectable({
  providedIn: 'root'
})
export class CompletionLogService {
  private http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/completionlogs`;

  private logsSubject = new BehaviorSubject<CompletionLog[]>([]);
  public logs$ = this.logsSubject.asObservable();

  getAll(): Observable<CompletionLog[]> {
    return this.http.get<CompletionLog[]>(this.baseUrl).pipe(
      tap(logs => this.logsSubject.next(logs))
    );
  }

  getById(id: string): Observable<CompletionLog> {
    return this.http.get<CompletionLog>(`${this.baseUrl}/${id}`);
  }

  getByRoutineId(routineId: string): Observable<CompletionLog[]> {
    return this.http.get<CompletionLog[]>(`${this.baseUrl}/routine/${routineId}`).pipe(
      tap(logs => this.logsSubject.next(logs))
    );
  }

  create(request: CreateCompletionLogRequest): Observable<CompletionLog> {
    return this.http.post<CompletionLog>(this.baseUrl, request).pipe(
      tap(log => {
        const current = this.logsSubject.value;
        this.logsSubject.next([...current, log]);
      })
    );
  }

  update(id: string, request: UpdateCompletionLogRequest): Observable<CompletionLog> {
    return this.http.put<CompletionLog>(`${this.baseUrl}/${id}`, request).pipe(
      tap(updated => {
        const current = this.logsSubject.value;
        const index = current.findIndex(l => l.completionLogId === id);
        if (index !== -1) {
          current[index] = updated;
          this.logsSubject.next([...current]);
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const current = this.logsSubject.value;
        this.logsSubject.next(current.filter(l => l.completionLogId !== id));
      })
    );
  }
}
