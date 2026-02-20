import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-reward-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './reward-card.component.html',
  styleUrls: ['./reward-card.component.scss']
})
export class RewardCardComponent {
  @Input({ required: true }) name!: string;
  @Input({ required: true }) description!: string;
  @Input({ required: true }) pointsCost!: number;
  @Input() iconColor: string = '#E3F2FD';
  @Input() canRedeem: boolean = true;
  @Output() redeem = new EventEmitter<void>();

  onRedeem(): void {
    this.redeem.emit();
  }
}
