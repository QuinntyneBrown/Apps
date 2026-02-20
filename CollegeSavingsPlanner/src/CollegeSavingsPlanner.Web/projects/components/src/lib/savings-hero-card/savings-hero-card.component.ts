import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-savings-hero-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './savings-hero-card.component.html',
  styleUrls: ['./savings-hero-card.component.scss']
})
export class SavingsHeroCardComponent {
  @Input({ required: true }) totalSavings!: string;
  @Input() label: string = 'Total Savings';
  @Input() growthPercent: string = '';
  @Input() goalAmount: string = '';
  @Input() goalProgress: number = 0;
}
