import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments/environment';
import { TimeEntry, CreateTimeEntryRequest, UpdateTimeEntryRequest } from '../models';

@Injectable({
  providedIn: 'root'
})
export class TimeEntryService {
  private readonly apiUrl = `${environment.baseUrl}/api/TimeEntries`;
  private timeEntriesSubject = new BehaviorSubject<TimeEntry[]>([]);
  public timeEntries$ = this.timeEntriesSubject.asObservable();

  constructor(private http: HttpClient) {}

  getTimeEntries(userId: string, projectId?: string): Observable<TimeEntry[]> {
    let url = `${this.apiUrl}?userId=${userId}`;
    if (projectId) {
      url += `&projectId=${projectId}`;
    }
    return this.http.get<TimeEntry[]>(url).pipe(
      tap(timeEntries => this.timeEntriesSubject.next(timeEntries))
    );
  }

  getTimeEntryById(id: string, userId: string): Observable<TimeEntry> {
    return this.http.get<TimeEntry>(`${this.apiUrl}/${id}?userId=${userId}`);
  }

  createTimeEntry(request: CreateTimeEntryRequest): Observable<TimeEntry> {
    return this.http.post<TimeEntry>(this.apiUrl, request).pipe(
      tap(timeEntry => {
        const current = this.timeEntriesSubject.value;
        this.timeEntriesSubject.next([...current, timeEntry]);
      })
    );
  }

  updateTimeEntry(id: string, request: UpdateTimeEntryRequest): Observable<TimeEntry> {
    return this.http.put<TimeEntry>(`${this.apiUrl}/${id}`, request).pipe(
      tap(updatedTimeEntry => {
        const current = this.timeEntriesSubject.value;
        const index = current.findIndex(t => t.timeEntryId === id);
        if (index !== -1) {
          current[index] = updatedTimeEntry;
          this.timeEntriesSubject.next([...current]);
        }
      })
    );
  }

  deleteTimeEntry(id: string, userId: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}?userId=${userId}`).pipe(
      tap(() => {
        const current = this.timeEntriesSubject.value;
        this.timeEntriesSubject.next(current.filter(t => t.timeEntryId !== id));
      })
    );
  }
}
