import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { Exercise } from '../../models';

@Component({
  selector: 'app-exercise-card',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule, MatChipsModule],
  templateUrl: './exercise-card.html',
  styleUrl: './exercise-card.scss'
})
export class ExerciseCard {
  @Input() exercise!: Exercise;
  @Output() edit = new EventEmitter<Exercise>();
  @Output() delete = new EventEmitter<Exercise>();

  onEdit(): void {
    this.edit.emit(this.exercise);
  }

  onDelete(): void {
    this.delete.emit(this.exercise);
  }
}
