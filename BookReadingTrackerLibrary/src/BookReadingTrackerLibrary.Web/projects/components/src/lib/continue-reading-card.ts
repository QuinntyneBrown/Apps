import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-continue-reading-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './continue-reading-card.html',
  styleUrls: ['./continue-reading-card.scss']
})
export class ContinueReadingCardComponent {
  @Input({ required: true }) title!: string;
  @Input({ required: true }) author!: string;
  @Input({ required: true }) progress!: number;
  @Input() coverIcon: string = 'menu_book';
  @Input() currentPage: number = 0;
  @Input() totalPages: number = 0;
}
