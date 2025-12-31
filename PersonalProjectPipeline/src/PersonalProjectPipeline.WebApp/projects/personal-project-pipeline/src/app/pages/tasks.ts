import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { ProjectTaskService, ProjectService, MilestoneService } from '../services';
import { ProjectTask } from '../models';

@Component({
  selector: 'app-tasks',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatChipsModule
  ],
  template: `
    <div class="tasks">
      <div class="tasks__header">
        <h1 class="tasks__title">Tasks</h1>
        <button mat-raised-button color="primary" (click)="createTask()" class="tasks__add-button">
          <mat-icon>add</mat-icon>
          New Task
        </button>
      </div>

      <div class="tasks__table-container">
        <table mat-table [dataSource]="(taskService.tasks$ | async) || []" class="tasks__table">
          <ng-container matColumnDef="title">
            <th mat-header-cell *matHeaderCellDef>Title</th>
            <td mat-cell *matCellDef="let task">{{ task.title }}</td>
          </ng-container>

          <ng-container matColumnDef="project">
            <th mat-header-cell *matHeaderCellDef>Project</th>
            <td mat-cell *matCellDef="let task">
              {{ getProjectName(task.projectId) }}
            </td>
          </ng-container>

          <ng-container matColumnDef="milestone">
            <th mat-header-cell *matHeaderCellDef>Milestone</th>
            <td mat-cell *matCellDef="let task">
              {{ task.milestoneId ? getMilestoneName(task.milestoneId) : '-' }}
            </td>
          </ng-container>

          <ng-container matColumnDef="dueDate">
            <th mat-header-cell *matHeaderCellDef>Due Date</th>
            <td mat-cell *matCellDef="let task">
              {{ task.dueDate ? (task.dueDate | date: 'mediumDate') : '-' }}
            </td>
          </ng-container>

          <ng-container matColumnDef="estimatedHours">
            <th mat-header-cell *matHeaderCellDef>Estimated Hours</th>
            <td mat-cell *matCellDef="let task">
              {{ task.estimatedHours ? task.estimatedHours + 'h' : '-' }}
            </td>
          </ng-container>

          <ng-container matColumnDef="status">
            <th mat-header-cell *matHeaderCellDef>Status</th>
            <td mat-cell *matCellDef="let task">
              @if (task.isCompleted) {
                <mat-chip class="tasks__chip--completed">Completed</mat-chip>
              } @else {
                <mat-chip class="tasks__chip--pending">Pending</mat-chip>
              }
            </td>
          </ng-container>

          <ng-container matColumnDef="completionDate">
            <th mat-header-cell *matHeaderCellDef>Completion Date</th>
            <td mat-cell *matCellDef="let task">
              {{ task.completionDate ? (task.completionDate | date: 'mediumDate') : '-' }}
            </td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let task">
              <button mat-icon-button (click)="editTask(task)" color="primary">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button (click)="deleteTask(task)" color="warn">
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
    }

    .tasks__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 2rem;
    }

    .tasks__title {
      margin: 0;
      font-size: 2rem;
      font-weight: 500;
    }

    .tasks__add-button {
      display: flex;
      align-items: center;
      gap: 0.5rem;
    }

    .tasks__table-container {
      overflow-x: auto;
      background: white;
      border-radius: 4px;
      box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
    }

    .tasks__table {
      width: 100%;
    }

    .tasks__chip--completed {
      background-color: #c8e6c9 !important;
    }

    .tasks__chip--pending {
      background-color: #fff9c4 !important;
    }
  `]
})
export class Tasks implements OnInit {
  taskService = inject(ProjectTaskService);
  projectService = inject(ProjectService);
  milestoneService = inject(MilestoneService);
  private router = inject(Router);

  displayedColumns: string[] = ['title', 'project', 'milestone', 'dueDate', 'estimatedHours', 'status', 'completionDate', 'actions'];

  ngOnInit(): void {
    this.taskService.getTasks().subscribe();
    this.projectService.getProjects().subscribe();
    this.milestoneService.getMilestones().subscribe();
  }

  getProjectName(projectId: string): string {
    const projects = this.projectService['projectsSubject'].value;
    const project = projects.find(p => p.projectId === projectId);
    return project?.name || 'Unknown';
  }

  getMilestoneName(milestoneId: string): string {
    const milestones = this.milestoneService['milestonesSubject'].value;
    const milestone = milestones.find(m => m.milestoneId === milestoneId);
    return milestone?.name || 'Unknown';
  }

  createTask(): void {
    this.router.navigate(['/tasks/new']);
  }

  editTask(task: ProjectTask): void {
    this.taskService.selectTask(task);
    this.router.navigate(['/tasks', task.projectTaskId]);
  }

  deleteTask(task: ProjectTask): void {
    if (confirm(`Are you sure you want to delete "${task.title}"?`)) {
      this.taskService.deleteTask(task.projectTaskId).subscribe();
    }
  }
}
