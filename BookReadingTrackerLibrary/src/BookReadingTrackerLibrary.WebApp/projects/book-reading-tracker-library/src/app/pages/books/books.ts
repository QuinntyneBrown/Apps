import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormsModule } from '@angular/forms';
import { BookService } from '../../services';
import { BookCard } from '../../components';
import { Genre, ReadingStatus } from '../../models';

@Component({
  selector: 'app-books',
  imports: [CommonModule, MatButtonModule, MatIconModule, MatSelectModule, MatFormFieldModule, FormsModule, BookCard],
  templateUrl: './books.html',
  styleUrl: './books.scss'
})
export class Books implements OnInit {
  private readonly bookService = inject(BookService);

  books$ = this.bookService.books$;

  genres = Object.values(Genre);
  statuses = Object.values(ReadingStatus);

  selectedGenre?: Genre;
  selectedStatus?: ReadingStatus;

  ngOnInit(): void {
    this.loadBooks();
  }

  loadBooks(): void {
    this.bookService.getBooks(undefined, this.selectedGenre, this.selectedStatus).subscribe();
  }

  onFilterChange(): void {
    this.loadBooks();
  }

  onEditBook(book: any): void {
    console.log('Edit book:', book);
  }

  onDeleteBook(book: any): void {
    if (confirm(`Are you sure you want to delete "${book.title}"?`)) {
      this.bookService.deleteBook(book.bookId).subscribe();
    }
  }

  onViewDetails(book: any): void {
    console.log('View details:', book);
  }
}
