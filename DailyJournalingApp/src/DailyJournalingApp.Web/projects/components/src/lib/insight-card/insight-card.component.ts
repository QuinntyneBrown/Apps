import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-insight-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './insight-card.component.html',
  styleUrls: ['./insight-card.component.scss']
})
export class InsightCardComponent {
  @Input({ required: true }) title!: string;
  @Input({ required: true }) description!: string;
  @Input() icon: string = 'lightbulb';
}
