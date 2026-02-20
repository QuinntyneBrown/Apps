import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-follow-up-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './follow-up-card.component.html',
  styleUrls: ['./follow-up-card.component.scss']
})
export class FollowUpCardComponent {
  @Input({ required: true }) title!: string;
  @Input({ required: true }) description!: string;
  @Input({ required: true }) dueDate!: string;
  @Input() priorityLabel: string = '';
  @Input() priorityColor: 'danger' | 'warning' | 'accent' | 'primary' | 'success' = 'warning';
  @Input() overdue: boolean = false;
}
