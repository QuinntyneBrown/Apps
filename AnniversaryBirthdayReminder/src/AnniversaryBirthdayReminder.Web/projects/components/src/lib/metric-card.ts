import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-metric-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './metric-card.html',
  styleUrl: './metric-card.scss'
})
export class MetricCard {
  @Input({ required: true }) value!: string | number;
  @Input({ required: true }) label!: string;
  @Input() tagText?: string;
  @Input() tagColor: 'primary' | 'indigo' | 'green' | 'yellow' = 'primary';
}
