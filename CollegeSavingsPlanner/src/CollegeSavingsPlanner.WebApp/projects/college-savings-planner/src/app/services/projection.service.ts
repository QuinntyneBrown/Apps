import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { Projection, CreateProjection, UpdateProjection } from '../models';

@Injectable({
  providedIn: 'root'
})
export class ProjectionService {
  private apiUrl = `${environment.baseUrl}/api/projections`;
  private projectionsSubject = new BehaviorSubject<Projection[]>([]);
  public projections$ = this.projectionsSubject.asObservable();

  private selectedProjectionSubject = new BehaviorSubject<Projection | null>(null);
  public selectedProjection$ = this.selectedProjectionSubject.asObservable();

  constructor(private http: HttpClient) {}

  getProjections(planId?: string): Observable<Projection[]> {
    const url = planId ? `${this.apiUrl}?planId=${planId}` : this.apiUrl;
    return this.http.get<Projection[]>(url).pipe(
      tap(projections => this.projectionsSubject.next(projections))
    );
  }

  getProjectionById(id: string): Observable<Projection> {
    return this.http.get<Projection>(`${this.apiUrl}/${id}`).pipe(
      tap(projection => this.selectedProjectionSubject.next(projection))
    );
  }

  createProjection(projection: CreateProjection): Observable<Projection> {
    return this.http.post<Projection>(this.apiUrl, projection).pipe(
      tap(newProjection => {
        const currentProjections = this.projectionsSubject.value;
        this.projectionsSubject.next([...currentProjections, newProjection]);
      })
    );
  }

  updateProjection(id: string, projection: UpdateProjection): Observable<Projection> {
    return this.http.put<Projection>(`${this.apiUrl}/${id}`, projection).pipe(
      tap(updatedProjection => {
        const currentProjections = this.projectionsSubject.value;
        const index = currentProjections.findIndex(p => p.projectionId === id);
        if (index !== -1) {
          currentProjections[index] = updatedProjection;
          this.projectionsSubject.next([...currentProjections]);
        }
        if (this.selectedProjectionSubject.value?.projectionId === id) {
          this.selectedProjectionSubject.next(updatedProjection);
        }
      })
    );
  }

  deleteProjection(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => {
        const currentProjections = this.projectionsSubject.value;
        this.projectionsSubject.next(currentProjections.filter(p => p.projectionId !== id));
        if (this.selectedProjectionSubject.value?.projectionId === id) {
          this.selectedProjectionSubject.next(null);
        }
      })
    );
  }

  clearSelectedProjection(): void {
    this.selectedProjectionSubject.next(null);
  }
}
