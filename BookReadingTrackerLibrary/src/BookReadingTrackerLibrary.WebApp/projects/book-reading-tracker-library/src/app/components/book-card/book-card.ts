import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { Book } from '../../models';

@Component({
  selector: 'app-book-card',
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule, MatChipsModule, MatProgressBarModule],
  templateUrl: './book-card.html',
  styleUrl: './book-card.scss'
})
export class BookCard {
  @Input() book!: Book;
  @Output() edit = new EventEmitter<Book>();
  @Output() delete = new EventEmitter<Book>();
  @Output() viewDetails = new EventEmitter<Book>();

  onEdit(): void {
    this.edit.emit(this.book);
  }

  onDelete(): void {
    this.delete.emit(this.book);
  }

  onViewDetails(): void {
    this.viewDetails.emit(this.book);
  }
}
