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
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/projects`;

  private projectsSubject = new BehaviorSubject<Project[]>([]);
  public projects$ = this.projectsSubject.asObservable();

  private selectedProjectSubject = new BehaviorSubject<Project | null>(null);
  public selectedProject$ = this.selectedProjectSubject.asObservable();

  getProjects(): Observable<Project[]> {
    return this.http.get<Project[]>(this.baseUrl).pipe(
      tap(projects => this.projectsSubject.next(projects))
    );
  }

  getProjectById(id: string): Observable<Project> {
    return this.http.get<Project>(`${this.baseUrl}/${id}`).pipe(
      tap(project => this.selectedProjectSubject.next(project))
    );
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
        this.selectedProjectSubject.next(updatedProject);
      })
    );
  }

  deleteProject(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const currentProjects = this.projectsSubject.value;
        this.projectsSubject.next(currentProjects.filter(p => p.projectId !== id));
        if (this.selectedProjectSubject.value?.projectId === id) {
          this.selectedProjectSubject.next(null);
        }
      })
    );
  }

  selectProject(project: Project | null): void {
    this.selectedProjectSubject.next(project);
  }
}
