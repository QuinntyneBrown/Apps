import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { ProjectService } from '../services';
import { Project, ProjectStatusLabels, ProjectPriorityLabels } from '../models';

@Component({
  selector: 'app-projects',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatChipsModule,
    MatProgressBarModule
  ],
  template: `
    <div class="projects">
      <div class="projects__header">
        <h1 class="projects__title">Projects</h1>
        <button mat-raised-button color="primary" (click)="createProject()" class="projects__add-button">
          <mat-icon>add</mat-icon>
          New Project
        </button>
      </div>

      <div class="projects__table-container">
        <table mat-table [dataSource]="(projectService.projects$ | async) || []" class="projects__table">
          <ng-container matColumnDef="name">
            <th mat-header-cell *matHeaderCellDef>Name</th>
            <td mat-cell *matCellDef="let project">{{ project.name }}</td>
          </ng-container>

          <ng-container matColumnDef="status">
            <th mat-header-cell *matHeaderCellDef>Status</th>
            <td mat-cell *matCellDef="let project">
              <mat-chip [class]="'projects__chip--' + getStatusClass(project.status)">
                {{ getStatusLabel(project.status) }}
              </mat-chip>
            </td>
          </ng-container>

          <ng-container matColumnDef="priority">
            <th mat-header-cell *matHeaderCellDef>Priority</th>
            <td mat-cell *matCellDef="let project">
              <mat-chip [class]="'projects__chip--' + getPriorityClass(project.priority)">
                {{ getPriorityLabel(project.priority) }}
              </mat-chip>
            </td>
          </ng-container>

          <ng-container matColumnDef="progress">
            <th mat-header-cell *matHeaderCellDef>Progress</th>
            <td mat-cell *matCellDef="let project">
              <div class="projects__progress">
                <mat-progress-bar mode="determinate" [value]="project.progressPercentage"></mat-progress-bar>
                <span class="projects__progress-text">{{ project.progressPercentage }}%</span>
              </div>
            </td>
          </ng-container>

          <ng-container matColumnDef="targetDate">
            <th mat-header-cell *matHeaderCellDef>Target Date</th>
            <td mat-cell *matCellDef="let project">
              {{ project.targetDate ? (project.targetDate | date: 'mediumDate') : '-' }}
            </td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let project">
              <button mat-icon-button (click)="editProject(project)" color="primary">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button (click)="deleteProject(project)" color="warn">
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
    .projects {
      padding: 2rem;
    }

    .projects__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 2rem;
    }

    .projects__title {
      margin: 0;
      font-size: 2rem;
      font-weight: 500;
    }

    .projects__add-button {
      display: flex;
      align-items: center;
      gap: 0.5rem;
    }

    .projects__table-container {
      overflow-x: auto;
      background: white;
      border-radius: 4px;
      box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
    }

    .projects__table {
      width: 100%;
    }

    .projects__chip--idea {
      background-color: #e3f2fd !important;
    }

    .projects__chip--planned {
      background-color: #fff3e0 !important;
    }

    .projects__chip--inprogress {
      background-color: #e8f5e9 !important;
    }

    .projects__chip--onhold {
      background-color: #fff9c4 !important;
    }

    .projects__chip--completed {
      background-color: #c8e6c9 !important;
    }

    .projects__chip--cancelled {
      background-color: #ffebee !important;
    }

    .projects__chip--low {
      background-color: #e0e0e0 !important;
    }

    .projects__chip--medium {
      background-color: #fff9c4 !important;
    }

    .projects__chip--high {
      background-color: #ffcc80 !important;
    }

    .projects__chip--critical {
      background-color: #ef9a9a !important;
    }

    .projects__progress {
      display: flex;
      align-items: center;
      gap: 0.5rem;
      min-width: 150px;
    }

    .projects__progress-text {
      min-width: 40px;
      font-size: 0.875rem;
    }
  `]
})
export class Projects implements OnInit {
  projectService = inject(ProjectService);
  private router = inject(Router);

  displayedColumns: string[] = ['name', 'status', 'priority', 'progress', 'targetDate', 'actions'];

  ngOnInit(): void {
    this.projectService.getProjects().subscribe();
  }

  getStatusLabel(status: number): string {
    return ProjectStatusLabels[status] || '';
  }

  getStatusClass(status: number): string {
    return ProjectStatusLabels[status]?.toLowerCase().replace(/\s+/g, '') || '';
  }

  getPriorityLabel(priority: number): string {
    return ProjectPriorityLabels[priority] || '';
  }

  getPriorityClass(priority: number): string {
    return ProjectPriorityLabels[priority]?.toLowerCase() || '';
  }

  createProject(): void {
    this.router.navigate(['/projects/new']);
  }

  editProject(project: Project): void {
    this.projectService.selectProject(project);
    this.router.navigate(['/projects', project.projectId]);
  }

  deleteProject(project: Project): void {
    if (confirm(`Are you sure you want to delete "${project.name}"?`)) {
      this.projectService.deleteProject(project.projectId).subscribe();
    }
  }
}
