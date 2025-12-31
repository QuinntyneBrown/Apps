import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { Progress, CreateProgress, UpdateProgress } from '../models';

@Injectable({
  providedIn: 'root'
})
export class ProgressService {
  private readonly apiUrl = `${environment.baseUrl}/api/progresses`;
  private progressesSubject = new BehaviorSubject<Progress[]>([]);
  public progresses$ = this.progressesSubject.asObservable();

  constructor(private http: HttpClient) {}

  getByGoal(goalId: string): Observable<Progress[]> {
    return this.http.get<Progress[]>(`${this.apiUrl}/by-goal/${goalId}`).pipe(
      tap(progresses => this.progressesSubject.next(progresses))
    );
  }

  getById(id: string): Observable<Progress> {
    return this.http.get<Progress>(`${this.apiUrl}/${id}`);
  }

  create(command: CreateProgress): Observable<Progress> {
    return this.http.post<Progress>(this.apiUrl, command).pipe(
      tap(progress => {
        const current = this.progressesSubject.value;
        this.progressesSubject.next([...current, progress]);
      })
    );
  }

  update(id: string, command: UpdateProgress): Observable<Progress> {
    return this.http.put<Progress>(`${this.apiUrl}/${id}`, command).pipe(
      tap(updatedProgress => {
        const current = this.progressesSubject.value;
        const index = current.findIndex(p => p.progressId === id);
        if (index !== -1) {
          current[index] = updatedProgress;
          this.progressesSubject.next([...current]);
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => {
        const current = this.progressesSubject.value;
        this.progressesSubject.next(current.filter(p => p.progressId !== id));
      })
    );
  }
}
