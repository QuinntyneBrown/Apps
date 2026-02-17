import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { Milestone, CreateMilestone, UpdateMilestone } from '../models';

@Injectable({
  providedIn: 'root'
})
export class MilestoneService {
  private readonly apiUrl = `${environment.baseUrl}/api/milestones`;
  private milestonesSubject = new BehaviorSubject<Milestone[]>([]);
  public milestones$ = this.milestonesSubject.asObservable();

  constructor(private http: HttpClient) {}

  getByGoal(goalId: string): Observable<Milestone[]> {
    return this.http.get<Milestone[]>(`${this.apiUrl}/by-goal/${goalId}`).pipe(
      tap(milestones => this.milestonesSubject.next(milestones))
    );
  }

  getById(id: string): Observable<Milestone> {
    return this.http.get<Milestone>(`${this.apiUrl}/${id}`);
  }

  create(command: CreateMilestone): Observable<Milestone> {
    return this.http.post<Milestone>(this.apiUrl, command).pipe(
      tap(milestone => {
        const current = this.milestonesSubject.value;
        this.milestonesSubject.next([...current, milestone]);
      })
    );
  }

  update(id: string, command: UpdateMilestone): Observable<Milestone> {
    return this.http.put<Milestone>(`${this.apiUrl}/${id}`, command).pipe(
      tap(updatedMilestone => {
        const current = this.milestonesSubject.value;
        const index = current.findIndex(m => m.milestoneId === id);
        if (index !== -1) {
          current[index] = updatedMilestone;
          this.milestonesSubject.next([...current]);
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => {
        const current = this.milestonesSubject.value;
        this.milestonesSubject.next(current.filter(m => m.milestoneId !== id));
      })
    );
  }
}
