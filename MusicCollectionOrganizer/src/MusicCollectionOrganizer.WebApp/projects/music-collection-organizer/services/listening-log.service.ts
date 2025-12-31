import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments';
import { ListeningLog, CreateListeningLog, UpdateListeningLog } from '../models';

@Injectable({
  providedIn: 'root'
})
export class ListeningLogService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/listeninglogs`;

  private listeningLogsSubject = new BehaviorSubject<ListeningLog[]>([]);
  public listeningLogs$ = this.listeningLogsSubject.asObservable();

  getListeningLogs(): Observable<ListeningLog[]> {
    return this.http.get<ListeningLog[]>(this.baseUrl).pipe(
      tap(logs => this.listeningLogsSubject.next(logs))
    );
  }

  getListeningLogById(id: string): Observable<ListeningLog> {
    return this.http.get<ListeningLog>(`${this.baseUrl}/${id}`);
  }

  createListeningLog(log: CreateListeningLog): Observable<ListeningLog> {
    return this.http.post<ListeningLog>(this.baseUrl, log).pipe(
      tap(newLog => {
        const currentLogs = this.listeningLogsSubject.value;
        this.listeningLogsSubject.next([...currentLogs, newLog]);
      })
    );
  }

  updateListeningLog(log: UpdateListeningLog): Observable<ListeningLog> {
    return this.http.put<ListeningLog>(`${this.baseUrl}/${log.listeningLogId}`, log).pipe(
      tap(updatedLog => {
        const currentLogs = this.listeningLogsSubject.value;
        const index = currentLogs.findIndex(l => l.listeningLogId === updatedLog.listeningLogId);
        if (index !== -1) {
          currentLogs[index] = updatedLog;
          this.listeningLogsSubject.next([...currentLogs]);
        }
      })
    );
  }

  deleteListeningLog(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const currentLogs = this.listeningLogsSubject.value;
        this.listeningLogsSubject.next(currentLogs.filter(l => l.listeningLogId !== id));
      })
    );
  }
}
