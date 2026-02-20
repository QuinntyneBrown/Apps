import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-goal-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './goal-card.html',
  styleUrls: ['./goal-card.scss']
})
export class GoalCardComponent {
  @Input({ required: true }) target!: string;
  @Input({ required: true }) progress!: number;
  @Input() description: string = '';
}
