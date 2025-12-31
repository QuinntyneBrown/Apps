import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Receipt } from '../models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ReceiptService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;

  private _receipts$ = new BehaviorSubject<Receipt[]>([]);
  private _selectedReceipt$ = new BehaviorSubject<Receipt | null>(null);

  public readonly receipts$ = this._receipts$.asObservable();
  public readonly selectedReceipt$ = this._selectedReceipt$.asObservable();

  getAll(): Observable<Receipt[]> {
    return this._http.get<Receipt[]>(`${this._baseUrl}/api/receipts`).pipe(
      tap(receipts => this._receipts$.next(receipts))
    );
  }

  getById(id: string): Observable<Receipt> {
    return this._http.get<Receipt>(`${this._baseUrl}/api/receipts/${id}`).pipe(
      tap(receipt => this._selectedReceipt$.next(receipt))
    );
  }

  create(receipt: Partial<Receipt>): Observable<Receipt> {
    return this._http.post<Receipt>(`${this._baseUrl}/api/receipts`, receipt).pipe(
      tap(newReceipt => {
        const current = this._receipts$.value;
        this._receipts$.next([...current, newReceipt]);
      })
    );
  }

  update(id: string, receipt: Partial<Receipt>): Observable<Receipt> {
    return this._http.put<Receipt>(`${this._baseUrl}/api/receipts/${id}`, receipt).pipe(
      tap(updated => {
        const current = this._receipts$.value;
        const index = current.findIndex(r => r.receiptId === id);
        if (index !== -1) {
          current[index] = updated;
          this._receipts$.next([...current]);
        }
        this._selectedReceipt$.next(updated);
      })
    );
  }

  delete(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/receipts/${id}`).pipe(
      tap(() => {
        const current = this._receipts$.value;
        this._receipts$.next(current.filter(r => r.receiptId !== id));
        if (this._selectedReceipt$.value?.receiptId === id) {
          this._selectedReceipt$.next(null);
        }
      })
    );
  }

  setSelectedReceipt(receipt: Receipt | null): void {
    this._selectedReceipt$.next(receipt);
  }
}
