import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Reminder } from '../models';
import { environment } from '../environments';

@Injectable({
  providedIn: 'root'
})
export class ReminderService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = `${environment.baseUrl}/api/reminders`;

  private remindersSubject = new BehaviorSubject<Reminder[]>([]);
  public reminders$ = this.remindersSubject.asObservable();

  getAll(userId?: string, screeningId?: string, isSent?: boolean): Observable<Reminder[]> {
    let url = this.apiUrl;
    const params: string[] = [];
    if (userId) params.push(`userId=${userId}`);
    if (screeningId) params.push(`screeningId=${screeningId}`);
    if (isSent !== undefined) params.push(`isSent=${isSent}`);
    if (params.length > 0) url += `?${params.join('&')}`;

    return this.http.get<Reminder[]>(url).pipe(
      tap(reminders => this.remindersSubject.next(reminders))
    );
  }

  getById(id: string): Observable<Reminder> {
    return this.http.get<Reminder>(`${this.apiUrl}/${id}`);
  }

  create(reminder: Partial<Reminder>): Observable<Reminder> {
    return this.http.post<Reminder>(this.apiUrl, reminder).pipe(
      tap(() => this.refreshReminders())
    );
  }

  update(id: string, reminder: Partial<Reminder>): Observable<Reminder> {
    return this.http.put<Reminder>(`${this.apiUrl}/${id}`, reminder).pipe(
      tap(() => this.refreshReminders())
    );
  }

  markAsSent(id: string): Observable<Reminder> {
    return this.http.patch<Reminder>(`${this.apiUrl}/${id}/mark-sent`, {}).pipe(
      tap(() => this.refreshReminders())
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => this.refreshReminders())
    );
  }

  private refreshReminders(): void {
    this.getAll().subscribe();
  }
}
