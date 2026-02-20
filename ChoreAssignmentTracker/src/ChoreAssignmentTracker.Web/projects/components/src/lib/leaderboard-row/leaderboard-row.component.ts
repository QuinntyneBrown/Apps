import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-leaderboard-row',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './leaderboard-row.component.html',
  styleUrls: ['./leaderboard-row.component.scss']
})
export class LeaderboardRowComponent {
  @Input({ required: true }) rank!: number;
  @Input({ required: true }) name!: string;
  @Input({ required: true }) choresCompleted!: number;
  @Input({ required: true }) points!: number;

  get rankColor(): string {
    switch (this.rank) {
      case 1: return '#FFD54F';
      case 2: return '#E0E0E0';
      case 3: return '#FFCCBC';
      default: return '#F3F4F6';
    }
  }

  get cardBg(): string {
    return this.rank === 1 ? '#FFF8E1' : 'var(--card, #FFFFFF)';
  }

  get cardBorder(): string {
    return this.rank === 1 ? '#FFD54F' : 'var(--border, #E5E7EB)';
  }
}
