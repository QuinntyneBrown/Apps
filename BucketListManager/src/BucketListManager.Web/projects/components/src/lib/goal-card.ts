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
  @Input({ required: true }) title!: string;
  @Input() category: string = '';
  @Input() targetDate: string = '';
  @Input() priority: 'high' | 'medium' | 'low' | '' = '';
  @Input() status: 'planning' | 'in-progress' | 'completed' | '' = '';
  @Input() progress: number = 0;
}
