import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
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
    MatChipsModule
  ],
  templateUrl: './habit-card.html',
  styleUrl: './habit-card.scss'
})
export class HabitCard {
  @Input() habit!: Habit;
  @Output() edit = new EventEmitter<Habit>();
  @Output() delete = new EventEmitter<string>();

  getImpactColor(level: number): string {
    if (level >= 4) return 'warn';
    if (level >= 3) return 'accent';
    return '';
  }

  onEdit(): void {
    this.edit.emit(this.habit);
  }

  onDelete(): void {
    this.delete.emit(this.habit.habitId);
  }
}
