import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Purchase } from '../models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PurchaseService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;

  private _purchases$ = new BehaviorSubject<Purchase[]>([]);
  private _selectedPurchase$ = new BehaviorSubject<Purchase | null>(null);

  public readonly purchases$ = this._purchases$.asObservable();
  public readonly selectedPurchase$ = this._selectedPurchase$.asObservable();

  getAll(): Observable<Purchase[]> {
    return this._http.get<Purchase[]>(`${this._baseUrl}/api/purchases`).pipe(
      tap(purchases => this._purchases$.next(purchases))
    );
  }

  getById(id: string): Observable<Purchase> {
    return this._http.get<Purchase>(`${this._baseUrl}/api/purchases/${id}`).pipe(
      tap(purchase => this._selectedPurchase$.next(purchase))
    );
  }

  create(purchase: Partial<Purchase>): Observable<Purchase> {
    return this._http.post<Purchase>(`${this._baseUrl}/api/purchases`, purchase).pipe(
      tap(newPurchase => {
        const current = this._purchases$.value;
        this._purchases$.next([...current, newPurchase]);
      })
    );
  }

  update(id: string, purchase: Partial<Purchase>): Observable<Purchase> {
    return this._http.put<Purchase>(`${this._baseUrl}/api/purchases/${id}`, purchase).pipe(
      tap(updated => {
        const current = this._purchases$.value;
        const index = current.findIndex(p => p.purchaseId === id);
        if (index !== -1) {
          current[index] = updated;
          this._purchases$.next([...current]);
        }
        this._selectedPurchase$.next(updated);
      })
    );
  }

  delete(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/purchases/${id}`).pipe(
      tap(() => {
        const current = this._purchases$.value;
        this._purchases$.next(current.filter(p => p.purchaseId !== id));
        if (this._selectedPurchase$.value?.purchaseId === id) {
          this._selectedPurchase$.next(null);
        }
      })
    );
  }

  setSelectedPurchase(purchase: Purchase | null): void {
    this._selectedPurchase$.next(purchase);
  }
}
