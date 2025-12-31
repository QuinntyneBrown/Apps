import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments';
import { Bankroll, CreateBankroll, UpdateBankroll } from '../models';

@Injectable({
  providedIn: 'root'
})
export class BankrollService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/bankrolls`;

  private bankrollsSubject = new BehaviorSubject<Bankroll[]>([]);
  public bankrolls$ = this.bankrollsSubject.asObservable();

  getBankrolls(): Observable<Bankroll[]> {
    return this.http.get<Bankroll[]>(this.baseUrl).pipe(
      tap(bankrolls => this.bankrollsSubject.next(bankrolls))
    );
  }

  getBankrollById(id: string): Observable<Bankroll> {
    return this.http.get<Bankroll>(`${this.baseUrl}/${id}`);
  }

  createBankroll(bankroll: CreateBankroll): Observable<Bankroll> {
    return this.http.post<Bankroll>(this.baseUrl, bankroll).pipe(
      tap(() => this.getBankrolls().subscribe())
    );
  }

  updateBankroll(bankroll: UpdateBankroll): Observable<Bankroll> {
    return this.http.put<Bankroll>(`${this.baseUrl}/${bankroll.bankrollId}`, bankroll).pipe(
      tap(() => this.getBankrolls().subscribe())
    );
  }

  deleteBankroll(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => this.getBankrolls().subscribe())
    );
  }
}
