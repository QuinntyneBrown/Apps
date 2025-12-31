import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { Group, CreateGroup, UpdateGroup } from '../models';

@Injectable({
  providedIn: 'root'
})
export class GroupsService {
  private readonly apiUrl = `${environment.baseUrl}/api/groups`;
  private groupsSubject = new BehaviorSubject<Group[]>([]);
  public groups$ = this.groupsSubject.asObservable();

  constructor(private http: HttpClient) {}

  getGroups(): Observable<Group[]> {
    return this.http.get<Group[]>(this.apiUrl).pipe(
      tap(groups => this.groupsSubject.next(groups))
    );
  }

  getGroup(id: string): Observable<Group> {
    return this.http.get<Group>(`${this.apiUrl}/${id}`);
  }

  createGroup(group: CreateGroup): Observable<Group> {
    return this.http.post<Group>(this.apiUrl, group).pipe(
      tap(() => this.getGroups().subscribe())
    );
  }

  updateGroup(id: string, group: UpdateGroup): Observable<Group> {
    return this.http.put<Group>(`${this.apiUrl}/${id}`, group).pipe(
      tap(() => this.getGroups().subscribe())
    );
  }

  deactivateGroup(id: string): Observable<Group> {
    return this.http.post<Group>(`${this.apiUrl}/${id}/deactivate`, {}).pipe(
      tap(() => this.getGroups().subscribe())
    );
  }

  deleteGroup(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => this.getGroups().subscribe())
    );
  }
}
