// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class RemindersService {
  private readonly apiUrl = '/api/reminders';

  constructor(private http: HttpClient) {}

  getReminders(eventId?: string, recipientId?: string): Observable<any[]> {
    let params = '';
    if (eventId) params += `eventId=${eventId}`;
    if (recipientId) params += `${params ? '&' : ''}recipientId=${recipientId}`;
    return this.http.get<any[]>(`${this.apiUrl}${params ? '?' + params : ''}`);
  }

  createReminder(command: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, command);
  }

  rescheduleReminder(reminderId: string, command: any): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/${reminderId}/reschedule`, command);
  }
}
