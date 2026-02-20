import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-screening-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './screening-card.html',
  styleUrl: './screening-card.scss'
})
export class ScreeningCard {
  @Input({ required: true }) name!: string;
  @Input({ required: true }) dateLabel!: string;
  @Input({ required: true }) icon!: string;
  @Input() statusColor: 'danger' | 'warning' | 'success' | 'info' | 'primary' = 'primary';
  @Input() showChevron = true;
  @Output() cardClick = new EventEmitter<void>();
}
