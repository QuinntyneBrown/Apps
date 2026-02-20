import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-session-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './session-card.component.html',
  styleUrls: ['./session-card.component.scss']
})
export class SessionCardComponent {
  @Input({ required: true }) title!: string;
  @Input({ required: true }) speaker!: string;
  @Input({ required: true }) time!: string;
  @Input({ required: true }) period!: string;
  @Input() color: 'primary' | 'accent' | 'success' | 'warning' | 'default' = 'primary';
  @Input() badges: { label: string; color: string }[] = [];
}
