import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-bp-reading-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './bp-reading-card.html',
  styleUrls: ['./bp-reading-card.scss']
})
export class BpReadingCardComponent {
  @Input({ required: true }) systolic!: number;
  @Input({ required: true }) diastolic!: number;
  @Input({ required: true }) pulse!: number;
  @Input({ required: true }) status!: 'normal' | 'elevated' | 'high';
  @Input() timestamp: string = '';
  @Input() label: string = 'Latest Reading';
}
