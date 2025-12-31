import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Project } from '../models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;
  private _projectsSubject = new BehaviorSubject<Project[]>([]);

  projects$ = this._projectsSubject.asObservable();

  getProjects(): Observable<Project[]> {
    return this._http.get<Project[]>(`${this._baseUrl}/api/projects`).pipe(
      tap(projects => this._projectsSubject.next(projects))
    );
  }

  getProjectById(id: string): Observable<Project> {
    return this._http.get<Project>(`${this._baseUrl}/api/projects/${id}`);
  }

  createProject(project: Partial<Project>): Observable<Project> {
    return this._http.post<Project>(`${this._baseUrl}/api/projects`, project).pipe(
      tap(() => this.getProjects().subscribe())
    );
  }

  updateProject(id: string, project: Partial<Project>): Observable<Project> {
    return this._http.put<Project>(`${this._baseUrl}/api/projects/${id}`, project).pipe(
      tap(() => this.getProjects().subscribe())
    );
  }

  deleteProject(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/projects/${id}`).pipe(
      tap(() => this.getProjects().subscribe())
    );
  }
}
