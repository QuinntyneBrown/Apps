import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { LessonReminder, CreateLessonReminder, UpdateLessonReminder } from '../models';
import { environment } from '../environments';

@Injectable({
  providedIn: 'root'
})
export class LessonReminderService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/lessonreminders`;

  private remindersSubject = new BehaviorSubject<LessonReminder[]>([]);
  public reminders$ = this.remindersSubject.asObservable();

  private loadingSubject = new BehaviorSubject<boolean>(false);
  public loading$ = this.loadingSubject.asObservable();

  getReminders(): Observable<LessonReminder[]> {
    this.loadingSubject.next(true);
    return this.http.get<LessonReminder[]>(this.baseUrl).pipe(
      tap(reminders => {
        this.remindersSubject.next(reminders);
        this.loadingSubject.next(false);
      })
    );
  }

  getReminder(id: string): Observable<LessonReminder> {
    return this.http.get<LessonReminder>(`${this.baseUrl}/${id}`);
  }

  createReminder(reminder: CreateLessonReminder): Observable<LessonReminder> {
    return this.http.post<LessonReminder>(this.baseUrl, reminder).pipe(
      tap(newReminder => {
        const reminders = this.remindersSubject.value;
        this.remindersSubject.next([...reminders, newReminder]);
      })
    );
  }

  updateReminder(reminder: UpdateLessonReminder): Observable<LessonReminder> {
    return this.http.put<LessonReminder>(`${this.baseUrl}/${reminder.lessonReminderId}`, reminder).pipe(
      tap(updatedReminder => {
        const reminders = this.remindersSubject.value;
        const index = reminders.findIndex(r => r.lessonReminderId === updatedReminder.lessonReminderId);
        if (index !== -1) {
          reminders[index] = updatedReminder;
          this.remindersSubject.next([...reminders]);
        }
      })
    );
  }

  deleteReminder(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const reminders = this.remindersSubject.value;
        this.remindersSubject.next(reminders.filter(r => r.lessonReminderId !== id));
      })
    );
  }
}
