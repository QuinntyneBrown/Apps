import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import {
  MealPlan,
  CreateMealPlanRequest,
  UpdateMealPlanRequest,
} from '../models';

@Injectable({
  providedIn: 'root',
})
export class MealPlanService {
  private readonly apiUrl = `${environment.apiUrl}/api/mealplans`;
  private mealPlansSubject = new BehaviorSubject<MealPlan[]>([]);
  private loadingSubject = new BehaviorSubject<boolean>(false);

  mealPlans$ = this.mealPlansSubject.asObservable();
  loading$ = this.loadingSubject.asObservable();

  constructor(private http: HttpClient) {}

  getMealPlans(userId?: string, startDate?: string, endDate?: string): Observable<MealPlan[]> {
    this.loadingSubject.next(true);
    let params = new HttpParams();

    if (userId) params = params.set('userId', userId);
    if (startDate) params = params.set('startDate', startDate);
    if (endDate) params = params.set('endDate', endDate);

    return this.http.get<MealPlan[]>(this.apiUrl, { params }).pipe(
      tap((mealPlans) => {
        this.mealPlansSubject.next(mealPlans);
        this.loadingSubject.next(false);
      })
    );
  }

  getMealPlanById(mealPlanId: string): Observable<MealPlan> {
    this.loadingSubject.next(true);
    return this.http.get<MealPlan>(`${this.apiUrl}/${mealPlanId}`).pipe(
      tap(() => this.loadingSubject.next(false))
    );
  }

  createMealPlan(request: CreateMealPlanRequest): Observable<MealPlan> {
    this.loadingSubject.next(true);
    return this.http.post<MealPlan>(this.apiUrl, request).pipe(
      tap((mealPlan) => {
        const current = this.mealPlansSubject.value;
        this.mealPlansSubject.next([...current, mealPlan]);
        this.loadingSubject.next(false);
      })
    );
  }

  updateMealPlan(request: UpdateMealPlanRequest): Observable<MealPlan> {
    this.loadingSubject.next(true);
    return this.http.put<MealPlan>(`${this.apiUrl}/${request.mealPlanId}`, request).pipe(
      tap((mealPlan) => {
        const current = this.mealPlansSubject.value;
        const index = current.findIndex((m) => m.mealPlanId === mealPlan.mealPlanId);
        if (index !== -1) {
          current[index] = mealPlan;
          this.mealPlansSubject.next([...current]);
        }
        this.loadingSubject.next(false);
      })
    );
  }

  deleteMealPlan(mealPlanId: string): Observable<void> {
    this.loadingSubject.next(true);
    return this.http.delete<void>(`${this.apiUrl}/${mealPlanId}`).pipe(
      tap(() => {
        const current = this.mealPlansSubject.value;
        this.mealPlansSubject.next(
          current.filter((m) => m.mealPlanId !== mealPlanId)
        );
        this.loadingSubject.next(false);
      })
    );
  }
}
