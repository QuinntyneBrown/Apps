import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Project } from '../models';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/projects`;

  private projectsSubject = new BehaviorSubject<Project[]>([]);
  public projects$ = this.projectsSubject.asObservable();

  private currentProjectSubject = new BehaviorSubject<Project | null>(null);
  public currentProject$ = this.currentProjectSubject.asObservable();

  getByUserId(userId: string): Observable<Project[]> {
    return this.http.get<Project[]>(`${this.baseUrl}?userId=${userId}`).pipe(
      tap(projects => this.projectsSubject.next(projects))
    );
  }

  getById(id: string): Observable<Project> {
    return this.http.get<Project>(`${this.baseUrl}/${id}`).pipe(
      tap(project => this.currentProjectSubject.next(project))
    );
  }

  create(project: Partial<Project>): Observable<Project> {
    return this.http.post<Project>(this.baseUrl, project).pipe(
      tap(newProject => {
        const current = this.projectsSubject.value;
        this.projectsSubject.next([...current, newProject]);
      })
    );
  }

  update(id: string, project: Partial<Project>): Observable<Project> {
    return this.http.put<Project>(`${this.baseUrl}/${id}`, { ...project, projectId: id }).pipe(
      tap(updatedProject => {
        const current = this.projectsSubject.value;
        const index = current.findIndex(p => p.projectId === id);
        if (index !== -1) {
          current[index] = updatedProject;
          this.projectsSubject.next([...current]);
        }
        this.currentProjectSubject.next(updatedProject);
      })
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const current = this.projectsSubject.value;
        this.projectsSubject.next(current.filter(p => p.projectId !== id));
        if (this.currentProjectSubject.value?.projectId === id) {
          this.currentProjectSubject.next(null);
        }
      })
    );
  }
}
