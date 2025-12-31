import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { Contribution, CreateContribution, UpdateContribution } from '../models';

@Injectable({
  providedIn: 'root'
})
export class ContributionService {
  private apiUrl = `${environment.baseUrl}/api/contributions`;
  private contributionsSubject = new BehaviorSubject<Contribution[]>([]);
  public contributions$ = this.contributionsSubject.asObservable();

  private selectedContributionSubject = new BehaviorSubject<Contribution | null>(null);
  public selectedContribution$ = this.selectedContributionSubject.asObservable();

  constructor(private http: HttpClient) {}

  getContributions(planId?: string): Observable<Contribution[]> {
    const url = planId ? `${this.apiUrl}?planId=${planId}` : this.apiUrl;
    return this.http.get<Contribution[]>(url).pipe(
      tap(contributions => this.contributionsSubject.next(contributions))
    );
  }

  getContributionById(id: string): Observable<Contribution> {
    return this.http.get<Contribution>(`${this.apiUrl}/${id}`).pipe(
      tap(contribution => this.selectedContributionSubject.next(contribution))
    );
  }

  createContribution(contribution: CreateContribution): Observable<Contribution> {
    return this.http.post<Contribution>(this.apiUrl, contribution).pipe(
      tap(newContribution => {
        const currentContributions = this.contributionsSubject.value;
        this.contributionsSubject.next([...currentContributions, newContribution]);
      })
    );
  }

  updateContribution(id: string, contribution: UpdateContribution): Observable<Contribution> {
    return this.http.put<Contribution>(`${this.apiUrl}/${id}`, contribution).pipe(
      tap(updatedContribution => {
        const currentContributions = this.contributionsSubject.value;
        const index = currentContributions.findIndex(c => c.contributionId === id);
        if (index !== -1) {
          currentContributions[index] = updatedContribution;
          this.contributionsSubject.next([...currentContributions]);
        }
        if (this.selectedContributionSubject.value?.contributionId === id) {
          this.selectedContributionSubject.next(updatedContribution);
        }
      })
    );
  }

  deleteContribution(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => {
        const currentContributions = this.contributionsSubject.value;
        this.contributionsSubject.next(currentContributions.filter(c => c.contributionId !== id));
        if (this.selectedContributionSubject.value?.contributionId === id) {
          this.selectedContributionSubject.next(null);
        }
      })
    );
  }

  clearSelectedContribution(): void {
    this.selectedContributionSubject.next(null);
  }
}
