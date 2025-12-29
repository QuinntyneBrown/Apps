import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { tap, map } from 'rxjs/operators';
import { Gift, GiftStatus } from '../models';
import { apiBaseUrl } from './api-config';

@Injectable({
  providedIn: 'root'
})
export class GiftService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = apiBaseUrl;
  private readonly giftsSubject = new BehaviorSubject<Gift[]>([]);

  gifts$ = this.giftsSubject.asObservable();

  getGifts(): Observable<Gift[]> {
    return this.http.get<Gift[]>(`${this.baseUrl}/api/gifts`).pipe(
      tap(gifts => this.giftsSubject.next(gifts))
    );
  }

  getGiftsByDate(dateId: string): Observable<Gift[]> {
    return this.http.get<Gift[]>(`${this.baseUrl}/api/dates/${dateId}/gifts`);
  }

  addGift(dateId: string, gift: Omit<Gift, 'giftId' | 'dateId'>): Observable<Gift> {
    return this.http.post<Gift>(`${this.baseUrl}/api/dates/${dateId}/gifts`, gift).pipe(
      tap(newGift => {
        const current = this.giftsSubject.value;
        this.giftsSubject.next([...current, newGift]);
      })
    );
  }

  updateGift(giftId: string, gift: Partial<Gift>): Observable<Gift> {
    return this.http.put<Gift>(`${this.baseUrl}/api/gifts/${giftId}`, gift).pipe(
      tap(updatedGift => {
        const current = this.giftsSubject.value;
        const index = current.findIndex(g => g.giftId === giftId);
        if (index >= 0) {
          const updated = [...current];
          updated[index] = updatedGift;
          this.giftsSubject.next(updated);
        }
      })
    );
  }

  markAsPurchased(giftId: string, actualPrice: number): Observable<Gift> {
    return this.http.post<Gift>(`${this.baseUrl}/api/gifts/${giftId}/purchase`, { actualPrice }).pipe(
      tap(updatedGift => {
        const current = this.giftsSubject.value;
        const index = current.findIndex(g => g.giftId === giftId);
        if (index >= 0) {
          const updated = [...current];
          updated[index] = updatedGift;
          this.giftsSubject.next(updated);
        }
      })
    );
  }

  markAsDelivered(giftId: string): Observable<Gift> {
    return this.http.post<Gift>(`${this.baseUrl}/api/gifts/${giftId}/deliver`, {}).pipe(
      tap(updatedGift => {
        const current = this.giftsSubject.value;
        const index = current.findIndex(g => g.giftId === giftId);
        if (index >= 0) {
          const updated = [...current];
          updated[index] = updatedGift;
          this.giftsSubject.next(updated);
        }
      })
    );
  }

  deleteGift(giftId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/api/gifts/${giftId}`).pipe(
      tap(() => {
        const current = this.giftsSubject.value;
        this.giftsSubject.next(current.filter(g => g.giftId !== giftId));
      })
    );
  }

  getTotalBudget(): Observable<number> {
    return this.gifts$.pipe(
      map(gifts => gifts.reduce((total, gift) => total + gift.estimatedPrice, 0))
    );
  }

  getTotalSpent(): Observable<number> {
    return this.gifts$.pipe(
      map(gifts => gifts
        .filter(g => g.status === GiftStatus.Purchased || g.status === GiftStatus.Delivered)
        .reduce((total, gift) => total + (gift.actualPrice || gift.estimatedPrice), 0))
    );
  }
}
