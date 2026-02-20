import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-goal-progress-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './goal-progress-card.component.html',
  styleUrls: ['./goal-progress-card.component.scss']
})
export class GoalProgressCardComponent {
  @Input({ required: true }) current!: string;
  @Input({ required: true }) goal!: string;
  @Input({ required: true }) percentage!: number;
  @Input() title: string = 'Annual Goal';
}
