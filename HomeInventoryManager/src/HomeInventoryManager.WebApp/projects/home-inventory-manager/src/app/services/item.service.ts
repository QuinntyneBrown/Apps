import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { Item, CreateItemCommand, UpdateItemCommand } from '../models';
import { Category } from '../models/category.enum';
import { Room } from '../models/room.enum';

@Injectable({
  providedIn: 'root'
})
export class ItemService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.apiUrl}/items`;

  private itemsSubject = new BehaviorSubject<Item[]>([]);
  public items$ = this.itemsSubject.asObservable();

  private selectedItemSubject = new BehaviorSubject<Item | null>(null);
  public selectedItem$ = this.selectedItemSubject.asObservable();

  private loadingSubject = new BehaviorSubject<boolean>(false);
  public loading$ = this.loadingSubject.asObservable();

  getItems(userId?: string, category?: Category, room?: Room): Observable<Item[]> {
    this.loadingSubject.next(true);

    let params = new HttpParams();
    if (userId) params = params.set('userId', userId);
    if (category !== undefined) params = params.set('category', category.toString());
    if (room !== undefined) params = params.set('room', room.toString());

    return this.http.get<Item[]>(this.baseUrl, { params }).pipe(
      tap(items => {
        this.itemsSubject.next(items);
        this.loadingSubject.next(false);
      })
    );
  }

  getItemById(itemId: string): Observable<Item> {
    this.loadingSubject.next(true);

    return this.http.get<Item>(`${this.baseUrl}/${itemId}`).pipe(
      tap(item => {
        this.selectedItemSubject.next(item);
        this.loadingSubject.next(false);
      })
    );
  }

  createItem(command: CreateItemCommand): Observable<Item> {
    this.loadingSubject.next(true);

    return this.http.post<Item>(this.baseUrl, command).pipe(
      tap(item => {
        const currentItems = this.itemsSubject.value;
        this.itemsSubject.next([...currentItems, item]);
        this.loadingSubject.next(false);
      })
    );
  }

  updateItem(itemId: string, command: UpdateItemCommand): Observable<Item> {
    this.loadingSubject.next(true);

    return this.http.put<Item>(`${this.baseUrl}/${itemId}`, command).pipe(
      tap(updatedItem => {
        const currentItems = this.itemsSubject.value;
        const index = currentItems.findIndex(i => i.itemId === itemId);
        if (index !== -1) {
          const newItems = [...currentItems];
          newItems[index] = updatedItem;
          this.itemsSubject.next(newItems);
        }
        this.selectedItemSubject.next(updatedItem);
        this.loadingSubject.next(false);
      })
    );
  }

  deleteItem(itemId: string): Observable<void> {
    this.loadingSubject.next(true);

    return this.http.delete<void>(`${this.baseUrl}/${itemId}`).pipe(
      tap(() => {
        const currentItems = this.itemsSubject.value;
        this.itemsSubject.next(currentItems.filter(i => i.itemId !== itemId));
        if (this.selectedItemSubject.value?.itemId === itemId) {
          this.selectedItemSubject.next(null);
        }
        this.loadingSubject.next(false);
      })
    );
  }

  clearSelection(): void {
    this.selectedItemSubject.next(null);
  }
}
