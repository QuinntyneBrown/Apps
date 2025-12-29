import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments';
import { AvailabilityBlock, CreateAvailabilityBlockRequest } from './models';

@Injectable({
  providedIn: 'root'
})
export class AvailabilityService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = environment.apiBaseUrl;

  getAvailabilityBlocks(memberId: string): Observable<AvailabilityBlock[]> {
    const params = new HttpParams().set('memberId', memberId);
    return this.http.get<AvailabilityBlock[]>(`${this.baseUrl}/api/availability`, { params });
  }

  createAvailabilityBlock(request: CreateAvailabilityBlockRequest): Observable<AvailabilityBlock> {
    return this.http.post<AvailabilityBlock>(`${this.baseUrl}/api/availability`, request);
  }

  deleteAvailabilityBlock(blockId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/api/availability/${blockId}`);
  }
}
