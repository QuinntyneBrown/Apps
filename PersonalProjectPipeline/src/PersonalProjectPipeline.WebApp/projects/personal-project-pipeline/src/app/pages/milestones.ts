import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MilestoneService, ProjectService } from '../services';
import { Milestone } from '../models';

@Component({
  selector: 'app-milestones',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatChipsModule
  ],
  template: `
    <div class="milestones">
      <div class="milestones__header">
        <h1 class="milestones__title">Milestones</h1>
        <button mat-raised-button color="primary" (click)="createMilestone()" class="milestones__add-button">
          <mat-icon>add</mat-icon>
          New Milestone
        </button>
      </div>

      <div class="milestones__table-container">
        <table mat-table [dataSource]="(milestoneService.milestones$ | async) || []" class="milestones__table">
          <ng-container matColumnDef="name">
            <th mat-header-cell *matHeaderCellDef>Name</th>
            <td mat-cell *matCellDef="let milestone">{{ milestone.name }}</td>
          </ng-container>

          <ng-container matColumnDef="project">
            <th mat-header-cell *matHeaderCellDef>Project</th>
            <td mat-cell *matCellDef="let milestone">
              {{ getProjectName(milestone.projectId) }}
            </td>
          </ng-container>

          <ng-container matColumnDef="targetDate">
            <th mat-header-cell *matHeaderCellDef>Target Date</th>
            <td mat-cell *matCellDef="let milestone">
              {{ milestone.targetDate ? (milestone.targetDate | date: 'mediumDate') : '-' }}
            </td>
          </ng-container>

          <ng-container matColumnDef="status">
            <th mat-header-cell *matHeaderCellDef>Status</th>
            <td mat-cell *matCellDef="let milestone">
              @if (milestone.isAchieved) {
                <mat-chip class="milestones__chip--achieved">Achieved</mat-chip>
              } @else if (milestone.isOverdue) {
                <mat-chip class="milestones__chip--overdue">Overdue</mat-chip>
              } @else {
                <mat-chip class="milestones__chip--pending">Pending</mat-chip>
              }
            </td>
          </ng-container>

          <ng-container matColumnDef="achievementDate">
            <th mat-header-cell *matHeaderCellDef>Achievement Date</th>
            <td mat-cell *matCellDef="let milestone">
              {{ milestone.achievementDate ? (milestone.achievementDate | date: 'mediumDate') : '-' }}
            </td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let milestone">
              <button mat-icon-button (click)="editMilestone(milestone)" color="primary">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button (click)="deleteMilestone(milestone)" color="warn">
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
    .milestones {
      padding: 2rem;
    }

    .milestones__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 2rem;
    }

    .milestones__title {
      margin: 0;
      font-size: 2rem;
      font-weight: 500;
    }

    .milestones__add-button {
      display: flex;
      align-items: center;
      gap: 0.5rem;
    }

    .milestones__table-container {
      overflow-x: auto;
      background: white;
      border-radius: 4px;
      box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
    }

    .milestones__table {
      width: 100%;
    }

    .milestones__chip--achieved {
      background-color: #c8e6c9 !important;
    }

    .milestones__chip--overdue {
      background-color: #ffcdd2 !important;
    }

    .milestones__chip--pending {
      background-color: #fff9c4 !important;
    }
  `]
})
export class Milestones implements OnInit {
  milestoneService = inject(MilestoneService);
  projectService = inject(ProjectService);
  private router = inject(Router);

  displayedColumns: string[] = ['name', 'project', 'targetDate', 'status', 'achievementDate', 'actions'];

  ngOnInit(): void {
    this.milestoneService.getMilestones().subscribe();
    this.projectService.getProjects().subscribe();
  }

  getProjectName(projectId: string): string {
    const projects = this.projectService['projectsSubject'].value;
    const project = projects.find(p => p.projectId === projectId);
    return project?.name || 'Unknown';
  }

  createMilestone(): void {
    this.router.navigate(['/milestones/new']);
  }

  editMilestone(milestone: Milestone): void {
    this.milestoneService.selectMilestone(milestone);
    this.router.navigate(['/milestones', milestone.milestoneId]);
  }

  deleteMilestone(milestone: Milestone): void {
    if (confirm(`Are you sure you want to delete "${milestone.name}"?`)) {
      this.milestoneService.deleteMilestone(milestone.milestoneId).subscribe();
    }
  }
}
