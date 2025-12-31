import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { Batch, CreateBatchRequest, UpdateBatchRequest } from '../models';

@Injectable({
  providedIn: 'root'
})
export class BatchService {
  private readonly apiUrl = `${environment.baseUrl}/api/batches`;
  private batchesSubject = new BehaviorSubject<Batch[]>([]);
  public batches$ = this.batchesSubject.asObservable();

  constructor(private http: HttpClient) {}

  getBatches(userId?: string, recipeId?: string): Observable<Batch[]> {
    let url = this.apiUrl;
    const params: string[] = [];

    if (userId) params.push(`userId=${userId}`);
    if (recipeId) params.push(`recipeId=${recipeId}`);

    if (params.length > 0) {
      url += `?${params.join('&')}`;
    }

    return this.http.get<Batch[]>(url).pipe(
      tap(batches => this.batchesSubject.next(batches))
    );
  }

  getBatch(id: string): Observable<Batch> {
    return this.http.get<Batch>(`${this.apiUrl}/${id}`);
  }

  createBatch(request: CreateBatchRequest): Observable<Batch> {
    return this.http.post<Batch>(this.apiUrl, request).pipe(
      tap(batch => {
        const current = this.batchesSubject.value;
        this.batchesSubject.next([...current, batch]);
      })
    );
  }

  updateBatch(id: string, request: UpdateBatchRequest): Observable<Batch> {
    return this.http.put<Batch>(`${this.apiUrl}/${id}`, request).pipe(
      tap(updatedBatch => {
        const current = this.batchesSubject.value;
        const index = current.findIndex(b => b.batchId === id);
        if (index !== -1) {
          current[index] = updatedBatch;
          this.batchesSubject.next([...current]);
        }
      })
    );
  }

  deleteBatch(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => {
        const current = this.batchesSubject.value;
        this.batchesSubject.next(current.filter(b => b.batchId !== id));
      })
    );
  }
}
