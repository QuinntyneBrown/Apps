import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments';
import { Renewal, CreateRenewal, UpdateRenewal } from '../models';

@Injectable({
  providedIn: 'root'
})
export class RenewalService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/renewals`;

  private renewalsSubject = new BehaviorSubject<Renewal[]>([]);
  public renewals$ = this.renewalsSubject.asObservable();

  getAll(): Observable<Renewal[]> {
    return this.http.get<Renewal[]>(this.baseUrl).pipe(
      tap(renewals => this.renewalsSubject.next(renewals))
    );
  }

  getById(id: string): Observable<Renewal> {
    return this.http.get<Renewal>(`${this.baseUrl}/${id}`);
  }

  create(renewal: CreateRenewal): Observable<Renewal> {
    return this.http.post<Renewal>(this.baseUrl, renewal).pipe(
      tap(newRenewal => {
        const renewals = this.renewalsSubject.value;
        this.renewalsSubject.next([...renewals, newRenewal]);
      })
    );
  }

  update(renewal: UpdateRenewal): Observable<Renewal> {
    return this.http.put<Renewal>(`${this.baseUrl}/${renewal.renewalId}`, renewal).pipe(
      tap(updatedRenewal => {
        const renewals = this.renewalsSubject.value;
        const index = renewals.findIndex(r => r.renewalId === updatedRenewal.renewalId);
        if (index !== -1) {
          renewals[index] = updatedRenewal;
          this.renewalsSubject.next([...renewals]);
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const renewals = this.renewalsSubject.value.filter(r => r.renewalId !== id);
        this.renewalsSubject.next(renewals);
      })
    );
  }
}
