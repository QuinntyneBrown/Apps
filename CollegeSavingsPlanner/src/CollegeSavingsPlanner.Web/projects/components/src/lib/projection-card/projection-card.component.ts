import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

export interface ProjectionStat {
  value: string;
  label: string;
}

@Component({
  selector: 'lib-projection-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './projection-card.component.html',
  styleUrls: ['./projection-card.component.scss']
})
export class ProjectionCardComponent {
  @Input({ required: true }) planName!: string;
  @Input({ required: true }) beneficiaryName!: string;
  @Input() stats: ProjectionStat[] = [];
  @Input() progressLabel: string = '';
  @Input() progressPercent: number = 0;
  @Input() recommendation: string = '';
}
