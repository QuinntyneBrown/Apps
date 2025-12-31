import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatChipsModule } from '@angular/material/chips';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { GoalsService } from '../services';
import { Goal, GoalType, GoalStatus, CreateGoalCommand } from '../models';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-goal-form-dialog',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatButtonModule,
    MatDialogModule
  ],
  template: `
    <h2 mat-dialog-title>Create New Goal</h2>
    <mat-dialog-content>
      <div class="goal-form">
        <mat-form-field appearance="outline" class="goal-form__field">
          <mat-label>Name</mat-label>
          <input matInput [(ngModel)]="command.name" required>
        </mat-form-field>

        <mat-form-field appearance="outline" class="goal-form__field">
          <mat-label>Description</mat-label>
          <textarea matInput [(ngModel)]="command.description" rows="3"></textarea>
        </mat-form-field>

        <mat-form-field appearance="outline" class="goal-form__field">
          <mat-label>Goal Type</mat-label>
          <mat-select [(ngModel)]="command.goalType" required>
            <mat-option [value]="0">Savings</mat-option>
            <mat-option [value]="1">Debt Payoff</mat-option>
            <mat-option [value]="2">Investment</mat-option>
            <mat-option [value]="3">Purchase</mat-option>
            <mat-option [value]="4">Emergency</mat-option>
            <mat-option [value]="5">Retirement</mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field appearance="outline" class="goal-form__field">
          <mat-label>Target Amount</mat-label>
          <input matInput type="number" [(ngModel)]="command.targetAmount" required>
          <span matPrefix>$&nbsp;</span>
        </mat-form-field>

        <mat-form-field appearance="outline" class="goal-form__field">
          <mat-label>Target Date</mat-label>
          <input matInput [matDatepicker]="picker" [(ngModel)]="command.targetDate" required>
          <mat-datepicker-toggle matSuffix [for]="picker"></mat-datepicker-toggle>
          <mat-datepicker #picker></mat-datepicker>
        </mat-form-field>

        <mat-form-field appearance="outline" class="goal-form__field">
          <mat-label>Notes</mat-label>
          <textarea matInput [(ngModel)]="command.notes" rows="2"></textarea>
        </mat-form-field>
      </div>
    </mat-dialog-content>
    <mat-dialog-actions align="end">
      <button mat-button mat-dialog-close>Cancel</button>
      <button mat-raised-button color="primary" [mat-dialog-close]="command">Create</button>
    </mat-dialog-actions>
  `,
  styles: [`
    .goal-form {
      display: flex;
      flex-direction: column;
      gap: 16px;
      min-width: 400px;

      &__field {
        width: 100%;
      }
    }
  `]
})
export class GoalFormDialog {
  command: CreateGoalCommand = {
    name: '',
    description: '',
    goalType: GoalType.Savings,
    targetAmount: 0,
    targetDate: new Date().toISOString(),
    notes: ''
  };
}

@Component({
  selector: 'app-goals',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatSelectModule,
    MatProgressBarModule,
    MatChipsModule,
    MatSnackBarModule
  ],
  template: `
    <div class="goals">
      <div class="goals__header">
        <h1 class="goals__title">Financial Goals</h1>
        <button mat-raised-button color="primary" (click)="openCreateDialog()">
          <mat-icon>add</mat-icon>
          New Goal
        </button>
      </div>

      <div class="goals__filters">
        <mat-form-field appearance="outline" class="goals__filter">
          <mat-label>Filter by Type</mat-label>
          <mat-select [(ngModel)]="filterType" (ngModelChange)="applyFilters()">
            <mat-option [value]="null">All Types</mat-option>
            <mat-option [value]="0">Savings</mat-option>
            <mat-option [value]="1">Debt Payoff</mat-option>
            <mat-option [value]="2">Investment</mat-option>
            <mat-option [value]="3">Purchase</mat-option>
            <mat-option [value]="4">Emergency</mat-option>
            <mat-option [value]="5">Retirement</mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field appearance="outline" class="goals__filter">
          <mat-label>Filter by Status</mat-label>
          <mat-select [(ngModel)]="filterStatus" (ngModelChange)="applyFilters()">
            <mat-option [value]="null">All Statuses</mat-option>
            <mat-option [value]="0">Not Started</mat-option>
            <mat-option [value]="1">In Progress</mat-option>
            <mat-option [value]="2">Completed</mat-option>
            <mat-option [value]="3">Paused</mat-option>
            <mat-option [value]="4">Cancelled</mat-option>
          </mat-select>
        </mat-form-field>
      </div>

      <div class="goals__list" *ngIf="(goals$ | async)?.length; else noGoals">
        <mat-card *ngFor="let goal of goals$ | async" class="goals__card" (click)="navigateToGoalDetails(goal.goalId)">
          <mat-card-header>
            <mat-card-title class="goals__card-title">{{ goal.name }}</mat-card-title>
            <mat-card-subtitle>{{ getGoalTypeLabel(goal.goalType) }}</mat-card-subtitle>
          </mat-card-header>
          <mat-card-content>
            <p class="goals__description">{{ goal.description }}</p>
            <div class="goals__progress">
              <div class="goals__progress-info">
                <span>\${{ goal.currentAmount | number:'1.2-2' }}</span>
                <span>\${{ goal.targetAmount | number:'1.2-2' }}</span>
              </div>
              <mat-progress-bar mode="determinate" [value]="goal.progress"></mat-progress-bar>
              <div class="goals__progress-label">{{ goal.progress }}% Complete</div>
            </div>
            <div class="goals__meta">
              <mat-chip [class]="'goals__status-chip goals__status-chip--' + getStatusClass(goal.status)">
                {{ getStatusLabel(goal.status) }}
              </mat-chip>
              <span class="goals__date">Due: {{ goal.targetDate | date:'mediumDate' }}</span>
            </div>
          </mat-card-content>
          <mat-card-actions align="end">
            <button mat-icon-button (click)="deleteGoal($event, goal.goalId)" color="warn">
              <mat-icon>delete</mat-icon>
            </button>
          </mat-card-actions>
        </mat-card>
      </div>

      <ng-template #noGoals>
        <div class="goals__empty">
          <mat-icon>inbox</mat-icon>
          <p>No goals found. Create your first goal to get started!</p>
          <button mat-raised-button color="primary" (click)="openCreateDialog()">
            Create Goal
          </button>
        </div>
      </ng-template>
    </div>
  `,
  styles: [`
    .goals {
      padding: 24px;
      max-width: 1400px;
      margin: 0 auto;

      &__header {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-bottom: 32px;
      }

      &__title {
        font-size: 32px;
        font-weight: 500;
        margin: 0;
      }

      &__filters {
        display: flex;
        gap: 16px;
        margin-bottom: 24px;
      }

      &__filter {
        min-width: 200px;
      }

      &__list {
        display: grid;
        grid-template-columns: repeat(auto-fill, minmax(400px, 1fr));
        gap: 24px;
      }

      &__card {
        cursor: pointer;
        transition: transform 0.2s, box-shadow 0.2s;

        &:hover {
          transform: translateY(-4px);
          box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
        }

        &-title {
          font-size: 20px;
        }
      }

      &__description {
        color: rgba(0, 0, 0, 0.7);
        margin: 16px 0;
      }

      &__progress {
        margin: 16px 0;

        &-info {
          display: flex;
          justify-content: space-between;
          font-weight: 500;
          margin-bottom: 8px;
        }

        &-label {
          font-size: 12px;
          color: rgba(0, 0, 0, 0.6);
          margin-top: 4px;
        }
      }

      &__meta {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-top: 16px;
      }

      &__date {
        font-size: 14px;
        color: rgba(0, 0, 0, 0.6);
      }

      &__status-chip {
        font-size: 12px;

        &--not-started {
          background-color: #9e9e9e;
          color: white;
        }

        &--in-progress {
          background-color: #2196f3;
          color: white;
        }

        &--completed {
          background-color: #4caf50;
          color: white;
        }

        &--paused {
          background-color: #ff9800;
          color: white;
        }

        &--cancelled {
          background-color: #f44336;
          color: white;
        }
      }

      &__empty {
        text-align: center;
        padding: 64px 24px;

        mat-icon {
          font-size: 64px;
          width: 64px;
          height: 64px;
          color: rgba(0, 0, 0, 0.3);
        }

        p {
          font-size: 18px;
          color: rgba(0, 0, 0, 0.6);
          margin: 16px 0 24px;
        }
      }
    }
  `]
})
export class Goals implements OnInit {
  goals$: Observable<Goal[]>;
  filterType: GoalType | null = null;
  filterStatus: GoalStatus | null = null;

  constructor(
    private goalsService: GoalsService,
    private router: Router,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) {
    this.goals$ = this.goalsService.goals$;
  }

  ngOnInit(): void {
    this.applyFilters();
  }

  applyFilters(): void {
    this.goalsService.getGoals(
      this.filterType ?? undefined,
      this.filterStatus ?? undefined
    ).subscribe();
  }

  openCreateDialog(): void {
    const dialogRef = this.dialog.open(GoalFormDialog);

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.createGoal(result);
      }
    });
  }

  createGoal(command: CreateGoalCommand): void {
    const formattedCommand = {
      ...command,
      targetDate: typeof command.targetDate === 'string'
        ? command.targetDate
        : new Date(command.targetDate).toISOString()
    };

    this.goalsService.createGoal(formattedCommand).subscribe({
      next: () => {
        this.snackBar.open('Goal created successfully!', 'Close', { duration: 3000 });
      },
      error: () => {
        this.snackBar.open('Failed to create goal. Please try again.', 'Close', { duration: 3000 });
      }
    });
  }

  deleteGoal(event: Event, goalId: string): void {
    event.stopPropagation();
    if (confirm('Are you sure you want to delete this goal?')) {
      this.goalsService.deleteGoal(goalId).subscribe({
        next: () => {
          this.snackBar.open('Goal deleted successfully!', 'Close', { duration: 3000 });
        },
        error: () => {
          this.snackBar.open('Failed to delete goal. Please try again.', 'Close', { duration: 3000 });
        }
      });
    }
  }

  getGoalTypeLabel(type: number): string {
    const labels: { [key: number]: string } = {
      0: 'Savings',
      1: 'Debt Payoff',
      2: 'Investment',
      3: 'Purchase',
      4: 'Emergency',
      5: 'Retirement'
    };
    return labels[type] || 'Unknown';
  }

  getStatusLabel(status: number): string {
    const labels: { [key: number]: string } = {
      0: 'Not Started',
      1: 'In Progress',
      2: 'Completed',
      3: 'Paused',
      4: 'Cancelled'
    };
    return labels[status] || 'Unknown';
  }

  getStatusClass(status: number): string {
    const classes: { [key: number]: string } = {
      0: 'not-started',
      1: 'in-progress',
      2: 'completed',
      3: 'paused',
      4: 'cancelled'
    };
    return classes[status] || 'not-started';
  }

  navigateToGoalDetails(goalId: string): void {
    this.router.navigate(['/goals', goalId]);
  }
}
