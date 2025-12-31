import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { ProjectService, MilestoneService, ProjectTaskService } from '../services';
import { Project, Milestone, ProjectTask, ProjectStatus } from '../models';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink, MatCardModule, MatButtonModule, MatIconModule],
  template: `
    <div class="dashboard">
      <h1 class="dashboard__title">Dashboard</h1>

      <div class="dashboard__cards">
        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-card-title class="dashboard__card-title">
              <mat-icon class="dashboard__card-icon">folder</mat-icon>
              Projects
            </mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__stats">
              <div class="dashboard__stat">
                <div class="dashboard__stat-value">{{ (projectService.projects$ | async)?.length || 0 }}</div>
                <div class="dashboard__stat-label">Total</div>
              </div>
              <div class="dashboard__stat">
                <div class="dashboard__stat-value">{{ getActiveProjectsCount() }}</div>
                <div class="dashboard__stat-label">In Progress</div>
              </div>
              <div class="dashboard__stat">
                <div class="dashboard__stat-value">{{ getCompletedProjectsCount() }}</div>
                <div class="dashboard__stat-label">Completed</div>
              </div>
            </div>
          </mat-card-content>
          <mat-card-actions>
            <button mat-raised-button color="primary" routerLink="/projects">
              View Projects
            </button>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-card-title class="dashboard__card-title">
              <mat-icon class="dashboard__card-icon">flag</mat-icon>
              Milestones
            </mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__stats">
              <div class="dashboard__stat">
                <div class="dashboard__stat-value">{{ (milestoneService.milestones$ | async)?.length || 0 }}</div>
                <div class="dashboard__stat-label">Total</div>
              </div>
              <div class="dashboard__stat">
                <div class="dashboard__stat-value">{{ getAchievedMilestonesCount() }}</div>
                <div class="dashboard__stat-label">Achieved</div>
              </div>
              <div class="dashboard__stat">
                <div class="dashboard__stat-value">{{ getOverdueMilestonesCount() }}</div>
                <div class="dashboard__stat-label">Overdue</div>
              </div>
            </div>
          </mat-card-content>
          <mat-card-actions>
            <button mat-raised-button color="primary" routerLink="/milestones">
              View Milestones
            </button>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-card-title class="dashboard__card-title">
              <mat-icon class="dashboard__card-icon">task</mat-icon>
              Tasks
            </mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__stats">
              <div class="dashboard__stat">
                <div class="dashboard__stat-value">{{ (taskService.tasks$ | async)?.length || 0 }}</div>
                <div class="dashboard__stat-label">Total</div>
              </div>
              <div class="dashboard__stat">
                <div class="dashboard__stat-value">{{ getPendingTasksCount() }}</div>
                <div class="dashboard__stat-label">Pending</div>
              </div>
              <div class="dashboard__stat">
                <div class="dashboard__stat-value">{{ getCompletedTasksCount() }}</div>
                <div class="dashboard__stat-label">Completed</div>
              </div>
            </div>
          </mat-card-content>
          <mat-card-actions>
            <button mat-raised-button color="primary" routerLink="/tasks">
              View Tasks
            </button>
          </mat-card-actions>
        </mat-card>
      </div>
    </div>
  `,
  styles: [`
    .dashboard {
      padding: 2rem;
    }

    .dashboard__title {
      margin: 0 0 2rem 0;
      font-size: 2rem;
      font-weight: 500;
    }

    .dashboard__cards {
      display: grid;
      grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
      gap: 1.5rem;
    }

    .dashboard__card {
      display: flex;
      flex-direction: column;
    }

    .dashboard__card-title {
      display: flex;
      align-items: center;
      gap: 0.5rem;
    }

    .dashboard__card-icon {
      color: #1976d2;
    }

    .dashboard__stats {
      display: flex;
      justify-content: space-around;
      padding: 1rem 0;
    }

    .dashboard__stat {
      text-align: center;
    }

    .dashboard__stat-value {
      font-size: 2rem;
      font-weight: bold;
      color: #1976d2;
    }

    .dashboard__stat-label {
      font-size: 0.875rem;
      color: #666;
      margin-top: 0.25rem;
    }
  `]
})
export class Dashboard implements OnInit {
  projectService = inject(ProjectService);
  milestoneService = inject(MilestoneService);
  taskService = inject(ProjectTaskService);

  ngOnInit(): void {
    this.projectService.getProjects().subscribe();
    this.milestoneService.getMilestones().subscribe();
    this.taskService.getTasks().subscribe();
  }

  getActiveProjectsCount(): number {
    const projects = this.projectService['projectsSubject'].value;
    return projects.filter(p => p.status === ProjectStatus.InProgress).length;
  }

  getCompletedProjectsCount(): number {
    const projects = this.projectService['projectsSubject'].value;
    return projects.filter(p => p.status === ProjectStatus.Completed).length;
  }

  getAchievedMilestonesCount(): number {
    const milestones = this.milestoneService['milestonesSubject'].value;
    return milestones.filter(m => m.isAchieved).length;
  }

  getOverdueMilestonesCount(): number {
    const milestones = this.milestoneService['milestonesSubject'].value;
    return milestones.filter(m => m.isOverdue && !m.isAchieved).length;
  }

  getPendingTasksCount(): number {
    const tasks = this.taskService['tasksSubject'].value;
    return tasks.filter(t => !t.isCompleted).length;
  }

  getCompletedTasksCount(): number {
    const tasks = this.taskService['tasksSubject'].value;
    return tasks.filter(t => t.isCompleted).length;
  }
}
