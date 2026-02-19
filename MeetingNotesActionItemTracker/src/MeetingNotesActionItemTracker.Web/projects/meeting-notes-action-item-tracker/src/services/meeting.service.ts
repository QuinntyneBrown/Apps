import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments';
import { Meeting, CreateMeetingDto, UpdateMeetingDto } from '../models';

@Injectable({
  providedIn: 'root'
})
export class MeetingService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/meetings`;

  private meetingsSubject = new BehaviorSubject<Meeting[]>([]);
  public meetings$ = this.meetingsSubject.asObservable();

  getMeetings(): Observable<Meeting[]> {
    return this.http.get<Meeting[]>(this.baseUrl).pipe(
      tap(meetings => this.meetingsSubject.next(meetings))
    );
  }

  getMeetingById(id: string): Observable<Meeting> {
    return this.http.get<Meeting>(`${this.baseUrl}/${id}`);
  }

  createMeeting(dto: CreateMeetingDto): Observable<Meeting> {
    return this.http.post<Meeting>(this.baseUrl, dto).pipe(
      tap(meeting => {
        const meetings = [...this.meetingsSubject.value, meeting];
        this.meetingsSubject.next(meetings);
      })
    );
  }

  updateMeeting(dto: UpdateMeetingDto): Observable<Meeting> {
    return this.http.put<Meeting>(`${this.baseUrl}/${dto.meetingId}`, dto).pipe(
      tap(meeting => {
        const meetings = this.meetingsSubject.value.map(m =>
          m.meetingId === meeting.meetingId ? meeting : m
        );
        this.meetingsSubject.next(meetings);
      })
    );
  }

  deleteMeeting(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const meetings = this.meetingsSubject.value.filter(m => m.meetingId !== id);
        this.meetingsSubject.next(meetings);
      })
    );
  }
}
