import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Journal } from '../models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class JournalService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;
  private _journalsSubject = new BehaviorSubject<Journal[]>([]);

  journals$ = this._journalsSubject.asObservable();

  getJournals(): Observable<Journal[]> {
    return this._http.get<Journal[]>(`${this._baseUrl}/api/journals`).pipe(
      tap(journals => this._journalsSubject.next(journals))
    );
  }

  getJournalById(id: string): Observable<Journal> {
    return this._http.get<Journal>(`${this._baseUrl}/api/journals/${id}`);
  }

  createJournal(journal: Partial<Journal>): Observable<Journal> {
    return this._http.post<Journal>(`${this._baseUrl}/api/journals`, journal).pipe(
      tap(newJournal => {
        const currentJournals = this._journalsSubject.value;
        this._journalsSubject.next([...currentJournals, newJournal]);
      })
    );
  }

  updateJournal(id: string, journal: Partial<Journal>): Observable<void> {
    return this._http.put<void>(`${this._baseUrl}/api/journals/${id}`, journal).pipe(
      tap(() => {
        const currentJournals = this._journalsSubject.value;
        const index = currentJournals.findIndex(j => j.journalId === id);
        if (index !== -1) {
          currentJournals[index] = { ...currentJournals[index], ...journal } as Journal;
          this._journalsSubject.next([...currentJournals]);
        }
      })
    );
  }

  deleteJournal(id: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/journals/${id}`).pipe(
      tap(() => {
        const currentJournals = this._journalsSubject.value;
        this._journalsSubject.next(currentJournals.filter(j => j.journalId !== id));
      })
    );
  }
}
