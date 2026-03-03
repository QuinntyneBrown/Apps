import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-daily-prompt-hero',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './daily-prompt-hero.component.html',
  styleUrls: ['./daily-prompt-hero.component.scss']
})
export class DailyPromptHeroComponent {
  @Input({ required: true }) prompt!: string;
  @Input() category: string = '';
  @Output() startWriting = new EventEmitter<void>();
}
