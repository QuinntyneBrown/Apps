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
import { CompletionLogService, RoutineService } from '../services';
import { CompletionLog, CreateCompletionLogRequest, UpdateCompletionLogRequest } from '../models';

@Component({
  selector: 'app-completion-log-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule
  ],
  template: `
    <h2 mat-dialog-title>{{ data ? 'Edit Completion Log' : 'New Completion Log' }}</h2>
    <mat-dialog-content>
      <form [formGroup]="form" class="log-form">
        <mat-form-field class="log-form__field" *ngIf="!data">
          <mat-label>Routine</mat-label>
          <mat-select formControlName="routineId" required>
            <mat-option *ngFor="let routine of routines$ | async" [value]="routine.routineId">
              {{ routine.name }}
            </mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field class="log-form__field">
          <mat-label>Completion Date</mat-label>
          <input matInput formControlName="completionDate" type="datetime-local" required>
        </mat-form-field>

        <mat-form-field class="log-form__field">
          <mat-label>Actual Start Time</mat-label>
          <input matInput formControlName="actualStartTime" type="datetime-local">
        </mat-form-field>

        <mat-form-field class="log-form__field">
          <mat-label>Actual End Time</mat-label>
          <input matInput formControlName="actualEndTime" type="datetime-local">
        </mat-form-field>

        <mat-form-field class="log-form__field">
          <mat-label>Tasks Completed</mat-label>
          <input matInput formControlName="tasksCompleted" type="number" required>
        </mat-form-field>

        <mat-form-field class="log-form__field">
          <mat-label>Total Tasks</mat-label>
          <input matInput formControlName="totalTasks" type="number" required>
        </mat-form-field>

        <mat-form-field class="log-form__field">
          <mat-label>Mood Rating (1-10)</mat-label>
          <input matInput formControlName="moodRating" type="number" min="1" max="10">
        </mat-form-field>

        <mat-form-field class="log-form__field">
          <mat-label>Notes</mat-label>
          <textarea matInput formControlName="notes" rows="3"></textarea>
        </mat-form-field>
      </form>
    </mat-dialog-content>
    <mat-dialog-actions align="end">
      <button mat-button mat-dialog-close>Cancel</button>
      <button mat-raised-button color="primary" (click)="save()" [disabled]="!form.valid">Save</button>
    </mat-dialog-actions>
  `,
  styles: [`
    .log-form {
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
export class CompletionLogDialog {
  private fb = inject(FormBuilder);
  private dialogRef = inject(MatDialog);
  private routineService = inject(RoutineService);
  data?: CompletionLog;

  form: FormGroup;
  routines$ = this.routineService.routines$;

  constructor() {
    this.form = this.fb.group({
      routineId: ['', Validators.required],
      userId: ['00000000-0000-0000-0000-000000000000', Validators.required],
      completionDate: ['', Validators.required],
      actualStartTime: [''],
      actualEndTime: [''],
      tasksCompleted: [0, [Validators.required, Validators.min(0)]],
      totalTasks: [0, [Validators.required, Validators.min(0)]],
      moodRating: [''],
      notes: ['']
    });

    if (this.data) {
      this.form.patchValue({
        completionDate: this.formatDateTime(this.data.completionDate),
        actualStartTime: this.data.actualStartTime ? this.formatDateTime(this.data.actualStartTime) : '',
        actualEndTime: this.data.actualEndTime ? this.formatDateTime(this.data.actualEndTime) : '',
        tasksCompleted: this.data.tasksCompleted,
        totalTasks: this.data.totalTasks,
        moodRating: this.data.moodRating,
        notes: this.data.notes
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
  selector: 'app-completion-logs',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule
  ],
  template: `
    <div class="logs">
      <div class="logs__header">
        <h1 class="logs__title">Completion Logs</h1>
        <button mat-raised-button color="primary" (click)="openDialog()">
          <mat-icon>add</mat-icon>
          New Completion Log
        </button>
      </div>

      <div class="logs__table">
        <table mat-table [dataSource]="logs$ | async" class="mat-elevation-z2">
          <ng-container matColumnDef="completionDate">
            <th mat-header-cell *matHeaderCellDef>Date</th>
            <td mat-cell *matCellDef="let log">{{ log.completionDate | date:'short' }}</td>
          </ng-container>

          <ng-container matColumnDef="tasksCompleted">
            <th mat-header-cell *matHeaderCellDef>Tasks</th>
            <td mat-cell *matCellDef="let log">{{ log.tasksCompleted }} / {{ log.totalTasks }}</td>
          </ng-container>

          <ng-container matColumnDef="actualStartTime">
            <th mat-header-cell *matHeaderCellDef>Start</th>
            <td mat-cell *matCellDef="let log">{{ log.actualStartTime ? (log.actualStartTime | date:'shortTime') : '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="actualEndTime">
            <th mat-header-cell *matHeaderCellDef>End</th>
            <td mat-cell *matCellDef="let log">{{ log.actualEndTime ? (log.actualEndTime | date:'shortTime') : '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="moodRating">
            <th mat-header-cell *matHeaderCellDef>Mood</th>
            <td mat-cell *matCellDef="let log">{{ log.moodRating || '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="notes">
            <th mat-header-cell *matHeaderCellDef>Notes</th>
            <td mat-cell *matCellDef="let log">{{ log.notes || '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let log">
              <button mat-icon-button color="primary" (click)="openDialog(log)">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" (click)="delete(log.completionLogId)">
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
    .logs {
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
      }
    }
  `]
})
export class CompletionLogs implements OnInit {
  private logService = inject(CompletionLogService);
  private dialog = inject(MatDialog);

  logs$ = this.logService.logs$;
  displayedColumns = ['completionDate', 'tasksCompleted', 'actualStartTime', 'actualEndTime', 'moodRating', 'notes', 'actions'];

  ngOnInit() {
    this.logService.getAll().subscribe();
  }

  openDialog(log?: CompletionLog) {
    const dialogRef = this.dialog.open(CompletionLogDialog, {
      width: '500px',
      data: log
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (log) {
          this.update(log.completionLogId, result);
        } else {
          this.create(result);
        }
      }
    });
  }

  create(request: CreateCompletionLogRequest) {
    this.logService.create(request).subscribe();
  }

  update(id: string, request: UpdateCompletionLogRequest) {
    this.logService.update(id, request).subscribe();
  }

  delete(id: string) {
    if (confirm('Are you sure you want to delete this completion log?')) {
      this.logService.delete(id).subscribe();
    }
  }
}
