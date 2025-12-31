import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatDividerModule } from '@angular/material/divider';
import { MilestoneCard, CreateMilestoneDialog, CreateProgressDialog } from '../../components';
import { GoalService, MilestoneService, ProgressService } from '../../services';
import { Goal, Milestone, Progress, GoalCategoryLabels, GoalStatusLabels, UpdateMilestone } from '../../models';

@Component({
  selector: 'app-goal-detail',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatDialogModule,
    MatCardModule,
    MatChipsModule,
    MatProgressBarModule,
    MatDividerModule,
    MilestoneCard
  ],
  templateUrl: './goal-detail.html',
  styleUrl: './goal-detail.scss'
})
export class GoalDetail implements OnInit {
  goal: Goal | null = null;
  milestones: Milestone[] = [];
  progresses: Progress[] = [];
  userId = '00000000-0000-0000-0000-000000000001'; // Mock user ID

  categoryLabels = GoalCategoryLabels;
  statusLabels = GoalStatusLabels;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private goalService: GoalService,
    private milestoneService: MilestoneService,
    private progressService: ProgressService,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    const goalId = this.route.snapshot.paramMap.get('id');
    if (goalId) {
      this.loadGoal(goalId);
      this.loadMilestones(goalId);
      this.loadProgresses(goalId);
    }
  }

  loadGoal(goalId: string): void {
    this.goalService.getById(goalId).subscribe(goal => {
      this.goal = goal;
    });
  }

  loadMilestones(goalId: string): void {
    this.milestoneService.getByGoal(goalId).subscribe(milestones => {
      this.milestones = milestones.sort((a, b) => a.sortOrder - b.sortOrder);
    });
  }

  loadProgresses(goalId: string): void {
    this.progressService.getByGoal(goalId).subscribe(progresses => {
      this.progresses = progresses.sort((a, b) =>
        new Date(b.progressDate).getTime() - new Date(a.progressDate).getTime()
      );
    });
  }

  openCreateMilestoneDialog(): void {
    if (!this.goal) return;

    const dialogRef = this.dialog.open(CreateMilestoneDialog, {
      width: '600px',
      data: {
        goalId: this.goal.goalId,
        userId: this.userId,
        sortOrder: this.milestones.length
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.milestoneService.create(result).subscribe(() => {
          this.loadMilestones(this.goal!.goalId);
        });
      }
    });
  }

  openCreateProgressDialog(): void {
    if (!this.goal) return;

    const dialogRef = this.dialog.open(CreateProgressDialog, {
      width: '600px',
      data: {
        goalId: this.goal.goalId,
        userId: this.userId
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.progressService.create(result).subscribe(() => {
          this.loadProgresses(this.goal!.goalId);
          this.loadGoal(this.goal!.goalId);
        });
      }
    });
  }

  onToggleMilestoneComplete(milestone: Milestone): void {
    const update: UpdateMilestone = {
      milestoneId: milestone.milestoneId,
      title: milestone.title,
      description: milestone.description,
      targetDate: milestone.targetDate,
      isCompleted: !milestone.isCompleted,
      sortOrder: milestone.sortOrder
    };

    this.milestoneService.update(milestone.milestoneId, update).subscribe(() => {
      this.loadMilestones(this.goal!.goalId);
      this.loadGoal(this.goal!.goalId);
    });
  }

  onDeleteMilestone(milestoneId: string): void {
    this.milestoneService.delete(milestoneId).subscribe(() => {
      this.loadMilestones(this.goal!.goalId);
      this.loadGoal(this.goal!.goalId);
    });
  }

  goBack(): void {
    this.router.navigate(['/dashboard']);
  }

  getPriorityLabel(priority: number): string {
    return `Priority ${priority}`;
  }
}
