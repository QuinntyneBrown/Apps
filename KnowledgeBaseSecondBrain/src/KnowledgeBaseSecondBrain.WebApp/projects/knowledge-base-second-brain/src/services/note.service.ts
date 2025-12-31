import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments';
import { Note, CreateNoteCommand, UpdateNoteCommand } from '../models';

@Injectable({
  providedIn: 'root'
})
export class NoteService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/notes`;

  private notesSubject = new BehaviorSubject<Note[]>([]);
  public notes$ = this.notesSubject.asObservable();

  getNotes(): Observable<Note[]> {
    return this.http.get<Note[]>(this.baseUrl).pipe(
      tap(notes => this.notesSubject.next(notes))
    );
  }

  getNoteById(id: string): Observable<Note> {
    return this.http.get<Note>(`${this.baseUrl}/${id}`);
  }

  createNote(command: CreateNoteCommand): Observable<Note> {
    return this.http.post<Note>(this.baseUrl, command).pipe(
      tap(note => {
        const notes = this.notesSubject.value;
        this.notesSubject.next([...notes, note]);
      })
    );
  }

  updateNote(command: UpdateNoteCommand): Observable<Note> {
    return this.http.put<Note>(`${this.baseUrl}/${command.noteId}`, command).pipe(
      tap(updatedNote => {
        const notes = this.notesSubject.value.map(note =>
          note.noteId === updatedNote.noteId ? updatedNote : note
        );
        this.notesSubject.next(notes);
      })
    );
  }

  deleteNote(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const notes = this.notesSubject.value.filter(note => note.noteId !== id);
        this.notesSubject.next(notes);
      })
    );
  }
}
