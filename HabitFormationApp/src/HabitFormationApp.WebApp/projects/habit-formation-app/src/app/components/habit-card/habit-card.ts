import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatChipsModule } from '@angular/material/chips';
import { Habit } from '../../models';

@Component({
  selector: 'app-habit-card',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatMenuModule,
    MatChipsModule
  ],
  templateUrl: './habit-card.html',
  styleUrl: './habit-card.scss'
})
export class HabitCard {
  @Input() habit!: Habit;
  @Input() currentStreak: number = 0;
  @Output() complete = new EventEmitter<void>();
  @Output() edit = new EventEmitter<void>();
  @Output() archive = new EventEmitter<void>();
  @Output() viewDetails = new EventEmitter<void>();

  onComplete(): void {
    this.complete.emit();
  }

  onEdit(): void {
    this.edit.emit();
  }

  onArchive(): void {
    this.archive.emit();
  }

  onViewDetails(): void {
    this.viewDetails.emit();
  }

  getFrequencyDisplay(): string {
    switch (this.habit.frequency) {
      case 'Daily':
        return 'Daily';
      case 'Weekly':
        return `${this.habit.targetDaysPerWeek}x per week`;
      case 'Custom':
        return 'Custom';
      default:
        return '';
    }
  }
}
