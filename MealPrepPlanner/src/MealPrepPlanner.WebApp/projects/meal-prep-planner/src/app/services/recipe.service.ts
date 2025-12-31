import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { Recipe, CreateRecipeCommand, UpdateRecipeCommand } from '../models';

@Injectable({
  providedIn: 'root'
})
export class RecipeService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/recipes`;

  private recipesSubject = new BehaviorSubject<Recipe[]>([]);
  public recipes$ = this.recipesSubject.asObservable();

  private selectedRecipeSubject = new BehaviorSubject<Recipe | null>(null);
  public selectedRecipe$ = this.selectedRecipeSubject.asObservable();

  getRecipes(userId?: string, mealPlanId?: string, mealType?: string, isFavorite?: boolean): Observable<Recipe[]> {
    let params = new HttpParams();
    if (userId) params = params.set('userId', userId);
    if (mealPlanId) params = params.set('mealPlanId', mealPlanId);
    if (mealType) params = params.set('mealType', mealType);
    if (isFavorite !== undefined) params = params.set('isFavorite', isFavorite.toString());

    return this.http.get<Recipe[]>(this.baseUrl, { params }).pipe(
      tap(recipes => this.recipesSubject.next(recipes))
    );
  }

  getRecipeById(recipeId: string): Observable<Recipe> {
    return this.http.get<Recipe>(`${this.baseUrl}/${recipeId}`).pipe(
      tap(recipe => this.selectedRecipeSubject.next(recipe))
    );
  }

  createRecipe(command: CreateRecipeCommand): Observable<Recipe> {
    return this.http.post<Recipe>(this.baseUrl, command).pipe(
      tap(recipe => {
        const current = this.recipesSubject.value;
        this.recipesSubject.next([...current, recipe]);
      })
    );
  }

  updateRecipe(recipeId: string, command: UpdateRecipeCommand): Observable<Recipe> {
    return this.http.put<Recipe>(`${this.baseUrl}/${recipeId}`, command).pipe(
      tap(updatedRecipe => {
        const current = this.recipesSubject.value;
        const index = current.findIndex(r => r.recipeId === recipeId);
        if (index !== -1) {
          const updated = [...current];
          updated[index] = updatedRecipe;
          this.recipesSubject.next(updated);
        }
        this.selectedRecipeSubject.next(updatedRecipe);
      })
    );
  }

  deleteRecipe(recipeId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${recipeId}`).pipe(
      tap(() => {
        const current = this.recipesSubject.value;
        this.recipesSubject.next(current.filter(r => r.recipeId !== recipeId));
        if (this.selectedRecipeSubject.value?.recipeId === recipeId) {
          this.selectedRecipeSubject.next(null);
        }
      })
    );
  }

  clearSelection(): void {
    this.selectedRecipeSubject.next(null);
  }
}
