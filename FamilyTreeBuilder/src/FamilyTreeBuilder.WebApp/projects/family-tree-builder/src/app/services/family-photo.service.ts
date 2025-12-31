import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { FamilyPhoto, CreateFamilyPhotoRequest, UpdateFamilyPhotoRequest } from '../models';

@Injectable({
  providedIn: 'root'
})
export class FamilyPhotoService {
  private readonly apiUrl = `${environment.baseUrl}/api/familyphotos`;
  private photosSubject = new BehaviorSubject<FamilyPhoto[]>([]);
  public photos$ = this.photosSubject.asObservable();

  constructor(private http: HttpClient) {}

  getFamilyPhotos(personId?: string): Observable<FamilyPhoto[]> {
    const params = personId ? { personId } : {};
    return this.http.get<FamilyPhoto[]>(this.apiUrl, { params }).pipe(
      tap(photos => this.photosSubject.next(photos))
    );
  }

  getFamilyPhotoById(id: string): Observable<FamilyPhoto> {
    return this.http.get<FamilyPhoto>(`${this.apiUrl}/${id}`);
  }

  createFamilyPhoto(request: CreateFamilyPhotoRequest): Observable<FamilyPhoto> {
    return this.http.post<FamilyPhoto>(this.apiUrl, request).pipe(
      tap(() => this.refreshPhotos())
    );
  }

  updateFamilyPhoto(id: string, request: UpdateFamilyPhotoRequest): Observable<FamilyPhoto> {
    return this.http.put<FamilyPhoto>(`${this.apiUrl}/${id}`, request).pipe(
      tap(() => this.refreshPhotos())
    );
  }

  deleteFamilyPhoto(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => this.refreshPhotos())
    );
  }

  private refreshPhotos(): void {
    this.getFamilyPhotos().subscribe();
  }
}
