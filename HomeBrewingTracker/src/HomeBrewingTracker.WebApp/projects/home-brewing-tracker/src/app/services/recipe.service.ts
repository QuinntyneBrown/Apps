import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { Recipe, CreateRecipeRequest, UpdateRecipeRequest } from '../models';

@Injectable({
  providedIn: 'root'
})
export class RecipeService {
  private readonly apiUrl = `${environment.baseUrl}/api/recipes`;
  private recipesSubject = new BehaviorSubject<Recipe[]>([]);
  public recipes$ = this.recipesSubject.asObservable();

  constructor(private http: HttpClient) {}

  getRecipes(userId?: string): Observable<Recipe[]> {
    const url = userId ? `${this.apiUrl}?userId=${userId}` : this.apiUrl;
    return this.http.get<Recipe[]>(url).pipe(
      tap(recipes => this.recipesSubject.next(recipes))
    );
  }

  getRecipe(id: string): Observable<Recipe> {
    return this.http.get<Recipe>(`${this.apiUrl}/${id}`);
  }

  createRecipe(request: CreateRecipeRequest): Observable<Recipe> {
    return this.http.post<Recipe>(this.apiUrl, request).pipe(
      tap(recipe => {
        const current = this.recipesSubject.value;
        this.recipesSubject.next([...current, recipe]);
      })
    );
  }

  updateRecipe(id: string, request: UpdateRecipeRequest): Observable<Recipe> {
    return this.http.put<Recipe>(`${this.apiUrl}/${id}`, request).pipe(
      tap(updatedRecipe => {
        const current = this.recipesSubject.value;
        const index = current.findIndex(r => r.recipeId === id);
        if (index !== -1) {
          current[index] = updatedRecipe;
          this.recipesSubject.next([...current]);
        }
      })
    );
  }

  deleteRecipe(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => {
        const current = this.recipesSubject.value;
        this.recipesSubject.next(current.filter(r => r.recipeId !== id));
      })
    );
  }
}
