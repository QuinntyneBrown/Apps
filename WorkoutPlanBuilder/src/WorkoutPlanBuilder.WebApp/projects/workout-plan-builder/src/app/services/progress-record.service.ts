import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { ProgressRecord } from '../models';

@Injectable({
  providedIn: 'root'
})
export class ProgressRecordService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/progressrecords`;

  private progressRecordsSubject = new BehaviorSubject<ProgressRecord[]>([]);
  public progressRecords$ = this.progressRecordsSubject.asObservable();

  getAll(): Observable<ProgressRecord[]> {
    return this.http.get<ProgressRecord[]>(this.baseUrl).pipe(
      tap(records => this.progressRecordsSubject.next(records))
    );
  }

  getById(id: string): Observable<ProgressRecord> {
    return this.http.get<ProgressRecord>(`${this.baseUrl}/${id}`);
  }

  create(record: Partial<ProgressRecord>): Observable<ProgressRecord> {
    return this.http.post<ProgressRecord>(this.baseUrl, record).pipe(
      tap(newRecord => {
        const currentRecords = this.progressRecordsSubject.value;
        this.progressRecordsSubject.next([...currentRecords, newRecord]);
      })
    );
  }

  update(id: string, record: Partial<ProgressRecord>): Observable<ProgressRecord> {
    return this.http.put<ProgressRecord>(`${this.baseUrl}/${id}`, record).pipe(
      tap(updatedRecord => {
        const currentRecords = this.progressRecordsSubject.value;
        const index = currentRecords.findIndex(r => r.progressRecordId === id);
        if (index !== -1) {
          currentRecords[index] = updatedRecord;
          this.progressRecordsSubject.next([...currentRecords]);
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const currentRecords = this.progressRecordsSubject.value;
        this.progressRecordsSubject.next(currentRecords.filter(r => r.progressRecordId !== id));
      })
    );
  }
}
