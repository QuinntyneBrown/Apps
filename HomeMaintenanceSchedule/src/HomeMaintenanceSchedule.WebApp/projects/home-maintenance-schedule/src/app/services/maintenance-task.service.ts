import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments';
import { MaintenanceTask, CreateMaintenanceTask, UpdateMaintenanceTask, MaintenanceType, TaskStatus } from '../models';

@Injectable({ providedIn: 'root' })
export class MaintenanceTaskService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;

  private _tasksSubject = new BehaviorSubject<MaintenanceTask[]>([]);
  tasks$ = this._tasksSubject.asObservable();

  getAll(userId?: string, maintenanceType?: MaintenanceType, status?: TaskStatus, contractorId?: string): Observable<MaintenanceTask[]> {
    let params = new HttpParams();
    if (userId) params = params.set('userId', userId);
    if (maintenanceType !== undefined) params = params.set('maintenanceType', maintenanceType.toString());
    if (status !== undefined) params = params.set('status', status.toString());
    if (contractorId) params = params.set('contractorId', contractorId);

    return this._http.get<MaintenanceTask[]>(`${this._baseUrl}/api/MaintenanceTasks`, { params }).pipe(
      tap(tasks => this._tasksSubject.next(tasks))
    );
  }

  getById(id: string): Observable<MaintenanceTask> {
    return this._http.get<MaintenanceTask>(`${this._baseUrl}/api/MaintenanceTasks/${id}`);
  }

  create(task: CreateMaintenanceTask): Observable<MaintenanceTask> {
    return this._http.post<MaintenanceTask>(`${this._baseUrl}/api/MaintenanceTasks`, task).pipe(
      tap(created => {
        const current = this._tasksSubject.value;
        this._tasksSubject.next([...current, created]);
      })
    );
  }

  update(id: string, task: UpdateMaintenanceTask): Observable<MaintenanceTask> {
    return this._http.put<MaintenanceTask>(`${this._baseUrl}/api/MaintenanceTasks/${id}`, task).pipe(
      tap(updated => {
        const current = this._tasksSubject.value;
        const index = current.findIndex(t => t.maintenanceTaskId === id);
        if (index !== -1) {
          current[index] = updated;
          this._tasksSubject.next([...current]);
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/MaintenanceTasks/${id}`).pipe(
      tap(() => {
        const current = this._tasksSubject.value;
        this._tasksSubject.next(current.filter(t => t.maintenanceTaskId !== id));
      })
    );
  }
}
