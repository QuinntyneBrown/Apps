import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments';
import { Photo, CreatePhoto, UpdatePhoto } from '../models';

@Injectable({
  providedIn: 'root'
})
export class PhotoService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/photos`;

  private photosSubject = new BehaviorSubject<Photo[]>([]);
  public photos$ = this.photosSubject.asObservable();

  getAll(): Observable<Photo[]> {
    return this.http.get<Photo[]>(this.baseUrl).pipe(
      tap(photos => this.photosSubject.next(photos))
    );
  }

  getById(id: string): Observable<Photo> {
    return this.http.get<Photo>(`${this.baseUrl}/${id}`);
  }

  getBySessionId(sessionId: string): Observable<Photo[]> {
    return this.http.get<Photo[]>(`${this.baseUrl}/session/${sessionId}`);
  }

  create(photo: CreatePhoto): Observable<Photo> {
    return this.http.post<Photo>(this.baseUrl, photo).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  update(photo: UpdatePhoto): Observable<Photo> {
    return this.http.put<Photo>(`${this.baseUrl}/${photo.photoId}`, photo).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => this.getAll().subscribe())
    );
  }
}
