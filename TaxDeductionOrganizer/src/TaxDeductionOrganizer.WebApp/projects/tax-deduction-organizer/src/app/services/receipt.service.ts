import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Receipt, CreateReceipt, UpdateReceipt } from '../models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ReceiptService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;

  private _receiptsSubject = new BehaviorSubject<Receipt[]>([]);
  public receipts$ = this._receiptsSubject.asObservable();

  getAll(): Observable<Receipt[]> {
    return this._http.get<Receipt[]>(`${this._baseUrl}/api/receipts`).pipe(
      tap(receipts => this._receiptsSubject.next(receipts))
    );
  }

  getById(id: string): Observable<Receipt> {
    return this._http.get<Receipt>(`${this._baseUrl}/api/receipts/${id}`);
  }

  getByDeduction(deductionId: string): Observable<Receipt[]> {
    return this._http.get<Receipt[]>(`${this._baseUrl}/api/receipts?deductionId=${deductionId}`).pipe(
      tap(receipts => this._receiptsSubject.next(receipts))
    );
  }

  create(receipt: CreateReceipt): Observable<Receipt> {
    return this._http.post<Receipt>(`${this._baseUrl}/api/receipts`, receipt).pipe(
      tap(newReceipt => {
        const current = this._receiptsSubject.value;
        this._receiptsSubject.next([...current, newReceipt]);
      })
    );
  }

  update(receipt: UpdateReceipt): Observable<Receipt> {
    return this._http.put<Receipt>(`${this._baseUrl}/api/receipts/${receipt.receiptId}`, receipt).pipe(
      tap(updated => {
        const current = this._receiptsSubject.value;
        const index = current.findIndex(r => r.receiptId === updated.receiptId);
        if (index !== -1) {
          current[index] = updated;
          this._receiptsSubject.next([...current]);
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/receipts/${id}`).pipe(
      tap(() => {
        const current = this._receiptsSubject.value;
        this._receiptsSubject.next(current.filter(r => r.receiptId !== id));
      })
    );
  }
}
