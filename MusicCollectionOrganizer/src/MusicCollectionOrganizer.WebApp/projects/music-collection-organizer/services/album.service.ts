import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments';
import { Album, CreateAlbum, UpdateAlbum } from '../models';

@Injectable({
  providedIn: 'root'
})
export class AlbumService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/albums`;

  private albumsSubject = new BehaviorSubject<Album[]>([]);
  public albums$ = this.albumsSubject.asObservable();

  getAlbums(): Observable<Album[]> {
    return this.http.get<Album[]>(this.baseUrl).pipe(
      tap(albums => this.albumsSubject.next(albums))
    );
  }

  getAlbumById(id: string): Observable<Album> {
    return this.http.get<Album>(`${this.baseUrl}/${id}`);
  }

  createAlbum(album: CreateAlbum): Observable<Album> {
    return this.http.post<Album>(this.baseUrl, album).pipe(
      tap(newAlbum => {
        const currentAlbums = this.albumsSubject.value;
        this.albumsSubject.next([...currentAlbums, newAlbum]);
      })
    );
  }

  updateAlbum(album: UpdateAlbum): Observable<Album> {
    return this.http.put<Album>(`${this.baseUrl}/${album.albumId}`, album).pipe(
      tap(updatedAlbum => {
        const currentAlbums = this.albumsSubject.value;
        const index = currentAlbums.findIndex(a => a.albumId === updatedAlbum.albumId);
        if (index !== -1) {
          currentAlbums[index] = updatedAlbum;
          this.albumsSubject.next([...currentAlbums]);
        }
      })
    );
  }

  deleteAlbum(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const currentAlbums = this.albumsSubject.value;
        this.albumsSubject.next(currentAlbums.filter(a => a.albumId !== id));
      })
    );
  }
}
