import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { Project, CreateProjectCommand, UpdateProjectCommand } from '../models';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {
  private readonly apiUrl = `${environment.baseUrl}/api/Projects`;
  private projectsSubject = new BehaviorSubject<Project[]>([]);
  private currentProjectSubject = new BehaviorSubject<Project | null>(null);

  public projects$ = this.projectsSubject.asObservable();
  public currentProject$ = this.currentProjectSubject.asObservable();

  constructor(private http: HttpClient) {}

  getProjects(userId?: string): Observable<Project[]> {
    const params = userId ? { userId } : {};
    return this.http.get<Project[]>(this.apiUrl, { params }).pipe(
      tap(projects => this.projectsSubject.next(projects))
    );
  }

  getProjectById(id: string): Observable<Project> {
    return this.http.get<Project>(`${this.apiUrl}/${id}`).pipe(
      tap(project => this.currentProjectSubject.next(project))
    );
  }

  createProject(command: CreateProjectCommand): Observable<Project> {
    return this.http.post<Project>(this.apiUrl, command).pipe(
      tap(project => {
        const projects = this.projectsSubject.value;
        this.projectsSubject.next([...projects, project]);
      })
    );
  }

  updateProject(id: string, command: UpdateProjectCommand): Observable<Project> {
    return this.http.put<Project>(`${this.apiUrl}/${id}`, command).pipe(
      tap(project => {
        const projects = this.projectsSubject.value;
        const index = projects.findIndex(p => p.projectId === id);
        if (index !== -1) {
          projects[index] = project;
          this.projectsSubject.next([...projects]);
        }
        this.currentProjectSubject.next(project);
      })
    );
  }

  deleteProject(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => {
        const projects = this.projectsSubject.value;
        this.projectsSubject.next(projects.filter(p => p.projectId !== id));
        if (this.currentProjectSubject.value?.projectId === id) {
          this.currentProjectSubject.next(null);
        }
      })
    );
  }
}
