import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments';
import { Offer, CreateOfferCommand, UpdateOfferCommand } from '../models';

@Injectable({
  providedIn: 'root'
})
export class OfferService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/offers`;

  private offersSubject = new BehaviorSubject<Offer[]>([]);
  public offers$ = this.offersSubject.asObservable();

  private selectedOfferSubject = new BehaviorSubject<Offer | null>(null);
  public selectedOffer$ = this.selectedOfferSubject.asObservable();

  getAll(): Observable<Offer[]> {
    return this.http.get<Offer[]>(this.baseUrl).pipe(
      tap(offers => this.offersSubject.next(offers))
    );
  }

  getById(id: string): Observable<Offer> {
    return this.http.get<Offer>(`${this.baseUrl}/${id}`).pipe(
      tap(offer => this.selectedOfferSubject.next(offer))
    );
  }

  create(command: CreateOfferCommand): Observable<Offer> {
    return this.http.post<Offer>(this.baseUrl, command).pipe(
      tap(offer => {
        const currentOffers = this.offersSubject.value;
        this.offersSubject.next([...currentOffers, offer]);
      })
    );
  }

  update(command: UpdateOfferCommand): Observable<Offer> {
    return this.http.put<Offer>(`${this.baseUrl}/${command.offerId}`, command).pipe(
      tap(updatedOffer => {
        const currentOffers = this.offersSubject.value;
        const index = currentOffers.findIndex(o => o.offerId === updatedOffer.offerId);
        if (index !== -1) {
          currentOffers[index] = updatedOffer;
          this.offersSubject.next([...currentOffers]);
        }
        this.selectedOfferSubject.next(updatedOffer);
      })
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const currentOffers = this.offersSubject.value;
        this.offersSubject.next(currentOffers.filter(o => o.offerId !== id));
        if (this.selectedOfferSubject.value?.offerId === id) {
          this.selectedOfferSubject.next(null);
        }
      })
    );
  }

  clearSelection(): void {
    this.selectedOfferSubject.next(null);
  }
}
