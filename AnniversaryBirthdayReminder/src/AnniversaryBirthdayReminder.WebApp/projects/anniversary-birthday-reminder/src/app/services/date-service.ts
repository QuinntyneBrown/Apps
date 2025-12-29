import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { tap, map } from 'rxjs/operators';
import { ImportantDate, DateType, RecurrencePattern } from '../models';
import { apiBaseUrl } from './api-config';

@Injectable({
  providedIn: 'root'
})
export class DateService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = apiBaseUrl;
  private readonly datesSubject = new BehaviorSubject<ImportantDate[]>([]);

  dates$ = this.datesSubject.asObservable();

  getDates(): Observable<ImportantDate[]> {
    return this.http.get<ImportantDate[]>(`${this.baseUrl}/api/dates`).pipe(
      tap(dates => this.datesSubject.next(dates))
    );
  }

  getUpcomingDates(): Observable<ImportantDate[]> {
    return this.http.get<ImportantDate[]>(`${this.baseUrl}/api/dates/upcoming`);
  }

  getDate(dateId: string): Observable<ImportantDate> {
    return this.http.get<ImportantDate>(`${this.baseUrl}/api/dates/${dateId}`);
  }

  createDate(date: Omit<ImportantDate, 'dateId' | 'userId' | 'createdAt'>): Observable<ImportantDate> {
    return this.http.post<ImportantDate>(`${this.baseUrl}/api/dates`, date).pipe(
      tap(newDate => {
        const current = this.datesSubject.value;
        this.datesSubject.next([...current, newDate]);
      })
    );
  }

  updateDate(dateId: string, date: Partial<ImportantDate>): Observable<ImportantDate> {
    return this.http.put<ImportantDate>(`${this.baseUrl}/api/dates/${dateId}`, date).pipe(
      tap(updatedDate => {
        const current = this.datesSubject.value;
        const index = current.findIndex(d => d.dateId === dateId);
        if (index >= 0) {
          const updated = [...current];
          updated[index] = updatedDate;
          this.datesSubject.next(updated);
        }
      })
    );
  }

  deleteDate(dateId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/api/dates/${dateId}`).pipe(
      tap(() => {
        const current = this.datesSubject.value;
        this.datesSubject.next(current.filter(d => d.dateId !== dateId));
      })
    );
  }

  getDaysUntil(date: ImportantDate): number {
    const today = new Date();
    today.setHours(0, 0, 0, 0);
    const targetDate = new Date(date.dateValue);
    targetDate.setFullYear(today.getFullYear());
    if (targetDate < today) {
      targetDate.setFullYear(today.getFullYear() + 1);
    }
    const diffTime = targetDate.getTime() - today.getTime();
    return Math.ceil(diffTime / (1000 * 60 * 60 * 24));
  }

  getDateTypeIcon(type: DateType): string {
    switch (type) {
      case DateType.Birthday:
        return 'cake';
      case DateType.Anniversary:
        return 'favorite';
      case DateType.Custom:
        return 'event';
    }
  }
}
