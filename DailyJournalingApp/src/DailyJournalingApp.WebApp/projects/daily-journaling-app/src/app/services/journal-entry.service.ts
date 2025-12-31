import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { JournalEntry, CreateJournalEntry, UpdateJournalEntry, Mood } from '../models';

@Injectable({
  providedIn: 'root'
})
export class JournalEntryService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/journalentries`;

  private readonly entriesSubject = new BehaviorSubject<JournalEntry[]>([]);
  public readonly entries$ = this.entriesSubject.asObservable();

  private readonly currentEntrySubject = new BehaviorSubject<JournalEntry | null>(null);
  public readonly currentEntry$ = this.currentEntrySubject.asObservable();

  getAll(
    userId?: string,
    startDate?: string,
    endDate?: string,
    mood?: Mood,
    favoritesOnly?: boolean
  ): Observable<JournalEntry[]> {
    let params = new HttpParams();
    if (userId) params = params.set('userId', userId);
    if (startDate) params = params.set('startDate', startDate);
    if (endDate) params = params.set('endDate', endDate);
    if (mood !== undefined) params = params.set('mood', mood.toString());
    if (favoritesOnly !== undefined) params = params.set('favoritesOnly', favoritesOnly.toString());

    return this.http.get<JournalEntry[]>(this.baseUrl, { params }).pipe(
      tap(entries => this.entriesSubject.next(entries))
    );
  }

  getById(id: string): Observable<JournalEntry> {
    return this.http.get<JournalEntry>(`${this.baseUrl}/${id}`).pipe(
      tap(entry => this.currentEntrySubject.next(entry))
    );
  }

  create(entry: CreateJournalEntry): Observable<JournalEntry> {
    return this.http.post<JournalEntry>(this.baseUrl, entry).pipe(
      tap(newEntry => {
        const current = this.entriesSubject.value;
        this.entriesSubject.next([newEntry, ...current]);
      })
    );
  }

  update(id: string, entry: UpdateJournalEntry): Observable<JournalEntry> {
    return this.http.put<JournalEntry>(`${this.baseUrl}/${id}`, entry).pipe(
      tap(updatedEntry => {
        const current = this.entriesSubject.value;
        const index = current.findIndex(e => e.journalEntryId === id);
        if (index !== -1) {
          current[index] = updatedEntry;
          this.entriesSubject.next([...current]);
        }
        this.currentEntrySubject.next(updatedEntry);
      })
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const current = this.entriesSubject.value;
        this.entriesSubject.next(current.filter(e => e.journalEntryId !== id));
        if (this.currentEntrySubject.value?.journalEntryId === id) {
          this.currentEntrySubject.next(null);
        }
      })
    );
  }

  toggleFavorite(id: string): Observable<JournalEntry> {
    return this.http.put<JournalEntry>(`${this.baseUrl}/${id}/favorite`, {}).pipe(
      tap(updatedEntry => {
        const current = this.entriesSubject.value;
        const index = current.findIndex(e => e.journalEntryId === id);
        if (index !== -1) {
          current[index] = updatedEntry;
          this.entriesSubject.next([...current]);
        }
        this.currentEntrySubject.next(updatedEntry);
      })
    );
  }
}
