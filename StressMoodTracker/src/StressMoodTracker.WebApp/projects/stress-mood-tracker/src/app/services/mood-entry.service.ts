import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { MoodEntry } from '../models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MoodEntryService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;
  private _moodEntriesSubject = new BehaviorSubject<MoodEntry[]>([]);

  moodEntries$ = this._moodEntriesSubject.asObservable();

  getMoodEntries(): Observable<MoodEntry[]> {
    return this._http.get<MoodEntry[]>(`${this._baseUrl}/api/moodentries`).pipe(
      tap(entries => this._moodEntriesSubject.next(entries))
    );
  }

  getMoodEntryById(id: string): Observable<MoodEntry> {
    return this._http.get<MoodEntry>(`${this._baseUrl}/api/moodentries/${id}`);
  }

  createMoodEntry(moodEntry: Partial<MoodEntry>): Observable<MoodEntry> {
    return this._http.post<MoodEntry>(`${this._baseUrl}/api/moodentries`, moodEntry).pipe(
      tap(newEntry => {
        const currentEntries = this._moodEntriesSubject.value;
        this._moodEntriesSubject.next([...currentEntries, newEntry]);
      })
    );
  }

  updateMoodEntry(id: string, moodEntry: Partial<MoodEntry>): Observable<void> {
    return this._http.put<void>(`${this._baseUrl}/api/moodentries/${id}`, moodEntry).pipe(
      tap(() => {
        const currentEntries = this._moodEntriesSubject.value;
        const index = currentEntries.findIndex(e => e.moodEntryId === id);
        if (index !== -1) {
          currentEntries[index] = { ...currentEntries[index], ...moodEntry } as MoodEntry;
          this._moodEntriesSubject.next([...currentEntries]);
        }
      })
    );
  }

  deleteMoodEntry(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/moodentries/${id}`).pipe(
      tap(() => {
        const currentEntries = this._moodEntriesSubject.value;
        this._moodEntriesSubject.next(currentEntries.filter(e => e.moodEntryId !== id));
      })
    );
  }
}
