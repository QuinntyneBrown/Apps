import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { Observable } from 'rxjs';
import { HabitService } from '../../services';
import { Habit } from '../../models';
import { HabitCard } from '../../components';

@Component({
  selector: 'app-habits',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule,
    HabitCard
  ],
  templateUrl: './habits.html',
  styleUrl: './habits.scss'
})
export class Habits implements OnInit {
  private habitService = inject(HabitService);

  habits$!: Observable<Habit[]>;

  ngOnInit(): void {
    this.loadHabits();
  }

  loadHabits(): void {
    this.habits$ = this.habitService.getHabits();
  }

  onEdit(habit: Habit): void {
    console.log('Edit habit:', habit);
    // TODO: Implement edit dialog
  }

  onDelete(habitId: string): void {
    if (confirm('Are you sure you want to delete this habit?')) {
      this.habitService.deleteHabit(habitId).subscribe({
        next: () => console.log('Habit deleted successfully'),
        error: (error) => console.error('Error deleting habit:', error)
      });
    }
  }

  onCreate(): void {
    console.log('Create new habit');
    // TODO: Implement create dialog
  }
}
