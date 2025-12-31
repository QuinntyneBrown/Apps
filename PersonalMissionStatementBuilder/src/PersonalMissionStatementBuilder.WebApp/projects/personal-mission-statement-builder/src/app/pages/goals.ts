import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatChipsModule } from '@angular/material/chips';
import { GoalService } from '../services';
import { Goal, CreateGoal, UpdateGoal, GoalStatus, GoalStatusLabels } from '../models';

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
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  template: `
    <h2 mat-dialog-title>{{ data ? 'Edit' : 'Add' }} Goal</h2>
    <form [formGroup]="form" (ngSubmit)="onSubmit()">
      <mat-dialog-content>
        <mat-form-field class="dialog__field">
          <mat-label>Title</mat-label>
          <input matInput formControlName="title" required>
        </mat-form-field>

        <mat-form-field class="dialog__field">
          <mat-label>Description</mat-label>
          <textarea matInput formControlName="description" rows="4"></textarea>
        </mat-form-field>

        <mat-form-field class="dialog__field">
          <mat-label>Status</mat-label>
          <mat-select formControlName="status" required>
            <mat-option *ngFor="let status of goalStatuses" [value]="status.value">
              {{ status.label }}
            </mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field class="dialog__field">
          <mat-label>Target Date</mat-label>
          <input matInput [matDatepicker]="picker" formControlName="targetDate">
          <mat-datepicker-toggle matSuffix [for]="picker"></mat-datepicker-toggle>
          <mat-datepicker #picker></mat-datepicker>
        </mat-form-field>
      </mat-dialog-content>
      <mat-dialog-actions align="end">
        <button mat-button type="button" mat-dialog-close>Cancel</button>
        <button mat-raised-button color="primary" type="submit" [disabled]="form.invalid">
          {{ data ? 'Update' : 'Create' }}
        </button>
      </mat-dialog-actions>
    </form>
  `,
  styles: [`
    .dialog__field {
      width: 100%;
      margin-bottom: 1rem;
    }
  `]
})
export class GoalDialog {
  private fb = inject(FormBuilder);
  data: Goal | null = inject(MatDialog).openDialogs[0]?.componentInstance?.data || null;

  goalStatuses = Object.keys(GoalStatus)
    .filter(key => !isNaN(Number(key)))
    .map(key => ({
      value: Number(key),
      label: GoalStatusLabels[Number(key) as GoalStatus]
    }));

  form = this.fb.group({
    title: [this.data?.title || '', Validators.required],
    description: [this.data?.description || ''],
    status: [this.data?.status ?? GoalStatus.NotStarted, Validators.required],
    targetDate: [this.data?.targetDate || null]
  });

  onSubmit() {
    if (this.form.valid) {
      inject(MatDialog).closeAll();
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
    MatIconModule,
    MatDialogModule,
    MatChipsModule
  ],
  templateUrl: './goals.html',
  styleUrl: './goals.scss'
})
export class Goals implements OnInit {
  private goalService = inject(GoalService);
  private dialog = inject(MatDialog);

  goals$ = this.goalService.goals$;
  displayedColumns = ['title', 'status', 'targetDate', 'completedDate', 'createdAt', 'actions'];
  GoalStatusLabels = GoalStatusLabels;

  ngOnInit() {
    this.goalService.getAll().subscribe();
  }

  openDialog(goal?: Goal) {
    const dialogRef = this.dialog.open(GoalDialog, {
      width: '500px',
      data: goal
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result !== undefined) {
        if (goal) {
          this.updateGoal(goal.goalId, result);
        } else {
          this.createGoal(result);
        }
      }
    });
  }

  createGoal(data: any) {
    const create: CreateGoal = {
      userId: '00000000-0000-0000-0000-000000000000', // Replace with actual user ID
      title: data.title,
      description: data.description,
      status: data.status,
      targetDate: data.targetDate
    };
    this.goalService.create(create).subscribe();
  }

  updateGoal(id: string, data: any) {
    const update: UpdateGoal = {
      goalId: id,
      title: data.title,
      description: data.description,
      status: data.status,
      targetDate: data.targetDate
    };
    this.goalService.update(update).subscribe();
  }

  deleteGoal(id: string) {
    if (confirm('Are you sure you want to delete this goal?')) {
      this.goalService.delete(id).subscribe();
    }
  }

  getStatusClass(status: GoalStatus): string {
    switch (status) {
      case GoalStatus.Completed:
        return 'goals__status--completed';
      case GoalStatus.InProgress:
        return 'goals__status--in-progress';
      case GoalStatus.OnHold:
        return 'goals__status--on-hold';
      case GoalStatus.Abandoned:
        return 'goals__status--abandoned';
      default:
        return 'goals__status--not-started';
    }
  }
}
