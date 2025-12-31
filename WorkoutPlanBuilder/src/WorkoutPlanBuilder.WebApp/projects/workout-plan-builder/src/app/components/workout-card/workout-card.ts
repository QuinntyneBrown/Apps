import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { Workout } from '../../models';

@Component({
  selector: 'app-workout-card',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule, MatChipsModule],
  templateUrl: './workout-card.html',
  styleUrl: './workout-card.scss'
})
export class WorkoutCard {
  @Input() workout!: Workout;
  @Output() edit = new EventEmitter<Workout>();
  @Output() delete = new EventEmitter<Workout>();

  onEdit(): void {
    this.edit.emit(this.workout);
  }

  onDelete(): void {
    this.delete.emit(this.workout);
  }
}
