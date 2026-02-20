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
  @Input({ required: true }) current!: number;
  @Input({ required: true }) target!: number;
  @Input() description: string = '';
  @Input() variant: 'default' | 'hero' = 'default';

  get progress(): number {
    return this.target > 0 ? Math.round((this.current / this.target) * 100) : 0;
  }
}
