import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-date-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './date-card.html',
  styleUrl: './date-card.scss'
})
export class DateCard {
  @Input({ required: true }) name!: string;
  @Input({ required: true }) subtitle!: string;
  @Input({ required: true }) icon!: string;
  @Input() iconColor: 'primary' | 'success' | 'info' | 'warning' = 'primary';
  @Input() badgeText?: string;
  @Input() badgeColor: 'primary' | 'success' | 'info' | 'warning' = 'primary';
  @Input() highlighted = false;
  @Output() cardClick = new EventEmitter<void>();
}
