import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments';
import { FamilyMemberDto } from '../models/family-member-dto';
import { CreateFamilyMemberCommand } from '../models/create-family-member-command';

@Injectable({ providedIn: 'root' })
export class FamilyMembersService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = environment.apiBaseUrl;

  getFamilyMembers(familyId?: string): Observable<FamilyMemberDto[]> {
    let params = new HttpParams();
    if (familyId) {
      params = params.set('familyId', familyId);
    }
    return this.http.get<FamilyMemberDto[]>(`${this.baseUrl}/api/familymembers`, { params });
  }

  getFamilyMemberById(memberId: string): Observable<FamilyMemberDto> {
    return this.http.get<FamilyMemberDto>(`${this.baseUrl}/api/familymembers/${memberId}`);
  }

  createFamilyMember(command: CreateFamilyMemberCommand): Observable<FamilyMemberDto> {
    return this.http.post<FamilyMemberDto>(`${this.baseUrl}/api/familymembers`, command);
  }

  updateFamilyMember(memberId: string, command: Partial<CreateFamilyMemberCommand>): Observable<FamilyMemberDto> {
    return this.http.put<FamilyMemberDto>(`${this.baseUrl}/api/familymembers/${memberId}`, command);
  }

  deleteFamilyMember(memberId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/api/familymembers/${memberId}`);
  }
}
