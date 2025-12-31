import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { ProjectService } from '../services';

@Component({
  selector: 'app-projects-list',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatChipsModule,
    MatTooltipModule,
    MatProgressSpinnerModule
  ],
  template: `
    <div class="projects-list">
      <div class="projects-list__header">
        <div>
          <h1 class="projects-list__title">Projects</h1>
          <p class="projects-list__subtitle">Manage your professional projects and portfolios</p>
        </div>
        <a mat-raised-button color="primary" routerLink="/projects/new" class="projects-list__add-btn">
          <mat-icon>add</mat-icon>
          Add Project
        </a>
      </div>

      <div class="projects-list__content">
        @if (loading$ | async) {
          <div class="projects-list__loading">
            <mat-spinner></mat-spinner>
          </div>
        } @else if ((projects$ | async)?.length === 0) {
          <div class="projects-list__empty">
            <mat-icon class="projects-list__empty-icon">work</mat-icon>
            <h2>No projects yet</h2>
            <p>Start documenting your professional projects</p>
            <a mat-raised-button color="primary" routerLink="/projects/new">
              Add Your First Project
            </a>
          </div>
        } @else {
          <table mat-table [dataSource]="projects$ | async" class="projects-list__table">
            <ng-container matColumnDef="name">
              <th mat-header-cell *matHeaderCellDef>Name</th>
              <td mat-cell *matCellDef="let project">
                <div class="projects-list__cell-title">
                  {{ project.name }}
                  @if (project.isFeatured) {
                    <mat-icon class="projects-list__featured-icon" matTooltip="Featured">star</mat-icon>
                  }
                </div>
              </td>
            </ng-container>

            <ng-container matColumnDef="role">
              <th mat-header-cell *matHeaderCellDef>Role</th>
              <td mat-cell *matCellDef="let project">
                {{ project.role || '-' }}
              </td>
            </ng-container>

            <ng-container matColumnDef="organization">
              <th mat-header-cell *matHeaderCellDef>Organization</th>
              <td mat-cell *matCellDef="let project">
                {{ project.organization || '-' }}
              </td>
            </ng-container>

            <ng-container matColumnDef="period">
              <th mat-header-cell *matHeaderCellDef>Period</th>
              <td mat-cell *matCellDef="let project">
                {{ project.startDate | date:'MMM y' }} - {{ project.endDate ? (project.endDate | date:'MMM y') : 'Present' }}
              </td>
            </ng-container>

            <ng-container matColumnDef="technologies">
              <th mat-header-cell *matHeaderCellDef>Technologies</th>
              <td mat-cell *matCellDef="let project">
                @if (project.technologies?.length > 0) {
                  <div class="projects-list__technologies">
                    @for (tech of project.technologies.slice(0, 2); track tech) {
                      <mat-chip class="projects-list__tech">{{ tech }}</mat-chip>
                    }
                    @if (project.technologies.length > 2) {
                      <span class="projects-list__tech-more">+{{ project.technologies.length - 2 }}</span>
                    }
                  </div>
                } @else {
                  <span>-</span>
                }
              </td>
            </ng-container>

            <ng-container matColumnDef="actions">
              <th mat-header-cell *matHeaderCellDef>Actions</th>
              <td mat-cell *matCellDef="let project">
                <div class="projects-list__actions">
                  <button
                    mat-icon-button
                    [matTooltip]="project.isFeatured ? 'Unfeature' : 'Feature'"
                    (click)="toggleFeatured(project.projectId)">
                    <mat-icon [class.projects-list__action-featured]="project.isFeatured">
                      {{ project.isFeatured ? 'star' : 'star_border' }}
                    </mat-icon>
                  </button>
                  <a
                    mat-icon-button
                    [routerLink]="['/projects', project.projectId]"
                    matTooltip="Edit">
                    <mat-icon>edit</mat-icon>
                  </a>
                  <button
                    mat-icon-button
                    color="warn"
                    matTooltip="Delete"
                    (click)="deleteProject(project.projectId)">
                    <mat-icon>delete</mat-icon>
                  </button>
                </div>
              </td>
            </ng-container>

            <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
            <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
          </table>
        }
      </div>
    </div>
  `,
  styles: [`
    .projects-list {
      padding: 2rem;
      max-width: 1400px;
      margin: 0 auto;
    }

    .projects-list__header {
      display: flex;
      justify-content: space-between;
      align-items: flex-start;
      margin-bottom: 2rem;
      gap: 1rem;
    }

    .projects-list__title {
      font-size: 2rem;
      font-weight: 500;
      margin: 0 0 0.5rem 0;
      color: rgba(0, 0, 0, 0.87);
    }

    .projects-list__subtitle {
      font-size: 1rem;
      margin: 0;
      color: rgba(0, 0, 0, 0.6);
    }

    .projects-list__add-btn {
      display: flex;
      align-items: center;
      gap: 0.5rem;
    }

    .projects-list__content {
      background: white;
      border-radius: 4px;
      box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
    }

    .projects-list__loading {
      display: flex;
      justify-content: center;
      align-items: center;
      padding: 4rem;
    }

    .projects-list__empty {
      display: flex;
      flex-direction: column;
      align-items: center;
      justify-content: center;
      padding: 4rem 2rem;
      text-align: center;
    }

    .projects-list__empty-icon {
      width: 80px;
      height: 80px;
      font-size: 80px;
      color: rgba(0, 0, 0, 0.26);
      margin-bottom: 1rem;
    }

    .projects-list__table {
      width: 100%;
    }

    .projects-list__cell-title {
      display: flex;
      align-items: center;
      gap: 0.5rem;
    }

    .projects-list__featured-icon {
      width: 20px;
      height: 20px;
      font-size: 20px;
      color: #ffd700;
    }

    .projects-list__technologies {
      display: flex;
      align-items: center;
      gap: 0.25rem;
      flex-wrap: wrap;
    }

    .projects-list__tech {
      font-size: 0.75rem;
    }

    .projects-list__tech-more {
      font-size: 0.75rem;
      color: rgba(0, 0, 0, 0.6);
    }

    .projects-list__actions {
      display: flex;
      gap: 0.25rem;
    }

    .projects-list__action-featured {
      color: #ffd700;
    }

    @media (max-width: 768px) {
      .projects-list {
        padding: 1rem;
      }

      .projects-list__header {
        flex-direction: column;
      }

      .projects-list__add-btn {
        width: 100%;
      }
    }
  `]
})
export class ProjectsList implements OnInit {
  private projectService = inject(ProjectService);

  projects$ = this.projectService.projects$;
  loading$ = this.projectService.loading$;

  displayedColumns = ['name', 'role', 'organization', 'period', 'technologies', 'actions'];

  ngOnInit(): void {
    this.projectService.getProjects().subscribe();
  }

  toggleFeatured(id: string): void {
    this.projectService.toggleFeatured(id).subscribe();
  }

  deleteProject(id: string): void {
    if (confirm('Are you sure you want to delete this project?')) {
      this.projectService.deleteProject(id).subscribe();
    }
  }
}
