import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-conversation-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './conversation-card.component.html',
  styleUrls: ['./conversation-card.component.scss']
})
export class ConversationCardComponent {
  @Input({ required: true }) title!: string;
  @Input({ required: true }) description!: string;
  @Input() rating: string = '';
  @Input() badgeLabel: string = '';
  @Input() badgeColor: 'primary' | 'accent' | 'success' | 'warning' = 'accent';
  @Input() dateMeta: string = '';
}
