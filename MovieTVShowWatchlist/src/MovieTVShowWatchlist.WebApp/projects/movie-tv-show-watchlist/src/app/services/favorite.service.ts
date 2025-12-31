import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Favorite, CreateFavoriteRequest } from '../models';
import { environment } from '../environments';

@Injectable({
  providedIn: 'root'
})
export class FavoriteService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = `${environment.baseUrl}/api/favorites`;

  private favoritesSubject = new BehaviorSubject<Favorite[]>([]);
  public favorites$ = this.favoritesSubject.asObservable();

  getAll(): Observable<Favorite[]> {
    return this.http.get<Favorite[]>(this.apiUrl).pipe(
      tap(favorites => this.favoritesSubject.next(favorites))
    );
  }

  getById(id: string): Observable<Favorite> {
    return this.http.get<Favorite>(`${this.apiUrl}/${id}`);
  }

  create(contentId: string, request: CreateFavoriteRequest): Observable<Favorite> {
    return this.http.post<Favorite>(`${this.apiUrl}/${contentId}`, request).pipe(
      tap(() => this.refreshFavorites())
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => this.refreshFavorites())
    );
  }

  private refreshFavorites(): void {
    this.getAll().subscribe();
  }
}
