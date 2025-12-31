import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { Spot, CreateSpotRequest, UpdateSpotRequest } from '../models';

@Injectable({
  providedIn: 'root'
})
export class SpotsService {
  private readonly apiUrl = `${environment.baseUrl}/api/Spots`;
  private spotsSubject = new BehaviorSubject<Spot[]>([]);
  public spots$ = this.spotsSubject.asObservable();

  constructor(private http: HttpClient) {}

  getSpots(userId?: string, favoritesOnly?: boolean): Observable<Spot[]> {
    let params = new HttpParams();
    if (userId) params = params.set('userId', userId);
    if (favoritesOnly !== undefined) params = params.set('favoritesOnly', favoritesOnly.toString());

    return this.http.get<Spot[]>(this.apiUrl, { params }).pipe(
      tap(spots => this.spotsSubject.next(spots))
    );
  }

  getSpotById(id: string): Observable<Spot> {
    return this.http.get<Spot>(`${this.apiUrl}/${id}`);
  }

  createSpot(request: CreateSpotRequest): Observable<Spot> {
    return this.http.post<Spot>(this.apiUrl, request).pipe(
      tap(() => this.refreshSpots())
    );
  }

  updateSpot(id: string, request: UpdateSpotRequest): Observable<Spot> {
    return this.http.put<Spot>(`${this.apiUrl}/${id}`, request).pipe(
      tap(() => this.refreshSpots())
    );
  }

  deleteSpot(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => this.refreshSpots())
    );
  }

  private refreshSpots(): void {
    this.getSpots().subscribe();
  }
}
