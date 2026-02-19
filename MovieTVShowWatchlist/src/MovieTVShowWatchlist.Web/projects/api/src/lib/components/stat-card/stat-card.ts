import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';

@Component({
  selector: 'app-stat-card',
  standalone: true,
  imports: [CommonModule, MatCardModule],
  templateUrl: './stat-card.html',
  styleUrl: './stat-card.scss'
})
export class StatCard {
  @Input({ required: true }) icon!: string;
  @Input({ required: true }) value!: string | number;
  @Input({ required: true }) label!: string;
  @Input() change?: string;
}
