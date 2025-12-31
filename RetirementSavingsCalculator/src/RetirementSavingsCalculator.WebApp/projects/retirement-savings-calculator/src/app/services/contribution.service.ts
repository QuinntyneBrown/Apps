import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments';
import { Contribution, CreateContribution, UpdateContribution } from '../models';

@Injectable({
  providedIn: 'root'
})
export class ContributionService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/contributions`;

  private contributionsSubject = new BehaviorSubject<Contribution[]>([]);
  public contributions$ = this.contributionsSubject.asObservable();

  private selectedContributionSubject = new BehaviorSubject<Contribution | null>(null);
  public selectedContribution$ = this.selectedContributionSubject.asObservable();

  loadContributions(retirementScenarioId?: string): Observable<Contribution[]> {
    const url = retirementScenarioId
      ? `${this.baseUrl}?retirementScenarioId=${retirementScenarioId}`
      : this.baseUrl;
    return this.http.get<Contribution[]>(url).pipe(
      tap(contributions => this.contributionsSubject.next(contributions))
    );
  }

  getContribution(id: string): Observable<Contribution> {
    return this.http.get<Contribution>(`${this.baseUrl}/${id}`).pipe(
      tap(contribution => this.selectedContributionSubject.next(contribution))
    );
  }

  createContribution(contribution: CreateContribution): Observable<Contribution> {
    return this.http.post<Contribution>(this.baseUrl, contribution).pipe(
      tap(newContribution => {
        const current = this.contributionsSubject.value;
        this.contributionsSubject.next([...current, newContribution]);
      })
    );
  }

  updateContribution(contribution: UpdateContribution): Observable<Contribution> {
    return this.http.put<Contribution>(`${this.baseUrl}/${contribution.contributionId}`, contribution).pipe(
      tap(updatedContribution => {
        const current = this.contributionsSubject.value;
        const index = current.findIndex(c => c.contributionId === updatedContribution.contributionId);
        if (index !== -1) {
          current[index] = updatedContribution;
          this.contributionsSubject.next([...current]);
        }
        this.selectedContributionSubject.next(updatedContribution);
      })
    );
  }

  deleteContribution(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const current = this.contributionsSubject.value;
        this.contributionsSubject.next(current.filter(c => c.contributionId !== id));
        if (this.selectedContributionSubject.value?.contributionId === id) {
          this.selectedContributionSubject.next(null);
        }
      })
    );
  }
}
