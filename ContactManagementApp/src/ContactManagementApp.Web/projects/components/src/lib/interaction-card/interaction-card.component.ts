import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-interaction-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './interaction-card.component.html',
  styleUrls: ['./interaction-card.component.scss']
})
export class InteractionCardComponent {
  @Input({ required: true }) title!: string;
  @Input({ required: true }) description!: string;
  @Input({ required: true }) date!: string;
  @Input() badgeLabel: string = '';
  @Input() badgeColor: 'primary' | 'accent' | 'success' | 'warning' | 'danger' = 'primary';
  @Input() duration: string = '';
}
