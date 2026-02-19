import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments';
import { 
  Opportunity, 
  CreateOpportunityRequest, 
  UpdateOpportunityRequest,
  OpportunityType,
  OpportunityStatus 
} from '../models';

@Injectable({
  providedIn: 'root'
})
export class OpportunitiesService {
  private http = inject(HttpClient);
  private baseUrl = environment.baseUrl + '/api';

  private opportunitiesSubject = new BehaviorSubject<Opportunity[]>([]);
  public opportunities$ = this.opportunitiesSubject.asObservable();

  private selectedOpportunitySubject = new BehaviorSubject<Opportunity | null>(null);
  public selectedOpportunity$ = this.selectedOpportunitySubject.asObservable();

  loadOpportunities(
    contactId?: string,
    opportunityType?: OpportunityType,
    status?: OpportunityStatus
  ): Observable<Opportunity[]> {
    let params = new HttpParams();
    if (contactId) params = params.set('contactId', contactId);
    if (opportunityType !== undefined) params = params.set('opportunityType', opportunityType.toString());
    if (status !== undefined) params = params.set('status', status.toString());

    return this.http.get<Opportunity[]>(`${this.baseUrl}/opportunities`, { params }).pipe(
      tap(opportunities => this.opportunitiesSubject.next(opportunities))
    );
  }

  getOpportunityById(id: string): Observable<Opportunity> {
    return this.http.get<Opportunity>(`${this.baseUrl}/opportunities/${id}`).pipe(
      tap(opportunity => this.selectedOpportunitySubject.next(opportunity))
    );
  }

  createOpportunity(request: CreateOpportunityRequest): Observable<Opportunity> {
    return this.http.post<Opportunity>(`${this.baseUrl}/opportunities`, request).pipe(
      tap(opportunity => {
        const currentOpportunities = this.opportunitiesSubject.value;
        this.opportunitiesSubject.next([...currentOpportunities, opportunity]);
      })
    );
  }

  updateOpportunity(request: UpdateOpportunityRequest): Observable<Opportunity> {
    return this.http.put<Opportunity>(`${this.baseUrl}/opportunities/${request.opportunityId}`, request).pipe(
      tap(updatedOpportunity => {
        const currentOpportunities = this.opportunitiesSubject.value;
        const index = currentOpportunities.findIndex(o => o.opportunityId === updatedOpportunity.opportunityId);
        if (index !== -1) {
          currentOpportunities[index] = updatedOpportunity;
          this.opportunitiesSubject.next([...currentOpportunities]);
        }
      })
    );
  }

  deleteOpportunity(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/opportunities/${id}`).pipe(
      tap(() => {
        const currentOpportunities = this.opportunitiesSubject.value;
        this.opportunitiesSubject.next(currentOpportunities.filter(o => o.opportunityId !== id));
      })
    );
  }

  clearSelectedOpportunity(): void {
    this.selectedOpportunitySubject.next(null);
  }
}
