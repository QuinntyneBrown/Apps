import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments';
import { Milestone, CreateMilestone, UpdateMilestone } from '../models';

@Injectable({ providedIn: 'root' })
export class MilestoneService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;

  private _milestonesSubject = new BehaviorSubject<Milestone[]>([]);
  milestones$ = this._milestonesSubject.asObservable();

  getAll(injuryId?: string): Observable<Milestone[]> {
    let params = new HttpParams();
    if (injuryId) params = params.set('injuryId', injuryId);

    return this._http.get<Milestone[]>(`${this._baseUrl}/api/Milestones`, { params }).pipe(
      tap(milestones => this._milestonesSubject.next(milestones))
    );
  }

  getById(id: string): Observable<Milestone> {
    return this._http.get<Milestone>(`${this._baseUrl}/api/Milestones/${id}`);
  }

  create(milestone: CreateMilestone): Observable<Milestone> {
    return this._http.post<Milestone>(`${this._baseUrl}/api/Milestones`, milestone).pipe(
      tap(created => {
        const current = this._milestonesSubject.value;
        this._milestonesSubject.next([...current, created]);
      })
    );
  }

  update(id: string, milestone: UpdateMilestone): Observable<Milestone> {
    return this._http.put<Milestone>(`${this._baseUrl}/api/Milestones/${id}`, milestone).pipe(
      tap(updated => {
        const current = this._milestonesSubject.value;
        const index = current.findIndex(m => m.milestoneId === id);
        if (index !== -1) {
          current[index] = updated;
          this._milestonesSubject.next([...current]);
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/Milestones/${id}`).pipe(
      tap(() => {
        const current = this._milestonesSubject.value;
        this._milestonesSubject.next(current.filter(m => m.milestoneId !== id));
      })
    );
  }
}
