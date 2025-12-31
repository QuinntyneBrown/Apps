import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { StreakService, RoutineService } from '../services';
import { Streak, CreateStreakRequest, UpdateStreakRequest } from '../models';

@Component({
  selector: 'app-streak-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatCheckboxModule,
    MatButtonModule
  ],
  template: `
    <h2 mat-dialog-title>{{ data ? 'Edit Streak' : 'New Streak' }}</h2>
    <mat-dialog-content>
      <form [formGroup]="form" class="streak-form">
        <mat-form-field class="streak-form__field" *ngIf="!data">
          <mat-label>Routine</mat-label>
          <mat-select formControlName="routineId" required>
            <mat-option *ngFor="let routine of routines$ | async" [value]="routine.routineId">
              {{ routine.name }}
            </mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field class="streak-form__field" *ngIf="data">
          <mat-label>Current Streak</mat-label>
          <input matInput formControlName="currentStreak" type="number" required>
        </mat-form-field>

        <mat-form-field class="streak-form__field" *ngIf="data">
          <mat-label>Longest Streak</mat-label>
          <input matInput formControlName="longestStreak" type="number" required>
        </mat-form-field>

        <mat-form-field class="streak-form__field" *ngIf="data">
          <mat-label>Last Completion Date</mat-label>
          <input matInput formControlName="lastCompletionDate" type="datetime-local">
        </mat-form-field>

        <mat-form-field class="streak-form__field" *ngIf="data">
          <mat-label>Streak Start Date</mat-label>
          <input matInput formControlName="streakStartDate" type="datetime-local">
        </mat-form-field>

        <mat-checkbox formControlName="isActive" *ngIf="data">Active</mat-checkbox>
      </form>
    </mat-dialog-content>
    <mat-dialog-actions align="end">
      <button mat-button mat-dialog-close>Cancel</button>
      <button mat-raised-button color="primary" (click)="save()" [disabled]="!form.valid">Save</button>
    </mat-dialog-actions>
  `,
  styles: [`
    .streak-form {
      display: flex;
      flex-direction: column;
      gap: 1rem;
      min-width: 400px;
      padding: 1rem 0;

      &__field {
        width: 100%;
      }
    }
  `]
})
export class StreakDialog {
  private fb = inject(FormBuilder);
  private dialogRef = inject(MatDialog);
  private routineService = inject(RoutineService);
  data?: Streak;

  form: FormGroup;
  routines$ = this.routineService.routines$;

  constructor() {
    this.form = this.fb.group({
      routineId: ['', Validators.required],
      userId: ['00000000-0000-0000-0000-000000000000', Validators.required],
      currentStreak: [0, [Validators.required, Validators.min(0)]],
      longestStreak: [0, [Validators.required, Validators.min(0)]],
      lastCompletionDate: [''],
      streakStartDate: [''],
      isActive: [true]
    });

    if (this.data) {
      this.form.patchValue({
        currentStreak: this.data.currentStreak,
        longestStreak: this.data.longestStreak,
        lastCompletionDate: this.data.lastCompletionDate ? this.formatDateTime(this.data.lastCompletionDate) : '',
        streakStartDate: this.data.streakStartDate ? this.formatDateTime(this.data.streakStartDate) : '',
        isActive: this.data.isActive
      });
    }

    this.routineService.getAll().subscribe();
  }

  formatDateTime(dateString: string): string {
    const date = new Date(dateString);
    return date.toISOString().slice(0, 16);
  }

  save() {
    if (this.form.valid) {
      this.dialogRef.closeAll();
    }
  }
}

@Component({
  selector: 'app-streaks',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule
  ],
  template: `
    <div class="streaks">
      <div class="streaks__header">
        <h1 class="streaks__title">Streaks</h1>
        <button mat-raised-button color="primary" (click)="openDialog()">
          <mat-icon>add</mat-icon>
          New Streak
        </button>
      </div>

      <div class="streaks__table">
        <table mat-table [dataSource]="streaks$ | async" class="mat-elevation-z2">
          <ng-container matColumnDef="currentStreak">
            <th mat-header-cell *matHeaderCellDef>Current Streak</th>
            <td mat-cell *matCellDef="let streak">
              <span class="streak-badge">{{ streak.currentStreak }} days</span>
            </td>
          </ng-container>

          <ng-container matColumnDef="longestStreak">
            <th mat-header-cell *matHeaderCellDef>Longest Streak</th>
            <td mat-cell *matCellDef="let streak">
              <span class="streak-badge streak-badge--gold">{{ streak.longestStreak }} days</span>
            </td>
          </ng-container>

          <ng-container matColumnDef="lastCompletionDate">
            <th mat-header-cell *matHeaderCellDef>Last Completion</th>
            <td mat-cell *matCellDef="let streak">
              {{ streak.lastCompletionDate ? (streak.lastCompletionDate | date:'short') : '-' }}
            </td>
          </ng-container>

          <ng-container matColumnDef="streakStartDate">
            <th mat-header-cell *matHeaderCellDef>Streak Start</th>
            <td mat-cell *matCellDef="let streak">
              {{ streak.streakStartDate ? (streak.streakStartDate | date:'short') : '-' }}
            </td>
          </ng-container>

          <ng-container matColumnDef="isActive">
            <th mat-header-cell *matHeaderCellDef>Active</th>
            <td mat-cell *matCellDef="let streak">
              <mat-icon [class.active]="streak.isActive">
                {{ streak.isActive ? 'check_circle' : 'cancel' }}
              </mat-icon>
            </td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let streak">
              <button mat-icon-button color="primary" (click)="openDialog(streak)">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" (click)="delete(streak.streakId)">
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
    .streaks {
      padding: 2rem;

      &__header {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-bottom: 2rem;
      }

      &__title {
        margin: 0;
        font-size: 2rem;
        font-weight: 400;
      }

      &__table {
        width: 100%;

        table {
          width: 100%;
        }

        .streak-badge {
          background-color: #1976d2;
          color: white;
          padding: 0.25rem 0.5rem;
          border-radius: 4px;
          font-weight: 500;

          &--gold {
            background-color: #ff9800;
          }
        }

        .active {
          color: #4caf50;
        }

        mat-icon:not(.active) {
          color: #f44336;
        }
      }
    }
  `]
})
export class Streaks implements OnInit {
  private streakService = inject(StreakService);
  private dialog = inject(MatDialog);

  streaks$ = this.streakService.streaks$;
  displayedColumns = ['currentStreak', 'longestStreak', 'lastCompletionDate', 'streakStartDate', 'isActive', 'actions'];

  ngOnInit() {
    this.streakService.getAll().subscribe();
  }

  openDialog(streak?: Streak) {
    const dialogRef = this.dialog.open(StreakDialog, {
      width: '500px',
      data: streak
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (streak) {
          this.update(streak.streakId, result);
        } else {
          this.create(result);
        }
      }
    });
  }

  create(request: CreateStreakRequest) {
    this.streakService.create(request).subscribe();
  }

  update(id: string, request: UpdateStreakRequest) {
    this.streakService.update(id, request).subscribe();
  }

  delete(id: string) {
    if (confirm('Are you sure you want to delete this streak?')) {
      this.streakService.delete(id).subscribe();
    }
  }
}
