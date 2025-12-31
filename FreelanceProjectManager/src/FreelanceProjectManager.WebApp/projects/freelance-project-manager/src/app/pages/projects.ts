import { Component, OnInit, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule, MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatCardModule } from '@angular/material/card';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { ProjectService, ClientService } from '../services';
import { Project, ProjectStatus, Client } from '../models';

@Component({
  selector: 'app-projects',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatCardModule,
    MatSnackBarModule
  ],
  template: `
    <div class="projects">
      <div class="projects__header">
        <h1 class="projects__title">Projects</h1>
        <button mat-raised-button color="primary" (click)="openDialog()" class="projects__add-btn">
          <mat-icon>add</mat-icon>
          Add Project
        </button>
      </div>

      <mat-card class="projects__card">
        <table mat-table [dataSource]="projects$ | async" class="projects__table">
          <ng-container matColumnDef="name">
            <th mat-header-cell *matHeaderCellDef>Name</th>
            <td mat-cell *matCellDef="let project">{{ project.name }}</td>
          </ng-container>

          <ng-container matColumnDef="description">
            <th mat-header-cell *matHeaderCellDef>Description</th>
            <td mat-cell *matCellDef="let project">{{ project.description }}</td>
          </ng-container>

          <ng-container matColumnDef="status">
            <th mat-header-cell *matHeaderCellDef>Status</th>
            <td mat-cell *matCellDef="let project">{{ getStatusLabel(project.status) }}</td>
          </ng-container>

          <ng-container matColumnDef="startDate">
            <th mat-header-cell *matHeaderCellDef>Start Date</th>
            <td mat-cell *matCellDef="let project">{{ project.startDate | date:'shortDate' }}</td>
          </ng-container>

          <ng-container matColumnDef="dueDate">
            <th mat-header-cell *matHeaderCellDef>Due Date</th>
            <td mat-cell *matCellDef="let project">{{ project.dueDate ? (project.dueDate | date:'shortDate') : '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let project">
              <button mat-icon-button color="primary" (click)="openDialog(project)">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" (click)="deleteProject(project)">
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
      gap: 0.5rem;
    }

    .projects__card {
      padding: 0;
    }

    .projects__table {
      width: 100%;
    }
  `]
})
export class Projects implements OnInit {
  displayedColumns: string[] = ['name', 'description', 'status', 'startDate', 'dueDate', 'actions'];
  projects$ = this.projectService.projects$;
  private userId = '00000000-0000-0000-0000-000000000000';

  constructor(
    private projectService: ProjectService,
    private clientService: ClientService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.loadProjects();
    this.loadClients();
  }

  loadProjects(): void {
    this.projectService.getProjects(this.userId).subscribe();
  }

  loadClients(): void {
    this.clientService.getClients(this.userId).subscribe();
  }

  getStatusLabel(status: ProjectStatus): string {
    const labels: { [key in ProjectStatus]: string } = {
      [ProjectStatus.Planning]: 'Planning',
      [ProjectStatus.InProgress]: 'In Progress',
      [ProjectStatus.OnHold]: 'On Hold',
      [ProjectStatus.UnderReview]: 'Under Review',
      [ProjectStatus.Completed]: 'Completed',
      [ProjectStatus.Cancelled]: 'Cancelled'
    };
    return labels[status];
  }

  openDialog(project?: Project): void {
    const dialogRef = this.dialog.open(ProjectDialog, {
      width: '700px',
      data: project
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

  createProject(data: any): void {
    this.projectService.createProject({ ...data, userId: this.userId }).subscribe({
      next: () => {
        this.snackBar.open('Project created successfully', 'Close', { duration: 3000 });
      },
      error: () => {
        this.snackBar.open('Error creating project', 'Close', { duration: 3000 });
      }
    });
  }

  updateProject(id: string, data: any): void {
    this.projectService.updateProject(id, { ...data, projectId: id, userId: this.userId }).subscribe({
      next: () => {
        this.snackBar.open('Project updated successfully', 'Close', { duration: 3000 });
      },
      error: () => {
        this.snackBar.open('Error updating project', 'Close', { duration: 3000 });
      }
    });
  }

  deleteProject(project: Project): void {
    if (confirm(`Are you sure you want to delete ${project.name}?`)) {
      this.projectService.deleteProject(project.projectId, this.userId).subscribe({
        next: () => {
          this.snackBar.open('Project deleted successfully', 'Close', { duration: 3000 });
        },
        error: () => {
          this.snackBar.open('Error deleting project', 'Close', { duration: 3000 });
        }
      });
    }
  }
}

@Component({
  selector: 'app-project-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatButtonModule
  ],
  template: `
    <h2 mat-dialog-title>{{ data ? 'Edit Project' : 'Add Project' }}</h2>
    <mat-dialog-content>
      <form [formGroup]="form" class="project-dialog__form">
        <mat-form-field appearance="outline" class="project-dialog__field">
          <mat-label>Client</mat-label>
          <mat-select formControlName="clientId" required>
            <mat-option *ngFor="let client of clients$ | async" [value]="client.clientId">
              {{ client.name }}
            </mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field appearance="outline" class="project-dialog__field">
          <mat-label>Name</mat-label>
          <input matInput formControlName="name" required>
        </mat-form-field>

        <mat-form-field appearance="outline" class="project-dialog__field">
          <mat-label>Description</mat-label>
          <textarea matInput formControlName="description" rows="3" required></textarea>
        </mat-form-field>

        <mat-form-field appearance="outline" class="project-dialog__field">
          <mat-label>Status</mat-label>
          <mat-select formControlName="status" required>
            <mat-option [value]="0">Planning</mat-option>
            <mat-option [value]="1">In Progress</mat-option>
            <mat-option [value]="2">On Hold</mat-option>
            <mat-option [value]="3">Under Review</mat-option>
            <mat-option [value]="4">Completed</mat-option>
            <mat-option [value]="5">Cancelled</mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field appearance="outline" class="project-dialog__field">
          <mat-label>Start Date</mat-label>
          <input matInput [matDatepicker]="startPicker" formControlName="startDate" required>
          <mat-datepicker-toggle matSuffix [for]="startPicker"></mat-datepicker-toggle>
          <mat-datepicker #startPicker></mat-datepicker>
        </mat-form-field>

        <mat-form-field appearance="outline" class="project-dialog__field">
          <mat-label>Due Date</mat-label>
          <input matInput [matDatepicker]="duePicker" formControlName="dueDate">
          <mat-datepicker-toggle matSuffix [for]="duePicker"></mat-datepicker-toggle>
          <mat-datepicker #duePicker></mat-datepicker>
        </mat-form-field>

        <mat-form-field appearance="outline" class="project-dialog__field" *ngIf="data">
          <mat-label>Completion Date</mat-label>
          <input matInput [matDatepicker]="completionPicker" formControlName="completionDate">
          <mat-datepicker-toggle matSuffix [for]="completionPicker"></mat-datepicker-toggle>
          <mat-datepicker #completionPicker></mat-datepicker>
        </mat-form-field>

        <mat-form-field appearance="outline" class="project-dialog__field">
          <mat-label>Hourly Rate</mat-label>
          <input matInput type="number" formControlName="hourlyRate">
        </mat-form-field>

        <mat-form-field appearance="outline" class="project-dialog__field">
          <mat-label>Fixed Budget</mat-label>
          <input matInput type="number" formControlName="fixedBudget">
        </mat-form-field>

        <mat-form-field appearance="outline" class="project-dialog__field">
          <mat-label>Currency</mat-label>
          <input matInput formControlName="currency">
        </mat-form-field>

        <mat-form-field appearance="outline" class="project-dialog__field">
          <mat-label>Notes</mat-label>
          <textarea matInput formControlName="notes" rows="3"></textarea>
        </mat-form-field>
      </form>
    </mat-dialog-content>
    <mat-dialog-actions align="end">
      <button mat-button mat-dialog-close>Cancel</button>
      <button mat-raised-button color="primary" [disabled]="!form.valid" (click)="save()">Save</button>
    </mat-dialog-actions>
  `,
  styles: [`
    .project-dialog__form {
      display: flex;
      flex-direction: column;
      gap: 1rem;
      min-width: 600px;
    }

    .project-dialog__field {
      width: 100%;
    }
  `]
})
export class ProjectDialog {
  form: FormGroup;
  clients$ = this.clientService.clients$;

  constructor(
    private fb: FormBuilder,
    private clientService: ClientService,
    public dialogRef: MatDialogRef<ProjectDialog>,
    @Inject(MAT_DIALOG_DATA) public data: Project
  ) {
    this.form = this.fb.group({
      clientId: [data?.clientId || '', Validators.required],
      name: [data?.name || '', Validators.required],
      description: [data?.description || '', Validators.required],
      status: [data?.status ?? ProjectStatus.Planning, Validators.required],
      startDate: [data?.startDate || new Date(), Validators.required],
      dueDate: [data?.dueDate || null],
      completionDate: [data?.completionDate || null],
      hourlyRate: [data?.hourlyRate || null],
      fixedBudget: [data?.fixedBudget || null],
      currency: [data?.currency || 'USD'],
      notes: [data?.notes || '']
    });
  }

  save(): void {
    if (this.form.valid) {
      this.dialogRef.close(this.form.value);
    }
  }
}
