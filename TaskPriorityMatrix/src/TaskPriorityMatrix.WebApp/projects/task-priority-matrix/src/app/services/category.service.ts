import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Category, CreateCategoryRequest, UpdateCategoryRequest } from '../models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.apiUrl;

  private _categoriesSubject = new BehaviorSubject<Category[]>([]);
  categories$ = this._categoriesSubject.asObservable();

  getAll(): Observable<Category[]> {
    return this._http.get<Category[]>(`${this._baseUrl}/api/Categories`).pipe(
      tap(categories => this._categoriesSubject.next(categories))
    );
  }

  getById(id: string): Observable<Category> {
    return this._http.get<Category>(`${this._baseUrl}/api/Categories/${id}`);
  }

  create(request: CreateCategoryRequest): Observable<Category> {
    return this._http.post<Category>(`${this._baseUrl}/api/Categories`, request).pipe(
      tap(newCategory => {
        const currentCategories = this._categoriesSubject.value;
        this._categoriesSubject.next([...currentCategories, newCategory]);
      })
    );
  }

  update(request: UpdateCategoryRequest): Observable<Category> {
    return this._http.put<Category>(`${this._baseUrl}/api/Categories/${request.categoryId}`, request).pipe(
      tap(updatedCategory => {
        const currentCategories = this._categoriesSubject.value;
        const index = currentCategories.findIndex(c => c.categoryId === updatedCategory.categoryId);
        if (index !== -1) {
          currentCategories[index] = updatedCategory;
          this._categoriesSubject.next([...currentCategories]);
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/Categories/${id}`).pipe(
      tap(() => {
        const currentCategories = this._categoriesSubject.value;
        this._categoriesSubject.next(currentCategories.filter(c => c.categoryId !== id));
      })
    );
  }
}
