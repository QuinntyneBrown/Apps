import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments';
import { Milestone, CreateMilestone, UpdateMilestone } from '../models';

@Injectable({
  providedIn: 'root'
})
export class MilestoneService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/milestones`;

  private milestonesSubject = new BehaviorSubject<Milestone[]>([]);
  public milestones$ = this.milestonesSubject.asObservable();

  private selectedMilestoneSubject = new BehaviorSubject<Milestone | null>(null);
  public selectedMilestone$ = this.selectedMilestoneSubject.asObservable();

  getMilestones(): Observable<Milestone[]> {
    return this.http.get<Milestone[]>(this.baseUrl).pipe(
      tap(milestones => this.milestonesSubject.next(milestones))
    );
  }

  getMilestoneById(id: string): Observable<Milestone> {
    return this.http.get<Milestone>(`${this.baseUrl}/${id}`).pipe(
      tap(milestone => this.selectedMilestoneSubject.next(milestone))
    );
  }

  createMilestone(milestone: CreateMilestone): Observable<Milestone> {
    return this.http.post<Milestone>(this.baseUrl, milestone).pipe(
      tap(newMilestone => {
        const currentMilestones = this.milestonesSubject.value;
        this.milestonesSubject.next([...currentMilestones, newMilestone]);
      })
    );
  }

  updateMilestone(milestone: UpdateMilestone): Observable<Milestone> {
    return this.http.put<Milestone>(`${this.baseUrl}/${milestone.milestoneId}`, milestone).pipe(
      tap(updatedMilestone => {
        const currentMilestones = this.milestonesSubject.value;
        const index = currentMilestones.findIndex(m => m.milestoneId === updatedMilestone.milestoneId);
        if (index !== -1) {
          currentMilestones[index] = updatedMilestone;
          this.milestonesSubject.next([...currentMilestones]);
        }
        this.selectedMilestoneSubject.next(updatedMilestone);
      })
    );
  }

  deleteMilestone(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const currentMilestones = this.milestonesSubject.value;
        this.milestonesSubject.next(currentMilestones.filter(m => m.milestoneId !== id));
        if (this.selectedMilestoneSubject.value?.milestoneId === id) {
          this.selectedMilestoneSubject.next(null);
        }
      })
    );
  }

  selectMilestone(milestone: Milestone | null): void {
    this.selectedMilestoneSubject.next(milestone);
  }
}
