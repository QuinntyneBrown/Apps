import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-appliance-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './appliance-card.html',
  styleUrl: './appliance-card.scss'
})
export class ApplianceCard {
  @Input({ required: true }) name!: string;
  @Input({ required: true }) subtitle!: string;
  @Input({ required: true }) icon!: string;
  @Input() iconColor: 'primary' | 'success' | 'warning' | 'danger' | 'info' = 'primary';
  @Input() showChevron = true;
  @Output() cardClick = new EventEmitter<void>();
}
