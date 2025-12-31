import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../../environments';
import { Topic, CreateTopic, UpdateTopic } from '../models';

@Injectable({
  providedIn: 'root'
})
export class TopicService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/topics`;

  private topicsSubject = new BehaviorSubject<Topic[]>([]);
  public topics$ = this.topicsSubject.asObservable();

  getAll(): Observable<Topic[]> {
    return this.http.get<Topic[]>(this.baseUrl).pipe(
      tap(topics => this.topicsSubject.next(topics))
    );
  }

  getById(id: string): Observable<Topic> {
    return this.http.get<Topic>(`${this.baseUrl}/${id}`);
  }

  getByMeetingId(meetingId: string): Observable<Topic[]> {
    return this.http.get<Topic[]>(`${this.baseUrl}/meeting/${meetingId}`);
  }

  create(topic: CreateTopic): Observable<Topic> {
    return this.http.post<Topic>(this.baseUrl, topic).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  update(topic: UpdateTopic): Observable<Topic> {
    return this.http.put<Topic>(`${this.baseUrl}/${topic.topicId}`, topic).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => this.getAll().subscribe())
    );
  }
}
