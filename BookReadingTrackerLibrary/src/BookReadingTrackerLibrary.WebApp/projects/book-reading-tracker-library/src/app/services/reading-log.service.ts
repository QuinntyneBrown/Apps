import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { ReadingLog } from '../models';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ReadingLogService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/readinglogs`;

  private readingLogsSubject = new BehaviorSubject<ReadingLog[]>([]);
  public readingLogs$ = this.readingLogsSubject.asObservable();

  getReadingLogs(userId?: string, bookId?: string): Observable<ReadingLog[]> {
    let url = this.baseUrl;
    const params: string[] = [];

    if (userId) params.push(`userId=${userId}`);
    if (bookId) params.push(`bookId=${bookId}`);

    if (params.length > 0) {
      url += `?${params.join('&')}`;
    }

    return this.http.get<ReadingLog[]>(url).pipe(
      tap(logs => this.readingLogsSubject.next(logs))
    );
  }

  getReadingLogById(readingLogId: string): Observable<ReadingLog> {
    return this.http.get<ReadingLog>(`${this.baseUrl}/${readingLogId}`);
  }

  createReadingLog(readingLog: Partial<ReadingLog>): Observable<ReadingLog> {
    return this.http.post<ReadingLog>(this.baseUrl, readingLog).pipe(
      tap(newLog => {
        const currentLogs = this.readingLogsSubject.value;
        this.readingLogsSubject.next([...currentLogs, newLog]);
      })
    );
  }

  updateReadingLog(readingLogId: string, readingLog: Partial<ReadingLog>): Observable<ReadingLog> {
    return this.http.put<ReadingLog>(`${this.baseUrl}/${readingLogId}`, { ...readingLog, readingLogId }).pipe(
      tap(updatedLog => {
        const currentLogs = this.readingLogsSubject.value;
        const index = currentLogs.findIndex(l => l.readingLogId === readingLogId);
        if (index !== -1) {
          const newLogs = [...currentLogs];
          newLogs[index] = updatedLog;
          this.readingLogsSubject.next(newLogs);
        }
      })
    );
  }

  deleteReadingLog(readingLogId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${readingLogId}`).pipe(
      tap(() => {
        const currentLogs = this.readingLogsSubject.value;
        this.readingLogsSubject.next(currentLogs.filter(l => l.readingLogId !== readingLogId));
      })
    );
  }
}
