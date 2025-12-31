import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { TrainingPlan, CreateTrainingPlanRequest, UpdateTrainingPlanRequest } from '../models';

@Injectable({
  providedIn: 'root'
})
export class TrainingPlanService {
  private readonly _http = inject(HttpClient);
  private readonly _baseUrl = environment.baseUrl;

  private readonly _trainingPlansSubject = new BehaviorSubject<TrainingPlan[]>([]);
  public readonly trainingPlans$ = this._trainingPlansSubject.asObservable();

  getTrainingPlans(): Observable<TrainingPlan[]> {
    return this._http.get<TrainingPlan[]>(`${this._baseUrl}/api/trainingplan`).pipe(
      tap(plans => this._trainingPlansSubject.next(plans))
    );
  }

  getTrainingPlanById(id: string): Observable<TrainingPlan> {
    return this._http.get<TrainingPlan>(`${this._baseUrl}/api/trainingplan/${id}`);
  }

  createTrainingPlan(request: CreateTrainingPlanRequest): Observable<TrainingPlan> {
    return this._http.post<TrainingPlan>(`${this._baseUrl}/api/trainingplan`, request).pipe(
      tap(newPlan => {
        const currentPlans = this._trainingPlansSubject.value;
        this._trainingPlansSubject.next([...currentPlans, newPlan]);
      })
    );
  }

  updateTrainingPlan(id: string, request: UpdateTrainingPlanRequest): Observable<TrainingPlan> {
    return this._http.put<TrainingPlan>(`${this._baseUrl}/api/trainingplan/${id}`, request).pipe(
      tap(updatedPlan => {
        const currentPlans = this._trainingPlansSubject.value;
        const index = currentPlans.findIndex(p => p.trainingPlanId === id);
        if (index !== -1) {
          currentPlans[index] = updatedPlan;
          this._trainingPlansSubject.next([...currentPlans]);
        }
      })
    );
  }

  deleteTrainingPlan(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/trainingplan/${id}`).pipe(
      tap(() => {
        const currentPlans = this._trainingPlansSubject.value;
        this._trainingPlansSubject.next(currentPlans.filter(p => p.trainingPlanId !== id));
      })
    );
  }
}
