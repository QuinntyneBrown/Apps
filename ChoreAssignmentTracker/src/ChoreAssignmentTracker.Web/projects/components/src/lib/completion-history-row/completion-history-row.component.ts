import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-completion-history-row',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './completion-history-row.component.html',
  styleUrls: ['./completion-history-row.component.scss']
})
export class CompletionHistoryRowComponent {
  @Input({ required: true }) completedBy!: string;
  @Input({ required: true }) date!: string;
  @Input({ required: true }) rating!: number;

  get stars(): string {
    return '\u2605'.repeat(this.rating) + '\u2606'.repeat(5 - this.rating);
  }
}
