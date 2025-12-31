import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { GroceryList, CreateGroceryListRequest, UpdateGroceryListRequest } from '../models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GroceryListService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.apiUrl;

  private _groceryListsSubject = new BehaviorSubject<GroceryList[]>([]);
  groceryLists$ = this._groceryListsSubject.asObservable();

  getAll(userId?: string): Observable<GroceryList[]> {
    let url = `${this._baseUrl}/api/GroceryLists`;
    if (userId) {
      url += `?userId=${userId}`;
    }
    return this._http.get<GroceryList[]>(url).pipe(
      tap(lists => this._groceryListsSubject.next(lists))
    );
  }

  getById(id: string): Observable<GroceryList> {
    return this._http.get<GroceryList>(`${this._baseUrl}/api/GroceryLists/${id}`);
  }

  create(request: CreateGroceryListRequest): Observable<GroceryList> {
    return this._http.post<GroceryList>(`${this._baseUrl}/api/GroceryLists`, request).pipe(
      tap(newList => {
        const currentLists = this._groceryListsSubject.value;
        this._groceryListsSubject.next([...currentLists, newList]);
      })
    );
  }

  update(request: UpdateGroceryListRequest): Observable<GroceryList> {
    return this._http.put<GroceryList>(`${this._baseUrl}/api/GroceryLists/${request.groceryListId}`, request).pipe(
      tap(updatedList => {
        const currentLists = this._groceryListsSubject.value;
        const index = currentLists.findIndex(l => l.groceryListId === updatedList.groceryListId);
        if (index !== -1) {
          currentLists[index] = updatedList;
          this._groceryListsSubject.next([...currentLists]);
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/GroceryLists/${id}`).pipe(
      tap(() => {
        const currentLists = this._groceryListsSubject.value;
        this._groceryListsSubject.next(currentLists.filter(l => l.groceryListId !== id));
      })
    );
  }
}
