import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments';
import { Chore, CreateChore, UpdateChore } from '../models';

@Injectable({
  providedIn: 'root'
})
export class ChoreService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/chores`;

  private choresSubject = new BehaviorSubject<Chore[]>([]);
  public chores$ = this.choresSubject.asObservable();

  getAll(userId?: string, isActive?: boolean): Observable<Chore[]> {
    let params = new HttpParams();
    if (userId) params = params.set('userId', userId);
    if (isActive !== undefined) params = params.set('isActive', isActive.toString());

    return this.http.get<Chore[]>(this.baseUrl, { params }).pipe(
      tap(chores => this.choresSubject.next(chores))
    );
  }

  getById(id: string): Observable<Chore> {
    return this.http.get<Chore>(`${this.baseUrl}/${id}`);
  }

  create(chore: CreateChore): Observable<Chore> {
    return this.http.post<Chore>(this.baseUrl, chore).pipe(
      tap(() => this.refresh())
    );
  }

  update(id: string, chore: UpdateChore): Observable<Chore> {
    return this.http.put<Chore>(`${this.baseUrl}/${id}`, chore).pipe(
      tap(() => this.refresh())
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => this.refresh())
    );
  }

  private refresh(): void {
    this.getAll().subscribe();
  }
}
