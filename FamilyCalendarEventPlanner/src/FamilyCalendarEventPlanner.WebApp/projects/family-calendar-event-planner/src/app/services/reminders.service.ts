import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments';
import { EventReminder, CreateReminderRequest, RescheduleReminderRequest } from '../models';

@Injectable({
  providedIn: 'root'
})
export class RemindersService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = environment.baseUrl;

  getReminders(eventId?: string, recipientId?: string): Observable<EventReminder[]> {
    let params = new HttpParams();
    if (eventId) {
      params = params.set('eventId', eventId);
    }
    if (recipientId) {
      params = params.set('recipientId', recipientId);
    }
    return this.http.get<EventReminder[]>(`${this.baseUrl}/api/reminders`, { params });
  }

  createReminder(request: CreateReminderRequest): Observable<EventReminder> {
    return this.http.post<EventReminder>(`${this.baseUrl}/api/reminders`, request);
  }

  rescheduleReminder(request: RescheduleReminderRequest): Observable<EventReminder> {
    return this.http.put<EventReminder>(
      `${this.baseUrl}/api/reminders/${request.reminderId}/reschedule`,
      request
    );
  }

  sendReminder(reminderId: string): Observable<EventReminder> {
    return this.http.post<EventReminder>(`${this.baseUrl}/api/reminders/${reminderId}/send`, {});
  }

  deleteReminder(reminderId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/api/reminders/${reminderId}`);
  }
}
