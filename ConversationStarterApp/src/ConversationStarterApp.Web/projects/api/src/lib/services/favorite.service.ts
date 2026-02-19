import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { Favorite, CreateFavoriteRequest, UpdateFavoriteRequest } from '../models';

@Injectable({
  providedIn: 'root'
})
export class FavoriteService {
  private readonly apiUrl = `${environment.baseUrl}/api/favorites`;
  private favoritesSubject = new BehaviorSubject<Favorite[]>([]);
  public favorites$ = this.favoritesSubject.asObservable();

  constructor(private http: HttpClient) {}

  getByUserId(userId: string): Observable<Favorite[]> {
    const params = new HttpParams().set('userId', userId);
    return this.http.get<Favorite[]>(this.apiUrl, { params }).pipe(
      tap(favorites => this.favoritesSubject.next(favorites))
    );
  }

  getById(id: string): Observable<Favorite> {
    return this.http.get<Favorite>(`${this.apiUrl}/${id}`);
  }

  create(request: CreateFavoriteRequest): Observable<Favorite> {
    return this.http.post<Favorite>(this.apiUrl, request).pipe(
      tap(favorite => {
        const current = this.favoritesSubject.value;
        this.favoritesSubject.next([...current, favorite]);
      })
    );
  }

  update(id: string, request: UpdateFavoriteRequest): Observable<Favorite> {
    return this.http.put<Favorite>(`${this.apiUrl}/${id}`, request).pipe(
      tap(updated => {
        const current = this.favoritesSubject.value;
        const index = current.findIndex(f => f.favoriteId === id);
        if (index !== -1) {
          const newFavorites = [...current];
          newFavorites[index] = updated;
          this.favoritesSubject.next(newFavorites);
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => {
        const current = this.favoritesSubject.value;
        this.favoritesSubject.next(current.filter(f => f.favoriteId !== id));
      })
    );
  }

  isFavorite(promptId: string): boolean {
    return this.favoritesSubject.value.some(f => f.promptId === promptId);
  }

  getFavoriteByPromptId(promptId: string): Favorite | undefined {
    return this.favoritesSubject.value.find(f => f.promptId === promptId);
  }
}
