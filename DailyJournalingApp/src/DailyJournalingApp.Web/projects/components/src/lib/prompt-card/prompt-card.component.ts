import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-prompt-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './prompt-card.component.html',
  styleUrls: ['./prompt-card.component.scss']
})
export class PromptCardComponent {
  @Input({ required: true }) text!: string;
  @Input() category: string = '';
  @Input() categoryColor: string = 'var(--primary-light, #E8EAF6)';
  @Input() responseCount: string = '';
  @Output() cardClick = new EventEmitter<void>();
}
