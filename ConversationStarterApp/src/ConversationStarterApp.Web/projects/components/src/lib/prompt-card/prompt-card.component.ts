import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-prompt-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './prompt-card.component.html',
  styleUrls: ['./prompt-card.component.scss']
})
export class PromptCardComponent {
  @Input({ required: true }) prompt!: string;
  @Input() badges: { label: string; color: string }[] = [];
  @Input() usageCount: string = '';
  @Input() isFavorite: boolean = false;
  @Output() favorite = new EventEmitter<void>();

  onFavorite(): void {
    this.favorite.emit();
  }
}
