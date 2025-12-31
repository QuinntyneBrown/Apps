import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatChipsModule } from '@angular/material/chips';
import { provideNativeDateAdapter } from '@angular/material/core';
import { AdminTaskService } from '../services';
import { AdminTask, CreateAdminTask, UpdateAdminTask, TaskCategory, TaskCategoryLabels, TaskPriority, TaskPriorityLabels } from '../models';

@Component({
  selector: 'app-admin-tasks',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatDatepickerModule,
    MatCheckboxModule,
    MatChipsModule
  ],
  providers: [provideNativeDateAdapter()],
  template: `
    <div class="admin-tasks">
      <div class="admin-tasks__header">
        <h1 class="admin-tasks__title">Admin Tasks</h1>
        <button mat-raised-button color="primary" (click)="showForm = !showForm" class="admin-tasks__add-btn">
          <mat-icon>{{ showForm ? 'close' : 'add' }}</mat-icon>
          {{ showForm ? 'Cancel' : 'Add Task' }}
        </button>
      </div>

      <mat-card *ngIf="showForm" class="admin-tasks__form-card">
        <mat-card-header>
          <mat-card-title>{{ editingTask ? 'Edit' : 'Create' }} Admin Task</mat-card-title>
        </mat-card-header>
        <mat-card-content>
          <form [formGroup]="taskForm" class="admin-tasks__form">
            <mat-form-field class="admin-tasks__form-field">
              <mat-label>Title</mat-label>
              <input matInput formControlName="title" required>
            </mat-form-field>

            <mat-form-field class="admin-tasks__form-field">
              <mat-label>Description</mat-label>
              <textarea matInput formControlName="description" rows="3"></textarea>
            </mat-form-field>

            <mat-form-field class="admin-tasks__form-field">
              <mat-label>Category</mat-label>
              <mat-select formControlName="category" required>
                <mat-option *ngFor="let category of categories" [value]="category.value">
                  {{ category.label }}
                </mat-option>
              </mat-select>
            </mat-form-field>

            <mat-form-field class="admin-tasks__form-field">
              <mat-label>Priority</mat-label>
              <mat-select formControlName="priority" required>
                <mat-option *ngFor="let priority of priorities" [value]="priority.value">
                  {{ priority.label }}
                </mat-option>
              </mat-select>
            </mat-form-field>

            <mat-form-field class="admin-tasks__form-field">
              <mat-label>Due Date</mat-label>
              <input matInput [matDatepicker]="picker" formControlName="dueDate">
              <mat-datepicker-toggle matSuffix [for]="picker"></mat-datepicker-toggle>
              <mat-datepicker #picker></mat-datepicker>
            </mat-form-field>

            <div class="admin-tasks__form-checkbox">
              <mat-checkbox formControlName="isRecurring">Is Recurring</mat-checkbox>
            </div>

            <mat-form-field *ngIf="taskForm.value.isRecurring" class="admin-tasks__form-field">
              <mat-label>Recurrence Pattern</mat-label>
              <input matInput formControlName="recurrencePattern">
            </mat-form-field>

            <mat-form-field class="admin-tasks__form-field">
              <mat-label>Notes</mat-label>
              <textarea matInput formControlName="notes" rows="3"></textarea>
            </mat-form-field>
          </form>
        </mat-card-content>
        <mat-card-actions>
          <button mat-raised-button color="primary" (click)="saveTask()" [disabled]="taskForm.invalid">
            {{ editingTask ? 'Update' : 'Create' }}
          </button>
          <button mat-button (click)="cancelEdit()">Cancel</button>
        </mat-card-actions>
      </mat-card>

      <mat-card class="admin-tasks__table-card">
        <table mat-table [dataSource]="tasks$ | async" class="admin-tasks__table">
          <ng-container matColumnDef="title">
            <th mat-header-cell *matHeaderCellDef>Title</th>
            <td mat-cell *matCellDef="let task">{{ task.title }}</td>
          </ng-container>

          <ng-container matColumnDef="category">
            <th mat-header-cell *matHeaderCellDef>Category</th>
            <td mat-cell *matCellDef="let task">{{ getCategoryLabel(task.category) }}</td>
          </ng-container>

          <ng-container matColumnDef="priority">
            <th mat-header-cell *matHeaderCellDef>Priority</th>
            <td mat-cell *matCellDef="let task">
              <mat-chip [class]="'priority-' + task.priority">
                {{ getPriorityLabel(task.priority) }}
              </mat-chip>
            </td>
          </ng-container>

          <ng-container matColumnDef="dueDate">
            <th mat-header-cell *matHeaderCellDef>Due Date</th>
            <td mat-cell *matCellDef="let task">{{ task.dueDate ? (task.dueDate | date: 'short') : '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="status">
            <th mat-header-cell *matHeaderCellDef>Status</th>
            <td mat-cell *matCellDef="let task">
              <mat-chip [class]="task.isCompleted ? 'status-completed' : 'status-active'">
                {{ task.isCompleted ? 'Completed' : 'Active' }}
              </mat-chip>
            </td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let task">
              <button mat-icon-button (click)="editTask(task)" [disabled]="task.isCompleted">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button (click)="completeTask(task)" [disabled]="task.isCompleted">
                <mat-icon>check_circle</mat-icon>
              </button>
              <button mat-icon-button (click)="deleteTask(task)" color="warn">
                <mat-icon>delete</mat-icon>
              </button>
            </td>
          </ng-container>

          <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
          <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
        </table>
      </mat-card>
    </div>
  `,
  styles: [`
    .admin-tasks {
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
        font-weight: 500;
      }

      &__add-btn {
        display: flex;
        align-items: center;
        gap: 0.5rem;
      }

      &__form-card {
        margin-bottom: 2rem;
      }

      &__form {
        display: flex;
        flex-direction: column;
        gap: 1rem;
      }

      &__form-field {
        width: 100%;
      }

      &__form-checkbox {
        margin: 0.5rem 0;
      }

      &__table-card {
        overflow-x: auto;
      }

      &__table {
        width: 100%;
      }
    }

    .priority-0 { background-color: #e3f2fd; color: #1976d2; }
    .priority-1 { background-color: #fff3e0; color: #f57c00; }
    .priority-2 { background-color: #fce4ec; color: #c2185b; }
    .priority-3 { background-color: #ffebee; color: #d32f2f; }

    .status-active { background-color: #e8f5e9; color: #388e3c; }
    .status-completed { background-color: #f5f5f5; color: #757575; }
  `]
})
export class AdminTasks implements OnInit {
  private adminTaskService = inject(AdminTaskService);
  private fb = inject(FormBuilder);

  tasks$ = this.adminTaskService.tasks$;
  showForm = false;
  editingTask: AdminTask | null = null;

  displayedColumns = ['title', 'category', 'priority', 'dueDate', 'status', 'actions'];

  categories = Object.keys(TaskCategory)
    .filter(key => !isNaN(Number(key)))
    .map(key => ({
      value: Number(key),
      label: TaskCategoryLabels[Number(key) as TaskCategory]
    }));

  priorities = Object.keys(TaskPriority)
    .filter(key => !isNaN(Number(key)))
    .map(key => ({
      value: Number(key),
      label: TaskPriorityLabels[Number(key) as TaskPriority]
    }));

  taskForm: FormGroup = this.fb.group({
    title: ['', Validators.required],
    description: [''],
    category: [TaskCategory.Other, Validators.required],
    priority: [TaskPriority.Medium, Validators.required],
    dueDate: [''],
    isRecurring: [false],
    recurrencePattern: [''],
    notes: ['']
  });

  ngOnInit(): void {
    this.adminTaskService.getAll().subscribe();
  }

  saveTask(): void {
    if (this.taskForm.invalid) return;

    const formValue = this.taskForm.value;
    const taskData = {
      ...formValue,
      dueDate: formValue.dueDate ? new Date(formValue.dueDate).toISOString() : undefined,
      userId: '00000000-0000-0000-0000-000000000000' // Placeholder user ID
    };

    if (this.editingTask) {
      const updateTask: UpdateAdminTask = {
        ...taskData,
        adminTaskId: this.editingTask.adminTaskId
      };
      this.adminTaskService.update(updateTask).subscribe(() => {
        this.cancelEdit();
      });
    } else {
      const createTask: CreateAdminTask = taskData;
      this.adminTaskService.create(createTask).subscribe(() => {
        this.cancelEdit();
      });
    }
  }

  editTask(task: AdminTask): void {
    this.editingTask = task;
    this.showForm = true;
    this.taskForm.patchValue({
      title: task.title,
      description: task.description,
      category: task.category,
      priority: task.priority,
      dueDate: task.dueDate ? new Date(task.dueDate) : null,
      isRecurring: task.isRecurring,
      recurrencePattern: task.recurrencePattern,
      notes: task.notes
    });
  }

  completeTask(task: AdminTask): void {
    this.adminTaskService.complete(task.adminTaskId).subscribe();
  }

  deleteTask(task: AdminTask): void {
    if (confirm(`Are you sure you want to delete "${task.title}"?`)) {
      this.adminTaskService.delete(task.adminTaskId).subscribe();
    }
  }

  cancelEdit(): void {
    this.editingTask = null;
    this.showForm = false;
    this.taskForm.reset({
      category: TaskCategory.Other,
      priority: TaskPriority.Medium,
      isRecurring: false
    });
  }

  getCategoryLabel(category: TaskCategory): string {
    return TaskCategoryLabels[category];
  }

  getPriorityLabel(priority: TaskPriority): string {
    return TaskPriorityLabels[priority];
  }
}
