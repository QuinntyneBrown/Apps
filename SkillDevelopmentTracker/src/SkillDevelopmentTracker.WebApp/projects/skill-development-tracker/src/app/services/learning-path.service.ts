import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { LearningPath } from '../models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LearningPathService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;

  private _learningPathsSubject = new BehaviorSubject<LearningPath[]>([]);
  public learningPaths$ = this._learningPathsSubject.asObservable();

  getLearningPaths(): Observable<LearningPath[]> {
    return this._http.get<LearningPath[]>(`${this._baseUrl}/api/learningpaths`).pipe(
      tap(learningPaths => this._learningPathsSubject.next(learningPaths))
    );
  }

  getLearningPathById(id: string): Observable<LearningPath> {
    return this._http.get<LearningPath>(`${this._baseUrl}/api/learningpaths/${id}`);
  }

  createLearningPath(learningPath: Partial<LearningPath>): Observable<LearningPath> {
    return this._http.post<LearningPath>(`${this._baseUrl}/api/learningpaths`, learningPath).pipe(
      tap(() => this.getLearningPaths().subscribe())
    );
  }

  updateLearningPath(id: string, learningPath: Partial<LearningPath>): Observable<LearningPath> {
    return this._http.put<LearningPath>(`${this._baseUrl}/api/learningpaths/${id}`, learningPath).pipe(
      tap(() => this.getLearningPaths().subscribe())
    );
  }

  deleteLearningPath(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/learningpaths/${id}`).pipe(
      tap(() => this.getLearningPaths().subscribe())
    );
  }
}
