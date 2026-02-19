import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments';
import { Interview, CreateInterview, UpdateInterview } from '../models';

@Injectable({
  providedIn: 'root'
})
export class InterviewService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/interviews`;

  private interviewsSubject = new BehaviorSubject<Interview[]>([]);
  public interviews$ = this.interviewsSubject.asObservable();

  getInterviews(): Observable<Interview[]> {
    return this.http.get<Interview[]>(this.baseUrl).pipe(
      tap(interviews => this.interviewsSubject.next(interviews))
    );
  }

  getInterviewById(id: string): Observable<Interview> {
    return this.http.get<Interview>(`${this.baseUrl}/${id}`);
  }

  createInterview(interview: CreateInterview): Observable<Interview> {
    return this.http.post<Interview>(this.baseUrl, interview).pipe(
      tap(newInterview => {
        const current = this.interviewsSubject.value;
        this.interviewsSubject.next([...current, newInterview]);
      })
    );
  }

  updateInterview(interview: UpdateInterview): Observable<Interview> {
    return this.http.put<Interview>(`${this.baseUrl}/${interview.interviewId}`, interview).pipe(
      tap(updatedInterview => {
        const current = this.interviewsSubject.value;
        const index = current.findIndex(i => i.interviewId === updatedInterview.interviewId);
        if (index !== -1) {
          current[index] = updatedInterview;
          this.interviewsSubject.next([...current]);
        }
      })
    );
  }

  deleteInterview(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const current = this.interviewsSubject.value;
        this.interviewsSubject.next(current.filter(i => i.interviewId !== id));
      })
    );
  }
}
