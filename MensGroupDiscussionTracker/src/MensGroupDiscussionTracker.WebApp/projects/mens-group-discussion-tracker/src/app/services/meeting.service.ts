import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../../environments';
import { Meeting, CreateMeeting, UpdateMeeting } from '../models';

@Injectable({
  providedIn: 'root'
})
export class MeetingService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/meetings`;

  private meetingsSubject = new BehaviorSubject<Meeting[]>([]);
  public meetings$ = this.meetingsSubject.asObservable();

  getAll(): Observable<Meeting[]> {
    return this.http.get<Meeting[]>(this.baseUrl).pipe(
      tap(meetings => this.meetingsSubject.next(meetings))
    );
  }

  getById(id: string): Observable<Meeting> {
    return this.http.get<Meeting>(`${this.baseUrl}/${id}`);
  }

  getByGroupId(groupId: string): Observable<Meeting[]> {
    return this.http.get<Meeting[]>(`${this.baseUrl}/group/${groupId}`);
  }

  create(meeting: CreateMeeting): Observable<Meeting> {
    return this.http.post<Meeting>(this.baseUrl, meeting).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  update(meeting: UpdateMeeting): Observable<Meeting> {
    return this.http.put<Meeting>(`${this.baseUrl}/${meeting.meetingId}`, meeting).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => this.getAll().subscribe())
    );
  }
}
