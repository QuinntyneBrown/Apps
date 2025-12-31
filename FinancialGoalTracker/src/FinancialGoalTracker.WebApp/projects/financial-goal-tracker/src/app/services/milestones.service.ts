import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { Milestone, CreateMilestoneCommand, UpdateMilestoneCommand } from '../models';

@Injectable({
  providedIn: 'root'
})
export class MilestonesService {
  private readonly baseUrl = `${environment.baseUrl}/api/milestones`;
  private milestonesSubject = new BehaviorSubject<Milestone[]>([]);
  public milestones$ = this.milestonesSubject.asObservable();

  private selectedMilestoneSubject = new BehaviorSubject<Milestone | null>(null);
  public selectedMilestone$ = this.selectedMilestoneSubject.asObservable();

  constructor(private http: HttpClient) {}

  getMilestones(goalId?: string, isCompleted?: boolean): Observable<Milestone[]> {
    let params = new HttpParams();
    if (goalId) {
      params = params.set('goalId', goalId);
    }
    if (isCompleted !== undefined) {
      params = params.set('isCompleted', isCompleted.toString());
    }

    return this.http.get<Milestone[]>(this.baseUrl, { params }).pipe(
      tap(milestones => this.milestonesSubject.next(milestones))
    );
  }

  getMilestoneById(milestoneId: string): Observable<Milestone> {
    return this.http.get<Milestone>(`${this.baseUrl}/${milestoneId}`).pipe(
      tap(milestone => this.selectedMilestoneSubject.next(milestone))
    );
  }

  createMilestone(command: CreateMilestoneCommand): Observable<Milestone> {
    return this.http.post<Milestone>(this.baseUrl, command).pipe(
      tap(milestone => {
        const currentMilestones = this.milestonesSubject.value;
        this.milestonesSubject.next([...currentMilestones, milestone]);
      })
    );
  }

  updateMilestone(milestoneId: string, command: UpdateMilestoneCommand): Observable<Milestone> {
    return this.http.put<Milestone>(`${this.baseUrl}/${milestoneId}`, command).pipe(
      tap(updatedMilestone => {
        const currentMilestones = this.milestonesSubject.value;
        const index = currentMilestones.findIndex(m => m.milestoneId === milestoneId);
        if (index !== -1) {
          currentMilestones[index] = updatedMilestone;
          this.milestonesSubject.next([...currentMilestones]);
        }
        if (this.selectedMilestoneSubject.value?.milestoneId === milestoneId) {
          this.selectedMilestoneSubject.next(updatedMilestone);
        }
      })
    );
  }

  deleteMilestone(milestoneId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${milestoneId}`).pipe(
      tap(() => {
        const currentMilestones = this.milestonesSubject.value;
        this.milestonesSubject.next(currentMilestones.filter(m => m.milestoneId !== milestoneId));
        if (this.selectedMilestoneSubject.value?.milestoneId === milestoneId) {
          this.selectedMilestoneSubject.next(null);
        }
      })
    );
  }
}
