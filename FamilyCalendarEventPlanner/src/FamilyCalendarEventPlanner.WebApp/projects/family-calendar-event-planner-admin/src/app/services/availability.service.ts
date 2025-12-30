import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class AvailabilityService {
  private readonly apiUrl = '/api/availability';

  constructor(private http: HttpClient) {}

  getAvailabilityBlocks(memberId: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}?memberId=${memberId}`);
  }

  createAvailabilityBlock(command: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, command);
  }

  deleteAvailabilityBlock(blockId: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${blockId}`);
  }
}
