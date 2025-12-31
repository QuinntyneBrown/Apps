import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatTabsModule } from '@angular/material/tabs';
import { GoalCard, CreateGoalDialog } from '../../components';
import { GoalService } from '../../services';
import { Goal, GoalStatus } from '../../models';

@Component({
  selector: 'app-goals',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatDialogModule,
    MatTabsModule,
    GoalCard
  ],
  templateUrl: './goals.html',
  styleUrl: './goals.scss'
})
export class Goals implements OnInit {
  goals$ = this.goalService.goals$;
  userId = '00000000-0000-0000-0000-000000000001'; // Mock user ID

  GoalStatus = GoalStatus;

  constructor(
    private goalService: GoalService,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.loadGoals();
  }

  loadGoals(): void {
    this.goalService.getAll(this.userId).subscribe();
  }

  filterGoalsByStatus(goals: Goal[], status: GoalStatus): Goal[] {
    return goals.filter(g => g.status === status);
  }

  openCreateGoalDialog(): void {
    const dialogRef = this.dialog.open(CreateGoalDialog, {
      width: '600px',
      data: { userId: this.userId }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.goalService.create(result).subscribe(() => {
          this.loadGoals();
        });
      }
    });
  }

  onDeleteGoal(goalId: string): void {
    this.goalService.delete(goalId).subscribe(() => {
      this.loadGoals();
    });
  }
}
