import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-hero-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './hero-card.html',
  styleUrls: ['./hero-card.scss']
})
export class HeroCardComponent {
  @Input({ required: true }) totalGoals!: number;
  @Input({ required: true }) completed!: number;
  @Input({ required: true }) progress!: number;
  @Input() title: string = 'Life Goals Progress';
}
