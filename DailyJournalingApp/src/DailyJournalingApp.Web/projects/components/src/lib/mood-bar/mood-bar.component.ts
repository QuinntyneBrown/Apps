import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-mood-bar',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './mood-bar.component.html',
  styleUrls: ['./mood-bar.component.scss']
})
export class MoodBarComponent {
  @Input({ required: true }) label!: string;
  @Input({ required: true }) percentage!: number;
  @Input() barColor: string = 'var(--primary-light, #E8EAF6)';
  @Input() fillColor: string = 'var(--primary, #5C6BC0)';
}
