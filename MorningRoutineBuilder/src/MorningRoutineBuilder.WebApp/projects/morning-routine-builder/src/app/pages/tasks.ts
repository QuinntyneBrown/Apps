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
import { RoutineTaskService, RoutineService } from '../services';
import { RoutineTask, CreateRoutineTaskRequest, UpdateRoutineTaskRequest, TaskType, TaskTypeLabels } from '../models';

@Component({
  selector: 'app-task-dialog',
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
    <h2 mat-dialog-title>{{ data ? 'Edit Task' : 'New Task' }}</h2>
    <mat-dialog-content>
      <form [formGroup]="form" class="task-form">
        <mat-form-field class="task-form__field" *ngIf="!data">
          <mat-label>Routine</mat-label>
          <mat-select formControlName="routineId" required>
            <mat-option *ngFor="let routine of routines$ | async" [value]="routine.routineId">
              {{ routine.name }}
            </mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field class="task-form__field">
          <mat-label>Name</mat-label>
          <input matInput formControlName="name" required>
        </mat-form-field>

        <mat-form-field class="task-form__field">
          <mat-label>Task Type</mat-label>
          <mat-select formControlName="taskType" required>
            <mat-option *ngFor="let type of taskTypes" [value]="type.value">
              {{ type.label }}
            </mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field class="task-form__field">
          <mat-label>Description</mat-label>
          <textarea matInput formControlName="description" rows="3"></textarea>
        </mat-form-field>

        <mat-form-field class="task-form__field">
          <mat-label>Estimated Duration (minutes)</mat-label>
          <input matInput formControlName="estimatedDurationMinutes" type="number" required>
        </mat-form-field>

        <mat-form-field class="task-form__field">
          <mat-label>Sort Order</mat-label>
          <input matInput formControlName="sortOrder" type="number" required>
        </mat-form-field>

        <mat-checkbox formControlName="isOptional">Optional</mat-checkbox>
      </form>
    </mat-dialog-content>
    <mat-dialog-actions align="end">
      <button mat-button mat-dialog-close>Cancel</button>
      <button mat-raised-button color="primary" (click)="save()" [disabled]="!form.valid">Save</button>
    </mat-dialog-actions>
  `,
  styles: [`
    .task-form {
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
export class TaskDialog {
  private fb = inject(FormBuilder);
  private dialogRef = inject(MatDialog);
  private routineService = inject(RoutineService);
  data?: RoutineTask;

  form: FormGroup;
  routines$ = this.routineService.routines$;
  taskTypes = Object.entries(TaskTypeLabels).map(([value, label]) => ({ value: Number(value), label }));

  constructor() {
    this.form = this.fb.group({
      routineId: ['', Validators.required],
      name: ['', Validators.required],
      taskType: [TaskType.Other, Validators.required],
      description: [''],
      estimatedDurationMinutes: [0, [Validators.required, Validators.min(1)]],
      sortOrder: [0, Validators.required],
      isOptional: [false]
    });

    if (this.data) {
      this.form.patchValue({
        name: this.data.name,
        taskType: this.data.taskType,
        description: this.data.description,
        estimatedDurationMinutes: this.data.estimatedDurationMinutes,
        sortOrder: this.data.sortOrder,
        isOptional: this.data.isOptional
      });
    }

    this.routineService.getAll().subscribe();
  }

  save() {
    if (this.form.valid) {
      this.dialogRef.closeAll();
    }
  }
}

@Component({
  selector: 'app-tasks',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule
  ],
  template: `
    <div class="tasks">
      <div class="tasks__header">
        <h1 class="tasks__title">Routine Tasks</h1>
        <button mat-raised-button color="primary" (click)="openDialog()">
          <mat-icon>add</mat-icon>
          New Task
        </button>
      </div>

      <div class="tasks__table">
        <table mat-table [dataSource]="tasks$ | async" class="mat-elevation-z2">
          <ng-container matColumnDef="name">
            <th mat-header-cell *matHeaderCellDef>Name</th>
            <td mat-cell *matCellDef="let task">{{ task.name }}</td>
          </ng-container>

          <ng-container matColumnDef="taskType">
            <th mat-header-cell *matHeaderCellDef>Type</th>
            <td mat-cell *matCellDef="let task">{{ getTaskTypeLabel(task.taskType) }}</td>
          </ng-container>

          <ng-container matColumnDef="description">
            <th mat-header-cell *matHeaderCellDef>Description</th>
            <td mat-cell *matCellDef="let task">{{ task.description || '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="estimatedDurationMinutes">
            <th mat-header-cell *matHeaderCellDef>Duration (min)</th>
            <td mat-cell *matCellDef="let task">{{ task.estimatedDurationMinutes }}</td>
          </ng-container>

          <ng-container matColumnDef="sortOrder">
            <th mat-header-cell *matHeaderCellDef>Order</th>
            <td mat-cell *matCellDef="let task">{{ task.sortOrder }}</td>
          </ng-container>

          <ng-container matColumnDef="isOptional">
            <th mat-header-cell *matHeaderCellDef>Optional</th>
            <td mat-cell *matCellDef="let task">
              <mat-icon [class.optional]="task.isOptional">
                {{ task.isOptional ? 'check_circle' : 'cancel' }}
              </mat-icon>
            </td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let task">
              <button mat-icon-button color="primary" (click)="openDialog(task)">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" (click)="delete(task.routineTaskId)">
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
    .tasks {
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

        .optional {
          color: #4caf50;
        }

        mat-icon:not(.optional) {
          color: #f44336;
        }
      }
    }
  `]
})
export class Tasks implements OnInit {
  private taskService = inject(RoutineTaskService);
  private dialog = inject(MatDialog);

  tasks$ = this.taskService.tasks$;
  displayedColumns = ['name', 'taskType', 'description', 'estimatedDurationMinutes', 'sortOrder', 'isOptional', 'actions'];

  ngOnInit() {
    this.taskService.getAll().subscribe();
  }

  getTaskTypeLabel(type: TaskType): string {
    return TaskTypeLabels[type];
  }

  openDialog(task?: RoutineTask) {
    const dialogRef = this.dialog.open(TaskDialog, {
      width: '500px',
      data: task
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (task) {
          this.update(task.routineTaskId, result);
        } else {
          this.create(result);
        }
      }
    });
  }

  create(request: CreateRoutineTaskRequest) {
    this.taskService.create(request).subscribe();
  }

  update(id: string, request: UpdateRoutineTaskRequest) {
    this.taskService.update(id, request).subscribe();
  }

  delete(id: string) {
    if (confirm('Are you sure you want to delete this task?')) {
      this.taskService.delete(id).subscribe();
    }
  }
}
