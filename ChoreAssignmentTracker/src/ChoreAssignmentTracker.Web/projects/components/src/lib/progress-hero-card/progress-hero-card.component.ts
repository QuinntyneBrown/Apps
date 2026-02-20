import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-progress-hero-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './progress-hero-card.component.html',
  styleUrls: ['./progress-hero-card.component.scss']
})
export class ProgressHeroCardComponent {
  @Input({ required: true }) completed!: number;
  @Input({ required: true }) total!: number;
  @Input() label: string = 'Today\'s Progress';

  get percentage(): number {
    return this.total > 0 ? Math.round((this.completed / this.total) * 100) : 0;
  }
}
