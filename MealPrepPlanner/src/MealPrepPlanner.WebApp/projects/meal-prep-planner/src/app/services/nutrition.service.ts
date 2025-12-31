import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { Nutrition, CreateNutritionCommand, UpdateNutritionCommand } from '../models';

@Injectable({
  providedIn: 'root'
})
export class NutritionService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/nutritions`;

  private nutritionsSubject = new BehaviorSubject<Nutrition[]>([]);
  public nutritions$ = this.nutritionsSubject.asObservable();

  private selectedNutritionSubject = new BehaviorSubject<Nutrition | null>(null);
  public selectedNutrition$ = this.selectedNutritionSubject.asObservable();

  getNutritions(userId?: string, recipeId?: string): Observable<Nutrition[]> {
    let params = new HttpParams();
    if (userId) params = params.set('userId', userId);
    if (recipeId) params = params.set('recipeId', recipeId);

    return this.http.get<Nutrition[]>(this.baseUrl, { params }).pipe(
      tap(nutritions => this.nutritionsSubject.next(nutritions))
    );
  }

  getNutritionById(nutritionId: string): Observable<Nutrition> {
    return this.http.get<Nutrition>(`${this.baseUrl}/${nutritionId}`).pipe(
      tap(nutrition => this.selectedNutritionSubject.next(nutrition))
    );
  }

  createNutrition(command: CreateNutritionCommand): Observable<Nutrition> {
    return this.http.post<Nutrition>(this.baseUrl, command).pipe(
      tap(nutrition => {
        const current = this.nutritionsSubject.value;
        this.nutritionsSubject.next([...current, nutrition]);
      })
    );
  }

  updateNutrition(nutritionId: string, command: UpdateNutritionCommand): Observable<Nutrition> {
    return this.http.put<Nutrition>(`${this.baseUrl}/${nutritionId}`, command).pipe(
      tap(updatedNutrition => {
        const current = this.nutritionsSubject.value;
        const index = current.findIndex(n => n.nutritionId === nutritionId);
        if (index !== -1) {
          const updated = [...current];
          updated[index] = updatedNutrition;
          this.nutritionsSubject.next(updated);
        }
        this.selectedNutritionSubject.next(updatedNutrition);
      })
    );
  }

  deleteNutrition(nutritionId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${nutritionId}`).pipe(
      tap(() => {
        const current = this.nutritionsSubject.value;
        this.nutritionsSubject.next(current.filter(n => n.nutritionId !== nutritionId));
        if (this.selectedNutritionSubject.value?.nutritionId === nutritionId) {
          this.selectedNutritionSubject.next(null);
        }
      })
    );
  }

  clearSelection(): void {
    this.selectedNutritionSubject.next(null);
  }
}
