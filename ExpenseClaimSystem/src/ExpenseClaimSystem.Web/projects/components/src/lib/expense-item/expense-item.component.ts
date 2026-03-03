import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-expense-item',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './expense-item.component.html',
  styleUrls: ['./expense-item.component.scss']
})
export class ExpenseItemComponent {
  @Input({ required: true }) title!: string;
  @Input({ required: true }) amount!: string;
  @Input() category: string = '';
  @Input() date: string = '';
  @Input() iconBackground: string = '#E3F2FD';
  @Input() icon: string = 'receipt_long';
  @Output() itemClick = new EventEmitter<void>();

  get metaText(): string {
    const parts = [];
    if (this.category) parts.push(this.category);
    if (this.date) parts.push(this.date);
    return parts.join(' · ');
  }
}
