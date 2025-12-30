import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class ConflictsService {
  private readonly apiUrl = '/api/conflicts';

  constructor(private http: HttpClient) {}

  getConflicts(memberId?: string, isResolved?: boolean): Observable<any[]> {
    let params = '';
    if (memberId) params += `memberId=${memberId}`;
    if (isResolved !== undefined) params += `${params ? '&' : ''}isResolved=${isResolved}`;
    return this.http.get<any[]>(`${this.apiUrl}${params ? '?' + params : ''}`);
  }

  createConflict(command: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, command);
  }

  resolveConflict(conflictId: string): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/${conflictId}/resolve`, {});
  }
}
