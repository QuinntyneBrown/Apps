import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Store, CreateStoreRequest, UpdateStoreRequest } from '../models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class StoreService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.apiUrl;

  private _storesSubject = new BehaviorSubject<Store[]>([]);
  stores$ = this._storesSubject.asObservable();

  getAll(): Observable<Store[]> {
    return this._http.get<Store[]>(`${this._baseUrl}/api/Stores`).pipe(
      tap(stores => this._storesSubject.next(stores))
    );
  }

  getById(id: string): Observable<Store> {
    return this._http.get<Store>(`${this._baseUrl}/api/Stores/${id}`);
  }

  create(request: CreateStoreRequest): Observable<Store> {
    return this._http.post<Store>(`${this._baseUrl}/api/Stores`, request).pipe(
      tap(newStore => {
        const currentStores = this._storesSubject.value;
        this._storesSubject.next([...currentStores, newStore]);
      })
    );
  }

  update(request: UpdateStoreRequest): Observable<Store> {
    return this._http.put<Store>(`${this._baseUrl}/api/Stores/${request.storeId}`, request).pipe(
      tap(updatedStore => {
        const currentStores = this._storesSubject.value;
        const index = currentStores.findIndex(s => s.storeId === updatedStore.storeId);
        if (index !== -1) {
          currentStores[index] = updatedStore;
          this._storesSubject.next([...currentStores]);
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/Stores/${id}`).pipe(
      tap(() => {
        const currentStores = this._storesSubject.value;
        this._storesSubject.next(currentStores.filter(s => s.storeId !== id));
      })
    );
  }
}
