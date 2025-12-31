import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import {
  ShoppingList,
  CreateShoppingListRequest,
  UpdateShoppingListRequest,
} from '../models';

@Injectable({
  providedIn: 'root',
})
export class ShoppingListService {
  private readonly apiUrl = `${environment.apiUrl}/api/shoppinglists`;
  private shoppingListsSubject = new BehaviorSubject<ShoppingList[]>([]);
  private loadingSubject = new BehaviorSubject<boolean>(false);

  shoppingLists$ = this.shoppingListsSubject.asObservable();
  loading$ = this.loadingSubject.asObservable();

  constructor(private http: HttpClient) {}

  getShoppingLists(userId?: string): Observable<ShoppingList[]> {
    this.loadingSubject.next(true);
    let params = new HttpParams();

    if (userId) params = params.set('userId', userId);

    return this.http.get<ShoppingList[]>(this.apiUrl, { params }).pipe(
      tap((shoppingLists) => {
        this.shoppingListsSubject.next(shoppingLists);
        this.loadingSubject.next(false);
      })
    );
  }

  getShoppingListById(shoppingListId: string): Observable<ShoppingList> {
    this.loadingSubject.next(true);
    return this.http.get<ShoppingList>(`${this.apiUrl}/${shoppingListId}`).pipe(
      tap(() => this.loadingSubject.next(false))
    );
  }

  createShoppingList(request: CreateShoppingListRequest): Observable<ShoppingList> {
    this.loadingSubject.next(true);
    return this.http.post<ShoppingList>(this.apiUrl, request).pipe(
      tap((shoppingList) => {
        const current = this.shoppingListsSubject.value;
        this.shoppingListsSubject.next([...current, shoppingList]);
        this.loadingSubject.next(false);
      })
    );
  }

  updateShoppingList(request: UpdateShoppingListRequest): Observable<ShoppingList> {
    this.loadingSubject.next(true);
    return this.http
      .put<ShoppingList>(`${this.apiUrl}/${request.shoppingListId}`, request)
      .pipe(
        tap((shoppingList) => {
          const current = this.shoppingListsSubject.value;
          const index = current.findIndex(
            (s) => s.shoppingListId === shoppingList.shoppingListId
          );
          if (index !== -1) {
            current[index] = shoppingList;
            this.shoppingListsSubject.next([...current]);
          }
          this.loadingSubject.next(false);
        })
      );
  }

  deleteShoppingList(shoppingListId: string): Observable<void> {
    this.loadingSubject.next(true);
    return this.http.delete<void>(`${this.apiUrl}/${shoppingListId}`).pipe(
      tap(() => {
        const current = this.shoppingListsSubject.value;
        this.shoppingListsSubject.next(
          current.filter((s) => s.shoppingListId !== shoppingListId)
        );
        this.loadingSubject.next(false);
      })
    );
  }
}
