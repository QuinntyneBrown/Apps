import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { Observable, combineLatest, map } from 'rxjs';
import { HabitService, StreakService } from '../../services';
import { HabitCard } from '../../components/habit-card/habit-card';
import { HabitFormDialog } from '../../components/habit-form-dialog/habit-form-dialog';
import { Habit, Streak } from '../../models';

interface HabitWithStreak {
  habit: Habit;
  streak: number;
}

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatSnackBarModule,
    MatDialogModule,
    HabitCard
  ],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard implements OnInit {
  private habitService = inject(HabitService);
  private streakService = inject(StreakService);
  private dialog = inject(MatDialog);
  private snackBar = inject(MatSnackBar);
  private router = inject(Router);

  // Using async pipe pattern - expose observables directly
  habitsWithStreaks$!: Observable<HabitWithStreak[]>;

  // Hardcoded userId for demo purposes
  private userId = '00000000-0000-0000-0000-000000000001';

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    // Load habits and streaks
    this.habitService.getHabits(this.userId, true).subscribe();
    this.streakService.getStreaks().subscribe();

    // Combine habits with their streaks
    this.habitsWithStreaks$ = combineLatest([
      this.habitService.habits$,
      this.streakService.streaks$
    ]).pipe(
      map(([habits, streaks]) => {
        return habits.map(habit => {
          const streak = streaks.find(s => s.habitId === habit.habitId);
          return {
            habit,
            streak: streak?.currentStreak || 0
          };
        });
      })
    );
  }

  onCreateHabit(): void {
    const dialogRef = this.dialog.open(HabitFormDialog, {
      width: '500px',
      data: { userId: this.userId }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.habitService.createHabit(result).subscribe({
          next: () => {
            this.snackBar.open('Habit created successfully!', 'Close', { duration: 3000 });
          },
          error: (error) => {
            console.error('Error creating habit:', error);
            this.snackBar.open('Failed to create habit', 'Close', { duration: 3000 });
          }
        });
      }
    });
  }

  onCompleteHabit(habitId: string): void {
    this.streakService.incrementStreak(habitId).subscribe({
      next: () => {
        this.snackBar.open('Habit completed! Great job!', 'Close', { duration: 3000 });
      },
      error: (error) => {
        console.error('Error completing habit:', error);
        this.snackBar.open('Failed to complete habit', 'Close', { duration: 3000 });
      }
    });
  }

  onEditHabit(habit: Habit): void {
    const dialogRef = this.dialog.open(HabitFormDialog, {
      width: '500px',
      data: { habit, userId: this.userId }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.habitService.updateHabit(habit.habitId, result).subscribe({
          next: () => {
            this.snackBar.open('Habit updated successfully!', 'Close', { duration: 3000 });
          },
          error: (error) => {
            console.error('Error updating habit:', error);
            this.snackBar.open('Failed to update habit', 'Close', { duration: 3000 });
          }
        });
      }
    });
  }

  onArchiveHabit(habitId: string): void {
    this.habitService.toggleActive(habitId).subscribe({
      next: () => {
        this.snackBar.open('Habit archived successfully!', 'Close', { duration: 3000 });
      },
      error: (error) => {
        console.error('Error archiving habit:', error);
        this.snackBar.open('Failed to archive habit', 'Close', { duration: 3000 });
      }
    });
  }

  onViewDetails(habitId: string): void {
    this.router.navigate(['/habits', habitId]);
  }
}
