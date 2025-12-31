import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { Memory, CreateMemory, UpdateMemory } from '../models';

@Injectable({
  providedIn: 'root'
})
export class MemoryService {
  private readonly apiUrl = `${environment.baseUrl}/api/Memories`;
  private memoriesSubject = new BehaviorSubject<Memory[]>([]);
  public memories$ = this.memoriesSubject.asObservable();

  constructor(private http: HttpClient) {}

  getMemories(userId?: string, bucketListItemId?: string): Observable<Memory[]> {
    let params = new HttpParams();
    if (userId) params = params.set('userId', userId);
    if (bucketListItemId) params = params.set('bucketListItemId', bucketListItemId);

    return this.http.get<Memory[]>(this.apiUrl, { params }).pipe(
      tap(memories => this.memoriesSubject.next(memories))
    );
  }

  getMemoryById(memoryId: string): Observable<Memory> {
    return this.http.get<Memory>(`${this.apiUrl}/${memoryId}`);
  }

  createMemory(command: CreateMemory): Observable<Memory> {
    return this.http.post<Memory>(this.apiUrl, command).pipe(
      tap(newMemory => {
        const currentMemories = this.memoriesSubject.value;
        this.memoriesSubject.next([...currentMemories, newMemory]);
      })
    );
  }

  updateMemory(memoryId: string, command: UpdateMemory): Observable<Memory> {
    return this.http.put<Memory>(`${this.apiUrl}/${memoryId}`, command).pipe(
      tap(updatedMemory => {
        const currentMemories = this.memoriesSubject.value;
        const index = currentMemories.findIndex(memory => memory.memoryId === memoryId);
        if (index !== -1) {
          currentMemories[index] = updatedMemory;
          this.memoriesSubject.next([...currentMemories]);
        }
      })
    );
  }

  deleteMemory(memoryId: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${memoryId}`).pipe(
      tap(() => {
        const currentMemories = this.memoriesSubject.value;
        this.memoriesSubject.next(currentMemories.filter(memory => memory.memoryId !== memoryId));
      })
    );
  }
}
