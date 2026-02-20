import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

export interface ChoreDetailStat {
  value: string;
  label: string;
}

@Component({
  selector: 'lib-chore-detail-hero-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './chore-detail-hero-card.component.html',
  styleUrls: ['./chore-detail-hero-card.component.scss']
})
export class ChoreDetailHeroCardComponent {
  @Input({ required: true }) choreName!: string;
  @Input() badges: string[] = [];
  @Input() stats: ChoreDetailStat[] = [];
}
