import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-streak-banner',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './streak-banner.component.html',
  styleUrls: ['./streak-banner.component.scss']
})
export class StreakBannerComponent {
  @Input({ required: true }) streakCount!: number;
  @Input() subtitle: string = 'Keep talking daily to maintain your streak';
  @Input() days: { label: string; completed: boolean }[] = [];
}
