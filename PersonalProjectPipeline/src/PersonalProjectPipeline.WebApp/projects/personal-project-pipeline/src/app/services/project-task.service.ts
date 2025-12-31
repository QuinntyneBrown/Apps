import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments';
import { ProjectTask, CreateProjectTask, UpdateProjectTask } from '../models';

@Injectable({
  providedIn: 'root'
})
export class ProjectTaskService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/projecttasks`;

  private tasksSubject = new BehaviorSubject<ProjectTask[]>([]);
  public tasks$ = this.tasksSubject.asObservable();

  private selectedTaskSubject = new BehaviorSubject<ProjectTask | null>(null);
  public selectedTask$ = this.selectedTaskSubject.asObservable();

  getTasks(): Observable<ProjectTask[]> {
    return this.http.get<ProjectTask[]>(this.baseUrl).pipe(
      tap(tasks => this.tasksSubject.next(tasks))
    );
  }

  getTaskById(id: string): Observable<ProjectTask> {
    return this.http.get<ProjectTask>(`${this.baseUrl}/${id}`).pipe(
      tap(task => this.selectedTaskSubject.next(task))
    );
  }

  createTask(task: CreateProjectTask): Observable<ProjectTask> {
    return this.http.post<ProjectTask>(this.baseUrl, task).pipe(
      tap(newTask => {
        const currentTasks = this.tasksSubject.value;
        this.tasksSubject.next([...currentTasks, newTask]);
      })
    );
  }

  updateTask(task: UpdateProjectTask): Observable<ProjectTask> {
    return this.http.put<ProjectTask>(`${this.baseUrl}/${task.projectTaskId}`, task).pipe(
      tap(updatedTask => {
        const currentTasks = this.tasksSubject.value;
        const index = currentTasks.findIndex(t => t.projectTaskId === updatedTask.projectTaskId);
        if (index !== -1) {
          currentTasks[index] = updatedTask;
          this.tasksSubject.next([...currentTasks]);
        }
        this.selectedTaskSubject.next(updatedTask);
      })
    );
  }

  deleteTask(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const currentTasks = this.tasksSubject.value;
        this.tasksSubject.next(currentTasks.filter(t => t.projectTaskId !== id));
        if (this.selectedTaskSubject.value?.projectTaskId === id) {
          this.selectedTaskSubject.next(null);
        }
      })
    );
  }

  selectTask(task: ProjectTask | null): void {
    this.selectedTaskSubject.next(task);
  }
}
