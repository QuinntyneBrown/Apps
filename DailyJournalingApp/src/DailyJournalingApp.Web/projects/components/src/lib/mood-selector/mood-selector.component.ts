import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

export interface MoodOption {
  emoji: string;
  label: string;
  color: string;
}

@Component({
  selector: 'lib-mood-selector',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './mood-selector.component.html',
  styleUrls: ['./mood-selector.component.scss']
})
export class MoodSelectorComponent {
  @Input() moods: MoodOption[] = [];
  @Input() selectedMood: string = '';
  @Output() moodSelected = new EventEmitter<MoodOption>();

  onSelect(mood: MoodOption): void {
    this.moodSelected.emit(mood);
  }
}
