import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments/environment';
import { Reminder, CreateReminderCommand, UpdateReminderCommand } from '../models';

@Injectable({
  providedIn: 'root'
})
export class ReminderService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.baseUrl}/api/reminder`;

  private remindersSubject = new BehaviorSubject<Reminder[]>([]);
  public reminders$ = this.remindersSubject.asObservable();

  private loadingSubject = new BehaviorSubject<boolean>(false);
  public loading$ = this.loadingSubject.asObservable();

  getReminders(): Observable<Reminder[]> {
    this.loadingSubject.next(true);
    return this.http.get<Reminder[]>(this.apiUrl).pipe(
      tap(reminders => {
        this.remindersSubject.next(reminders);
        this.loadingSubject.next(false);
      })
    );
  }

  getReminderById(id: string): Observable<Reminder> {
    return this.http.get<Reminder>(`${this.apiUrl}/${id}`);
  }

  createReminder(command: CreateReminderCommand): Observable<Reminder> {
    return this.http.post<Reminder>(this.apiUrl, command).pipe(
      tap(reminder => {
        const currentReminders = this.remindersSubject.value;
        this.remindersSubject.next([...currentReminders, reminder]);
      })
    );
  }

  updateReminder(id: string, command: UpdateReminderCommand): Observable<Reminder> {
    return this.http.put<Reminder>(`${this.apiUrl}/${id}`, command).pipe(
      tap(updatedReminder => {
        const currentReminders = this.remindersSubject.value;
        const index = currentReminders.findIndex(r => r.reminderId === id);
        if (index !== -1) {
          currentReminders[index] = updatedReminder;
          this.remindersSubject.next([...currentReminders]);
        }
      })
    );
  }

  deleteReminder(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => {
        const currentReminders = this.remindersSubject.value;
        this.remindersSubject.next(currentReminders.filter(r => r.reminderId !== id));
      })
    );
  }
}
