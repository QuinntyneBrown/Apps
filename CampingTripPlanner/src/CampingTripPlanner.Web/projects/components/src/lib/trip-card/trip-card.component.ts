import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-trip-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './trip-card.component.html',
  styleUrls: ['./trip-card.component.scss']
})
export class TripCardComponent {
  @Input({ required: true }) tripName!: string;
  @Input({ required: true }) dateRange!: string;
  @Input({ required: true }) peopleCount!: number;
  @Input({ required: true }) status!: 'Completed' | 'Upcoming' | 'Planning';
  @Input() iconColor: string = '#2D6A4F';
}
