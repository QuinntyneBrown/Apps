import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Wishlist } from '../models';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class WishlistService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/wishlists`;

  private wishlistsSubject = new BehaviorSubject<Wishlist[]>([]);
  public wishlists$ = this.wishlistsSubject.asObservable();

  getWishlists(userId?: string, isAcquired?: boolean): Observable<Wishlist[]> {
    let url = this.baseUrl;
    const params: string[] = [];

    if (userId) params.push(`userId=${userId}`);
    if (isAcquired !== undefined) params.push(`isAcquired=${isAcquired}`);

    if (params.length > 0) {
      url += `?${params.join('&')}`;
    }

    return this.http.get<Wishlist[]>(url).pipe(
      tap(wishlists => this.wishlistsSubject.next(wishlists))
    );
  }

  getWishlistById(wishlistId: string): Observable<Wishlist> {
    return this.http.get<Wishlist>(`${this.baseUrl}/${wishlistId}`);
  }

  createWishlist(wishlist: Partial<Wishlist>): Observable<Wishlist> {
    return this.http.post<Wishlist>(this.baseUrl, wishlist).pipe(
      tap(newWishlist => {
        const currentWishlists = this.wishlistsSubject.value;
        this.wishlistsSubject.next([...currentWishlists, newWishlist]);
      })
    );
  }

  updateWishlist(wishlistId: string, wishlist: Partial<Wishlist>): Observable<Wishlist> {
    return this.http.put<Wishlist>(`${this.baseUrl}/${wishlistId}`, { ...wishlist, wishlistId }).pipe(
      tap(updatedWishlist => {
        const currentWishlists = this.wishlistsSubject.value;
        const index = currentWishlists.findIndex(w => w.wishlistId === wishlistId);
        if (index !== -1) {
          const newWishlists = [...currentWishlists];
          newWishlists[index] = updatedWishlist;
          this.wishlistsSubject.next(newWishlists);
        }
      })
    );
  }

  deleteWishlist(wishlistId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${wishlistId}`).pipe(
      tap(() => {
        const currentWishlists = this.wishlistsSubject.value;
        this.wishlistsSubject.next(currentWishlists.filter(w => w.wishlistId !== wishlistId));
      })
    );
  }
}
