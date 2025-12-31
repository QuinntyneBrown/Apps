import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Motorcycle, CreateMotorcycle, UpdateMotorcycle } from '../models';
import { environment } from '../environments';

@Injectable({
  providedIn: 'root'
})
export class MotorcycleService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = `${environment.baseUrl}/api/motorcycles`;

  private motorcyclesSubject = new BehaviorSubject<Motorcycle[]>([]);
  public motorcycles$ = this.motorcyclesSubject.asObservable();

  getAll(userId?: string): Observable<Motorcycle[]> {
    const url = userId ? `${this.apiUrl}?userId=${userId}` : this.apiUrl;
    return this.http.get<Motorcycle[]>(url).pipe(
      tap(motorcycles => this.motorcyclesSubject.next(motorcycles))
    );
  }

  getById(id: string): Observable<Motorcycle> {
    return this.http.get<Motorcycle>(`${this.apiUrl}/${id}`);
  }

  create(motorcycle: CreateMotorcycle): Observable<Motorcycle> {
    return this.http.post<Motorcycle>(this.apiUrl, motorcycle).pipe(
      tap(() => this.refreshMotorcycles())
    );
  }

  update(id: string, motorcycle: UpdateMotorcycle): Observable<Motorcycle> {
    return this.http.put<Motorcycle>(`${this.apiUrl}/${id}`, motorcycle).pipe(
      tap(() => this.refreshMotorcycles())
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => this.refreshMotorcycles())
    );
  }

  private refreshMotorcycles(): void {
    this.getAll().subscribe();
  }
}
