import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments';
import { NoteLink, CreateNoteLinkCommand, UpdateNoteLinkCommand } from '../models';

@Injectable({
  providedIn: 'root'
})
export class NoteLinkService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/notelinks`;

  private noteLinksSubject = new BehaviorSubject<NoteLink[]>([]);
  public noteLinks$ = this.noteLinksSubject.asObservable();

  getNoteLinks(): Observable<NoteLink[]> {
    return this.http.get<NoteLink[]>(this.baseUrl).pipe(
      tap(noteLinks => this.noteLinksSubject.next(noteLinks))
    );
  }

  getNoteLinkById(id: string): Observable<NoteLink> {
    return this.http.get<NoteLink>(`${this.baseUrl}/${id}`);
  }

  createNoteLink(command: CreateNoteLinkCommand): Observable<NoteLink> {
    return this.http.post<NoteLink>(this.baseUrl, command).pipe(
      tap(noteLink => {
        const noteLinks = this.noteLinksSubject.value;
        this.noteLinksSubject.next([...noteLinks, noteLink]);
      })
    );
  }

  updateNoteLink(command: UpdateNoteLinkCommand): Observable<NoteLink> {
    return this.http.put<NoteLink>(`${this.baseUrl}/${command.noteLinkId}`, command).pipe(
      tap(updatedNoteLink => {
        const noteLinks = this.noteLinksSubject.value.map(noteLink =>
          noteLink.noteLinkId === updatedNoteLink.noteLinkId ? updatedNoteLink : noteLink
        );
        this.noteLinksSubject.next(noteLinks);
      })
    );
  }

  deleteNoteLink(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const noteLinks = this.noteLinksSubject.value.filter(noteLink => noteLink.noteLinkId !== id);
        this.noteLinksSubject.next(noteLinks);
      })
    );
  }
}
