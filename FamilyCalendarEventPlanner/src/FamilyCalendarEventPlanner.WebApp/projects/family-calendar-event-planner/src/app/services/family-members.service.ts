import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments';
import {
  FamilyMember,
  CreateFamilyMemberRequest,
  UpdateFamilyMemberRequest,
  ChangeMemberRoleRequest
} from './models';

@Injectable({
  providedIn: 'root'
})
export class FamilyMembersService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = environment.apiBaseUrl;

  getFamilyMembers(familyId?: string): Observable<FamilyMember[]> {
    let params = new HttpParams();
    if (familyId) {
      params = params.set('familyId', familyId);
    }
    return this.http.get<FamilyMember[]>(`${this.baseUrl}/api/familymembers`, { params });
  }

  getFamilyMemberById(memberId: string): Observable<FamilyMember> {
    return this.http.get<FamilyMember>(`${this.baseUrl}/api/familymembers/${memberId}`);
  }

  createFamilyMember(request: CreateFamilyMemberRequest): Observable<FamilyMember> {
    return this.http.post<FamilyMember>(`${this.baseUrl}/api/familymembers`, request);
  }

  updateFamilyMember(request: UpdateFamilyMemberRequest): Observable<FamilyMember> {
    return this.http.put<FamilyMember>(`${this.baseUrl}/api/familymembers/${request.memberId}`, request);
  }

  changeMemberRole(request: ChangeMemberRoleRequest): Observable<FamilyMember> {
    return this.http.put<FamilyMember>(`${this.baseUrl}/api/familymembers/${request.memberId}/role`, request);
  }
}
