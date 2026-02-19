import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments';
import { JournalEntry, CreateJournalEntry, UpdateJournalEntry } from '../models';

@Injectable({
  providedIn: 'root'
})
export class JournalEntryService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/journal-entries`;

  private journalEntriesSubject = new BehaviorSubject<JournalEntry[]>([]);
  public journalEntries$ = this.journalEntriesSubject.asObservable();

  private selectedJournalEntrySubject = new BehaviorSubject<JournalEntry | null>(null);
  public selectedJournalEntry$ = this.selectedJournalEntrySubject.asObservable();

  getAll(): Observable<JournalEntry[]> {
    return this.http.get<JournalEntry[]>(this.baseUrl).pipe(
      tap(journalEntries => this.journalEntriesSubject.next(journalEntries))
    );
  }

  getById(id: string): Observable<JournalEntry> {
    return this.http.get<JournalEntry>(`${this.baseUrl}/${id}`).pipe(
      tap(journalEntry => this.selectedJournalEntrySubject.next(journalEntry))
    );
  }

  create(journalEntry: CreateJournalEntry): Observable<JournalEntry> {
    return this.http.post<JournalEntry>(this.baseUrl, journalEntry).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  update(journalEntry: UpdateJournalEntry): Observable<JournalEntry> {
    return this.http.put<JournalEntry>(`${this.baseUrl}/${journalEntry.journalEntryId}`, journalEntry).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => this.getAll().subscribe())
    );
  }

  clearSelected(): void {
    this.selectedJournalEntrySubject.next(null);
  }
}
