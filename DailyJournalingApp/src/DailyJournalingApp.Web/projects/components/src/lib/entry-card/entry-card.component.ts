import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-entry-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './entry-card.component.html',
  styleUrls: ['./entry-card.component.scss']
})
export class EntryCardComponent {
  @Input({ required: true }) title!: string;
  @Input({ required: true }) body!: string;
  @Input({ required: true }) date!: string;
  @Input() moodEmoji: string = '';
  @Input() moodLabel: string = '';
  @Input() tags: string[] = [];
  @Input() tagColors: string[] = [];
  @Input() isFavorite: boolean = false;
  @Output() favoriteToggle = new EventEmitter<void>();
  @Output() cardClick = new EventEmitter<void>();

  onFavoriteToggle(event: Event): void {
    event.stopPropagation();
    this.favoriteToggle.emit();
  }

  getTagColor(index: number): string {
    return this.tagColors[index] || 'var(--primary-light, #E8EAF6)';
  }
}
