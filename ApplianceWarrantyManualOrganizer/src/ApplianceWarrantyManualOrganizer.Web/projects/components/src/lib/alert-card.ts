import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-alert-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './alert-card.html',
  styleUrl: './alert-card.scss'
})
export class AlertCard {
  @Input({ required: true }) title!: string;
  @Input({ required: true }) message!: string;
  @Input({ required: true }) icon!: string;
  @Input() severity: 'warning' | 'danger' = 'warning';
  @Output() cardClick = new EventEmitter<void>();
}
