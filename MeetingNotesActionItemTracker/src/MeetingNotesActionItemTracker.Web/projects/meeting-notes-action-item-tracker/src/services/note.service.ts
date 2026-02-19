import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments';
import { Note, CreateNoteDto, UpdateNoteDto } from '../models';

@Injectable({
  providedIn: 'root'
})
export class NoteService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/notes`;

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

  createNote(dto: CreateNoteDto): Observable<Note> {
    return this.http.post<Note>(this.baseUrl, dto).pipe(
      tap(note => {
        const notes = [...this.notesSubject.value, note];
        this.notesSubject.next(notes);
      })
    );
  }

  updateNote(dto: UpdateNoteDto): Observable<Note> {
    return this.http.put<Note>(`${this.baseUrl}/${dto.noteId}`, dto).pipe(
      tap(note => {
        const notes = this.notesSubject.value.map(n =>
          n.noteId === note.noteId ? note : n
        );
        this.notesSubject.next(notes);
      })
    );
  }

  deleteNote(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const notes = this.notesSubject.value.filter(n => n.noteId !== id);
        this.notesSubject.next(notes);
      })
    );
  }
}
