import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-collection-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './collection-card.component.html',
  styleUrls: ['./collection-card.component.scss']
})
export class CollectionCardComponent {
  @Input({ required: true }) title!: string;
  @Input({ required: true }) description!: string;
  @Input() promptCount: string = '';
  @Input() color: 'primary' | 'accent' | 'success' | 'warning' = 'primary';
}
