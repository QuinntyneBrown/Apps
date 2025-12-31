import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments/environment';
import { SavingsTip, CreateSavingsTipRequest, UpdateSavingsTipRequest } from '../models';

@Injectable({
  providedIn: 'root'
})
export class SavingsTipService {
  private readonly baseUrl = `${environment.baseUrl}/api/SavingsTips`;
  private savingsTipsSubject = new BehaviorSubject<SavingsTip[]>([]);
  public savingsTips$ = this.savingsTipsSubject.asObservable();

  constructor(private http: HttpClient) {}

  getAll(): Observable<SavingsTip[]> {
    return this.http.get<SavingsTip[]>(this.baseUrl).pipe(
      tap(tips => this.savingsTipsSubject.next(tips))
    );
  }

  getById(id: string): Observable<SavingsTip> {
    return this.http.get<SavingsTip>(`${this.baseUrl}/${id}`);
  }

  create(request: CreateSavingsTipRequest): Observable<SavingsTip> {
    return this.http.post<SavingsTip>(this.baseUrl, request).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  update(id: string, request: UpdateSavingsTipRequest): Observable<SavingsTip> {
    return this.http.put<SavingsTip>(`${this.baseUrl}/${id}`, request).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => this.getAll().subscribe())
    );
  }
}
