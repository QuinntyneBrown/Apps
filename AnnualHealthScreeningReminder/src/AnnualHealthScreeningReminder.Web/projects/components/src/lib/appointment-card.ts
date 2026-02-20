import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

export interface AppointmentDetail {
  icon: string;
  text: string;
}

@Component({
  selector: 'app-appointment-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './appointment-card.html',
  styleUrl: './appointment-card.scss'
})
export class AppointmentCard {
  @Input({ required: true }) title!: string;
  @Input() badgeText?: string;
  @Input() details: AppointmentDetail[] = [];
  @Input() highlighted = false;
  @Output() cardClick = new EventEmitter<void>();
}
