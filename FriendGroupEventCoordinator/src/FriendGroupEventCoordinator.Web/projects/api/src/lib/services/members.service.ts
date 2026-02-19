import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { Member, CreateMember, UpdateMember } from '../models';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  private readonly apiUrl = `${environment.baseUrl}/api/members`;
  private membersSubject = new BehaviorSubject<Member[]>([]);
  public members$ = this.membersSubject.asObservable();

  constructor(private http: HttpClient) {}

  getMember(id: string): Observable<Member> {
    return this.http.get<Member>(`${this.apiUrl}/${id}`);
  }

  getMembersByGroup(groupId: string): Observable<Member[]> {
    return this.http.get<Member[]>(`${this.apiUrl}/group/${groupId}`).pipe(
      tap(members => this.membersSubject.next(members))
    );
  }

  createMember(member: CreateMember): Observable<Member> {
    return this.http.post<Member>(this.apiUrl, member).pipe(
      tap(() => {
        if (member.groupId) {
          this.getMembersByGroup(member.groupId).subscribe();
        }
      })
    );
  }

  updateMember(id: string, member: UpdateMember): Observable<Member> {
    return this.http.put<Member>(`${this.apiUrl}/${id}`, member);
  }

  removeMember(id: string): Observable<Member> {
    return this.http.post<Member>(`${this.apiUrl}/${id}/remove`, {});
  }

  promoteToAdmin(id: string): Observable<Member> {
    return this.http.post<Member>(`${this.apiUrl}/${id}/promote`, {});
  }

  deleteMember(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
