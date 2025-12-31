import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { ExerciseService } from '../../services';
import { ExerciseCard } from '../../components';
import { Exercise } from '../../models';

@Component({
  selector: 'app-exercises',
  standalone: true,
  imports: [CommonModule, MatButtonModule, MatIconModule, ExerciseCard],
  templateUrl: './exercises.html',
  styleUrl: './exercises.scss'
})
export class Exercises {
  private exerciseService = inject(ExerciseService);

  exercises$ = this.exerciseService.getAll();

  onEdit(exercise: Exercise): void {
    console.log('Edit exercise:', exercise);
  }

  onDelete(exercise: Exercise): void {
    if (confirm(`Are you sure you want to delete "${exercise.name}"?`)) {
      this.exerciseService.delete(exercise.exerciseId).subscribe({
        next: () => console.log('Exercise deleted successfully'),
        error: (error) => console.error('Error deleting exercise:', error)
      });
    }
  }

  onCreate(): void {
    console.log('Create new exercise');
  }
}
