import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-milestone-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './milestone-card.html',
  styleUrls: ['./milestone-card.scss']
})
export class MilestoneCardComponent {
  @Input({ required: true }) title!: string;
  @Input() category: string = '';
  @Input() date: string = '';
  @Input() progress: number = 0;
  @Input() achieved: boolean = false;
}
