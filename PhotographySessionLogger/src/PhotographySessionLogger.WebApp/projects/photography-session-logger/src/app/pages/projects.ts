import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { ProjectService } from '../services';

@Component({
  selector: 'app-projects',
  standalone: true,
  imports: [CommonModule, RouterLink, MatTableModule, MatButtonModule, MatIconModule, MatCardModule, MatChipsModule],
  template: `
    <div class="projects">
      <div class="projects__header">
        <h1 class="projects__title">Photography Projects</h1>
        <a mat-raised-button color="primary" routerLink="/projects/new" class="projects__add-btn">
          <mat-icon>add</mat-icon>
          New Project
        </a>
      </div>

      <mat-card class="projects__card">
        <mat-card-content>
          <table mat-table [dataSource]="(projects$ | async) || []" class="projects__table">
            <ng-container matColumnDef="name">
              <th mat-header-cell *matHeaderCellDef>Name</th>
              <td mat-cell *matCellDef="let project">{{ project.name }}</td>
            </ng-container>

            <ng-container matColumnDef="description">
              <th mat-header-cell *matHeaderCellDef>Description</th>
              <td mat-cell *matCellDef="let project">{{ project.description || '-' }}</td>
            </ng-container>

            <ng-container matColumnDef="dueDate">
              <th mat-header-cell *matHeaderCellDef>Due Date</th>
              <td mat-cell *matCellDef="let project">{{ project.dueDate ? (project.dueDate | date:'shortDate') : '-' }}</td>
            </ng-container>

            <ng-container matColumnDef="isCompleted">
              <th mat-header-cell *matHeaderCellDef>Status</th>
              <td mat-cell *matCellDef="let project">
                <mat-chip [class.projects__chip--completed]="project.isCompleted">
                  {{ project.isCompleted ? 'Completed' : 'In Progress' }}
                </mat-chip>
              </td>
            </ng-container>

            <ng-container matColumnDef="actions">
              <th mat-header-cell *matHeaderCellDef>Actions</th>
              <td mat-cell *matCellDef="let project">
                <button mat-icon-button color="primary" [routerLink]="['/projects', project.projectId]">
                  <mat-icon>edit</mat-icon>
                </button>
                <button mat-icon-button color="warn" (click)="deleteProject(project.projectId)">
                  <mat-icon>delete</mat-icon>
                </button>
              </td>
            </ng-container>

            <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
            <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
          </table>
        </mat-card-content>
      </mat-card>
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

    .projects__add-btn {
      display: flex;
      align-items: center;
      gap: 0.5rem;
    }

    .projects__card {
      width: 100%;
    }

    .projects__table {
      width: 100%;
    }

    .projects__chip--completed {
      background-color: #4caf50 !important;
      color: white !important;
    }
  `]
})
export class Projects implements OnInit {
  private readonly projectService = inject(ProjectService);

  projects$ = this.projectService.projects$;
  displayedColumns = ['name', 'description', 'dueDate', 'isCompleted', 'actions'];

  ngOnInit(): void {
    this.projectService.getAll().subscribe();
  }

  deleteProject(id: string): void {
    if (confirm('Are you sure you want to delete this project?')) {
      this.projectService.delete(id).subscribe();
    }
  }
}
