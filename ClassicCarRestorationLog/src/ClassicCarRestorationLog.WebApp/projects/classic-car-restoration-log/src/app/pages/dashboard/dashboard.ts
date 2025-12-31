import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { Observable } from 'rxjs';
import { ProjectService } from '../../services';
import { Project } from '../../models';
import { ProjectCard } from '../../components/project-card';
import { ProjectDialog, ProjectDialogData } from '../../components/dialogs';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatDialogModule,
    MatSnackBarModule,
    ProjectCard
  ],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard implements OnInit {
  projects$: Observable<Project[]>;
  userId = '00000000-0000-0000-0000-000000000001'; // Mock user ID

  constructor(
    private projectService: ProjectService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar,
    private router: Router
  ) {
    this.projects$ = this.projectService.projects$;
  }

  ngOnInit(): void {
    this.loadProjects();
  }

  loadProjects(): void {
    this.projectService.getProjects(this.userId).subscribe({
      error: (err) => {
        this.snackBar.open('Failed to load projects', 'Close', { duration: 3000 });
        console.error('Error loading projects:', err);
      }
    });
  }

  openProjectDialog(project?: Project): void {
    const dialogData: ProjectDialogData = {
      project,
      userId: this.userId
    };

    const dialogRef = this.dialog.open(ProjectDialog, {
      width: '600px',
      data: dialogData
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (project) {
          this.updateProject(project.projectId, result);
        } else {
          this.createProject(result);
        }
      }
    });
  }

  createProject(command: any): void {
    this.projectService.createProject(command).subscribe({
      next: () => {
        this.snackBar.open('Project created successfully', 'Close', { duration: 3000 });
      },
      error: (err) => {
        this.snackBar.open('Failed to create project', 'Close', { duration: 3000 });
        console.error('Error creating project:', err);
      }
    });
  }

  updateProject(id: string, command: any): void {
    this.projectService.updateProject(id, command).subscribe({
      next: () => {
        this.snackBar.open('Project updated successfully', 'Close', { duration: 3000 });
      },
      error: (err) => {
        this.snackBar.open('Failed to update project', 'Close', { duration: 3000 });
        console.error('Error updating project:', err);
      }
    });
  }

  deleteProject(project: Project): void {
    if (confirm(`Are you sure you want to delete ${project.year} ${project.carMake} ${project.carModel}?`)) {
      this.projectService.deleteProject(project.projectId).subscribe({
        next: () => {
          this.snackBar.open('Project deleted successfully', 'Close', { duration: 3000 });
        },
        error: (err) => {
          this.snackBar.open('Failed to delete project', 'Close', { duration: 3000 });
          console.error('Error deleting project:', err);
        }
      });
    }
  }

  viewProject(project: Project): void {
    this.router.navigate(['/projects', project.projectId]);
  }
}
