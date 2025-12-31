import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { Milestone, CreateMilestone, UpdateMilestone } from '../models';

@Injectable({
  providedIn: 'root'
})
export class MilestoneService {
  private readonly apiUrl = `${environment.baseUrl}/api/Milestones`;
  private milestonesSubject = new BehaviorSubject<Milestone[]>([]);
  public milestones$ = this.milestonesSubject.asObservable();

  constructor(private http: HttpClient) {}

  getMilestones(userId?: string, bucketListItemId?: string, isCompleted?: boolean): Observable<Milestone[]> {
    let params = new HttpParams();
    if (userId) params = params.set('userId', userId);
    if (bucketListItemId) params = params.set('bucketListItemId', bucketListItemId);
    if (isCompleted !== undefined) params = params.set('isCompleted', isCompleted.toString());

    return this.http.get<Milestone[]>(this.apiUrl, { params }).pipe(
      tap(milestones => this.milestonesSubject.next(milestones))
    );
  }

  getMilestoneById(milestoneId: string): Observable<Milestone> {
    return this.http.get<Milestone>(`${this.apiUrl}/${milestoneId}`);
  }

  createMilestone(command: CreateMilestone): Observable<Milestone> {
    return this.http.post<Milestone>(this.apiUrl, command).pipe(
      tap(newMilestone => {
        const currentMilestones = this.milestonesSubject.value;
        this.milestonesSubject.next([...currentMilestones, newMilestone]);
      })
    );
  }

  updateMilestone(milestoneId: string, command: UpdateMilestone): Observable<Milestone> {
    return this.http.put<Milestone>(`${this.apiUrl}/${milestoneId}`, command).pipe(
      tap(updatedMilestone => {
        const currentMilestones = this.milestonesSubject.value;
        const index = currentMilestones.findIndex(milestone => milestone.milestoneId === milestoneId);
        if (index !== -1) {
          currentMilestones[index] = updatedMilestone;
          this.milestonesSubject.next([...currentMilestones]);
        }
      })
    );
  }

  deleteMilestone(milestoneId: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${milestoneId}`).pipe(
      tap(() => {
        const currentMilestones = this.milestonesSubject.value;
        this.milestonesSubject.next(currentMilestones.filter(milestone => milestone.milestoneId !== milestoneId));
      })
    );
  }
}
