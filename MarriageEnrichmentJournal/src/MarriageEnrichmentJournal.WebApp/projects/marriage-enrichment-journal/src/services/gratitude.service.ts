import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments';
import { Gratitude, CreateGratitude, UpdateGratitude } from '../models';

@Injectable({
  providedIn: 'root'
})
export class GratitudeService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/gratitudes`;

  private gratitudesSubject = new BehaviorSubject<Gratitude[]>([]);
  public gratitudes$ = this.gratitudesSubject.asObservable();

  private selectedGratitudeSubject = new BehaviorSubject<Gratitude | null>(null);
  public selectedGratitude$ = this.selectedGratitudeSubject.asObservable();

  getAll(): Observable<Gratitude[]> {
    return this.http.get<Gratitude[]>(this.baseUrl).pipe(
      tap(gratitudes => this.gratitudesSubject.next(gratitudes))
    );
  }

  getById(id: string): Observable<Gratitude> {
    return this.http.get<Gratitude>(`${this.baseUrl}/${id}`).pipe(
      tap(gratitude => this.selectedGratitudeSubject.next(gratitude))
    );
  }

  create(gratitude: CreateGratitude): Observable<Gratitude> {
    return this.http.post<Gratitude>(this.baseUrl, gratitude).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  update(gratitude: UpdateGratitude): Observable<Gratitude> {
    return this.http.put<Gratitude>(`${this.baseUrl}/${gratitude.gratitudeId}`, gratitude).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  clearSelected(): void {
    this.selectedGratitudeSubject.next(null);
  }
}
