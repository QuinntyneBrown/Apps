import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Screening } from '../models';
import { environment } from '../environments';

@Injectable({
  providedIn: 'root'
})
export class ScreeningService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = `${environment.baseUrl}/api/screenings`;

  private screeningsSubject = new BehaviorSubject<Screening[]>([]);
  public screenings$ = this.screeningsSubject.asObservable();

  getAll(userId?: string): Observable<Screening[]> {
    const url = userId ? `${this.apiUrl}?userId=${userId}` : this.apiUrl;
    return this.http.get<Screening[]>(url).pipe(
      tap(screenings => this.screeningsSubject.next(screenings))
    );
  }

  getById(id: string): Observable<Screening> {
    return this.http.get<Screening>(`${this.apiUrl}/${id}`);
  }

  create(screening: Partial<Screening>): Observable<Screening> {
    return this.http.post<Screening>(this.apiUrl, screening).pipe(
      tap(() => this.refreshScreenings())
    );
  }

  update(id: string, screening: Partial<Screening>): Observable<Screening> {
    return this.http.put<Screening>(`${this.apiUrl}/${id}`, screening).pipe(
      tap(() => this.refreshScreenings())
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => this.refreshScreenings())
    );
  }

  private refreshScreenings(): void {
    this.getAll().subscribe();
  }
}
