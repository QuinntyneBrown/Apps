import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Routine, CreateRoutineRequest, UpdateRoutineRequest } from '../models';
import { environment } from '../environments';

@Injectable({
  providedIn: 'root'
})
export class RoutineService {
  private http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/routines`;

  private routinesSubject = new BehaviorSubject<Routine[]>([]);
  public routines$ = this.routinesSubject.asObservable();

  getAll(): Observable<Routine[]> {
    return this.http.get<Routine[]>(this.baseUrl).pipe(
      tap(routines => this.routinesSubject.next(routines))
    );
  }

  getById(id: string): Observable<Routine> {
    return this.http.get<Routine>(`${this.baseUrl}/${id}`);
  }

  create(request: CreateRoutineRequest): Observable<Routine> {
    return this.http.post<Routine>(this.baseUrl, request).pipe(
      tap(routine => {
        const current = this.routinesSubject.value;
        this.routinesSubject.next([...current, routine]);
      })
    );
  }

  update(id: string, request: UpdateRoutineRequest): Observable<Routine> {
    return this.http.put<Routine>(`${this.baseUrl}/${id}`, request).pipe(
      tap(updated => {
        const current = this.routinesSubject.value;
        const index = current.findIndex(r => r.routineId === id);
        if (index !== -1) {
          current[index] = updated;
          this.routinesSubject.next([...current]);
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const current = this.routinesSubject.value;
        this.routinesSubject.next(current.filter(r => r.routineId !== id));
      })
    );
  }
}
