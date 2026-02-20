import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-insight-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './insight-card.html',
  styleUrls: ['./insight-card.scss']
})
export class InsightCardComponent {
  @Input({ required: true }) title!: string;
  @Input({ required: true }) description!: string;
  @Input({ required: true }) icon!: string;
  @Input() variant: 'success' | 'warning' | 'primary' = 'primary';
}
