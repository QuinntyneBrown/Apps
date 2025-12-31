import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Liability, CreateLiability, UpdateLiability } from '../models';
import { environment } from '../environments';

@Injectable({
  providedIn: 'root'
})
export class LiabilityService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/liabilities`;

  private liabilitiesSubject = new BehaviorSubject<Liability[]>([]);
  public liabilities$ = this.liabilitiesSubject.asObservable();

  getLiabilities(): Observable<Liability[]> {
    return this.http.get<Liability[]>(this.baseUrl).pipe(
      tap(liabilities => this.liabilitiesSubject.next(liabilities))
    );
  }

  getLiabilityById(id: string): Observable<Liability> {
    return this.http.get<Liability>(`${this.baseUrl}/${id}`);
  }

  createLiability(liability: CreateLiability): Observable<Liability> {
    return this.http.post<Liability>(this.baseUrl, liability).pipe(
      tap(() => this.getLiabilities().subscribe())
    );
  }

  updateLiability(liability: UpdateLiability): Observable<Liability> {
    return this.http.put<Liability>(`${this.baseUrl}/${liability.liabilityId}`, liability).pipe(
      tap(() => this.getLiabilities().subscribe())
    );
  }

  deleteLiability(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => this.getLiabilities().subscribe())
    );
  }
}
