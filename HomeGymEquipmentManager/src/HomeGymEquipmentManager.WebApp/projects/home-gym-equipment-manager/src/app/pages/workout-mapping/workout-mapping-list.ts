import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { WorkoutMappingService } from '../../services';
import { WorkoutMapping } from '../../models';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-workout-mapping-list',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule, MatCardModule, MatChipsModule],
  templateUrl: './workout-mapping-list.html',
  styleUrl: './workout-mapping-list.scss'
})
export class WorkoutMappingList implements OnInit {
  workoutMappingList$: Observable<WorkoutMapping[]>;
  displayedColumns: string[] = ['exerciseName', 'muscleGroup', 'isFavorite', 'actions'];

  constructor(
    private workoutMappingService: WorkoutMappingService,
    private router: Router
  ) {
    this.workoutMappingList$ = this.workoutMappingService.workoutMappingList$;
  }

  ngOnInit(): void {
    this.workoutMappingService.getAll().subscribe();
  }

  onCreate(): void {
    this.router.navigate(['/workouts/create']);
  }

  onEdit(id: string): void {
    this.router.navigate(['/workouts/edit', id]);
  }

  onDelete(id: string): void {
    if (confirm('Are you sure you want to delete this workout mapping?')) {
      this.workoutMappingService.delete(id).subscribe();
    }
  }
}
