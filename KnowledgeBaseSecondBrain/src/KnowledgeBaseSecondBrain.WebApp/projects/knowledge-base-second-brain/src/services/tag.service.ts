import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments';
import { Tag, CreateTagCommand, UpdateTagCommand } from '../models';

@Injectable({
  providedIn: 'root'
})
export class TagService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/tags`;

  private tagsSubject = new BehaviorSubject<Tag[]>([]);
  public tags$ = this.tagsSubject.asObservable();

  getTags(): Observable<Tag[]> {
    return this.http.get<Tag[]>(this.baseUrl).pipe(
      tap(tags => this.tagsSubject.next(tags))
    );
  }

  getTagById(id: string): Observable<Tag> {
    return this.http.get<Tag>(`${this.baseUrl}/${id}`);
  }

  createTag(command: CreateTagCommand): Observable<Tag> {
    return this.http.post<Tag>(this.baseUrl, command).pipe(
      tap(tag => {
        const tags = this.tagsSubject.value;
        this.tagsSubject.next([...tags, tag]);
      })
    );
  }

  updateTag(command: UpdateTagCommand): Observable<Tag> {
    return this.http.put<Tag>(`${this.baseUrl}/${command.tagId}`, command).pipe(
      tap(updatedTag => {
        const tags = this.tagsSubject.value.map(tag =>
          tag.tagId === updatedTag.tagId ? updatedTag : tag
        );
        this.tagsSubject.next(tags);
      })
    );
  }

  deleteTag(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const tags = this.tagsSubject.value.filter(tag => tag.tagId !== id);
        this.tagsSubject.next(tags);
      })
    );
  }
}
