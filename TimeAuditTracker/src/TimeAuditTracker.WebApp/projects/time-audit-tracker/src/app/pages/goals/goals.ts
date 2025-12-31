import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatChipModule } from '@angular/material/chip';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { GoalService } from '../../services';
import { GoalDialog } from '../../components/goal-dialog/goal-dialog';

@Component({
  selector: 'app-goals',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatChipModule,
    MatDialogModule
  ],
  templateUrl: './goals.html',
  styleUrl: './goals.scss'
})
export class Goals implements OnInit {
  private _goalService = inject(GoalService);
  private _dialog = inject(MatDialog);

  goals$ = this._goalService.goals$;
  displayedColumns = ['description', 'category', 'targetHoursPerWeek', 'minimumHoursPerWeek', 'targetHoursPerDay', 'startDate', 'endDate', 'isActive', 'actions'];

  ngOnInit(): void {
    this._goalService.getAll().subscribe();
  }

  openDialog(goal?: any): void {
    const dialogRef = this._dialog.open(GoalDialog, {
      width: '500px',
      data: {
        goal,
        userId: '00000000-0000-0000-0000-000000000000' // TODO: Get from auth service
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (goal) {
          this._goalService.update(goal.goalId, result).subscribe();
        } else {
          this._goalService.create(result).subscribe();
        }
      }
    });
  }

  deleteGoal(id: string): void {
    if (confirm('Are you sure you want to delete this goal?')) {
      this._goalService.delete(id).subscribe();
    }
  }
}
