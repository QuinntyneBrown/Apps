import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-event-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './event-card.component.html',
  styleUrls: ['./event-card.component.scss']
})
export class EventCardComponent {
  @Input({ required: true }) name!: string;
  @Input({ required: true }) subtitle!: string;
  @Input({ required: true }) dateMonth!: string;
  @Input({ required: true }) dateDay!: string;
  @Input() color: 'primary' | 'accent' | 'success' | 'warning' = 'primary';
  @Input() badges: { label: string; color: string }[] = [];
  @Input() showArrow: boolean = false;
}
