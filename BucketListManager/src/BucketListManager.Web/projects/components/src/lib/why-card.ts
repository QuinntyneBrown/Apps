import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-why-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './why-card.html',
  styleUrls: ['./why-card.scss']
})
export class WhyCardComponent {
  @Input({ required: true }) content!: string;
  @Input() title: string = 'Why It Matters';
}
