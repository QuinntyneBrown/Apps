import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments';
import { AdminTask, CreateAdminTask, UpdateAdminTask } from '../models';

@Injectable({
  providedIn: 'root'
})
export class AdminTaskService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/admin-tasks`;

  private tasksSubject = new BehaviorSubject<AdminTask[]>([]);
  public tasks$ = this.tasksSubject.asObservable();

  getAll(): Observable<AdminTask[]> {
    return this.http.get<AdminTask[]>(this.baseUrl).pipe(
      tap(tasks => this.tasksSubject.next(tasks))
    );
  }

  getById(id: string): Observable<AdminTask> {
    return this.http.get<AdminTask>(`${this.baseUrl}/${id}`);
  }

  create(task: CreateAdminTask): Observable<AdminTask> {
    return this.http.post<AdminTask>(this.baseUrl, task).pipe(
      tap(newTask => {
        const tasks = this.tasksSubject.value;
        this.tasksSubject.next([...tasks, newTask]);
      })
    );
  }

  update(task: UpdateAdminTask): Observable<AdminTask> {
    return this.http.put<AdminTask>(`${this.baseUrl}/${task.adminTaskId}`, task).pipe(
      tap(updatedTask => {
        const tasks = this.tasksSubject.value;
        const index = tasks.findIndex(t => t.adminTaskId === updatedTask.adminTaskId);
        if (index !== -1) {
          tasks[index] = updatedTask;
          this.tasksSubject.next([...tasks]);
        }
      })
    );
  }

  complete(id: string): Observable<AdminTask> {
    return this.http.patch<AdminTask>(`${this.baseUrl}/${id}/complete`, {}).pipe(
      tap(updatedTask => {
        const tasks = this.tasksSubject.value;
        const index = tasks.findIndex(t => t.adminTaskId === updatedTask.adminTaskId);
        if (index !== -1) {
          tasks[index] = updatedTask;
          this.tasksSubject.next([...tasks]);
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const tasks = this.tasksSubject.value.filter(t => t.adminTaskId !== id);
        this.tasksSubject.next(tasks);
      })
    );
  }
}
