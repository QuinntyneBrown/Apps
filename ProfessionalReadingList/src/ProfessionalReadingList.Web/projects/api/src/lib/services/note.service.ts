import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments';
import { Note, CreateNoteCommand, UpdateNoteCommand } from '../models';

@Injectable({
  providedIn: 'root'
})
export class NoteService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/notes`;

  private notesSubject = new BehaviorSubject<Note[]>([]);
  public notes$ = this.notesSubject.asObservable();

  getAll(): Observable<Note[]> {
    return this.http.get<Note[]>(this.baseUrl).pipe(
      tap(notes => this.notesSubject.next(notes))
    );
  }

  getById(id: string): Observable<Note> {
    return this.http.get<Note>(`${this.baseUrl}/${id}`);
  }

  getByResourceId(resourceId: string): Observable<Note[]> {
    return this.http.get<Note[]>(`${this.baseUrl}/resource/${resourceId}`);
  }

  create(command: CreateNoteCommand): Observable<Note> {
    return this.http.post<Note>(this.baseUrl, command).pipe(
      tap(note => {
        const current = this.notesSubject.value;
        this.notesSubject.next([...current, note]);
      })
    );
  }

  update(id: string, command: UpdateNoteCommand): Observable<Note> {
    return this.http.put<Note>(`${this.baseUrl}/${id}`, command).pipe(
      tap(updated => {
        const current = this.notesSubject.value;
        const index = current.findIndex(n => n.noteId === id);
        if (index !== -1) {
          current[index] = updated;
          this.notesSubject.next([...current]);
        }
      })
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const current = this.notesSubject.value;
        this.notesSubject.next(current.filter(n => n.noteId !== id));
      })
    );
  }
}
