import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments';
import { ScheduleConflict, CreateConflictRequest } from '../models';

@Injectable({
  providedIn: 'root'
})
export class ConflictsService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = environment.baseUrl;

  getConflicts(memberId?: string, isResolved?: boolean): Observable<ScheduleConflict[]> {
    let params = new HttpParams();
    if (memberId) {
      params = params.set('memberId', memberId);
    }
    if (isResolved !== undefined) {
      params = params.set('isResolved', isResolved.toString());
    }
    return this.http.get<ScheduleConflict[]>(`${this.baseUrl}/api/conflicts`, { params });
  }

  createConflict(request: CreateConflictRequest): Observable<ScheduleConflict> {
    return this.http.post<ScheduleConflict>(`${this.baseUrl}/api/conflicts`, request);
  }

  resolveConflict(conflictId: string): Observable<ScheduleConflict> {
    return this.http.post<ScheduleConflict>(`${this.baseUrl}/api/conflicts/${conflictId}/resolve`, {});
  }
}
