import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { Tag, CreateTagCommand } from '../models';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TagService {
  private tagsSubject = new BehaviorSubject<Tag[]>([]);
  public tags$ = this.tagsSubject.asObservable();

  private baseUrl = `${environment.baseUrl}/api/Tags`;

  constructor(private http: HttpClient) {}

  loadTags(userId?: string): void {
    const url = userId ? `${this.baseUrl}?userId=${userId}` : this.baseUrl;
    this.http.get<Tag[]>(url).subscribe(tags => {
      this.tagsSubject.next(tags);
    });
  }

  createTag(command: CreateTagCommand): Observable<Tag> {
    return this.http.post<Tag>(this.baseUrl, command);
  }

  deleteTag(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}
