import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { PriorityTask, CreatePriorityTaskRequest, UpdatePriorityTaskRequest, Urgency, Importance, TaskStatus } from '../models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PriorityTaskService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.apiUrl;

  private _tasksSubject = new BehaviorSubject<PriorityTask[]>([]);
  tasks$ = this._tasksSubject.asObservable();

  getAll(urgency?: Urgency, importance?: Importance, status?: TaskStatus, categoryId?: string): Observable<PriorityTask[]> {
    let params = new HttpParams();
    if (urgency !== undefined) {
      params = params.set('urgency', urgency.toString());
    }
    if (importance !== undefined) {
      params = params.set('importance', importance.toString());
    }
    if (status !== undefined) {
      params = params.set('status', status.toString());
    }
    if (categoryId) {
      params = params.set('categoryId', categoryId);
    }
    return this._http.get<PriorityTask[]>(`${this._baseUrl}/api/PriorityTasks`, { params }).pipe(
      tap(tasks => this._tasksSubject.next(tasks))
    );
  }

  getById(id: string): Observable<PriorityTask> {
    return this._http.get<PriorityTask>(`${this._baseUrl}/api/PriorityTasks/${id}`);
  }

  create(request: CreatePriorityTaskRequest): Observable<PriorityTask> {
    return this._http.post<PriorityTask>(`${this._baseUrl}/api/PriorityTasks`, request).pipe(
      tap(newTask => {
        const currentTasks = this._tasksSubject.value;
        this._tasksSubject.next([...currentTasks, newTask]);
      })
    );
  }

  update(request: UpdatePriorityTaskRequest): Observable<PriorityTask> {
    return this._http.put<PriorityTask>(`${this._baseUrl}/api/PriorityTasks/${request.priorityTaskId}`, request).pipe(
      tap(updatedTask => {
        const currentTasks = this._tasksSubject.value;
        const index = currentTasks.findIndex(t => t.priorityTaskId === updatedTask.priorityTaskId);
        if (index !== -1) {
          currentTasks[index] = updatedTask;
          this._tasksSubject.next([...currentTasks]);
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/PriorityTasks/${id}`).pipe(
      tap(() => {
        const currentTasks = this._tasksSubject.value;
        this._tasksSubject.next(currentTasks.filter(t => t.priorityTaskId !== id));
      })
    );
  }

  updateStatus(task: PriorityTask, status: TaskStatus): Observable<PriorityTask> {
    const request: UpdatePriorityTaskRequest = {
      priorityTaskId: task.priorityTaskId,
      title: task.title,
      description: task.description,
      urgency: task.urgency,
      importance: task.importance,
      status: status,
      dueDate: task.dueDate,
      categoryId: task.categoryId
    };
    return this.update(request);
  }
}
