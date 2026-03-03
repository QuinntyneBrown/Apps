import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-idea-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './idea-card.component.html',
  styleUrls: ['./idea-card.component.scss']
})
export class IdeaCardComponent {
  @Input({ required: true }) title!: string;
  @Input() emoji: string = '';
  @Input() emojiBackground: string = 'var(--primary-light, #FCE4EC)';
  @Input() category: string = '';
  @Input() categoryColor: string = 'var(--primary-light, #FCE4EC)';
  @Input() cost: string = '';
  @Output() cardClick = new EventEmitter<void>();
  @Output() actionClick = new EventEmitter<void>();
}
