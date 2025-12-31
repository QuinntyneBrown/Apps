import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { Tag, CreateTag, UpdateTag } from '../models';

@Injectable({
  providedIn: 'root'
})
export class TagService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/tags`;

  private readonly tagsSubject = new BehaviorSubject<Tag[]>([]);
  public readonly tags$ = this.tagsSubject.asObservable();

  getAll(userId?: string): Observable<Tag[]> {
    let params = new HttpParams();
    if (userId) params = params.set('userId', userId);

    return this.http.get<Tag[]>(this.baseUrl, { params }).pipe(
      tap(tags => this.tagsSubject.next(tags))
    );
  }

  create(tag: CreateTag): Observable<Tag> {
    return this.http.post<Tag>(this.baseUrl, tag).pipe(
      tap(newTag => {
        const current = this.tagsSubject.value;
        this.tagsSubject.next([newTag, ...current]);
      })
    );
  }

  update(id: string, tag: UpdateTag): Observable<Tag> {
    return this.http.put<Tag>(`${this.baseUrl}/${id}`, tag).pipe(
      tap(updatedTag => {
        const current = this.tagsSubject.value;
        const index = current.findIndex(t => t.tagId === id);
        if (index !== -1) {
          current[index] = updatedTag;
          this.tagsSubject.next([...current]);
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const current = this.tagsSubject.value;
        this.tagsSubject.next(current.filter(t => t.tagId !== id));
      })
    );
  }
}
