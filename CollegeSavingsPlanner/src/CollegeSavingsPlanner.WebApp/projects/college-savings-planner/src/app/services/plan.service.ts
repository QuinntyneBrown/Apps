import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { Plan, CreatePlan, UpdatePlan } from '../models';

@Injectable({
  providedIn: 'root'
})
export class PlanService {
  private apiUrl = `${environment.baseUrl}/api/plans`;
  private plansSubject = new BehaviorSubject<Plan[]>([]);
  public plans$ = this.plansSubject.asObservable();

  private selectedPlanSubject = new BehaviorSubject<Plan | null>(null);
  public selectedPlan$ = this.selectedPlanSubject.asObservable();

  constructor(private http: HttpClient) {}

  getPlans(): Observable<Plan[]> {
    return this.http.get<Plan[]>(this.apiUrl).pipe(
      tap(plans => this.plansSubject.next(plans))
    );
  }

  getPlanById(id: string): Observable<Plan> {
    return this.http.get<Plan>(`${this.apiUrl}/${id}`).pipe(
      tap(plan => this.selectedPlanSubject.next(plan))
    );
  }

  createPlan(plan: CreatePlan): Observable<Plan> {
    return this.http.post<Plan>(this.apiUrl, plan).pipe(
      tap(newPlan => {
        const currentPlans = this.plansSubject.value;
        this.plansSubject.next([...currentPlans, newPlan]);
      })
    );
  }

  updatePlan(id: string, plan: UpdatePlan): Observable<Plan> {
    return this.http.put<Plan>(`${this.apiUrl}/${id}`, plan).pipe(
      tap(updatedPlan => {
        const currentPlans = this.plansSubject.value;
        const index = currentPlans.findIndex(p => p.planId === id);
        if (index !== -1) {
          currentPlans[index] = updatedPlan;
          this.plansSubject.next([...currentPlans]);
        }
        if (this.selectedPlanSubject.value?.planId === id) {
          this.selectedPlanSubject.next(updatedPlan);
        }
      })
    );
  }

  deletePlan(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => {
        const currentPlans = this.plansSubject.value;
        this.plansSubject.next(currentPlans.filter(p => p.planId !== id));
        if (this.selectedPlanSubject.value?.planId === id) {
          this.selectedPlanSubject.next(null);
        }
      })
    );
  }

  clearSelectedPlan(): void {
    this.selectedPlanSubject.next(null);
  }
}
