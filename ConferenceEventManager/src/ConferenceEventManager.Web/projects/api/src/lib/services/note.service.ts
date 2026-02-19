import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { Note, CreateNoteCommand, UpdateNoteCommand } from '../models';

@Injectable({
  providedIn: 'root'
})
export class NoteService {
  private readonly apiUrl = `${environment.baseUrl}/api/notes`;
  private notesSubject = new BehaviorSubject<Note[]>([]);
  public notes$ = this.notesSubject.asObservable();

  private currentNoteSubject = new BehaviorSubject<Note | null>(null);
  public currentNote$ = this.currentNoteSubject.asObservable();

  constructor(private http: HttpClient) {}

  getAll(eventId?: string, userId?: string): Observable<Note[]> {
    let url = this.apiUrl;
    const params: string[] = [];
    if (eventId) params.push(`eventId=${eventId}`);
    if (userId) params.push(`userId=${userId}`);
    if (params.length > 0) url += '?' + params.join('&');

    return this.http.get<Note[]>(url).pipe(
      tap(notes => this.notesSubject.next(notes))
    );
  }

  getById(id: string): Observable<Note> {
    return this.http.get<Note>(`${this.apiUrl}/${id}`).pipe(
      tap(note => this.currentNoteSubject.next(note))
    );
  }

  create(command: CreateNoteCommand): Observable<Note> {
    return this.http.post<Note>(this.apiUrl, command).pipe(
      tap(note => {
        const current = this.notesSubject.value;
        this.notesSubject.next([...current, note]);
      })
    );
  }

  update(id: string, command: UpdateNoteCommand): Observable<Note> {
    return this.http.put<Note>(`${this.apiUrl}/${id}`, command).pipe(
      tap(updatedNote => {
        const current = this.notesSubject.value;
        const index = current.findIndex(n => n.noteId === id);
        if (index !== -1) {
          current[index] = updatedNote;
          this.notesSubject.next([...current]);
        }
        this.currentNoteSubject.next(updatedNote);
      })
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => {
        const current = this.notesSubject.value;
        this.notesSubject.next(current.filter(n => n.noteId !== id));
        if (this.currentNoteSubject.value?.noteId === id) {
          this.currentNoteSubject.next(null);
        }
      })
    );
  }
}
