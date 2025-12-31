import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { RoutineTask, CreateRoutineTaskRequest, UpdateRoutineTaskRequest } from '../models';
import { environment } from '../environments';

@Injectable({
  providedIn: 'root'
})
export class RoutineTaskService {
  private http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/routinetasks`;

  private tasksSubject = new BehaviorSubject<RoutineTask[]>([]);
  public tasks$ = this.tasksSubject.asObservable();

  getAll(): Observable<RoutineTask[]> {
    return this.http.get<RoutineTask[]>(this.baseUrl).pipe(
      tap(tasks => this.tasksSubject.next(tasks))
    );
  }

  getById(id: string): Observable<RoutineTask> {
    return this.http.get<RoutineTask>(`${this.baseUrl}/${id}`);
  }

  getByRoutineId(routineId: string): Observable<RoutineTask[]> {
    return this.http.get<RoutineTask[]>(`${this.baseUrl}/routine/${routineId}`).pipe(
      tap(tasks => this.tasksSubject.next(tasks))
    );
  }

  create(request: CreateRoutineTaskRequest): Observable<RoutineTask> {
    return this.http.post<RoutineTask>(this.baseUrl, request).pipe(
      tap(task => {
        const current = this.tasksSubject.value;
        this.tasksSubject.next([...current, task]);
      })
    );
  }

  update(id: string, request: UpdateRoutineTaskRequest): Observable<RoutineTask> {
    return this.http.put<RoutineTask>(`${this.baseUrl}/${id}`, request).pipe(
      tap(updated => {
        const current = this.tasksSubject.value;
        const index = current.findIndex(t => t.routineTaskId === id);
        if (index !== -1) {
          current[index] = updated;
          this.tasksSubject.next([...current]);
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const current = this.tasksSubject.value;
        this.tasksSubject.next(current.filter(t => t.routineTaskId !== id));
      })
    );
  }
}
