import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { TastingNote, CreateTastingNoteRequest, UpdateTastingNoteRequest } from '../models';

@Injectable({
  providedIn: 'root'
})
export class TastingNoteService {
  private readonly apiUrl = `${environment.baseUrl}/api/tastingnotes`;
  private tastingNotesSubject = new BehaviorSubject<TastingNote[]>([]);
  public tastingNotes$ = this.tastingNotesSubject.asObservable();

  constructor(private http: HttpClient) {}

  getTastingNotes(userId?: string, batchId?: string): Observable<TastingNote[]> {
    let url = this.apiUrl;
    const params: string[] = [];

    if (userId) params.push(`userId=${userId}`);
    if (batchId) params.push(`batchId=${batchId}`);

    if (params.length > 0) {
      url += `?${params.join('&')}`;
    }

    return this.http.get<TastingNote[]>(url).pipe(
      tap(notes => this.tastingNotesSubject.next(notes))
    );
  }

  getTastingNote(id: string): Observable<TastingNote> {
    return this.http.get<TastingNote>(`${this.apiUrl}/${id}`);
  }

  createTastingNote(request: CreateTastingNoteRequest): Observable<TastingNote> {
    return this.http.post<TastingNote>(this.apiUrl, request).pipe(
      tap(note => {
        const current = this.tastingNotesSubject.value;
        this.tastingNotesSubject.next([...current, note]);
      })
    );
  }

  updateTastingNote(id: string, request: UpdateTastingNoteRequest): Observable<TastingNote> {
    return this.http.put<TastingNote>(`${this.apiUrl}/${id}`, request).pipe(
      tap(updatedNote => {
        const current = this.tastingNotesSubject.value;
        const index = current.findIndex(n => n.tastingNoteId === id);
        if (index !== -1) {
          current[index] = updatedNote;
          this.tastingNotesSubject.next([...current]);
        }
      })
    );
  }

  deleteTastingNote(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => {
        const current = this.tastingNotesSubject.value;
        this.tastingNotesSubject.next(current.filter(n => n.tastingNoteId !== id));
      })
    );
  }
}
