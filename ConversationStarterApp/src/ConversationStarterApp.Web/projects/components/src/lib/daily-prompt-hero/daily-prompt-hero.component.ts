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
  @Input() difficulty: string = '';
  @Output() usePrompt = new EventEmitter<void>();
  @Output() skip = new EventEmitter<void>();

  onUsePrompt(): void {
    this.usePrompt.emit();
  }

  onSkip(): void {
    this.skip.emit();
  }
}
