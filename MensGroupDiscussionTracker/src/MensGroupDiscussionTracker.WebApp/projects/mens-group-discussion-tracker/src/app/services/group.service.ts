import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../../environments';
import { Group, CreateGroup, UpdateGroup } from '../models';

@Injectable({
  providedIn: 'root'
})
export class GroupService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/groups`;

  private groupsSubject = new BehaviorSubject<Group[]>([]);
  public groups$ = this.groupsSubject.asObservable();

  getAll(): Observable<Group[]> {
    return this.http.get<Group[]>(this.baseUrl).pipe(
      tap(groups => this.groupsSubject.next(groups))
    );
  }

  getById(id: string): Observable<Group> {
    return this.http.get<Group>(`${this.baseUrl}/${id}`);
  }

  create(group: CreateGroup): Observable<Group> {
    return this.http.post<Group>(this.baseUrl, group).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  update(group: UpdateGroup): Observable<Group> {
    return this.http.put<Group>(`${this.baseUrl}/${group.groupId}`, group).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => this.getAll().subscribe())
    );
  }
}
