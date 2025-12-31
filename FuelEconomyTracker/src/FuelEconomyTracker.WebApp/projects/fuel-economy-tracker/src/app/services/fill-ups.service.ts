import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments/environment';
import { FillUp, CreateFillUpRequest, UpdateFillUpRequest } from '../models';

@Injectable({
  providedIn: 'root'
})
export class FillUpsService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/FillUps`;

  private fillUpsSubject = new BehaviorSubject<FillUp[]>([]);
  public fillUps$ = this.fillUpsSubject.asObservable();

  private currentFillUpSubject = new BehaviorSubject<FillUp | null>(null);
  public currentFillUp$ = this.currentFillUpSubject.asObservable();

  getAll(vehicleId?: string): Observable<FillUp[]> {
    const params = vehicleId ? { vehicleId } : {};
    return this.http.get<FillUp[]>(this.baseUrl, { params }).pipe(
      tap(fillUps => this.fillUpsSubject.next(fillUps))
    );
  }

  getById(id: string): Observable<FillUp> {
    return this.http.get<FillUp>(`${this.baseUrl}/${id}`).pipe(
      tap(fillUp => this.currentFillUpSubject.next(fillUp))
    );
  }

  create(request: CreateFillUpRequest): Observable<FillUp> {
    return this.http.post<FillUp>(this.baseUrl, request).pipe(
      tap(() => this.getAll(request.vehicleId).subscribe())
    );
  }

  update(id: string, request: UpdateFillUpRequest): Observable<FillUp> {
    return this.http.put<FillUp>(`${this.baseUrl}/${id}`, request).pipe(
      tap(fillUp => this.getAll(fillUp.vehicleId).subscribe())
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => this.getAll().subscribe())
    );
  }
}
