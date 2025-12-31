import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Category } from '../models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class CategoriesService {
  private readonly _http = inject(HttpClient);
  private readonly _baseUrl = `${environment.baseUrl}/api/categories`;
  private readonly _categoriesSubject = new BehaviorSubject<Category[]>([]);

  public readonly categories$ = this._categoriesSubject.asObservable();

  public getAll(): Observable<Category[]> {
    return this._http.get<Category[]>(this._baseUrl).pipe(
      tap((categories) => this._categoriesSubject.next(categories))
    );
  }

  public getById(id: string): Observable<Category> {
    return this._http.get<Category>(`${this._baseUrl}/${id}`);
  }

  public create(category: Partial<Category>): Observable<Category> {
    return this._http.post<Category>(this._baseUrl, category).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  public update(id: string, category: Partial<Category>): Observable<Category> {
    return this._http.put<Category>(`${this._baseUrl}/${id}`, { ...category, categoryId: id }).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  public delete(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/${id}`).pipe(
      tap(() => this.getAll().subscribe())
    );
  }
}
