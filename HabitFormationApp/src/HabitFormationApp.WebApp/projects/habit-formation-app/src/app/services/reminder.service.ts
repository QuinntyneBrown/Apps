import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { Reminder, CreateReminderRequest, UpdateReminderRequest } from '../models';

@Injectable({
  providedIn: 'root'
})
export class ReminderService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/reminders`;

  private remindersSubject = new BehaviorSubject<Reminder[]>([]);
  public reminders$ = this.remindersSubject.asObservable();

  getReminders(userId?: string, habitId?: string): Observable<Reminder[]> {
    let url = this.baseUrl;
    const params: string[] = [];

    if (userId) {
      params.push(`userId=${userId}`);
    }
    if (habitId) {
      params.push(`habitId=${habitId}`);
    }

    if (params.length > 0) {
      url += `?${params.join('&')}`;
    }

    return this.http.get<Reminder[]>(url).pipe(
      tap(reminders => this.remindersSubject.next(reminders))
    );
  }

  getReminderById(reminderId: string): Observable<Reminder> {
    return this.http.get<Reminder>(`${this.baseUrl}/${reminderId}`);
  }

  createReminder(request: CreateReminderRequest): Observable<Reminder> {
    return this.http.post<Reminder>(this.baseUrl, request).pipe(
      tap(reminder => {
        const currentReminders = this.remindersSubject.value;
        this.remindersSubject.next([...currentReminders, reminder]);
      })
    );
  }

  updateReminder(reminderId: string, request: UpdateReminderRequest): Observable<Reminder> {
    return this.http.put<Reminder>(`${this.baseUrl}/${reminderId}`, request).pipe(
      tap(updatedReminder => {
        const currentReminders = this.remindersSubject.value;
        const index = currentReminders.findIndex(r => r.reminderId === reminderId);
        if (index !== -1) {
          currentReminders[index] = updatedReminder;
          this.remindersSubject.next([...currentReminders]);
        }
      })
    );
  }

  deleteReminder(reminderId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${reminderId}`).pipe(
      tap(() => {
        const currentReminders = this.remindersSubject.value;
        this.remindersSubject.next(currentReminders.filter(r => r.reminderId !== reminderId));
      })
    );
  }

  toggleReminder(reminderId: string): Observable<Reminder> {
    return this.http.put<Reminder>(`${this.baseUrl}/${reminderId}/toggle`, {}).pipe(
      tap(updatedReminder => {
        const currentReminders = this.remindersSubject.value;
        const index = currentReminders.findIndex(r => r.reminderId === reminderId);
        if (index !== -1) {
          currentReminders[index] = updatedReminder;
          this.remindersSubject.next([...currentReminders]);
        }
      })
    );
  }
}
