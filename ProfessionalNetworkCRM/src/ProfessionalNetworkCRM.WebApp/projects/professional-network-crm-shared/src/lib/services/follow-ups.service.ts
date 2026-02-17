import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments';
import { FollowUp, CreateFollowUpRequest, UpdateFollowUpRequest } from '../models';

@Injectable({
  providedIn: 'root'
})
export class FollowUpsService {
  private http = inject(HttpClient);
  private baseUrl = environment.baseUrl + '/api';

  private followUpsSubject = new BehaviorSubject<FollowUp[]>([]);
  public followUps$ = this.followUpsSubject.asObservable();

  private selectedFollowUpSubject = new BehaviorSubject<FollowUp | null>(null);
  public selectedFollowUp$ = this.selectedFollowUpSubject.asObservable();

  loadFollowUps(): Observable<FollowUp[]> {
    return this.http.get<FollowUp[]>(`${this.baseUrl}/followups`).pipe(
      tap(followUps => this.followUpsSubject.next(followUps))
    );
  }

  getFollowUpById(id: string): Observable<FollowUp> {
    return this.http.get<FollowUp>(`${this.baseUrl}/followups/${id}`).pipe(
      tap(followUp => this.selectedFollowUpSubject.next(followUp))
    );
  }

  createFollowUp(request: CreateFollowUpRequest): Observable<FollowUp> {
    return this.http.post<FollowUp>(`${this.baseUrl}/followups`, request).pipe(
      tap(followUp => {
        const currentFollowUps = this.followUpsSubject.value;
        this.followUpsSubject.next([...currentFollowUps, followUp]);
      })
    );
  }

  updateFollowUp(request: UpdateFollowUpRequest): Observable<FollowUp> {
    return this.http.put<FollowUp>(`${this.baseUrl}/followups/${request.followUpId}`, request).pipe(
      tap(updatedFollowUp => {
        const currentFollowUps = this.followUpsSubject.value;
        const index = currentFollowUps.findIndex(f => f.followUpId === updatedFollowUp.followUpId);
        if (index !== -1) {
          currentFollowUps[index] = updatedFollowUp;
          this.followUpsSubject.next([...currentFollowUps]);
        }
      })
    );
  }

  completeFollowUp(id: string): Observable<FollowUp> {
    return this.http.post<FollowUp>(`${this.baseUrl}/followups/${id}/complete`, {}).pipe(
      tap(completedFollowUp => {
        const currentFollowUps = this.followUpsSubject.value;
        const index = currentFollowUps.findIndex(f => f.followUpId === completedFollowUp.followUpId);
        if (index !== -1) {
          currentFollowUps[index] = completedFollowUp;
          this.followUpsSubject.next([...currentFollowUps]);
        }
      })
    );
  }

  deleteFollowUp(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/followups/${id}`).pipe(
      tap(() => {
        const currentFollowUps = this.followUpsSubject.value;
        this.followUpsSubject.next(currentFollowUps.filter(f => f.followUpId !== id));
      })
    );
  }

  clearSelectedFollowUp(): void {
    this.selectedFollowUpSubject.next(null);
  }
}
