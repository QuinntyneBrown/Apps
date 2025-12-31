import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { MatSliderModule } from '@angular/material/slider';
import { GoalService, ReviewPeriodService } from '../services';
import { Goal, CreateGoal, UpdateGoal, GoalStatus, GOAL_STATUS_LABELS } from '../models';

@Component({
  selector: 'app-goal-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatSelectModule,
    MatSliderModule
  ],
  template: `
    <h2 mat-dialog-title>{{ data ? 'Edit' : 'Add' }} Goal</h2>
    <mat-dialog-content>
      <form [formGroup]="form" class="goal-dialog__form">
        <mat-form-field class="goal-dialog__field">
          <mat-label>Review Period</mat-label>
          <mat-select formControlName="reviewPeriodId" required>
            <mat-option *ngFor="let period of reviewPeriods$ | async" [value]="period.reviewPeriodId">
              {{ period.title }}
            </mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field class="goal-dialog__field">
          <mat-label>Title</mat-label>
          <input matInput formControlName="title" required>
        </mat-form-field>

        <mat-form-field class="goal-dialog__field">
          <mat-label>Description</mat-label>
          <textarea matInput formControlName="description" rows="3" required></textarea>
        </mat-form-field>

        <mat-form-field class="goal-dialog__field">
          <mat-label>Status</mat-label>
          <mat-select formControlName="status" required>
            <mat-option *ngFor="let status of goalStatuses" [value]="status.value">
              {{ status.label }}
            </mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field class="goal-dialog__field">
          <mat-label>Target Date</mat-label>
          <input matInput [matDatepicker]="targetPicker" formControlName="targetDate">
          <mat-datepicker-toggle matSuffix [for]="targetPicker"></mat-datepicker-toggle>
          <mat-datepicker #targetPicker></mat-datepicker>
        </mat-form-field>

        <div class="goal-dialog__field">
          <label class="goal-dialog__label">Progress: {{ form.get('progressPercentage')?.value }}%</label>
          <mat-slider min="0" max="100" step="5" class="goal-dialog__slider">
            <input matSliderThumb formControlName="progressPercentage">
          </mat-slider>
        </div>

        <mat-form-field class="goal-dialog__field">
          <mat-label>Success Metrics</mat-label>
          <textarea matInput formControlName="successMetrics" rows="2"></textarea>
        </mat-form-field>

        <mat-form-field class="goal-dialog__field">
          <mat-label>Notes</mat-label>
          <textarea matInput formControlName="notes" rows="2"></textarea>
        </mat-form-field>
      </form>
    </mat-dialog-content>
    <mat-dialog-actions align="end">
      <button mat-button mat-dialog-close>Cancel</button>
      <button mat-raised-button color="primary" (click)="onSave()" [disabled]="!form.valid">Save</button>
    </mat-dialog-actions>
  `,
  styles: [`
    .goal-dialog__form {
      display: flex;
      flex-direction: column;
      gap: 1rem;
      min-width: 400px;
    }

    .goal-dialog__field {
      width: 100%;
    }

    .goal-dialog__label {
      display: block;
      font-size: 0.875rem;
      margin-bottom: 0.5rem;
    }

    .goal-dialog__slider {
      width: 100%;
    }
  `]
})
export class GoalDialog {
  private fb = inject(FormBuilder);
  private dialogRef = inject(MatDialog);
  private reviewPeriodService = inject(ReviewPeriodService);

  data?: Goal;
  form: FormGroup;
  reviewPeriods$ = this.reviewPeriodService.reviewPeriods$;
  goalStatuses = Object.keys(GoalStatus)
    .filter(key => !isNaN(Number(GoalStatus[key as keyof typeof GoalStatus])))
    .map(key => ({
      value: GoalStatus[key as keyof typeof GoalStatus],
      label: GOAL_STATUS_LABELS[GoalStatus[key as keyof typeof GoalStatus] as GoalStatus]
    }));

  constructor() {
    this.form = this.fb.group({
      reviewPeriodId: ['', Validators.required],
      title: ['', Validators.required],
      description: ['', Validators.required],
      status: [GoalStatus.NotStarted, Validators.required],
      targetDate: [''],
      progressPercentage: [0],
      successMetrics: [''],
      notes: ['']
    });

    if (this.data) {
      this.form.patchValue({
        reviewPeriodId: this.data.reviewPeriodId,
        title: this.data.title,
        description: this.data.description,
        status: this.data.status,
        targetDate: this.data.targetDate ? new Date(this.data.targetDate) : null,
        progressPercentage: this.data.progressPercentage,
        successMetrics: this.data.successMetrics,
        notes: this.data.notes
      });
    }
  }

  onSave(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const result = {
        ...formValue,
        targetDate: formValue.targetDate ? formValue.targetDate.toISOString() : undefined
      };

      if (this.data) {
        result.goalId = this.data.goalId;
      }
    }
  }
}

@Component({
  selector: 'app-goals',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule
  ],
  template: `
    <div class="goals">
      <div class="goals__header">
        <h1 class="goals__title">Goals</h1>
        <button mat-raised-button color="primary" (click)="openDialog()">
          <mat-icon>add</mat-icon>
          Add Goal
        </button>
      </div>

      <div class="goals__table">
        <table mat-table [dataSource]="goals$ | async" class="goals__mat-table">
          <ng-container matColumnDef="title">
            <th mat-header-cell *matHeaderCellDef>Title</th>
            <td mat-cell *matCellDef="let goal">{{ goal.title }}</td>
          </ng-container>

          <ng-container matColumnDef="description">
            <th mat-header-cell *matHeaderCellDef>Description</th>
            <td mat-cell *matCellDef="let goal">{{ goal.description }}</td>
          </ng-container>

          <ng-container matColumnDef="status">
            <th mat-header-cell *matHeaderCellDef>Status</th>
            <td mat-cell *matCellDef="let goal">
              <span [class]="'goals__status goals__status--' + getStatusClass(goal.status)">
                {{ getStatusLabel(goal.status) }}
              </span>
            </td>
          </ng-container>

          <ng-container matColumnDef="progressPercentage">
            <th mat-header-cell *matHeaderCellDef>Progress</th>
            <td mat-cell *matCellDef="let goal">{{ goal.progressPercentage }}%</td>
          </ng-container>

          <ng-container matColumnDef="targetDate">
            <th mat-header-cell *matHeaderCellDef>Target Date</th>
            <td mat-cell *matCellDef="let goal">{{ goal.targetDate ? (goal.targetDate | date:'short') : '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let goal">
              <button mat-icon-button (click)="openDialog(goal)">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" (click)="delete(goal.goalId)">
                <mat-icon>delete</mat-icon>
              </button>
            </td>
          </ng-container>

          <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
          <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
        </table>
      </div>
    </div>
  `,
  styles: [`
    .goals {
      padding: 2rem;
    }

    .goals__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 2rem;
    }

    .goals__title {
      margin: 0;
      font-size: 2rem;
      font-weight: 500;
    }

    .goals__table {
      background: white;
      border-radius: 4px;
      overflow: hidden;
    }

    .goals__mat-table {
      width: 100%;
    }

    .goals__status {
      padding: 0.25rem 0.5rem;
      border-radius: 4px;
      font-size: 0.875rem;
      font-weight: 500;
    }

    .goals__status--not-started {
      background-color: #f5f5f5;
      color: #666;
    }

    .goals__status--in-progress {
      background-color: #e3f2fd;
      color: #1976d2;
    }

    .goals__status--on-track {
      background-color: #e8f5e9;
      color: #388e3c;
    }

    .goals__status--at-risk {
      background-color: #fff3e0;
      color: #f57c00;
    }

    .goals__status--completed {
      background-color: #c8e6c9;
      color: #2e7d32;
    }

    .goals__status--deferred {
      background-color: #f3e5f5;
      color: #7b1fa2;
    }

    .goals__status--cancelled {
      background-color: #ffebee;
      color: #c62828;
    }
  `]
})
export class Goals implements OnInit {
  private goalService = inject(GoalService);
  private reviewPeriodService = inject(ReviewPeriodService);
  private dialog = inject(MatDialog);

  goals$ = this.goalService.goals$;
  displayedColumns = ['title', 'description', 'status', 'progressPercentage', 'targetDate', 'actions'];

  ngOnInit(): void {
    this.goalService.getAll().subscribe();
    this.reviewPeriodService.getAll().subscribe();
  }

  openDialog(goal?: Goal): void {
    const dialogRef = this.dialog.open(GoalDialog, {
      width: '500px',
      data: goal
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (goal) {
          this.goalService.update(result as UpdateGoal).subscribe();
        } else {
          // Generate a dummy userId for demo purposes
          const createData: CreateGoal = {
            ...result,
            userId: '00000000-0000-0000-0000-000000000000'
          };
          this.goalService.create(createData).subscribe();
        }
      }
    });
  }

  delete(id: string): void {
    if (confirm('Are you sure you want to delete this goal?')) {
      this.goalService.delete(id).subscribe();
    }
  }

  getStatusLabel(status: GoalStatus): string {
    return GOAL_STATUS_LABELS[status];
  }

  getStatusClass(status: GoalStatus): string {
    return GOAL_STATUS_LABELS[status].toLowerCase().replace(/\s+/g, '-');
  }
}
