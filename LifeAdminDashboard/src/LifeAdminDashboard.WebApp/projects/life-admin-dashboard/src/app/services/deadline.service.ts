import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments';
import { Deadline, CreateDeadline, UpdateDeadline } from '../models';

@Injectable({
  providedIn: 'root'
})
export class DeadlineService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/deadlines`;

  private deadlinesSubject = new BehaviorSubject<Deadline[]>([]);
  public deadlines$ = this.deadlinesSubject.asObservable();

  getAll(): Observable<Deadline[]> {
    return this.http.get<Deadline[]>(this.baseUrl).pipe(
      tap(deadlines => this.deadlinesSubject.next(deadlines))
    );
  }

  getById(id: string): Observable<Deadline> {
    return this.http.get<Deadline>(`${this.baseUrl}/${id}`);
  }

  create(deadline: CreateDeadline): Observable<Deadline> {
    return this.http.post<Deadline>(this.baseUrl, deadline).pipe(
      tap(newDeadline => {
        const deadlines = this.deadlinesSubject.value;
        this.deadlinesSubject.next([...deadlines, newDeadline]);
      })
    );
  }

  update(deadline: UpdateDeadline): Observable<Deadline> {
    return this.http.put<Deadline>(`${this.baseUrl}/${deadline.deadlineId}`, deadline).pipe(
      tap(updatedDeadline => {
        const deadlines = this.deadlinesSubject.value;
        const index = deadlines.findIndex(d => d.deadlineId === updatedDeadline.deadlineId);
        if (index !== -1) {
          deadlines[index] = updatedDeadline;
          this.deadlinesSubject.next([...deadlines]);
        }
      })
    );
  }

  complete(id: string): Observable<Deadline> {
    return this.http.patch<Deadline>(`${this.baseUrl}/${id}/complete`, {}).pipe(
      tap(updatedDeadline => {
        const deadlines = this.deadlinesSubject.value;
        const index = deadlines.findIndex(d => d.deadlineId === updatedDeadline.deadlineId);
        if (index !== -1) {
          deadlines[index] = updatedDeadline;
          this.deadlinesSubject.next([...deadlines]);
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const deadlines = this.deadlinesSubject.value.filter(d => d.deadlineId !== id);
        this.deadlinesSubject.next(deadlines);
      })
    );
  }
}
