import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Reminder, ReminderSettings, DeliveryChannel } from '../models';
import { apiBaseUrl } from './api-config';

@Injectable({
  providedIn: 'root'
})
export class ReminderService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = apiBaseUrl;
  private readonly remindersSubject = new BehaviorSubject<Reminder[]>([]);
  private readonly settingsSubject = new BehaviorSubject<ReminderSettings>({
    oneWeekBefore: true,
    threeDaysBefore: true,
    oneDayBefore: true,
    channels: [DeliveryChannel.Email, DeliveryChannel.Push]
  });

  reminders$ = this.remindersSubject.asObservable();
  settings$ = this.settingsSubject.asObservable();

  getReminders(): Observable<Reminder[]> {
    return this.http.get<Reminder[]>(`${this.baseUrl}/api/reminders`).pipe(
      tap(reminders => this.remindersSubject.next(reminders))
    );
  }

  getRemindersByDate(dateId: string): Observable<Reminder[]> {
    return this.http.get<Reminder[]>(`${this.baseUrl}/api/dates/${dateId}/reminders`);
  }

  scheduleReminder(dateId: string, reminder: Partial<Reminder>): Observable<Reminder> {
    return this.http.post<Reminder>(`${this.baseUrl}/api/dates/${dateId}/reminders`, reminder).pipe(
      tap(newReminder => {
        const current = this.remindersSubject.value;
        this.remindersSubject.next([...current, newReminder]);
      })
    );
  }

  dismissReminder(reminderId: string): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/api/reminders/${reminderId}/dismiss`, {}).pipe(
      tap(() => {
        const current = this.remindersSubject.value;
        this.remindersSubject.next(current.filter(r => r.reminderId !== reminderId));
      })
    );
  }

  snoozeReminder(reminderId: string, minutes: number): Observable<Reminder> {
    return this.http.post<Reminder>(`${this.baseUrl}/api/reminders/${reminderId}/snooze`, { minutes });
  }

  getSettings(): Observable<ReminderSettings> {
    return this.http.get<ReminderSettings>(`${this.baseUrl}/api/reminders/settings`).pipe(
      tap(settings => this.settingsSubject.next(settings))
    );
  }

  updateSettings(settings: ReminderSettings): Observable<ReminderSettings> {
    return this.http.put<ReminderSettings>(`${this.baseUrl}/api/reminders/settings`, settings).pipe(
      tap(updatedSettings => this.settingsSubject.next(updatedSettings))
    );
  }

  updateLocalSettings(settings: ReminderSettings): void {
    this.settingsSubject.next(settings);
  }
}
