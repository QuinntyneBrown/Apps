import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { Contribution, CreateContributionCommand, UpdateContributionCommand } from '../models';

@Injectable({
  providedIn: 'root'
})
export class ContributionsService {
  private readonly baseUrl = `${environment.baseUrl}/api/contributions`;
  private contributionsSubject = new BehaviorSubject<Contribution[]>([]);
  public contributions$ = this.contributionsSubject.asObservable();

  private selectedContributionSubject = new BehaviorSubject<Contribution | null>(null);
  public selectedContribution$ = this.selectedContributionSubject.asObservable();

  constructor(private http: HttpClient) {}

  getContributions(goalId?: string, fromDate?: string, toDate?: string): Observable<Contribution[]> {
    let params = new HttpParams();
    if (goalId) {
      params = params.set('goalId', goalId);
    }
    if (fromDate) {
      params = params.set('fromDate', fromDate);
    }
    if (toDate) {
      params = params.set('toDate', toDate);
    }

    return this.http.get<Contribution[]>(this.baseUrl, { params }).pipe(
      tap(contributions => this.contributionsSubject.next(contributions))
    );
  }

  getContributionById(contributionId: string): Observable<Contribution> {
    return this.http.get<Contribution>(`${this.baseUrl}/${contributionId}`).pipe(
      tap(contribution => this.selectedContributionSubject.next(contribution))
    );
  }

  createContribution(command: CreateContributionCommand): Observable<Contribution> {
    return this.http.post<Contribution>(this.baseUrl, command).pipe(
      tap(contribution => {
        const currentContributions = this.contributionsSubject.value;
        this.contributionsSubject.next([...currentContributions, contribution]);
      })
    );
  }

  updateContribution(contributionId: string, command: UpdateContributionCommand): Observable<Contribution> {
    return this.http.put<Contribution>(`${this.baseUrl}/${contributionId}`, command).pipe(
      tap(updatedContribution => {
        const currentContributions = this.contributionsSubject.value;
        const index = currentContributions.findIndex(c => c.contributionId === contributionId);
        if (index !== -1) {
          currentContributions[index] = updatedContribution;
          this.contributionsSubject.next([...currentContributions]);
        }
        if (this.selectedContributionSubject.value?.contributionId === contributionId) {
          this.selectedContributionSubject.next(updatedContribution);
        }
      })
    );
  }

  deleteContribution(contributionId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${contributionId}`).pipe(
      tap(() => {
        const currentContributions = this.contributionsSubject.value;
        this.contributionsSubject.next(currentContributions.filter(c => c.contributionId !== contributionId));
        if (this.selectedContributionSubject.value?.contributionId === contributionId) {
          this.selectedContributionSubject.next(null);
        }
      })
    );
  }
}
