import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { Story, CreateStoryRequest, UpdateStoryRequest } from '../models';

@Injectable({
  providedIn: 'root'
})
export class StoryService {
  private readonly apiUrl = `${environment.baseUrl}/api/stories`;
  private storiesSubject = new BehaviorSubject<Story[]>([]);
  public stories$ = this.storiesSubject.asObservable();

  constructor(private http: HttpClient) {}

  getStories(personId?: string): Observable<Story[]> {
    const params = personId ? { personId } : {};
    return this.http.get<Story[]>(this.apiUrl, { params }).pipe(
      tap(stories => this.storiesSubject.next(stories))
    );
  }

  getStoryById(id: string): Observable<Story> {
    return this.http.get<Story>(`${this.apiUrl}/${id}`);
  }

  createStory(request: CreateStoryRequest): Observable<Story> {
    return this.http.post<Story>(this.apiUrl, request).pipe(
      tap(() => this.refreshStories())
    );
  }

  updateStory(id: string, request: UpdateStoryRequest): Observable<Story> {
    return this.http.put<Story>(`${this.apiUrl}/${id}`, request).pipe(
      tap(() => this.refreshStories())
    );
  }

  deleteStory(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => this.refreshStories())
    );
  }

  private refreshStories(): void {
    this.getStories().subscribe();
  }
}
