import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class FamilyMembersService {
  private readonly apiUrl = '/api/familymembers';

  constructor(private http: HttpClient) {}

  getFamilyMembers(familyId?: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}${familyId ? `?familyId=${familyId}` : ''}`);
  }

  getFamilyMemberById(memberId: string): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${memberId}`);
  }

  createFamilyMember(command: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, command);
  }

  updateFamilyMember(memberId: string, command: any): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/${memberId}`, command);
  }

  deleteFamilyMember(memberId: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${memberId}`);
  }
}
