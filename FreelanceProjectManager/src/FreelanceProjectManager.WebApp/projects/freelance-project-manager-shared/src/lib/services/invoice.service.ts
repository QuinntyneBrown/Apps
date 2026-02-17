import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments/environment';
import { Invoice, CreateInvoiceRequest, UpdateInvoiceRequest } from '../models';

@Injectable({
  providedIn: 'root'
})
export class InvoiceService {
  private readonly apiUrl = `${environment.baseUrl}/api/Invoices`;
  private invoicesSubject = new BehaviorSubject<Invoice[]>([]);
  public invoices$ = this.invoicesSubject.asObservable();

  constructor(private http: HttpClient) {}

  getInvoices(userId: string): Observable<Invoice[]> {
    return this.http.get<Invoice[]>(`${this.apiUrl}?userId=${userId}`).pipe(
      tap(invoices => this.invoicesSubject.next(invoices))
    );
  }

  getInvoiceById(id: string, userId: string): Observable<Invoice> {
    return this.http.get<Invoice>(`${this.apiUrl}/${id}?userId=${userId}`);
  }

  createInvoice(request: CreateInvoiceRequest): Observable<Invoice> {
    return this.http.post<Invoice>(this.apiUrl, request).pipe(
      tap(invoice => {
        const current = this.invoicesSubject.value;
        this.invoicesSubject.next([...current, invoice]);
      })
    );
  }

  updateInvoice(id: string, request: UpdateInvoiceRequest): Observable<Invoice> {
    return this.http.put<Invoice>(`${this.apiUrl}/${id}`, request).pipe(
      tap(updatedInvoice => {
        const current = this.invoicesSubject.value;
        const index = current.findIndex(i => i.invoiceId === id);
        if (index !== -1) {
          current[index] = updatedInvoice;
          this.invoicesSubject.next([...current]);
        }
      })
    );
  }

  deleteInvoice(id: string, userId: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}?userId=${userId}`).pipe(
      tap(() => {
        const current = this.invoicesSubject.value;
        this.invoicesSubject.next(current.filter(i => i.invoiceId !== id));
      })
    );
  }
}
