import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { WearableData, CreateWearableData, UpdateWearableData } from '../models';
import { environment } from '../environments';

@Injectable({
  providedIn: 'root'
})
export class WearableDataService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/wearable-data`;

  private wearableDataSubject = new BehaviorSubject<WearableData[]>([]);
  public wearableData$ = this.wearableDataSubject.asObservable();

  private selectedWearableDataSubject = new BehaviorSubject<WearableData | null>(null);
  public selectedWearableData$ = this.selectedWearableDataSubject.asObservable();

  getAll(): Observable<WearableData[]> {
    return this.http.get<WearableData[]>(this.baseUrl).pipe(
      tap(wearableData => this.wearableDataSubject.next(wearableData))
    );
  }

  getById(id: string): Observable<WearableData> {
    return this.http.get<WearableData>(`${this.baseUrl}/${id}`).pipe(
      tap(wearableData => this.selectedWearableDataSubject.next(wearableData))
    );
  }

  create(wearableData: CreateWearableData): Observable<WearableData> {
    return this.http.post<WearableData>(this.baseUrl, wearableData).pipe(
      tap(newWearableData => {
        const current = this.wearableDataSubject.value;
        this.wearableDataSubject.next([...current, newWearableData]);
      })
    );
  }

  update(wearableData: UpdateWearableData): Observable<WearableData> {
    return this.http.put<WearableData>(`${this.baseUrl}/${wearableData.wearableDataId}`, wearableData).pipe(
      tap(updatedWearableData => {
        const current = this.wearableDataSubject.value;
        const index = current.findIndex(wd => wd.wearableDataId === updatedWearableData.wearableDataId);
        if (index !== -1) {
          current[index] = updatedWearableData;
          this.wearableDataSubject.next([...current]);
        }
        this.selectedWearableDataSubject.next(updatedWearableData);
      })
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const current = this.wearableDataSubject.value;
        this.wearableDataSubject.next(current.filter(wd => wd.wearableDataId !== id));
        if (this.selectedWearableDataSubject.value?.wearableDataId === id) {
          this.selectedWearableDataSubject.next(null);
        }
      })
    );
  }

  clearSelected(): void {
    this.selectedWearableDataSubject.next(null);
  }
}
