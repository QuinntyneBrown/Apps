import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { NetWorthSnapshot, CreateNetWorthSnapshot, UpdateNetWorthSnapshot } from '../models';
import { environment } from '../environments';

@Injectable({
  providedIn: 'root'
})
export class NetWorthSnapshotService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/networthsnapshots`;

  private snapshotsSubject = new BehaviorSubject<NetWorthSnapshot[]>([]);
  public snapshots$ = this.snapshotsSubject.asObservable();

  getSnapshots(): Observable<NetWorthSnapshot[]> {
    return this.http.get<NetWorthSnapshot[]>(this.baseUrl).pipe(
      tap(snapshots => this.snapshotsSubject.next(snapshots))
    );
  }

  getSnapshotById(id: string): Observable<NetWorthSnapshot> {
    return this.http.get<NetWorthSnapshot>(`${this.baseUrl}/${id}`);
  }

  createSnapshot(snapshot: CreateNetWorthSnapshot): Observable<NetWorthSnapshot> {
    return this.http.post<NetWorthSnapshot>(this.baseUrl, snapshot).pipe(
      tap(() => this.getSnapshots().subscribe())
    );
  }

  updateSnapshot(snapshot: UpdateNetWorthSnapshot): Observable<NetWorthSnapshot> {
    return this.http.put<NetWorthSnapshot>(`${this.baseUrl}/${snapshot.netWorthSnapshotId}`, snapshot).pipe(
      tap(() => this.getSnapshots().subscribe())
    );
  }

  deleteSnapshot(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => this.getSnapshots().subscribe())
    );
  }
}
