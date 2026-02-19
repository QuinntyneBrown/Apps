import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { Album, CreateAlbumCommand, UpdateAlbumCommand } from '../models';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AlbumService {
  private albumsSubject = new BehaviorSubject<Album[]>([]);
  public albums$ = this.albumsSubject.asObservable();

  private baseUrl = `${environment.baseUrl}/api/Albums`;

  constructor(private http: HttpClient) {}

  loadAlbums(userId?: string): void {
    const url = userId ? `${this.baseUrl}?userId=${userId}` : this.baseUrl;
    this.http.get<Album[]>(url).subscribe(albums => {
      this.albumsSubject.next(albums);
    });
  }

  getAlbum(id: string): Observable<Album> {
    return this.http.get<Album>(`${this.baseUrl}/${id}`);
  }

  createAlbum(command: CreateAlbumCommand): Observable<Album> {
    return this.http.post<Album>(this.baseUrl, command);
  }

  updateAlbum(id: string, command: UpdateAlbumCommand): Observable<Album> {
    return this.http.put<Album>(`${this.baseUrl}/${id}`, command);
  }

  deleteAlbum(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}
