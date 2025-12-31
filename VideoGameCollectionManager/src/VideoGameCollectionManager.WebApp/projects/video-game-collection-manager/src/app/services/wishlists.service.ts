import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Wishlist } from '../models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class WishlistsService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;
  private _wishlistsSubject = new BehaviorSubject<Wishlist[]>([]);

  public wishlists$ = this._wishlistsSubject.asObservable();

  getAll(): Observable<Wishlist[]> {
    return this._http.get<Wishlist[]>(`${this._baseUrl}/api/wishlists`).pipe(
      tap(wishlists => this._wishlistsSubject.next(wishlists))
    );
  }

  getById(id: string): Observable<Wishlist> {
    return this._http.get<Wishlist>(`${this._baseUrl}/api/wishlists/${id}`);
  }

  create(wishlist: Partial<Wishlist>): Observable<Wishlist> {
    return this._http.post<Wishlist>(`${this._baseUrl}/api/wishlists`, wishlist).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  update(id: string, wishlist: Partial<Wishlist>): Observable<Wishlist> {
    return this._http.put<Wishlist>(`${this._baseUrl}/api/wishlists/${id}`, wishlist).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  delete(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/wishlists/${id}`).pipe(
      tap(() => this.getAll().subscribe())
    );
  }
}
