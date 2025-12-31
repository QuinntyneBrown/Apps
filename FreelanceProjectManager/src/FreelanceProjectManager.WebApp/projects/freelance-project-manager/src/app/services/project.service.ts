import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments/environment';
import { Project, CreateProjectRequest, UpdateProjectRequest } from '../models';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {
  private readonly apiUrl = `${environment.baseUrl}/api/Projects`;
  private projectsSubject = new BehaviorSubject<Project[]>([]);
  public projects$ = this.projectsSubject.asObservable();

  constructor(private http: HttpClient) {}

  getProjects(userId: string): Observable<Project[]> {
    return this.http.get<Project[]>(`${this.apiUrl}?userId=${userId}`).pipe(
      tap(projects => this.projectsSubject.next(projects))
    );
  }

  getProjectById(id: string, userId: string): Observable<Project> {
    return this.http.get<Project>(`${this.apiUrl}/${id}?userId=${userId}`);
  }

  createProject(request: CreateProjectRequest): Observable<Project> {
    return this.http.post<Project>(this.apiUrl, request).pipe(
      tap(project => {
        const current = this.projectsSubject.value;
        this.projectsSubject.next([...current, project]);
      })
    );
  }

  updateProject(id: string, request: UpdateProjectRequest): Observable<Project> {
    return this.http.put<Project>(`${this.apiUrl}/${id}`, request).pipe(
      tap(updatedProject => {
        const current = this.projectsSubject.value;
        const index = current.findIndex(p => p.projectId === id);
        if (index !== -1) {
          current[index] = updatedProject;
          this.projectsSubject.next([...current]);
        }
      })
    );
  }

  deleteProject(id: string, userId: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}?userId=${userId}`).pipe(
      tap(() => {
        const current = this.projectsSubject.value;
        this.projectsSubject.next(current.filter(p => p.projectId !== id));
      })
    );
  }
}
