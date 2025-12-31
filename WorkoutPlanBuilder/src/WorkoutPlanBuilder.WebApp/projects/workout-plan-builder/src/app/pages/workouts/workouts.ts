import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { WorkoutService } from '../../services';
import { WorkoutCard } from '../../components';
import { Workout } from '../../models';

@Component({
  selector: 'app-workouts',
  standalone: true,
  imports: [CommonModule, MatButtonModule, MatIconModule, WorkoutCard],
  templateUrl: './workouts.html',
  styleUrl: './workouts.scss'
})
export class Workouts {
  private workoutService = inject(WorkoutService);

  workouts$ = this.workoutService.getAll();

  onEdit(workout: Workout): void {
    console.log('Edit workout:', workout);
  }

  onDelete(workout: Workout): void {
    if (confirm(`Are you sure you want to delete "${workout.name}"?`)) {
      this.workoutService.delete(workout.workoutId).subscribe({
        next: () => console.log('Workout deleted successfully'),
        error: (error) => console.error('Error deleting workout:', error)
      });
    }
  }

  onCreate(): void {
    console.log('Create new workout');
  }
}
