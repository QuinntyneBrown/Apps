import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Activity, CreateActivity, UpdateActivity } from '../models';
import { environment } from '../environments';

@Injectable({
  providedIn: 'root'
})
export class ActivityService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/activities`;

  private activitiesSubject = new BehaviorSubject<Activity[]>([]);
  public activities$ = this.activitiesSubject.asObservable();

  loadActivities(): Observable<Activity[]> {
    return this.http.get<Activity[]>(this.baseUrl).pipe(
      tap(activities => this.activitiesSubject.next(activities))
    );
  }

  getActivityById(id: string): Observable<Activity> {
    return this.http.get<Activity>(`${this.baseUrl}/${id}`);
  }

  createActivity(activity: CreateActivity): Observable<Activity> {
    return this.http.post<Activity>(this.baseUrl, activity).pipe(
      tap(() => this.loadActivities().subscribe())
    );
  }

  updateActivity(activity: UpdateActivity): Observable<Activity> {
    return this.http.put<Activity>(`${this.baseUrl}/${activity.activityId}`, activity).pipe(
      tap(() => this.loadActivities().subscribe())
    );
  }

  deleteActivity(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => this.loadActivities().subscribe())
    );
  }
}
