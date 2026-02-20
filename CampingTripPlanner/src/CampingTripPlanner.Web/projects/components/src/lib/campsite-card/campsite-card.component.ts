import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-campsite-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './campsite-card.component.html',
  styleUrls: ['./campsite-card.component.scss']
})
export class CampsiteCardComponent {
  @Input({ required: true }) name!: string;
  @Input({ required: true }) location!: string;
  @Input() amenities: string[] = [];
  @Input() reservationStatus: string = '';
  @Input() reservationDate: string = '';
}
