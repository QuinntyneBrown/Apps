import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments/environment';
import { UtilityBill, CreateUtilityBillRequest, UpdateUtilityBillRequest } from '../models';

@Injectable({
  providedIn: 'root'
})
export class UtilityBillService {
  private readonly baseUrl = `${environment.baseUrl}/api/UtilityBills`;
  private utilityBillsSubject = new BehaviorSubject<UtilityBill[]>([]);
  public utilityBills$ = this.utilityBillsSubject.asObservable();

  constructor(private http: HttpClient) {}

  getAll(userId?: string): Observable<UtilityBill[]> {
    const url = userId
      ? `${this.baseUrl}?userId=${userId}`
      : this.baseUrl;

    return this.http.get<UtilityBill[]>(url).pipe(
      tap(bills => this.utilityBillsSubject.next(bills))
    );
  }

  getById(id: string): Observable<UtilityBill> {
    return this.http.get<UtilityBill>(`${this.baseUrl}/${id}`);
  }

  create(request: CreateUtilityBillRequest): Observable<UtilityBill> {
    return this.http.post<UtilityBill>(this.baseUrl, request).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  update(id: string, request: UpdateUtilityBillRequest): Observable<UtilityBill> {
    return this.http.put<UtilityBill>(`${this.baseUrl}/${id}`, request).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => this.getAll().subscribe())
    );
  }
}
