import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

export interface GivingHeroStat {
  value: string;
  label: string;
}

@Component({
  selector: 'lib-giving-hero-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './giving-hero-card.component.html',
  styleUrls: ['./giving-hero-card.component.scss']
})
export class GivingHeroCardComponent {
  @Input({ required: true }) amount!: string;
  @Input() label: string = 'Year-to-Date Giving';
  @Input() stats: GivingHeroStat[] = [];
}
