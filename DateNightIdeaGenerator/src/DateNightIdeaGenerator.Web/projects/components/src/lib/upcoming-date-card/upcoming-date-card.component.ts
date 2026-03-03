import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-upcoming-date-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './upcoming-date-card.component.html',
  styleUrls: ['./upcoming-date-card.component.scss']
})
export class UpcomingDateCardComponent {
  @Input({ required: true }) dayLabel!: string;
  @Input({ required: true }) dayNumber!: string;
  @Input({ required: true }) title!: string;
  @Input() details: string = '';
  @Output() cardClick = new EventEmitter<void>();
}
