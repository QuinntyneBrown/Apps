import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Purchase, CreatePurchaseRequest } from '../models';

@Injectable({ providedIn: 'root' })
export class PurchasesService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = environment.apiBaseUrl;

  getPurchases(giftIdeaId?: string): Observable<Purchase[]> {
    let params = new HttpParams();
    if (giftIdeaId) {
      params = params.set('giftIdeaId', giftIdeaId);
    }
    return this.http.get<Purchase[]>(`${this.baseUrl}/api/purchases`, { params });
  }

  getPurchase(purchaseId: string): Observable<Purchase> {
    return this.http.get<Purchase>(`${this.baseUrl}/api/purchases/${purchaseId}`);
  }

  createPurchase(request: CreatePurchaseRequest): Observable<Purchase> {
    return this.http.post<Purchase>(`${this.baseUrl}/api/purchases`, request);
  }

  deletePurchase(purchaseId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/api/purchases/${purchaseId}`);
  }
}
