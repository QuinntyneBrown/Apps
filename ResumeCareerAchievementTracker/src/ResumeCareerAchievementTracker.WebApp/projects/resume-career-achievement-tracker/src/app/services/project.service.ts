import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments';
import { Project, CreateProject, UpdateProject } from '../models';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/projects`;

  private projectsSubject = new BehaviorSubject<Project[]>([]);
  public projects$ = this.projectsSubject.asObservable();

  private loadingSubject = new BehaviorSubject<boolean>(false);
  public loading$ = this.loadingSubject.asObservable();

  getProjects(): Observable<Project[]> {
    this.loadingSubject.next(true);
    return this.http.get<Project[]>(this.baseUrl).pipe(
      tap(projects => {
        this.projectsSubject.next(projects);
        this.loadingSubject.next(false);
      })
    );
  }

  getProjectById(id: string): Observable<Project> {
    return this.http.get<Project>(`${this.baseUrl}/${id}`);
  }

  createProject(project: CreateProject): Observable<Project> {
    return this.http.post<Project>(this.baseUrl, project).pipe(
      tap(newProject => {
        const currentProjects = this.projectsSubject.value;
        this.projectsSubject.next([...currentProjects, newProject]);
      })
    );
  }

  updateProject(project: UpdateProject): Observable<Project> {
    return this.http.put<Project>(`${this.baseUrl}/${project.projectId}`, project).pipe(
      tap(updatedProject => {
        const currentProjects = this.projectsSubject.value;
        const index = currentProjects.findIndex(p => p.projectId === updatedProject.projectId);
        if (index !== -1) {
          currentProjects[index] = updatedProject;
          this.projectsSubject.next([...currentProjects]);
        }
      })
    );
  }

  deleteProject(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const currentProjects = this.projectsSubject.value;
        this.projectsSubject.next(currentProjects.filter(p => p.projectId !== id));
      })
    );
  }

  toggleFeatured(id: string): Observable<Project> {
    return this.http.patch<Project>(`${this.baseUrl}/${id}/toggle-featured`, {}).pipe(
      tap(updatedProject => {
        const currentProjects = this.projectsSubject.value;
        const index = currentProjects.findIndex(p => p.projectId === updatedProject.projectId);
        if (index !== -1) {
          currentProjects[index] = updatedProject;
          this.projectsSubject.next([...currentProjects]);
        }
      })
    );
  }
}
