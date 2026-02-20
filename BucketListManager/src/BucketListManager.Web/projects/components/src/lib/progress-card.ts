import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-progress-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './progress-card.html',
  styleUrls: ['./progress-card.scss']
})
export class ProgressCardComponent {
  @Input({ required: true }) progress!: number;
  @Input() targetDate: string = '';
}
