import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { WorkLog, CreateWorkLogCommand, UpdateWorkLogCommand } from '../models';

@Injectable({
  providedIn: 'root'
})
export class WorkLogService {
  private readonly apiUrl = `${environment.baseUrl}/api/WorkLogs`;
  private workLogsSubject = new BehaviorSubject<WorkLog[]>([]);
  private currentWorkLogSubject = new BehaviorSubject<WorkLog | null>(null);

  public workLogs$ = this.workLogsSubject.asObservable();
  public currentWorkLog$ = this.currentWorkLogSubject.asObservable();

  constructor(private http: HttpClient) {}

  getWorkLogs(projectId?: string, userId?: string): Observable<WorkLog[]> {
    const params: any = {};
    if (projectId) params.projectId = projectId;
    if (userId) params.userId = userId;

    return this.http.get<WorkLog[]>(this.apiUrl, { params }).pipe(
      tap(workLogs => this.workLogsSubject.next(workLogs))
    );
  }

  getWorkLogById(id: string): Observable<WorkLog> {
    return this.http.get<WorkLog>(`${this.apiUrl}/${id}`).pipe(
      tap(workLog => this.currentWorkLogSubject.next(workLog))
    );
  }

  createWorkLog(command: CreateWorkLogCommand): Observable<WorkLog> {
    return this.http.post<WorkLog>(this.apiUrl, command).pipe(
      tap(workLog => {
        const workLogs = this.workLogsSubject.value;
        this.workLogsSubject.next([...workLogs, workLog]);
      })
    );
  }

  updateWorkLog(id: string, command: UpdateWorkLogCommand): Observable<WorkLog> {
    return this.http.put<WorkLog>(`${this.apiUrl}/${id}`, command).pipe(
      tap(workLog => {
        const workLogs = this.workLogsSubject.value;
        const index = workLogs.findIndex(w => w.workLogId === id);
        if (index !== -1) {
          workLogs[index] = workLog;
          this.workLogsSubject.next([...workLogs]);
        }
        this.currentWorkLogSubject.next(workLog);
      })
    );
  }

  deleteWorkLog(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => {
        const workLogs = this.workLogsSubject.value;
        this.workLogsSubject.next(workLogs.filter(w => w.workLogId !== id));
        if (this.currentWorkLogSubject.value?.workLogId === id) {
          this.currentWorkLogSubject.next(null);
        }
      })
    );
  }
}
