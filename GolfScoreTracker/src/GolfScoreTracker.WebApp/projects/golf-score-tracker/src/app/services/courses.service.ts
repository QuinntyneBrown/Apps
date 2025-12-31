import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Course, CreateCourseCommand, UpdateCourseCommand } from '../models';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CoursesService {
  private readonly apiUrl = `${environment.baseUrl}/api/Courses`;
  private coursesSubject = new BehaviorSubject<Course[]>([]);
  public courses$ = this.coursesSubject.asObservable();

  private currentCourseSubject = new BehaviorSubject<Course | null>(null);
  public currentCourse$ = this.currentCourseSubject.asObservable();

  constructor(private http: HttpClient) {}

  getCourses(): Observable<Course[]> {
    return this.http.get<Course[]>(this.apiUrl).pipe(
      tap(courses => this.coursesSubject.next(courses))
    );
  }

  getCourseById(id: string): Observable<Course> {
    return this.http.get<Course>(`${this.apiUrl}/${id}`).pipe(
      tap(course => this.currentCourseSubject.next(course))
    );
  }

  createCourse(command: CreateCourseCommand): Observable<Course> {
    return this.http.post<Course>(this.apiUrl, command).pipe(
      tap(course => {
        const currentCourses = this.coursesSubject.value;
        this.coursesSubject.next([...currentCourses, course]);
      })
    );
  }

  updateCourse(id: string, command: UpdateCourseCommand): Observable<Course> {
    return this.http.put<Course>(`${this.apiUrl}/${id}`, command).pipe(
      tap(updatedCourse => {
        const currentCourses = this.coursesSubject.value;
        const index = currentCourses.findIndex(c => c.courseId === id);
        if (index !== -1) {
          currentCourses[index] = updatedCourse;
          this.coursesSubject.next([...currentCourses]);
        }
        this.currentCourseSubject.next(updatedCourse);
      })
    );
  }

  deleteCourse(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => {
        const currentCourses = this.coursesSubject.value;
        this.coursesSubject.next(currentCourses.filter(c => c.courseId !== id));
      })
    );
  }

  clearCurrentCourse(): void {
    this.currentCourseSubject.next(null);
  }
}
