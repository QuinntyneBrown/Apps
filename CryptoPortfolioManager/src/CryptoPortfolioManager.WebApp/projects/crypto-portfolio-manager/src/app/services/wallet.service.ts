import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { Wallet, CreateWalletRequest, UpdateWalletRequest } from '../models';

@Injectable({
  providedIn: 'root'
})
export class WalletService {
  private readonly apiUrl = `${environment.baseUrl}/api/wallets`;
  private walletsSubject = new BehaviorSubject<Wallet[]>([]);
  public wallets$ = this.walletsSubject.asObservable();

  private selectedWalletSubject = new BehaviorSubject<Wallet | null>(null);
  public selectedWallet$ = this.selectedWalletSubject.asObservable();

  constructor(private http: HttpClient) {}

  getWallets(): Observable<Wallet[]> {
    return this.http.get<Wallet[]>(this.apiUrl).pipe(
      tap(wallets => this.walletsSubject.next(wallets))
    );
  }

  getWalletById(id: string): Observable<Wallet> {
    return this.http.get<Wallet>(`${this.apiUrl}/${id}`).pipe(
      tap(wallet => this.selectedWalletSubject.next(wallet))
    );
  }

  createWallet(request: CreateWalletRequest): Observable<Wallet> {
    return this.http.post<Wallet>(this.apiUrl, request).pipe(
      tap(wallet => {
        const currentWallets = this.walletsSubject.value;
        this.walletsSubject.next([...currentWallets, wallet]);
      })
    );
  }

  updateWallet(id: string, request: UpdateWalletRequest): Observable<Wallet> {
    return this.http.put<Wallet>(`${this.apiUrl}/${id}`, request).pipe(
      tap(wallet => {
        const currentWallets = this.walletsSubject.value;
        const index = currentWallets.findIndex(w => w.walletId === id);
        if (index !== -1) {
          currentWallets[index] = wallet;
          this.walletsSubject.next([...currentWallets]);
        }
        if (this.selectedWalletSubject.value?.walletId === id) {
          this.selectedWalletSubject.next(wallet);
        }
      })
    );
  }

  deleteWallet(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => {
        const currentWallets = this.walletsSubject.value;
        this.walletsSubject.next(currentWallets.filter(w => w.walletId !== id));
        if (this.selectedWalletSubject.value?.walletId === id) {
          this.selectedWalletSubject.next(null);
        }
      })
    );
  }
}
