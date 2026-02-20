import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-stat-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './stat-card.html',
  styleUrls: ['./stat-card.scss']
})
export class StatCardComponent {
  @Input({ required: true }) value!: string;
  @Input({ required: true }) label!: string;
  @Input() valueColor: 'primary' | 'success' | 'danger' | 'warning' | 'default' = 'default';
}
