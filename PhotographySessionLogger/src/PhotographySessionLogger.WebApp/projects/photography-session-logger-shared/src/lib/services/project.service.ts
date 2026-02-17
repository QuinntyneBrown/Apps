import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments';
import { Project, CreateProject, UpdateProject } from '../models';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/projects`;

  private projectsSubject = new BehaviorSubject<Project[]>([]);
  public projects$ = this.projectsSubject.asObservable();

  getAll(): Observable<Project[]> {
    return this.http.get<Project[]>(this.baseUrl).pipe(
      tap(projects => this.projectsSubject.next(projects))
    );
  }

  getById(id: string): Observable<Project> {
    return this.http.get<Project>(`${this.baseUrl}/${id}`);
  }

  create(project: CreateProject): Observable<Project> {
    return this.http.post<Project>(this.baseUrl, project).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  update(project: UpdateProject): Observable<Project> {
    return this.http.put<Project>(`${this.baseUrl}/${project.projectId}`, project).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => this.getAll().subscribe())
    );
  }
}
