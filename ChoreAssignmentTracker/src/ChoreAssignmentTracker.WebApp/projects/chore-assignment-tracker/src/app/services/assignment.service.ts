import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments';
import { Assignment, CreateAssignment, UpdateAssignment, CompleteAssignment, VerifyAssignment } from '../models';

@Injectable({
  providedIn: 'root'
})
export class AssignmentService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/assignments`;

  private assignmentsSubject = new BehaviorSubject<Assignment[]>([]);
  public assignments$ = this.assignmentsSubject.asObservable();

  getAll(familyMemberId?: string, choreId?: string, isCompleted?: boolean, isOverdue?: boolean): Observable<Assignment[]> {
    let params = new HttpParams();
    if (familyMemberId) params = params.set('familyMemberId', familyMemberId);
    if (choreId) params = params.set('choreId', choreId);
    if (isCompleted !== undefined) params = params.set('isCompleted', isCompleted.toString());
    if (isOverdue !== undefined) params = params.set('isOverdue', isOverdue.toString());

    return this.http.get<Assignment[]>(this.baseUrl, { params }).pipe(
      tap(assignments => this.assignmentsSubject.next(assignments))
    );
  }

  getById(id: string): Observable<Assignment> {
    return this.http.get<Assignment>(`${this.baseUrl}/${id}`);
  }

  create(assignment: CreateAssignment): Observable<Assignment> {
    return this.http.post<Assignment>(this.baseUrl, assignment).pipe(
      tap(() => this.refresh())
    );
  }

  update(id: string, assignment: UpdateAssignment): Observable<Assignment> {
    return this.http.put<Assignment>(`${this.baseUrl}/${id}`, assignment).pipe(
      tap(() => this.refresh())
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => this.refresh())
    );
  }

  complete(id: string, data: CompleteAssignment): Observable<Assignment> {
    return this.http.post<Assignment>(`${this.baseUrl}/${id}/complete`, data).pipe(
      tap(() => this.refresh())
    );
  }

  verify(id: string, data: VerifyAssignment): Observable<Assignment> {
    return this.http.post<Assignment>(`${this.baseUrl}/${id}/verify`, data).pipe(
      tap(() => this.refresh())
    );
  }

  private refresh(): void {
    this.getAll().subscribe();
  }
}
