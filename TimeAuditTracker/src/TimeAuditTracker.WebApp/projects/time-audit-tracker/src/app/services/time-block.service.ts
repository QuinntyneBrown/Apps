import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { TimeBlock, CreateTimeBlockRequest, UpdateTimeBlockRequest } from '../models';

@Injectable({
  providedIn: 'root'
})
export class TimeBlockService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;
  private _timeBlocksSubject = new BehaviorSubject<TimeBlock[]>([]);

  timeBlocks$ = this._timeBlocksSubject.asObservable();

  getAll(): Observable<TimeBlock[]> {
    return this._http.get<TimeBlock[]>(`${this._baseUrl}/api/timeblocks`).pipe(
      tap(timeBlocks => this._timeBlocksSubject.next(timeBlocks))
    );
  }

  getById(id: string): Observable<TimeBlock> {
    return this._http.get<TimeBlock>(`${this._baseUrl}/api/timeblocks/${id}`);
  }

  create(request: CreateTimeBlockRequest): Observable<TimeBlock> {
    return this._http.post<TimeBlock>(`${this._baseUrl}/api/timeblocks`, request).pipe(
      tap(timeBlock => {
        const current = this._timeBlocksSubject.value;
        this._timeBlocksSubject.next([...current, timeBlock]);
      })
    );
  }

  update(id: string, request: UpdateTimeBlockRequest): Observable<void> {
    return this._http.put<void>(`${this._baseUrl}/api/timeblocks/${id}`, request).pipe(
      tap(() => {
        const current = this._timeBlocksSubject.value;
        const index = current.findIndex(tb => tb.timeBlockId === id);
        if (index !== -1) {
          this.getAll().subscribe();
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/timeblocks/${id}`).pipe(
      tap(() => {
        const current = this._timeBlocksSubject.value;
        this._timeBlocksSubject.next(current.filter(tb => tb.timeBlockId !== id));
      })
    );
  }
}
