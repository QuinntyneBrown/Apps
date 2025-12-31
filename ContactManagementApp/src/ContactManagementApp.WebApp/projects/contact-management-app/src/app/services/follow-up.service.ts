import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { FollowUp, CreateFollowUpRequest } from '../models';

@Injectable({
  providedIn: 'root'
})
export class FollowUpService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.apiUrl}/api/followups`;

  private followUpsSubject = new BehaviorSubject<FollowUp[]>([]);
  private loadingSubject = new BehaviorSubject<boolean>(false);

  followUps$ = this.followUpsSubject.asObservable();
  loading$ = this.loadingSubject.asObservable();

  getFollowUps(userId?: string, contactId?: string, isCompleted?: boolean): Observable<FollowUp[]> {
    this.loadingSubject.next(true);

    let params = new HttpParams();
    if (userId) params = params.set('userId', userId);
    if (contactId) params = params.set('contactId', contactId);
    if (isCompleted !== undefined) params = params.set('isCompleted', isCompleted.toString());

    return this.http.get<FollowUp[]>(this.baseUrl, { params }).pipe(
      tap(followUps => {
        this.followUpsSubject.next(followUps);
        this.loadingSubject.next(false);
      })
    );
  }

  getFollowUpById(followUpId: string): Observable<FollowUp> {
    this.loadingSubject.next(true);

    return this.http.get<FollowUp>(`${this.baseUrl}/${followUpId}`).pipe(
      tap(() => this.loadingSubject.next(false))
    );
  }

  createFollowUp(request: CreateFollowUpRequest): Observable<FollowUp> {
    this.loadingSubject.next(true);

    return this.http.post<FollowUp>(this.baseUrl, request).pipe(
      tap(followUp => {
        const currentFollowUps = this.followUpsSubject.value;
        this.followUpsSubject.next([followUp, ...currentFollowUps]);
        this.loadingSubject.next(false);
      })
    );
  }

  completeFollowUp(followUpId: string): Observable<FollowUp> {
    this.loadingSubject.next(true);

    return this.http.put<FollowUp>(`${this.baseUrl}/${followUpId}/complete`, {}).pipe(
      tap(updatedFollowUp => {
        const currentFollowUps = this.followUpsSubject.value;
        const index = currentFollowUps.findIndex(f => f.followUpId === followUpId);
        if (index !== -1) {
          currentFollowUps[index] = updatedFollowUp;
          this.followUpsSubject.next([...currentFollowUps]);
        }
        this.loadingSubject.next(false);
      })
    );
  }

  deleteFollowUp(followUpId: string): Observable<void> {
    this.loadingSubject.next(true);

    return this.http.delete<void>(`${this.baseUrl}/${followUpId}`).pipe(
      tap(() => {
        const currentFollowUps = this.followUpsSubject.value;
        this.followUpsSubject.next(currentFollowUps.filter(f => f.followUpId !== followUpId));
        this.loadingSubject.next(false);
      })
    );
  }
}
