import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-plan-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './plan-card.component.html',
  styleUrls: ['./plan-card.component.scss']
})
export class PlanCardComponent {
  @Input({ required: true }) planName!: string;
  @Input({ required: true }) planType!: string;
  @Input({ required: true }) currentAmount!: string;
  @Input({ required: true }) goalAmount!: string;
  @Input({ required: true }) goalProgress!: number;
  @Input() accentColor: 'primary' | 'accent' = 'primary';
}
