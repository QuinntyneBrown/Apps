import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Recipe, CreateRecipe, UpdateRecipe } from '../models';
import { environment } from '../../environments';

@Injectable({
  providedIn: 'root'
})
export class RecipeService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/recipes`;

  private recipesSubject = new BehaviorSubject<Recipe[]>([]);
  public recipes$ = this.recipesSubject.asObservable();

  getRecipes(): Observable<Recipe[]> {
    return this.http.get<Recipe[]>(this.baseUrl).pipe(
      tap(recipes => this.recipesSubject.next(recipes))
    );
  }

  getRecipeById(id: string): Observable<Recipe> {
    return this.http.get<Recipe>(`${this.baseUrl}/${id}`);
  }

  createRecipe(recipe: CreateRecipe): Observable<Recipe> {
    return this.http.post<Recipe>(this.baseUrl, recipe).pipe(
      tap(newRecipe => {
        const current = this.recipesSubject.value;
        this.recipesSubject.next([...current, newRecipe]);
      })
    );
  }

  updateRecipe(recipe: UpdateRecipe): Observable<Recipe> {
    return this.http.put<Recipe>(`${this.baseUrl}/${recipe.recipeId}`, recipe).pipe(
      tap(updated => {
        const current = this.recipesSubject.value;
        const index = current.findIndex(r => r.recipeId === updated.recipeId);
        if (index !== -1) {
          current[index] = updated;
          this.recipesSubject.next([...current]);
        }
      })
    );
  }

  deleteRecipe(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const current = this.recipesSubject.value;
        this.recipesSubject.next(current.filter(r => r.recipeId !== id));
      })
    );
  }
}
