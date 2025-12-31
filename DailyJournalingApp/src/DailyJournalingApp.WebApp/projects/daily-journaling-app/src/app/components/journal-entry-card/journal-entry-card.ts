import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { JournalEntry, Mood } from '../../models';

@Component({
  selector: 'app-journal-entry-card',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatChipsModule
  ],
  templateUrl: './journal-entry-card.html',
  styleUrl: './journal-entry-card.scss'
})
export class JournalEntryCard {
  @Input({ required: true }) entry!: JournalEntry;
  @Output() edit = new EventEmitter<JournalEntry>();
  @Output() delete = new EventEmitter<string>();
  @Output() toggleFavorite = new EventEmitter<string>();

  getMoodIcon(mood: Mood): string {
    const moodIcons: Record<Mood, string> = {
      [Mood.VeryHappy]: 'sentiment_very_satisfied',
      [Mood.Happy]: 'sentiment_satisfied',
      [Mood.Neutral]: 'sentiment_neutral',
      [Mood.Sad]: 'sentiment_dissatisfied',
      [Mood.VerySad]: 'sentiment_very_dissatisfied',
      [Mood.Anxious]: 'psychology_alt',
      [Mood.Calm]: 'self_improvement',
      [Mood.Energetic]: 'bolt',
      [Mood.Tired]: 'bedtime'
    };
    return moodIcons[mood];
  }

  getMoodLabel(mood: Mood): string {
    return Mood[mood];
  }

  getTags(): string[] {
    return this.entry.tags ? this.entry.tags.split(',').map(t => t.trim()) : [];
  }

  onEdit(): void {
    this.edit.emit(this.entry);
  }

  onDelete(): void {
    this.delete.emit(this.entry.journalEntryId);
  }

  onToggleFavorite(): void {
    this.toggleFavorite.emit(this.entry.journalEntryId);
  }
}
