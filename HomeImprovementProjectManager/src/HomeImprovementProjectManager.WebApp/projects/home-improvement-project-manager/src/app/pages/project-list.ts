import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { ProjectService } from '../services';
import { Project, ProjectStatus } from '../models';

@Component({
  selector: 'app-project-list',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule, MatChipsModule],
  templateUrl: './project-list.html',
  styleUrl: './project-list.scss'
})
export class ProjectList implements OnInit {
  private projectService = inject(ProjectService);
  private router = inject(Router);

  projects$ = this.projectService.projects$;
  ProjectStatus = ProjectStatus;

  ngOnInit() {
    // For demo purposes, using a hardcoded userId
    const userId = '00000000-0000-0000-0000-000000000001';
    this.projectService.getByUserId(userId).subscribe();
  }

  viewProject(projectId: string) {
    this.router.navigate(['/projects', projectId]);
  }

  getStatusLabel(status: ProjectStatus): string {
    switch (status) {
      case ProjectStatus.Planning: return 'Planning';
      case ProjectStatus.InProgress: return 'In Progress';
      case ProjectStatus.Completed: return 'Completed';
      case ProjectStatus.OnHold: return 'On Hold';
      case ProjectStatus.Cancelled: return 'Cancelled';
      default: return 'Unknown';
    }
  }

  getStatusColor(status: ProjectStatus): string {
    switch (status) {
      case ProjectStatus.Planning: return 'accent';
      case ProjectStatus.InProgress: return 'primary';
      case ProjectStatus.Completed: return 'primary';
      case ProjectStatus.OnHold: return 'warn';
      case ProjectStatus.Cancelled: return '';
      default: return '';
    }
  }
}
