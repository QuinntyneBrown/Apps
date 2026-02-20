import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-chore-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './chore-card.component.html',
  styleUrls: ['./chore-card.component.scss']
})
export class ChoreCardComponent {
  @Input({ required: true }) name!: string;
  @Input({ required: true }) assignee!: string;
  @Input({ required: true }) difficulty!: string;
  @Input({ required: true }) duration!: string;
  @Input({ required: true }) points!: number;
  @Input() done: boolean = false;
  @Input() iconColor: string = '#E3F2FD';
  @Input() earnedPoints: number = 0;
}
