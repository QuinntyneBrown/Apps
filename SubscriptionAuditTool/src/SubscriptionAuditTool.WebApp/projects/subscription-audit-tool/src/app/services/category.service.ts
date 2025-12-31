import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import {
  Category,
  CreateCategoryRequest,
  UpdateCategoryRequest
} from '../models';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;

  private _categoriesSubject = new BehaviorSubject<Category[]>([]);
  public categories$ = this._categoriesSubject.asObservable();

  getCategories(): Observable<Category[]> {
    return this._http.get<Category[]>(`${this._baseUrl}/api/categories`).pipe(
      tap(categories => this._categoriesSubject.next(categories))
    );
  }

  getCategoryById(categoryId: string): Observable<Category> {
    return this._http.get<Category>(`${this._baseUrl}/api/categories/${categoryId}`);
  }

  createCategory(request: CreateCategoryRequest): Observable<Category> {
    return this._http.post<Category>(`${this._baseUrl}/api/categories`, request).pipe(
      tap(category => {
        const current = this._categoriesSubject.value;
        this._categoriesSubject.next([...current, category]);
      })
    );
  }

  updateCategory(request: UpdateCategoryRequest): Observable<Category> {
    return this._http.put<Category>(
      `${this._baseUrl}/api/categories/${request.categoryId}`,
      request
    ).pipe(
      tap(updated => {
        const current = this._categoriesSubject.value;
        const index = current.findIndex(c => c.categoryId === updated.categoryId);
        if (index !== -1) {
          current[index] = updated;
          this._categoriesSubject.next([...current]);
        }
      })
    );
  }

  deleteCategory(categoryId: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/categories/${categoryId}`).pipe(
      tap(() => {
        const current = this._categoriesSubject.value;
        const filtered = current.filter(c => c.categoryId !== categoryId);
        this._categoriesSubject.next(filtered);
      })
    );
  }
}
