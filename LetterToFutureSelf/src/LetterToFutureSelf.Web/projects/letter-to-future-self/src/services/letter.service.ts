import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments';
import { Letter, CreateLetter, UpdateLetter } from '../models';

@Injectable({
  providedIn: 'root'
})
export class LetterService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/letters`;

  private lettersSubject = new BehaviorSubject<Letter[]>([]);
  public letters$ = this.lettersSubject.asObservable();

  private selectedLetterSubject = new BehaviorSubject<Letter | null>(null);
  public selectedLetter$ = this.selectedLetterSubject.asObservable();

  private loadingSubject = new BehaviorSubject<boolean>(false);
  public loading$ = this.loadingSubject.asObservable();

  getAll(): Observable<Letter[]> {
    this.loadingSubject.next(true);
    return this.http.get<Letter[]>(this.baseUrl).pipe(
      tap({
        next: (letters) => {
          this.lettersSubject.next(letters);
          this.loadingSubject.next(false);
        },
        error: () => this.loadingSubject.next(false)
      })
    );
  }

  getById(id: string): Observable<Letter> {
    this.loadingSubject.next(true);
    return this.http.get<Letter>(`${this.baseUrl}/${id}`).pipe(
      tap({
        next: (letter) => {
          this.selectedLetterSubject.next(letter);
          this.loadingSubject.next(false);
        },
        error: () => this.loadingSubject.next(false)
      })
    );
  }

  create(letter: CreateLetter): Observable<Letter> {
    this.loadingSubject.next(true);
    return this.http.post<Letter>(this.baseUrl, letter).pipe(
      tap({
        next: (newLetter) => {
          const currentLetters = this.lettersSubject.value;
          this.lettersSubject.next([...currentLetters, newLetter]);
          this.loadingSubject.next(false);
        },
        error: () => this.loadingSubject.next(false)
      })
    );
  }

  update(letter: UpdateLetter): Observable<Letter> {
    this.loadingSubject.next(true);
    return this.http.put<Letter>(`${this.baseUrl}/${letter.letterId}`, letter).pipe(
      tap({
        next: (updatedLetter) => {
          const currentLetters = this.lettersSubject.value;
          const index = currentLetters.findIndex(l => l.letterId === updatedLetter.letterId);
          if (index !== -1) {
            currentLetters[index] = updatedLetter;
            this.lettersSubject.next([...currentLetters]);
          }
          this.selectedLetterSubject.next(updatedLetter);
          this.loadingSubject.next(false);
        },
        error: () => this.loadingSubject.next(false)
      })
    );
  }

  delete(id: string): Observable<void> {
    this.loadingSubject.next(true);
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap({
        next: () => {
          const currentLetters = this.lettersSubject.value;
          this.lettersSubject.next(currentLetters.filter(l => l.letterId !== id));
          if (this.selectedLetterSubject.value?.letterId === id) {
            this.selectedLetterSubject.next(null);
          }
          this.loadingSubject.next(false);
        },
        error: () => this.loadingSubject.next(false)
      })
    );
  }

  markAsRead(id: string): Observable<Letter> {
    this.loadingSubject.next(true);
    return this.http.post<Letter>(`${this.baseUrl}/${id}/mark-as-read`, {}).pipe(
      tap({
        next: (updatedLetter) => {
          const currentLetters = this.lettersSubject.value;
          const index = currentLetters.findIndex(l => l.letterId === updatedLetter.letterId);
          if (index !== -1) {
            currentLetters[index] = updatedLetter;
            this.lettersSubject.next([...currentLetters]);
          }
          this.selectedLetterSubject.next(updatedLetter);
          this.loadingSubject.next(false);
        },
        error: () => this.loadingSubject.next(false)
      })
    );
  }

  clearSelectedLetter(): void {
    this.selectedLetterSubject.next(null);
  }
}
