import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { Catch, CreateCatchRequest, UpdateCatchRequest, FishSpecies } from '../models';

@Injectable({
  providedIn: 'root'
})
export class CatchesService {
  private readonly apiUrl = `${environment.baseUrl}/api/Catches`;
  private catchesSubject = new BehaviorSubject<Catch[]>([]);
  public catches$ = this.catchesSubject.asObservable();

  constructor(private http: HttpClient) {}

  getCatches(userId?: string, tripId?: string, species?: FishSpecies): Observable<Catch[]> {
    let params = new HttpParams();
    if (userId) params = params.set('userId', userId);
    if (tripId) params = params.set('tripId', tripId);
    if (species !== undefined) params = params.set('species', species.toString());

    return this.http.get<Catch[]>(this.apiUrl, { params }).pipe(
      tap(catches => this.catchesSubject.next(catches))
    );
  }

  getCatchById(id: string): Observable<Catch> {
    return this.http.get<Catch>(`${this.apiUrl}/${id}`);
  }

  createCatch(request: CreateCatchRequest): Observable<Catch> {
    return this.http.post<Catch>(this.apiUrl, request).pipe(
      tap(() => this.refreshCatches())
    );
  }

  updateCatch(id: string, request: UpdateCatchRequest): Observable<Catch> {
    return this.http.put<Catch>(`${this.apiUrl}/${id}`, request).pipe(
      tap(() => this.refreshCatches())
    );
  }

  deleteCatch(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => this.refreshCatches())
    );
  }

  private refreshCatches(): void {
    this.getCatches().subscribe();
  }
}
