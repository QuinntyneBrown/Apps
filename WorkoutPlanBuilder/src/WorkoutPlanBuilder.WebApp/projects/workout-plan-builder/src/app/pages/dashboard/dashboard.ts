import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { ExerciseService, WorkoutService, ProgressRecordService } from '../../services';
import { map } from 'rxjs';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink, MatCardModule, MatButtonModule, MatIconModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard {
  private exerciseService = inject(ExerciseService);
  private workoutService = inject(WorkoutService);
  private progressRecordService = inject(ProgressRecordService);

  stats$ = this.progressRecordService.getAll().pipe(
    map(records => ({
      totalWorkouts: records.length,
      totalMinutes: records.reduce((sum, r) => sum + r.actualDurationMinutes, 0),
      totalCalories: records.reduce((sum, r) => sum + (r.caloriesBurned || 0), 0),
      averageRating: records.length > 0
        ? records.filter(r => r.performanceRating).reduce((sum, r) => sum + (r.performanceRating || 0), 0) / records.filter(r => r.performanceRating).length
        : 0
    }))
  );

  exerciseCount$ = this.exerciseService.getAll().pipe(
    map(exercises => exercises.length)
  );

  workoutCount$ = this.workoutService.getAll().pipe(
    map(workouts => workouts.length)
  );
}
