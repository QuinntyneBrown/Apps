import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-book-progress-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './book-progress-card.html',
  styleUrls: ['./book-progress-card.scss']
})
export class BookProgressCardComponent {
  @Input({ required: true }) title!: string;
  @Input({ required: true }) author!: string;
  @Input({ required: true }) progress!: number;
  @Input() currentPage: number = 0;
  @Input() totalPages: number = 0;
  @Input() coverColor: string = '#E8D5F5';
  @Input() coverIcon: string = 'menu_book';
  @Output() continueReading = new EventEmitter<void>();
}
