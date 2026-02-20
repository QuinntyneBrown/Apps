import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-hero-banner',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './hero-banner.component.html',
  styleUrls: ['./hero-banner.component.scss']
})
export class HeroBannerComponent {
  @Input({ required: true }) greeting!: string;
  @Input({ required: true }) date!: string;
  @Input() streakCount: number = 0;
  @Input() streakLabel: string = 'Day Writing Streak!';
}
