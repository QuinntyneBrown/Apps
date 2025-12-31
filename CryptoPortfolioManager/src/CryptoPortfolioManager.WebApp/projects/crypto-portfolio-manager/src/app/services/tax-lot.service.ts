import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { TaxLot, CreateTaxLotRequest, UpdateTaxLotRequest } from '../models';

@Injectable({
  providedIn: 'root'
})
export class TaxLotService {
  private readonly apiUrl = `${environment.baseUrl}/api/taxlots`;
  private taxLotsSubject = new BehaviorSubject<TaxLot[]>([]);
  public taxLots$ = this.taxLotsSubject.asObservable();

  private selectedTaxLotSubject = new BehaviorSubject<TaxLot | null>(null);
  public selectedTaxLot$ = this.selectedTaxLotSubject.asObservable();

  constructor(private http: HttpClient) {}

  getTaxLots(cryptoHoldingId?: string): Observable<TaxLot[]> {
    const url = cryptoHoldingId ? `${this.apiUrl}?cryptoHoldingId=${cryptoHoldingId}` : this.apiUrl;
    return this.http.get<TaxLot[]>(url).pipe(
      tap(taxLots => this.taxLotsSubject.next(taxLots))
    );
  }

  getTaxLotById(id: string): Observable<TaxLot> {
    return this.http.get<TaxLot>(`${this.apiUrl}/${id}`).pipe(
      tap(taxLot => this.selectedTaxLotSubject.next(taxLot))
    );
  }

  createTaxLot(request: CreateTaxLotRequest): Observable<TaxLot> {
    return this.http.post<TaxLot>(this.apiUrl, request).pipe(
      tap(taxLot => {
        const currentTaxLots = this.taxLotsSubject.value;
        this.taxLotsSubject.next([...currentTaxLots, taxLot]);
      })
    );
  }

  updateTaxLot(id: string, request: UpdateTaxLotRequest): Observable<TaxLot> {
    return this.http.put<TaxLot>(`${this.apiUrl}/${id}`, request).pipe(
      tap(taxLot => {
        const currentTaxLots = this.taxLotsSubject.value;
        const index = currentTaxLots.findIndex(t => t.taxLotId === id);
        if (index !== -1) {
          currentTaxLots[index] = taxLot;
          this.taxLotsSubject.next([...currentTaxLots]);
        }
        if (this.selectedTaxLotSubject.value?.taxLotId === id) {
          this.selectedTaxLotSubject.next(taxLot);
        }
      })
    );
  }

  deleteTaxLot(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => {
        const currentTaxLots = this.taxLotsSubject.value;
        this.taxLotsSubject.next(currentTaxLots.filter(t => t.taxLotId !== id));
        if (this.selectedTaxLotSubject.value?.taxLotId === id) {
          this.selectedTaxLotSubject.next(null);
        }
      })
    );
  }
}
