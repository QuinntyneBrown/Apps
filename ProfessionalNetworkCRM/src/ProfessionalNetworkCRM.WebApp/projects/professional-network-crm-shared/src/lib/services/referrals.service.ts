import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments';
import { 
  Referral, 
  CreateReferralRequest, 
  UpdateReferralRequest 
} from '../models';

@Injectable({
  providedIn: 'root'
})
export class ReferralsService {
  private http = inject(HttpClient);
  private baseUrl = environment.baseUrl + '/api';

  private referralsSubject = new BehaviorSubject<Referral[]>([]);
  public referrals$ = this.referralsSubject.asObservable();

  loadReferrals(
    sourceContactId?: string,
    thankYouSent?: boolean
  ): Observable<Referral[]> {
    let params = new HttpParams();
    if (sourceContactId) params = params.set('sourceContactId', sourceContactId);
    if (thankYouSent !== undefined) params = params.set('thankYouSent', thankYouSent.toString());

    return this.http.get<Referral[]>(`${this.baseUrl}/referrals`, { params }).pipe(
      tap(referrals => this.referralsSubject.next(referrals))
    );
  }

  createReferral(request: CreateReferralRequest): Observable<Referral> {
    return this.http.post<Referral>(`${this.baseUrl}/referrals`, request).pipe(
      tap(referral => {
        const currentReferrals = this.referralsSubject.value;
        this.referralsSubject.next([...currentReferrals, referral]);
      })
    );
  }

  updateReferral(request: UpdateReferralRequest): Observable<Referral> {
    return this.http.put<Referral>(`${this.baseUrl}/referrals/${request.referralId}`, request).pipe(
      tap(updatedReferral => {
        const currentReferrals = this.referralsSubject.value;
        const index = currentReferrals.findIndex(r => r.referralId === updatedReferral.referralId);
        if (index !== -1) {
          currentReferrals[index] = updatedReferral;
          this.referralsSubject.next([...currentReferrals]);
        }
      })
    );
  }
}
