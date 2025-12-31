import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Contractor } from '../models';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ContractorService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/contractors`;

  private contractorsSubject = new BehaviorSubject<Contractor[]>([]);
  public contractors$ = this.contractorsSubject.asObservable();

  getByProjectId(projectId: string): Observable<Contractor[]> {
    return this.http.get<Contractor[]>(`${this.baseUrl}?projectId=${projectId}`).pipe(
      tap(contractors => this.contractorsSubject.next(contractors))
    );
  }

  create(contractor: Partial<Contractor>): Observable<Contractor> {
    return this.http.post<Contractor>(this.baseUrl, contractor).pipe(
      tap(newContractor => {
        const current = this.contractorsSubject.value;
        this.contractorsSubject.next([...current, newContractor]);
      })
    );
  }
}
