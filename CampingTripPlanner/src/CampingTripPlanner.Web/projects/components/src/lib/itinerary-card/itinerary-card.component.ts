import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

export interface ItineraryDay {
  dayNumber: number;
  date: string;
  activities: string[];
}

@Component({
  selector: 'lib-itinerary-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './itinerary-card.component.html',
  styleUrls: ['./itinerary-card.component.scss']
})
export class ItineraryCardComponent {
  @Input({ required: true }) day!: ItineraryDay;
}
