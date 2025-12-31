import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments';
import { Loan, CreateLoanCommand, UpdateLoanCommand } from '../models';

@Injectable({
  providedIn: 'root'
})
export class LoanService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/loans`;

  private loansSubject = new BehaviorSubject<Loan[]>([]);
  public loans$ = this.loansSubject.asObservable();

  private selectedLoanSubject = new BehaviorSubject<Loan | null>(null);
  public selectedLoan$ = this.selectedLoanSubject.asObservable();

  getAll(): Observable<Loan[]> {
    return this.http.get<Loan[]>(this.baseUrl).pipe(
      tap(loans => this.loansSubject.next(loans))
    );
  }

  getById(id: string): Observable<Loan> {
    return this.http.get<Loan>(`${this.baseUrl}/${id}`).pipe(
      tap(loan => this.selectedLoanSubject.next(loan))
    );
  }

  create(command: CreateLoanCommand): Observable<Loan> {
    return this.http.post<Loan>(this.baseUrl, command).pipe(
      tap(loan => {
        const currentLoans = this.loansSubject.value;
        this.loansSubject.next([...currentLoans, loan]);
      })
    );
  }

  update(command: UpdateLoanCommand): Observable<Loan> {
    return this.http.put<Loan>(`${this.baseUrl}/${command.loanId}`, command).pipe(
      tap(updatedLoan => {
        const currentLoans = this.loansSubject.value;
        const index = currentLoans.findIndex(l => l.loanId === updatedLoan.loanId);
        if (index !== -1) {
          currentLoans[index] = updatedLoan;
          this.loansSubject.next([...currentLoans]);
        }
        this.selectedLoanSubject.next(updatedLoan);
      })
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const currentLoans = this.loansSubject.value;
        this.loansSubject.next(currentLoans.filter(l => l.loanId !== id));
        if (this.selectedLoanSubject.value?.loanId === id) {
          this.selectedLoanSubject.next(null);
        }
      })
    );
  }

  clearSelection(): void {
    this.selectedLoanSubject.next(null);
  }
}
