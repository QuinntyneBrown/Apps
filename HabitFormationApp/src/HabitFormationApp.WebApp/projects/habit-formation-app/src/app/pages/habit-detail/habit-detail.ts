import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { Observable, switchMap, combineLatest, map } from 'rxjs';
import { HabitService, StreakService } from '../../services';
import { HabitFormDialog } from '../../components/habit-form-dialog/habit-form-dialog';
import { Habit, Streak } from '../../models';

interface HabitDetailViewModel {
  habit: Habit;
  streak: Streak | null;
}

@Component({
  selector: 'app-habit-detail',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatProgressSpinnerModule,
    MatSnackBarModule,
    MatDialogModule
  ],
  templateUrl: './habit-detail.html',
  styleUrl: './habit-detail.scss'
})
export class HabitDetail implements OnInit {
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private habitService = inject(HabitService);
  private streakService = inject(StreakService);
  private dialog = inject(MatDialog);
  private snackBar = inject(MatSnackBar);

  viewModel$!: Observable<HabitDetailViewModel>;

  ngOnInit(): void {
    this.viewModel$ = this.route.params.pipe(
      switchMap(params => {
        const habitId = params['id'];
        return combineLatest([
          this.habitService.getHabitById(habitId),
          this.streakService.getStreakByHabitId(habitId)
        ]);
      }),
      map(([habit, streak]) => ({ habit, streak }))
    );
  }

  onBack(): void {
    this.router.navigate(['/']);
  }

  onEdit(habit: Habit): void {
    const dialogRef = this.dialog.open(HabitFormDialog, {
      width: '500px',
      data: { habit, userId: habit.userId }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.habitService.updateHabit(habit.habitId, result).subscribe({
          next: () => {
            this.snackBar.open('Habit updated successfully!', 'Close', { duration: 3000 });
            // Reload data
            this.ngOnInit();
          },
          error: (error) => {
            console.error('Error updating habit:', error);
            this.snackBar.open('Failed to update habit', 'Close', { duration: 3000 });
          }
        });
      }
    });
  }

  onArchive(habitId: string): void {
    this.habitService.toggleActive(habitId).subscribe({
      next: () => {
        this.snackBar.open('Habit archived successfully!', 'Close', { duration: 3000 });
        this.router.navigate(['/']);
      },
      error: (error) => {
        console.error('Error archiving habit:', error);
        this.snackBar.open('Failed to archive habit', 'Close', { duration: 3000 });
      }
    });
  }

  onComplete(habitId: string): void {
    this.streakService.incrementStreak(habitId).subscribe({
      next: () => {
        this.snackBar.open('Habit completed! Great job!', 'Close', { duration: 3000 });
        // Reload data
        this.ngOnInit();
      },
      error: (error) => {
        console.error('Error completing habit:', error);
        this.snackBar.open('Failed to complete habit', 'Close', { duration: 3000 });
      }
    });
  }

  getFrequencyDisplay(habit: Habit): string {
    switch (habit.frequency) {
      case 'Daily':
        return 'Daily';
      case 'Weekly':
        return `${habit.targetDaysPerWeek}x per week`;
      case 'Custom':
        return 'Custom';
      default:
        return '';
    }
  }
}
