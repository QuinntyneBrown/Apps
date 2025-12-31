import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { ProjectService } from '../../services';
import { ProjectDialog } from '../../components/project-dialog/project-dialog';
import { Project } from '../../models';

@Component({
  selector: 'app-projects',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatChipsModule,
    MatDialogModule
  ],
  templateUrl: './projects.html',
  styleUrl: './projects.scss'
})
export class Projects implements OnInit {
  private _projectService = inject(ProjectService);
  private _dialog = inject(MatDialog);

  projects$ = this._projectService.projects$;
  displayedColumns = ['name', 'description', 'status', 'woodType', 'estimatedCost', 'actualCost', 'actions'];

  ngOnInit(): void {
    this._projectService.getProjects().subscribe();
  }

  openCreateDialog(): void {
    const dialogRef = this._dialog.open(ProjectDialog, {
      width: '600px'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._projectService.createProject(result).subscribe();
      }
    });
  }

  openEditDialog(project: Project): void {
    const dialogRef = this._dialog.open(ProjectDialog, {
      width: '600px',
      data: project
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._projectService.updateProject(project.projectId, result).subscribe();
      }
    });
  }

  deleteProject(id: string): void {
    if (confirm('Are you sure you want to delete this project?')) {
      this._projectService.deleteProject(id).subscribe();
    }
  }

  getStatusColor(status: string): string {
    const statusColors: { [key: string]: string } = {
      'Planned': 'primary',
      'InProgress': 'accent',
      'OnHold': 'warn',
      'Completed': '',
      'Abandoned': ''
    };
    return statusColors[status] || '';
  }
}
