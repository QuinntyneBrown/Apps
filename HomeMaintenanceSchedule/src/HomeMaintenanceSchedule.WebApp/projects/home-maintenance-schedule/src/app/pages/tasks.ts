import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatCardModule } from '@angular/material/card';
import { MaintenanceTaskService } from '../services';
import { MaintenanceTask, MAINTENANCE_TYPE_LABELS, TASK_STATUS_LABELS, TaskStatus } from '../models';

@Component({
  selector: 'app-tasks',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule, MatChipsModule, MatCardModule],
  template: `
    <div class="tasks">
      <div class="tasks__header">
        <h1>Maintenance Tasks</h1>
        <button mat-raised-button color="primary">
          <mat-icon>add</mat-icon>
          Add Task
        </button>
      </div>

      <mat-card>
        <table mat-table [dataSource]="(tasks$ | async) || []" class="tasks__table">
          <ng-container matColumnDef="name">
            <th mat-header-cell *matHeaderCellDef>Name</th>
            <td mat-cell *matCellDef="let task">{{ task.name }}</td>
          </ng-container>

          <ng-container matColumnDef="type">
            <th mat-header-cell *matHeaderCellDef>Type</th>
            <td mat-cell *matCellDef="let task">{{ getTypeLabel(task.maintenanceType) }}</td>
          </ng-container>

          <ng-container matColumnDef="status">
            <th mat-header-cell *matHeaderCellDef>Status</th>
            <td mat-cell *matCellDef="let task">
              <mat-chip [class]="'status--' + task.status">
                {{ getStatusLabel(task.status) }}
              </mat-chip>
            </td>
          </ng-container>

          <ng-container matColumnDef="dueDate">
            <th mat-header-cell *matHeaderCellDef>Due Date</th>
            <td mat-cell *matCellDef="let task">{{ task.dueDate | date:'mediumDate' }}</td>
          </ng-container>

          <ng-container matColumnDef="priority">
            <th mat-header-cell *matHeaderCellDef>Priority</th>
            <td mat-cell *matCellDef="let task">{{ task.priority }}</td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let task">
              <button mat-icon-button color="primary" title="Edit">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" title="Delete" (click)="deleteTask(task)">
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
    .tasks {
      padding: 1.5rem;
    }
    .tasks__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 1.5rem;
    }
    .tasks__table {
      width: 100%;
    }
    .status--0 { background-color: #e3f2fd; }
    .status--1 { background-color: #fff3e0; }
    .status--2 { background-color: #e8f5e9; }
    .status--3 { background-color: #fce4ec; }
    .status--4 { background-color: #f5f5f5; }
  `]
})
export class Tasks implements OnInit {
  private _taskService = inject(MaintenanceTaskService);

  tasks$ = this._taskService.tasks$;
  displayedColumns = ['name', 'type', 'status', 'dueDate', 'priority', 'actions'];

  ngOnInit(): void {
    this._taskService.getAll().subscribe();
  }

  getTypeLabel(type: number): string {
    return MAINTENANCE_TYPE_LABELS[type as keyof typeof MAINTENANCE_TYPE_LABELS] || 'Unknown';
  }

  getStatusLabel(status: number): string {
    return TASK_STATUS_LABELS[status as keyof typeof TASK_STATUS_LABELS] || 'Unknown';
  }

  deleteTask(task: MaintenanceTask): void {
    if (confirm(`Are you sure you want to delete "${task.name}"?`)) {
      this._taskService.delete(task.maintenanceTaskId).subscribe();
    }
  }
}
