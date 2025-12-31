import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import {
  Recipe,
  CreateRecipeRequest,
  UpdateRecipeRequest,
  Cuisine,
  DifficultyLevel,
} from '../models';

@Injectable({
  providedIn: 'root',
})
export class RecipeService {
  private readonly apiUrl = `${environment.apiUrl}/api/recipes`;
  private recipesSubject = new BehaviorSubject<Recipe[]>([]);
  private selectedRecipeSubject = new BehaviorSubject<Recipe | null>(null);
  private loadingSubject = new BehaviorSubject<boolean>(false);

  recipes$ = this.recipesSubject.asObservable();
  selectedRecipe$ = this.selectedRecipeSubject.asObservable();
  loading$ = this.loadingSubject.asObservable();

  constructor(private http: HttpClient) {}

  getRecipes(
    userId?: string,
    cuisine?: Cuisine,
    difficultyLevel?: DifficultyLevel,
    favoritesOnly?: boolean
  ): Observable<Recipe[]> {
    this.loadingSubject.next(true);
    let params = new HttpParams();

    if (userId) params = params.set('userId', userId);
    if (cuisine !== undefined) params = params.set('cuisine', cuisine.toString());
    if (difficultyLevel !== undefined)
      params = params.set('difficultyLevel', difficultyLevel.toString());
    if (favoritesOnly !== undefined)
      params = params.set('favoritesOnly', favoritesOnly.toString());

    return this.http.get<Recipe[]>(this.apiUrl, { params }).pipe(
      tap((recipes) => {
        this.recipesSubject.next(recipes);
        this.loadingSubject.next(false);
      })
    );
  }

  getRecipeById(recipeId: string): Observable<Recipe> {
    this.loadingSubject.next(true);
    return this.http.get<Recipe>(`${this.apiUrl}/${recipeId}`).pipe(
      tap((recipe) => {
        this.selectedRecipeSubject.next(recipe);
        this.loadingSubject.next(false);
      })
    );
  }

  createRecipe(request: CreateRecipeRequest): Observable<Recipe> {
    this.loadingSubject.next(true);
    return this.http.post<Recipe>(this.apiUrl, request).pipe(
      tap((recipe) => {
        const current = this.recipesSubject.value;
        this.recipesSubject.next([...current, recipe]);
        this.loadingSubject.next(false);
      })
    );
  }

  updateRecipe(request: UpdateRecipeRequest): Observable<Recipe> {
    this.loadingSubject.next(true);
    return this.http.put<Recipe>(`${this.apiUrl}/${request.recipeId}`, request).pipe(
      tap((recipe) => {
        const current = this.recipesSubject.value;
        const index = current.findIndex((r) => r.recipeId === recipe.recipeId);
        if (index !== -1) {
          current[index] = recipe;
          this.recipesSubject.next([...current]);
        }
        this.selectedRecipeSubject.next(recipe);
        this.loadingSubject.next(false);
      })
    );
  }

  deleteRecipe(recipeId: string): Observable<void> {
    this.loadingSubject.next(true);
    return this.http.delete<void>(`${this.apiUrl}/${recipeId}`).pipe(
      tap(() => {
        const current = this.recipesSubject.value;
        this.recipesSubject.next(current.filter((r) => r.recipeId !== recipeId));
        this.loadingSubject.next(false);
      })
    );
  }

  clearSelectedRecipe(): void {
    this.selectedRecipeSubject.next(null);
  }
}
