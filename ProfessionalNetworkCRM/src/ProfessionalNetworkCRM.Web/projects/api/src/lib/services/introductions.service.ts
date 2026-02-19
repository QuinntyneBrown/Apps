import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments';
import { 
  Introduction, 
  RequestIntroductionRequest, 
  MakeIntroductionRequest,
  IntroductionStatus 
} from '../models';

@Injectable({
  providedIn: 'root'
})
export class IntroductionsService {
  private http = inject(HttpClient);
  private baseUrl = environment.baseUrl + '/api';

  private introductionsSubject = new BehaviorSubject<Introduction[]>([]);
  public introductions$ = this.introductionsSubject.asObservable();

  loadIntroductions(
    contactId?: string,
    status?: IntroductionStatus
  ): Observable<Introduction[]> {
    let params = new HttpParams();
    if (contactId) params = params.set('contactId', contactId);
    if (status !== undefined) params = params.set('status', status.toString());

    return this.http.get<Introduction[]>(`${this.baseUrl}/introductions`, { params }).pipe(
      tap(introductions => this.introductionsSubject.next(introductions))
    );
  }

  requestIntroduction(request: RequestIntroductionRequest): Observable<Introduction> {
    return this.http.post<Introduction>(`${this.baseUrl}/introductions/request`, request).pipe(
      tap(introduction => {
        const currentIntroductions = this.introductionsSubject.value;
        this.introductionsSubject.next([...currentIntroductions, introduction]);
      })
    );
  }

  makeIntroduction(request: MakeIntroductionRequest): Observable<Introduction> {
    return this.http.post<Introduction>(`${this.baseUrl}/introductions/make`, request).pipe(
      tap(updatedIntroduction => {
        const currentIntroductions = this.introductionsSubject.value;
        const index = currentIntroductions.findIndex(i => i.introductionId === updatedIntroduction.introductionId);
        if (index !== -1) {
          currentIntroductions[index] = updatedIntroduction;
          this.introductionsSubject.next([...currentIntroductions]);
        }
      })
    );
  }
}
