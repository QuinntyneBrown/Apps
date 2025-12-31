import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { Memory } from '../models';

@Injectable({
  providedIn: 'root'
})
export class MemoryService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/memories`;

  private memoriesSubject = new BehaviorSubject<Memory[]>([]);
  public memories$ = this.memoriesSubject.asObservable();

  getMemories(userId?: string, tripId?: string, startDate?: string, endDate?: string): Observable<Memory[]> {
    let url = this.baseUrl;
    const params: string[] = [];

    if (userId) params.push(`userId=${userId}`);
    if (tripId) params.push(`tripId=${tripId}`);
    if (startDate) params.push(`startDate=${startDate}`);
    if (endDate) params.push(`endDate=${endDate}`);

    if (params.length > 0) {
      url += `?${params.join('&')}`;
    }

    return this.http.get<Memory[]>(url).pipe(
      tap(memories => this.memoriesSubject.next(memories))
    );
  }

  getMemoryById(memoryId: string): Observable<Memory> {
    return this.http.get<Memory>(`${this.baseUrl}/${memoryId}`);
  }

  createMemory(memory: Partial<Memory>): Observable<Memory> {
    return this.http.post<Memory>(this.baseUrl, memory).pipe(
      tap(() => this.refreshMemories(memory.userId))
    );
  }

  updateMemory(memoryId: string, memory: Partial<Memory>): Observable<Memory> {
    return this.http.put<Memory>(`${this.baseUrl}/${memoryId}`, {
      ...memory,
      memoryId
    }).pipe(
      tap(() => this.refreshMemories(memory.userId))
    );
  }

  deleteMemory(memoryId: string, userId?: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${memoryId}`).pipe(
      tap(() => this.refreshMemories(userId))
    );
  }

  private refreshMemories(userId?: string): void {
    if (userId) {
      this.getMemories(userId).subscribe();
    }
  }
}
