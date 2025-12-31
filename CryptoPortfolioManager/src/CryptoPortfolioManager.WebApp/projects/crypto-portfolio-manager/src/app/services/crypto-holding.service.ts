import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { CryptoHolding, CreateCryptoHoldingRequest, UpdateCryptoHoldingRequest } from '../models';

@Injectable({
  providedIn: 'root'
})
export class CryptoHoldingService {
  private readonly apiUrl = `${environment.baseUrl}/api/cryptoholdings`;
  private cryptoHoldingsSubject = new BehaviorSubject<CryptoHolding[]>([]);
  public cryptoHoldings$ = this.cryptoHoldingsSubject.asObservable();

  private selectedCryptoHoldingSubject = new BehaviorSubject<CryptoHolding | null>(null);
  public selectedCryptoHolding$ = this.selectedCryptoHoldingSubject.asObservable();

  constructor(private http: HttpClient) {}

  getCryptoHoldings(walletId?: string): Observable<CryptoHolding[]> {
    const url = walletId ? `${this.apiUrl}?walletId=${walletId}` : this.apiUrl;
    return this.http.get<CryptoHolding[]>(url).pipe(
      tap(holdings => this.cryptoHoldingsSubject.next(holdings))
    );
  }

  getCryptoHoldingById(id: string): Observable<CryptoHolding> {
    return this.http.get<CryptoHolding>(`${this.apiUrl}/${id}`).pipe(
      tap(holding => this.selectedCryptoHoldingSubject.next(holding))
    );
  }

  createCryptoHolding(request: CreateCryptoHoldingRequest): Observable<CryptoHolding> {
    return this.http.post<CryptoHolding>(this.apiUrl, request).pipe(
      tap(holding => {
        const currentHoldings = this.cryptoHoldingsSubject.value;
        this.cryptoHoldingsSubject.next([...currentHoldings, holding]);
      })
    );
  }

  updateCryptoHolding(id: string, request: UpdateCryptoHoldingRequest): Observable<CryptoHolding> {
    return this.http.put<CryptoHolding>(`${this.apiUrl}/${id}`, request).pipe(
      tap(holding => {
        const currentHoldings = this.cryptoHoldingsSubject.value;
        const index = currentHoldings.findIndex(h => h.cryptoHoldingId === id);
        if (index !== -1) {
          currentHoldings[index] = holding;
          this.cryptoHoldingsSubject.next([...currentHoldings]);
        }
        if (this.selectedCryptoHoldingSubject.value?.cryptoHoldingId === id) {
          this.selectedCryptoHoldingSubject.next(holding);
        }
      })
    );
  }

  deleteCryptoHolding(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => {
        const currentHoldings = this.cryptoHoldingsSubject.value;
        this.cryptoHoldingsSubject.next(currentHoldings.filter(h => h.cryptoHoldingId !== id));
        if (this.selectedCryptoHoldingSubject.value?.cryptoHoldingId === id) {
          this.selectedCryptoHoldingSubject.next(null);
        }
      })
    );
  }
}
