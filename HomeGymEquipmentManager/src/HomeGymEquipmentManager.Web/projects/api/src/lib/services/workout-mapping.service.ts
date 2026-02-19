import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { WorkoutMapping } from '../models';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class WorkoutMappingService {
  private readonly apiUrl = `${environment.baseUrl}/api/WorkoutMapping`;
  private workoutMappingListSubject = new BehaviorSubject<WorkoutMapping[]>([]);
  private selectedWorkoutMappingSubject = new BehaviorSubject<WorkoutMapping | null>(null);

  workoutMappingList$ = this.workoutMappingListSubject.asObservable();
  selectedWorkoutMapping$ = this.selectedWorkoutMappingSubject.asObservable();

  constructor(private http: HttpClient) {}

  getAll(userId?: string, equipmentId?: string, isFavorite?: boolean): Observable<WorkoutMapping[]> {
    let params = new HttpParams();
    if (userId) params = params.set('userId', userId);
    if (equipmentId) params = params.set('equipmentId', equipmentId);
    if (isFavorite !== undefined) params = params.set('isFavorite', isFavorite.toString());

    return this.http.get<WorkoutMapping[]>(this.apiUrl, { params }).pipe(
      tap(workoutMappings => this.workoutMappingListSubject.next(workoutMappings))
    );
  }

  getById(id: string): Observable<WorkoutMapping> {
    return this.http.get<WorkoutMapping>(`${this.apiUrl}/${id}`).pipe(
      tap(workoutMapping => this.selectedWorkoutMappingSubject.next(workoutMapping))
    );
  }

  create(workoutMapping: WorkoutMapping): Observable<WorkoutMapping> {
    return this.http.post<WorkoutMapping>(this.apiUrl, workoutMapping).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  update(id: string, workoutMapping: WorkoutMapping): Observable<WorkoutMapping> {
    return this.http.put<WorkoutMapping>(`${this.apiUrl}/${id}`, workoutMapping).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => this.getAll().subscribe())
    );
  }
}
