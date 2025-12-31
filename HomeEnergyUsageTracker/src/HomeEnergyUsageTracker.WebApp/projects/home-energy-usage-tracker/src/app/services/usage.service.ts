import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments/environment';
import { Usage, CreateUsageRequest, UpdateUsageRequest } from '../models';

@Injectable({
  providedIn: 'root'
})
export class UsageService {
  private readonly baseUrl = `${environment.baseUrl}/api/Usages`;
  private usagesSubject = new BehaviorSubject<Usage[]>([]);
  public usages$ = this.usagesSubject.asObservable();

  constructor(private http: HttpClient) {}

  getAll(utilityBillId?: string): Observable<Usage[]> {
    const url = utilityBillId
      ? `${this.baseUrl}?utilityBillId=${utilityBillId}`
      : this.baseUrl;

    return this.http.get<Usage[]>(url).pipe(
      tap(usages => this.usagesSubject.next(usages))
    );
  }

  getById(id: string): Observable<Usage> {
    return this.http.get<Usage>(`${this.baseUrl}/${id}`);
  }

  create(request: CreateUsageRequest): Observable<Usage> {
    return this.http.post<Usage>(this.baseUrl, request).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  update(id: string, request: UpdateUsageRequest): Observable<Usage> {
    return this.http.put<Usage>(`${this.baseUrl}/${id}`, request).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => this.getAll().subscribe())
    );
  }
}
