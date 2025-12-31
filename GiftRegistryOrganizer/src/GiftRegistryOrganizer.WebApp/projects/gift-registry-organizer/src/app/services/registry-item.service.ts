import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { RegistryItem, Priority } from '../models';

export interface CreateRegistryItemRequest {
  registryId: string;
  name: string;
  description?: string;
  price?: number;
  url?: string;
  quantityDesired: number;
  priority: Priority;
}

export interface UpdateRegistryItemRequest {
  registryItemId: string;
  name: string;
  description?: string;
  price?: number;
  url?: string;
  quantityDesired: number;
  priority: Priority;
  isFulfilled: boolean;
}

@Injectable({
  providedIn: 'root'
})
export class RegistryItemService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.apiUrl}/api/registryitems`;

  private registryItemsSubject = new BehaviorSubject<RegistryItem[]>([]);
  private loadingSubject = new BehaviorSubject<boolean>(false);
  private selectedRegistryItemSubject = new BehaviorSubject<RegistryItem | null>(null);

  registryItems$ = this.registryItemsSubject.asObservable();
  loading$ = this.loadingSubject.asObservable();
  selectedRegistryItem$ = this.selectedRegistryItemSubject.asObservable();

  getRegistryItems(registryId?: string, isFulfilled?: boolean): Observable<RegistryItem[]> {
    this.loadingSubject.next(true);

    let params = new HttpParams();
    if (registryId) params = params.set('registryId', registryId);
    if (isFulfilled !== undefined) params = params.set('isFulfilled', isFulfilled.toString());

    return this.http.get<RegistryItem[]>(this.baseUrl, { params }).pipe(
      tap(items => {
        this.registryItemsSubject.next(items);
        this.loadingSubject.next(false);
      })
    );
  }

  getRegistryItemById(registryItemId: string): Observable<RegistryItem> {
    this.loadingSubject.next(true);

    return this.http.get<RegistryItem>(`${this.baseUrl}/${registryItemId}`).pipe(
      tap(item => {
        this.selectedRegistryItemSubject.next(item);
        this.loadingSubject.next(false);
      })
    );
  }

  createRegistryItem(request: CreateRegistryItemRequest): Observable<RegistryItem> {
    this.loadingSubject.next(true);

    return this.http.post<RegistryItem>(this.baseUrl, request).pipe(
      tap(item => {
        const currentItems = this.registryItemsSubject.value;
        this.registryItemsSubject.next([item, ...currentItems]);
        this.loadingSubject.next(false);
      })
    );
  }

  updateRegistryItem(registryItemId: string, request: UpdateRegistryItemRequest): Observable<RegistryItem> {
    this.loadingSubject.next(true);

    return this.http.put<RegistryItem>(`${this.baseUrl}/${registryItemId}`, request).pipe(
      tap(updatedItem => {
        const currentItems = this.registryItemsSubject.value;
        const index = currentItems.findIndex(i => i.registryItemId === registryItemId);
        if (index !== -1) {
          currentItems[index] = updatedItem;
          this.registryItemsSubject.next([...currentItems]);
        }
        if (this.selectedRegistryItemSubject.value?.registryItemId === registryItemId) {
          this.selectedRegistryItemSubject.next(updatedItem);
        }
        this.loadingSubject.next(false);
      })
    );
  }

  deleteRegistryItem(registryItemId: string): Observable<void> {
    this.loadingSubject.next(true);

    return this.http.delete<void>(`${this.baseUrl}/${registryItemId}`).pipe(
      tap(() => {
        const currentItems = this.registryItemsSubject.value;
        this.registryItemsSubject.next(currentItems.filter(i => i.registryItemId !== registryItemId));
        if (this.selectedRegistryItemSubject.value?.registryItemId === registryItemId) {
          this.selectedRegistryItemSubject.next(null);
        }
        this.loadingSubject.next(false);
      })
    );
  }

  setSelectedRegistryItem(item: RegistryItem | null): void {
    this.selectedRegistryItemSubject.next(item);
  }
}
