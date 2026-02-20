import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-next-date-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './next-date-card.component.html',
  styleUrls: ['./next-date-card.component.scss']
})
export class NextDateCardComponent {
  @Input({ required: true }) label!: string;
  @Input({ required: true }) title!: string;
  @Input() time: string = '';
  @Input() cost: string = '';
  @Input() category: string = '';
  @Input() categoryColor: string = 'var(--primary-light, #FCE4EC)';
  @Output() cardClick = new EventEmitter<void>();
}
