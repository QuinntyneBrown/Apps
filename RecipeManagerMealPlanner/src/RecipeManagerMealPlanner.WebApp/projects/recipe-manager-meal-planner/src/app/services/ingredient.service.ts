import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import {
  Ingredient,
  CreateIngredientRequest,
  UpdateIngredientRequest,
} from '../models';

@Injectable({
  providedIn: 'root',
})
export class IngredientService {
  private readonly apiUrl = `${environment.apiUrl}/api/ingredients`;
  private ingredientsSubject = new BehaviorSubject<Ingredient[]>([]);
  private loadingSubject = new BehaviorSubject<boolean>(false);

  ingredients$ = this.ingredientsSubject.asObservable();
  loading$ = this.loadingSubject.asObservable();

  constructor(private http: HttpClient) {}

  getIngredients(recipeId?: string): Observable<Ingredient[]> {
    this.loadingSubject.next(true);
    let params = new HttpParams();

    if (recipeId) params = params.set('recipeId', recipeId);

    return this.http.get<Ingredient[]>(this.apiUrl, { params }).pipe(
      tap((ingredients) => {
        this.ingredientsSubject.next(ingredients);
        this.loadingSubject.next(false);
      })
    );
  }

  getIngredientById(ingredientId: string): Observable<Ingredient> {
    this.loadingSubject.next(true);
    return this.http.get<Ingredient>(`${this.apiUrl}/${ingredientId}`).pipe(
      tap(() => this.loadingSubject.next(false))
    );
  }

  createIngredient(request: CreateIngredientRequest): Observable<Ingredient> {
    this.loadingSubject.next(true);
    return this.http.post<Ingredient>(this.apiUrl, request).pipe(
      tap((ingredient) => {
        const current = this.ingredientsSubject.value;
        this.ingredientsSubject.next([...current, ingredient]);
        this.loadingSubject.next(false);
      })
    );
  }

  updateIngredient(request: UpdateIngredientRequest): Observable<Ingredient> {
    this.loadingSubject.next(true);
    return this.http
      .put<Ingredient>(`${this.apiUrl}/${request.ingredientId}`, request)
      .pipe(
        tap((ingredient) => {
          const current = this.ingredientsSubject.value;
          const index = current.findIndex(
            (i) => i.ingredientId === ingredient.ingredientId
          );
          if (index !== -1) {
            current[index] = ingredient;
            this.ingredientsSubject.next([...current]);
          }
          this.loadingSubject.next(false);
        })
      );
  }

  deleteIngredient(ingredientId: string): Observable<void> {
    this.loadingSubject.next(true);
    return this.http.delete<void>(`${this.apiUrl}/${ingredientId}`).pipe(
      tap(() => {
        const current = this.ingredientsSubject.value;
        this.ingredientsSubject.next(
          current.filter((i) => i.ingredientId !== ingredientId)
        );
        this.loadingSubject.next(false);
      })
    );
  }
}
