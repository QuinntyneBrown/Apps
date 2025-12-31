import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { PhotoLog, CreatePhotoLogCommand, UpdatePhotoLogCommand } from '../models';

@Injectable({
  providedIn: 'root'
})
export class PhotoLogService {
  private readonly apiUrl = `${environment.baseUrl}/api/PhotoLogs`;
  private photoLogsSubject = new BehaviorSubject<PhotoLog[]>([]);
  private currentPhotoLogSubject = new BehaviorSubject<PhotoLog | null>(null);

  public photoLogs$ = this.photoLogsSubject.asObservable();
  public currentPhotoLog$ = this.currentPhotoLogSubject.asObservable();

  constructor(private http: HttpClient) {}

  getPhotoLogs(projectId?: string, userId?: string): Observable<PhotoLog[]> {
    const params: any = {};
    if (projectId) params.projectId = projectId;
    if (userId) params.userId = userId;

    return this.http.get<PhotoLog[]>(this.apiUrl, { params }).pipe(
      tap(photoLogs => this.photoLogsSubject.next(photoLogs))
    );
  }

  getPhotoLogById(id: string): Observable<PhotoLog> {
    return this.http.get<PhotoLog>(`${this.apiUrl}/${id}`).pipe(
      tap(photoLog => this.currentPhotoLogSubject.next(photoLog))
    );
  }

  createPhotoLog(command: CreatePhotoLogCommand): Observable<PhotoLog> {
    return this.http.post<PhotoLog>(this.apiUrl, command).pipe(
      tap(photoLog => {
        const photoLogs = this.photoLogsSubject.value;
        this.photoLogsSubject.next([...photoLogs, photoLog]);
      })
    );
  }

  updatePhotoLog(id: string, command: UpdatePhotoLogCommand): Observable<PhotoLog> {
    return this.http.put<PhotoLog>(`${this.apiUrl}/${id}`, command).pipe(
      tap(photoLog => {
        const photoLogs = this.photoLogsSubject.value;
        const index = photoLogs.findIndex(p => p.photoLogId === id);
        if (index !== -1) {
          photoLogs[index] = photoLog;
          this.photoLogsSubject.next([...photoLogs]);
        }
        this.currentPhotoLogSubject.next(photoLog);
      })
    );
  }

  deletePhotoLog(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => {
        const photoLogs = this.photoLogsSubject.value;
        this.photoLogsSubject.next(photoLogs.filter(p => p.photoLogId !== id));
        if (this.currentPhotoLogSubject.value?.photoLogId === id) {
          this.currentPhotoLogSubject.next(null);
        }
      })
    );
  }
}
