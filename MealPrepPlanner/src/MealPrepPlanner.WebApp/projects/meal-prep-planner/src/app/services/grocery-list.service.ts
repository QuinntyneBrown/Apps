import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { GroceryList, CreateGroceryListCommand, UpdateGroceryListCommand } from '../models';

@Injectable({
  providedIn: 'root'
})
export class GroceryListService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/grocerylists`;

  private groceryListsSubject = new BehaviorSubject<GroceryList[]>([]);
  public groceryLists$ = this.groceryListsSubject.asObservable();

  private selectedGroceryListSubject = new BehaviorSubject<GroceryList | null>(null);
  public selectedGroceryList$ = this.selectedGroceryListSubject.asObservable();

  getGroceryLists(userId?: string, mealPlanId?: string, isCompleted?: boolean): Observable<GroceryList[]> {
    let params = new HttpParams();
    if (userId) params = params.set('userId', userId);
    if (mealPlanId) params = params.set('mealPlanId', mealPlanId);
    if (isCompleted !== undefined) params = params.set('isCompleted', isCompleted.toString());

    return this.http.get<GroceryList[]>(this.baseUrl, { params }).pipe(
      tap(groceryLists => this.groceryListsSubject.next(groceryLists))
    );
  }

  getGroceryListById(groceryListId: string): Observable<GroceryList> {
    return this.http.get<GroceryList>(`${this.baseUrl}/${groceryListId}`).pipe(
      tap(groceryList => this.selectedGroceryListSubject.next(groceryList))
    );
  }

  createGroceryList(command: CreateGroceryListCommand): Observable<GroceryList> {
    return this.http.post<GroceryList>(this.baseUrl, command).pipe(
      tap(groceryList => {
        const current = this.groceryListsSubject.value;
        this.groceryListsSubject.next([...current, groceryList]);
      })
    );
  }

  updateGroceryList(groceryListId: string, command: UpdateGroceryListCommand): Observable<GroceryList> {
    return this.http.put<GroceryList>(`${this.baseUrl}/${groceryListId}`, command).pipe(
      tap(updatedGroceryList => {
        const current = this.groceryListsSubject.value;
        const index = current.findIndex(gl => gl.groceryListId === groceryListId);
        if (index !== -1) {
          const updated = [...current];
          updated[index] = updatedGroceryList;
          this.groceryListsSubject.next(updated);
        }
        this.selectedGroceryListSubject.next(updatedGroceryList);
      })
    );
  }

  deleteGroceryList(groceryListId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${groceryListId}`).pipe(
      tap(() => {
        const current = this.groceryListsSubject.value;
        this.groceryListsSubject.next(current.filter(gl => gl.groceryListId !== groceryListId));
        if (this.selectedGroceryListSubject.value?.groceryListId === groceryListId) {
          this.selectedGroceryListSubject.next(null);
        }
      })
    );
  }

  clearSelection(): void {
    this.selectedGroceryListSubject.next(null);
  }
}
