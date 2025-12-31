import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { Contribution } from '../models';

export interface CreateContributionRequest {
  registryItemId: string;
  contributorName: string;
  contributorEmail?: string;
  quantity: number;
}

@Injectable({
  providedIn: 'root'
})
export class ContributionService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.apiUrl}/api/contributions`;

  private contributionsSubject = new BehaviorSubject<Contribution[]>([]);
  private loadingSubject = new BehaviorSubject<boolean>(false);

  contributions$ = this.contributionsSubject.asObservable();
  loading$ = this.loadingSubject.asObservable();

  getContributions(registryItemId?: string): Observable<Contribution[]> {
    this.loadingSubject.next(true);

    let params = new HttpParams();
    if (registryItemId) params = params.set('registryItemId', registryItemId);

    return this.http.get<Contribution[]>(this.baseUrl, { params }).pipe(
      tap(contributions => {
        this.contributionsSubject.next(contributions);
        this.loadingSubject.next(false);
      })
    );
  }

  getContributionById(contributionId: string): Observable<Contribution> {
    this.loadingSubject.next(true);

    return this.http.get<Contribution>(`${this.baseUrl}/${contributionId}`).pipe(
      tap(() => {
        this.loadingSubject.next(false);
      })
    );
  }

  createContribution(request: CreateContributionRequest): Observable<Contribution> {
    this.loadingSubject.next(true);

    return this.http.post<Contribution>(this.baseUrl, request).pipe(
      tap(contribution => {
        const currentContributions = this.contributionsSubject.value;
        this.contributionsSubject.next([contribution, ...currentContributions]);
        this.loadingSubject.next(false);
      })
    );
  }

  deleteContribution(contributionId: string): Observable<void> {
    this.loadingSubject.next(true);

    return this.http.delete<void>(`${this.baseUrl}/${contributionId}`).pipe(
      tap(() => {
        const currentContributions = this.contributionsSubject.value;
        this.contributionsSubject.next(currentContributions.filter(c => c.contributionId !== contributionId));
        this.loadingSubject.next(false);
      })
    );
  }
}
