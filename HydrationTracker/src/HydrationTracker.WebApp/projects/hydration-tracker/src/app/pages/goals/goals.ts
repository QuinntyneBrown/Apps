import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { GoalService } from '../../services';
import { GoalCard } from '../../components/goal-card/goal-card';
import { GoalDialog } from '../../components/goal-dialog/goal-dialog';
import { Goal, CreateGoalCommand, UpdateGoalCommand } from '../../models';

@Component({
  selector: 'app-goals',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatDialogModule,
    GoalCard
  ],
  templateUrl: './goals.html',
  styleUrls: ['./goals.scss']
})
export class Goals implements OnInit {
  private goalService = inject(GoalService);
  private dialog = inject(MatDialog);

  goals$ = this.goalService.goals$;
  loading$ = this.goalService.loading$;

  ngOnInit(): void {
    this.goalService.getGoals().subscribe();
  }

  onAdd(): void {
    const dialogRef = this.dialog.open(GoalDialog, {
      width: '500px',
      data: {}
    });

    dialogRef.afterClosed().subscribe((result: CreateGoalCommand) => {
      if (result) {
        this.goalService.createGoal(result).subscribe();
      }
    });
  }

  onEdit(goal: Goal): void {
    const dialogRef = this.dialog.open(GoalDialog, {
      width: '500px',
      data: { goal }
    });

    dialogRef.afterClosed().subscribe((result: UpdateGoalCommand) => {
      if (result) {
        this.goalService.updateGoal(goal.goalId, result).subscribe();
      }
    });
  }

  onDelete(id: string): void {
    if (confirm('Are you sure you want to delete this goal?')) {
      this.goalService.deleteGoal(id).subscribe();
    }
  }
}
