import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-trip-hero-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './trip-hero-card.component.html',
  styleUrls: ['./trip-hero-card.component.scss']
})
export class TripHeroCardComponent {
  @Input({ required: true }) tripName!: string;
  @Input({ required: true }) dateRange!: string;
  @Input({ required: true }) peopleCount!: number;
  @Input({ required: true }) packingProgress!: number;
  @Input() label: string = 'Next Trip';
}
