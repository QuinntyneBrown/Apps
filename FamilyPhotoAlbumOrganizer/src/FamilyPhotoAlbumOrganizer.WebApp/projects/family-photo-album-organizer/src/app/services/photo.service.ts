import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { Photo, CreatePhotoCommand, UpdatePhotoCommand } from '../models';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PhotoService {
  private photosSubject = new BehaviorSubject<Photo[]>([]);
  public photos$ = this.photosSubject.asObservable();

  private baseUrl = `${environment.baseUrl}/api/Photos`;

  constructor(private http: HttpClient) {}

  loadPhotos(userId?: string, albumId?: string, favoritesOnly?: boolean): void {
    let url = this.baseUrl;
    const params: string[] = [];

    if (userId) params.push(`userId=${userId}`);
    if (albumId) params.push(`albumId=${albumId}`);
    if (favoritesOnly !== undefined) params.push(`favoritesOnly=${favoritesOnly}`);

    if (params.length > 0) {
      url += '?' + params.join('&');
    }

    this.http.get<Photo[]>(url).subscribe(photos => {
      this.photosSubject.next(photos);
    });
  }

  getPhoto(id: string): Observable<Photo> {
    return this.http.get<Photo>(`${this.baseUrl}/${id}`);
  }

  createPhoto(command: CreatePhotoCommand): Observable<Photo> {
    return this.http.post<Photo>(this.baseUrl, command);
  }

  updatePhoto(id: string, command: UpdatePhotoCommand): Observable<Photo> {
    return this.http.put<Photo>(`${this.baseUrl}/${id}`, command);
  }

  deletePhoto(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}
