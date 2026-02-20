import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-claim-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './claim-card.component.html',
  styleUrls: ['./claim-card.component.scss']
})
export class ClaimCardComponent {
  @Input({ required: true }) title!: string;
  @Input({ required: true }) amount!: string;
  @Input() category: string = '';
  @Input() expenseCount: string = '';
  @Input() status: string = '';
  @Input() statusColor: string = '';
  @Input() statusBackground: string = '';
  @Input() date: string = '';
  @Input() dateColor: string = 'var(--text-secondary, #64748B)';
  @Input() iconBackground: string = '#E3F2FD';
  @Output() cardClick = new EventEmitter<void>();

  get metaText(): string {
    const parts = [];
    if (this.category) parts.push(this.category);
    if (this.expenseCount) parts.push(this.expenseCount);
    return parts.join(' · ');
  }
}
