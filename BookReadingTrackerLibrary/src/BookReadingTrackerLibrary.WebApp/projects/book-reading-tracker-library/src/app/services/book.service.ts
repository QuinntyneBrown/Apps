import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Book, Genre, ReadingStatus } from '../models';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BookService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.baseUrl}/api/books`;

  private booksSubject = new BehaviorSubject<Book[]>([]);
  public books$ = this.booksSubject.asObservable();

  getBooks(userId?: string, genre?: Genre, status?: ReadingStatus): Observable<Book[]> {
    let url = this.baseUrl;
    const params: string[] = [];

    if (userId) params.push(`userId=${userId}`);
    if (genre) params.push(`genre=${genre}`);
    if (status) params.push(`status=${status}`);

    if (params.length > 0) {
      url += `?${params.join('&')}`;
    }

    return this.http.get<Book[]>(url).pipe(
      tap(books => this.booksSubject.next(books))
    );
  }

  getBookById(bookId: string): Observable<Book> {
    return this.http.get<Book>(`${this.baseUrl}/${bookId}`);
  }

  createBook(book: Partial<Book>): Observable<Book> {
    return this.http.post<Book>(this.baseUrl, book).pipe(
      tap(newBook => {
        const currentBooks = this.booksSubject.value;
        this.booksSubject.next([...currentBooks, newBook]);
      })
    );
  }

  updateBook(bookId: string, book: Partial<Book>): Observable<Book> {
    return this.http.put<Book>(`${this.baseUrl}/${bookId}`, { ...book, bookId }).pipe(
      tap(updatedBook => {
        const currentBooks = this.booksSubject.value;
        const index = currentBooks.findIndex(b => b.bookId === bookId);
        if (index !== -1) {
          const newBooks = [...currentBooks];
          newBooks[index] = updatedBook;
          this.booksSubject.next(newBooks);
        }
      })
    );
  }

  deleteBook(bookId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${bookId}`).pipe(
      tap(() => {
        const currentBooks = this.booksSubject.value;
        this.booksSubject.next(currentBooks.filter(b => b.bookId !== bookId));
      })
    );
  }
}
