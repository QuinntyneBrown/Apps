import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { GroceryItem, CreateGroceryItemRequest, UpdateGroceryItemRequest, Category } from '../models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GroceryItemService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.apiUrl;

  private _groceryItemsSubject = new BehaviorSubject<GroceryItem[]>([]);
  groceryItems$ = this._groceryItemsSubject.asObservable();

  getAll(groceryListId?: string, category?: Category): Observable<GroceryItem[]> {
    let params = new HttpParams();
    if (groceryListId) {
      params = params.set('groceryListId', groceryListId);
    }
    if (category) {
      params = params.set('category', category);
    }
    return this._http.get<GroceryItem[]>(`${this._baseUrl}/api/GroceryItems`, { params }).pipe(
      tap(items => this._groceryItemsSubject.next(items))
    );
  }

  getById(id: string): Observable<GroceryItem> {
    return this._http.get<GroceryItem>(`${this._baseUrl}/api/GroceryItems/${id}`);
  }

  create(request: CreateGroceryItemRequest): Observable<GroceryItem> {
    return this._http.post<GroceryItem>(`${this._baseUrl}/api/GroceryItems`, request).pipe(
      tap(newItem => {
        const currentItems = this._groceryItemsSubject.value;
        this._groceryItemsSubject.next([...currentItems, newItem]);
      })
    );
  }

  update(request: UpdateGroceryItemRequest): Observable<GroceryItem> {
    return this._http.put<GroceryItem>(`${this._baseUrl}/api/GroceryItems/${request.groceryItemId}`, request).pipe(
      tap(updatedItem => {
        const currentItems = this._groceryItemsSubject.value;
        const index = currentItems.findIndex(i => i.groceryItemId === updatedItem.groceryItemId);
        if (index !== -1) {
          currentItems[index] = updatedItem;
          this._groceryItemsSubject.next([...currentItems]);
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/GroceryItems/${id}`).pipe(
      tap(() => {
        const currentItems = this._groceryItemsSubject.value;
        this._groceryItemsSubject.next(currentItems.filter(i => i.groceryItemId !== id));
      })
    );
  }

  toggleChecked(item: GroceryItem): Observable<GroceryItem> {
    const request: UpdateGroceryItemRequest = {
      groceryItemId: item.groceryItemId,
      groceryListId: item.groceryListId,
      name: item.name,
      category: item.category,
      quantity: item.quantity,
      isChecked: !item.isChecked
    };
    return this.update(request);
  }
}
