import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-book-list-item',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './book-list-item.html',
  styleUrls: ['./book-list-item.scss']
})
export class BookListItemComponent {
  @Input({ required: true }) title!: string;
  @Input({ required: true }) author!: string;
  @Input() pages: string = '';
  @Input() genre: string = '';
  @Input() coverColor: string = '#E8D5F5';
  @Input() coverIcon: string = 'menu_book';
}
