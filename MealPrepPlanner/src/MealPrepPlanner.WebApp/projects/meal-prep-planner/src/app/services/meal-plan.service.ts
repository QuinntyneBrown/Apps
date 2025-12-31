import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { MealPlan, CreateMealPlanCommand, UpdateMealPlanCommand } from '../models';

@Injectable({
  providedIn: 'root'
})
export class MealPlanService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/mealplans`;

  private mealPlansSubject = new BehaviorSubject<MealPlan[]>([]);
  public mealPlans$ = this.mealPlansSubject.asObservable();

  private selectedMealPlanSubject = new BehaviorSubject<MealPlan | null>(null);
  public selectedMealPlan$ = this.selectedMealPlanSubject.asObservable();

  private activeMealPlanSubject = new BehaviorSubject<MealPlan | null>(null);
  public activeMealPlan$ = this.activeMealPlanSubject.asObservable();

  getMealPlans(userId?: string, isActive?: boolean): Observable<MealPlan[]> {
    let params = new HttpParams();
    if (userId) params = params.set('userId', userId);
    if (isActive !== undefined) params = params.set('isActive', isActive.toString());

    return this.http.get<MealPlan[]>(this.baseUrl, { params }).pipe(
      tap(mealPlans => {
        this.mealPlansSubject.next(mealPlans);
        const active = mealPlans.find(mp => mp.isActive);
        if (active) {
          this.activeMealPlanSubject.next(active);
        }
      })
    );
  }

  getMealPlanById(mealPlanId: string): Observable<MealPlan> {
    return this.http.get<MealPlan>(`${this.baseUrl}/${mealPlanId}`).pipe(
      tap(mealPlan => this.selectedMealPlanSubject.next(mealPlan))
    );
  }

  createMealPlan(command: CreateMealPlanCommand): Observable<MealPlan> {
    return this.http.post<MealPlan>(this.baseUrl, command).pipe(
      tap(mealPlan => {
        const current = this.mealPlansSubject.value;
        this.mealPlansSubject.next([...current, mealPlan]);
      })
    );
  }

  updateMealPlan(mealPlanId: string, command: UpdateMealPlanCommand): Observable<MealPlan> {
    return this.http.put<MealPlan>(`${this.baseUrl}/${mealPlanId}`, command).pipe(
      tap(updatedMealPlan => {
        const current = this.mealPlansSubject.value;
        const index = current.findIndex(mp => mp.mealPlanId === mealPlanId);
        if (index !== -1) {
          const updated = [...current];
          updated[index] = updatedMealPlan;
          this.mealPlansSubject.next(updated);
        }
        this.selectedMealPlanSubject.next(updatedMealPlan);
        if (updatedMealPlan.isActive) {
          this.activeMealPlanSubject.next(updatedMealPlan);
        }
      })
    );
  }

  deleteMealPlan(mealPlanId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${mealPlanId}`).pipe(
      tap(() => {
        const current = this.mealPlansSubject.value;
        this.mealPlansSubject.next(current.filter(mp => mp.mealPlanId !== mealPlanId));
        if (this.selectedMealPlanSubject.value?.mealPlanId === mealPlanId) {
          this.selectedMealPlanSubject.next(null);
        }
        if (this.activeMealPlanSubject.value?.mealPlanId === mealPlanId) {
          this.activeMealPlanSubject.next(null);
        }
      })
    );
  }

  clearSelection(): void {
    this.selectedMealPlanSubject.next(null);
  }
}
