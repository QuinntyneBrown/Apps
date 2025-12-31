import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Course } from '../models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CourseService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;

  private _coursesSubject = new BehaviorSubject<Course[]>([]);
  public courses$ = this._coursesSubject.asObservable();

  getCourses(): Observable<Course[]> {
    return this._http.get<Course[]>(`${this._baseUrl}/api/courses`).pipe(
      tap(courses => this._coursesSubject.next(courses))
    );
  }

  getCourseById(id: string): Observable<Course> {
    return this._http.get<Course>(`${this._baseUrl}/api/courses/${id}`);
  }

  createCourse(course: Partial<Course>): Observable<Course> {
    return this._http.post<Course>(`${this._baseUrl}/api/courses`, course).pipe(
      tap(() => this.getCourses().subscribe())
    );
  }

  updateCourse(id: string, course: Partial<Course>): Observable<Course> {
    return this._http.put<Course>(`${this._baseUrl}/api/courses/${id}`, course).pipe(
      tap(() => this.getCourses().subscribe())
    );
  }

  deleteCourse(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/courses/${id}`).pipe(
      tap(() => this.getCourses().subscribe())
    );
  }
}
