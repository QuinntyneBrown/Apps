import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatCardModule } from '@angular/material/card';
import { RecoveryExerciseService } from '../services';
import { RecoveryExercise } from '../models';

@Component({
  selector: 'app-exercises',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule, MatChipsModule, MatCardModule],
  template: `
    <div class="exercises">
      <div class="exercises__header">
        <h1>Recovery Exercises</h1>
        <button mat-raised-button color="primary">
          <mat-icon>add</mat-icon>
          Add Exercise
        </button>
      </div>

      <mat-card>
        <table mat-table [dataSource]="(exercises$ | async) || []" class="exercises__table">
          <ng-container matColumnDef="name">
            <th mat-header-cell *matHeaderCellDef>Name</th>
            <td mat-cell *matCellDef="let exercise">{{ exercise.name }}</td>
          </ng-container>

          <ng-container matColumnDef="frequency">
            <th mat-header-cell *matHeaderCellDef>Frequency</th>
            <td mat-cell *matCellDef="let exercise">{{ exercise.frequency }}</td>
          </ng-container>

          <ng-container matColumnDef="setsReps">
            <th mat-header-cell *matHeaderCellDef>Sets/Reps</th>
            <td mat-cell *matCellDef="let exercise">{{ exercise.setsAndReps || '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="duration">
            <th mat-header-cell *matHeaderCellDef>Duration</th>
            <td mat-cell *matCellDef="let exercise">{{ exercise.durationMinutes ? exercise.durationMinutes + ' min' : '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="lastCompleted">
            <th mat-header-cell *matHeaderCellDef>Last Completed</th>
            <td mat-cell *matCellDef="let exercise">{{ exercise.lastCompleted ? (exercise.lastCompleted | date:'short') : 'Never' }}</td>
          </ng-container>

          <ng-container matColumnDef="status">
            <th mat-header-cell *matHeaderCellDef>Status</th>
            <td mat-cell *matCellDef="let exercise">
              <mat-chip [color]="exercise.isActive ? 'primary' : 'warn'">
                {{ exercise.isActive ? 'Active' : 'Inactive' }}
              </mat-chip>
            </td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let exercise">
              <button mat-icon-button color="primary" title="Edit">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" title="Delete" (click)="deleteExercise(exercise)">
                <mat-icon>delete</mat-icon>
              </button>
            </td>
          </ng-container>

          <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
          <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
        </table>
      </mat-card>
    </div>
  `,
  styles: [`
    .exercises {
      padding: 1.5rem;
    }
    .exercises__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 1.5rem;
    }
    .exercises__table {
      width: 100%;
    }
  `]
})
export class Exercises implements OnInit {
  private _exerciseService = inject(RecoveryExerciseService);

  exercises$ = this._exerciseService.exercises$;
  displayedColumns = ['name', 'frequency', 'setsReps', 'duration', 'lastCompleted', 'status', 'actions'];

  ngOnInit(): void {
    this._exerciseService.getAll().subscribe();
  }

  deleteExercise(exercise: RecoveryExercise): void {
    if (confirm(`Are you sure you want to delete "${exercise.name}"?`)) {
      this._exerciseService.delete(exercise.recoveryExerciseId).subscribe();
    }
  }
}
