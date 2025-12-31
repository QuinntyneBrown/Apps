import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { TastingNote } from '../models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TastingNoteService {
  private _http = inject(HttpClient);
  private _baseUrl = environment.baseUrl;
  private _tastingNotesSubject = new BehaviorSubject<TastingNote[]>([]);

  tastingNotes$ = this._tastingNotesSubject.asObservable();

  getTastingNotes(): Observable<TastingNote[]> {
    return this._http.get<TastingNote[]>(`${this._baseUrl}/api/tastingnotes`).pipe(
      tap(notes => this._tastingNotesSubject.next(notes))
    );
  }

  getTastingNoteById(tastingNoteId: string): Observable<TastingNote> {
    return this._http.get<TastingNote>(`${this._baseUrl}/api/tastingnotes/${tastingNoteId}`);
  }

  createTastingNote(note: Partial<TastingNote>): Observable<TastingNote> {
    return this._http.post<TastingNote>(`${this._baseUrl}/api/tastingnotes`, note).pipe(
      tap(newNote => {
        const currentNotes = this._tastingNotesSubject.value;
        this._tastingNotesSubject.next([...currentNotes, newNote]);
      })
    );
  }

  updateTastingNote(tastingNoteId: string, note: Partial<TastingNote>): Observable<TastingNote> {
    return this._http.put<TastingNote>(`${this._baseUrl}/api/tastingnotes/${tastingNoteId}`, note).pipe(
      tap(updatedNote => {
        const currentNotes = this._tastingNotesSubject.value;
        const index = currentNotes.findIndex(n => n.tastingNoteId === tastingNoteId);
        if (index !== -1) {
          const updatedNotes = [...currentNotes];
          updatedNotes[index] = updatedNote;
          this._tastingNotesSubject.next(updatedNotes);
        }
      })
    );
  }

  deleteTastingNote(tastingNoteId: string): Observable<void> {
    return this._http.delete<void>(`${this._baseUrl}/api/tastingnotes/${tastingNoteId}`).pipe(
      tap(() => {
        const currentNotes = this._tastingNotesSubject.value;
        this._tastingNotesSubject.next(currentNotes.filter(n => n.tastingNoteId !== tastingNoteId));
      })
    );
  }
}
