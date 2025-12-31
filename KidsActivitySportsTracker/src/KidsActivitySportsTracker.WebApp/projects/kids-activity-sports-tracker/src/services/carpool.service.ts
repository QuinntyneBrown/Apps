import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Carpool, CreateCarpool, UpdateCarpool } from '../models';
import { environment } from '../environments';

@Injectable({
  providedIn: 'root'
})
export class CarpoolService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/carpools`;

  private carpoolsSubject = new BehaviorSubject<Carpool[]>([]);
  public carpools$ = this.carpoolsSubject.asObservable();

  loadCarpools(): Observable<Carpool[]> {
    return this.http.get<Carpool[]>(this.baseUrl).pipe(
      tap(carpools => this.carpoolsSubject.next(carpools))
    );
  }

  getCarpoolById(id: string): Observable<Carpool> {
    return this.http.get<Carpool>(`${this.baseUrl}/${id}`);
  }

  createCarpool(carpool: CreateCarpool): Observable<Carpool> {
    return this.http.post<Carpool>(this.baseUrl, carpool).pipe(
      tap(() => this.loadCarpools().subscribe())
    );
  }

  updateCarpool(carpool: UpdateCarpool): Observable<Carpool> {
    return this.http.put<Carpool>(`${this.baseUrl}/${carpool.carpoolId}`, carpool).pipe(
      tap(() => this.loadCarpools().subscribe())
    );
  }

  deleteCarpool(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => this.loadCarpools().subscribe())
    );
  }
}
