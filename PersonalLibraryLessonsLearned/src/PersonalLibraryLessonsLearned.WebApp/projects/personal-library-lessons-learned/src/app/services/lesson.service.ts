import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Lesson, CreateLesson, UpdateLesson } from '../models';
import { environment } from '../environments';

@Injectable({
  providedIn: 'root'
})
export class LessonService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/lessons`;

  private lessonsSubject = new BehaviorSubject<Lesson[]>([]);
  public lessons$ = this.lessonsSubject.asObservable();

  private loadingSubject = new BehaviorSubject<boolean>(false);
  public loading$ = this.loadingSubject.asObservable();

  getLessons(): Observable<Lesson[]> {
    this.loadingSubject.next(true);
    return this.http.get<Lesson[]>(this.baseUrl).pipe(
      tap(lessons => {
        this.lessonsSubject.next(lessons);
        this.loadingSubject.next(false);
      })
    );
  }

  getLesson(id: string): Observable<Lesson> {
    return this.http.get<Lesson>(`${this.baseUrl}/${id}`);
  }

  createLesson(lesson: CreateLesson): Observable<Lesson> {
    return this.http.post<Lesson>(this.baseUrl, lesson).pipe(
      tap(newLesson => {
        const lessons = this.lessonsSubject.value;
        this.lessonsSubject.next([...lessons, newLesson]);
      })
    );
  }

  updateLesson(lesson: UpdateLesson): Observable<Lesson> {
    return this.http.put<Lesson>(`${this.baseUrl}/${lesson.lessonId}`, lesson).pipe(
      tap(updatedLesson => {
        const lessons = this.lessonsSubject.value;
        const index = lessons.findIndex(l => l.lessonId === updatedLesson.lessonId);
        if (index !== -1) {
          lessons[index] = updatedLesson;
          this.lessonsSubject.next([...lessons]);
        }
      })
    );
  }

  deleteLesson(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const lessons = this.lessonsSubject.value;
        this.lessonsSubject.next(lessons.filter(l => l.lessonId !== id));
      })
    );
  }
}
