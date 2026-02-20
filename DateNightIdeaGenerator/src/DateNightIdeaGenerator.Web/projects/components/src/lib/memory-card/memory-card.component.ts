import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-memory-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './memory-card.component.html',
  styleUrls: ['./memory-card.component.scss']
})
export class MemoryCardComponent {
  @Input({ required: true }) title!: string;
  @Input() emoji: string = '';
  @Input() emojiBackground: string = 'var(--primary-light, #FCE4EC)';
  @Input() date: string = '';
  @Input() rating: string = '';
  @Input() cost: string = '';
  @Output() cardClick = new EventEmitter<void>();
}
