import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments';
import { FamilyMember, CreateFamilyMember, UpdateFamilyMember } from '../models';

@Injectable({
  providedIn: 'root'
})
export class FamilyMemberService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/familymembers`;

  private familyMembersSubject = new BehaviorSubject<FamilyMember[]>([]);
  public familyMembers$ = this.familyMembersSubject.asObservable();

  getAll(userId?: string): Observable<FamilyMember[]> {
    let params = new HttpParams();
    if (userId) params = params.set('userId', userId);

    return this.http.get<FamilyMember[]>(this.baseUrl, { params }).pipe(
      tap(familyMembers => this.familyMembersSubject.next(familyMembers))
    );
  }

  getById(id: string): Observable<FamilyMember> {
    return this.http.get<FamilyMember>(`${this.baseUrl}/${id}`);
  }

  create(familyMember: CreateFamilyMember): Observable<FamilyMember> {
    return this.http.post<FamilyMember>(this.baseUrl, familyMember).pipe(
      tap(() => this.refresh())
    );
  }

  update(id: string, familyMember: UpdateFamilyMember): Observable<FamilyMember> {
    return this.http.put<FamilyMember>(`${this.baseUrl}/${id}`, familyMember).pipe(
      tap(() => this.refresh())
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => this.refresh())
    );
  }

  private refresh(): void {
    this.getAll().subscribe();
  }
}
