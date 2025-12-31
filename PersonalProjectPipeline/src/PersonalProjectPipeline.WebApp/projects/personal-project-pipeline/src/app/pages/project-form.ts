import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { ProjectService } from '../services';
import { ProjectStatus, ProjectPriority, ProjectStatusLabels, ProjectPriorityLabels } from '../models';

@Component({
  selector: 'app-project-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  template: `
    <div class="project-form">
      <mat-card class="project-form__card">
        <mat-card-header>
          <mat-card-title>{{ isEditMode ? 'Edit Project' : 'New Project' }}</mat-card-title>
        </mat-card-header>
        <mat-card-content>
          <form [formGroup]="form" class="project-form__form">
            <mat-form-field class="project-form__field">
              <mat-label>Name</mat-label>
              <input matInput formControlName="name" required>
              @if (form.get('name')?.hasError('required') && form.get('name')?.touched) {
                <mat-error>Name is required</mat-error>
              }
            </mat-form-field>

            <mat-form-field class="project-form__field">
              <mat-label>Description</mat-label>
              <textarea matInput formControlName="description" rows="4"></textarea>
            </mat-form-field>

            <mat-form-field class="project-form__field">
              <mat-label>Status</mat-label>
              <mat-select formControlName="status" required>
                @for (status of statuses; track status.value) {
                  <mat-option [value]="status.value">{{ status.label }}</mat-option>
                }
              </mat-select>
            </mat-form-field>

            <mat-form-field class="project-form__field">
              <mat-label>Priority</mat-label>
              <mat-select formControlName="priority" required>
                @for (priority of priorities; track priority.value) {
                  <mat-option [value]="priority.value">{{ priority.label }}</mat-option>
                }
              </mat-select>
            </mat-form-field>

            <mat-form-field class="project-form__field">
              <mat-label>Start Date</mat-label>
              <input matInput [matDatepicker]="startPicker" formControlName="startDate">
              <mat-datepicker-toggle matIconSuffix [for]="startPicker"></mat-datepicker-toggle>
              <mat-datepicker #startPicker></mat-datepicker>
            </mat-form-field>

            <mat-form-field class="project-form__field">
              <mat-label>Target Date</mat-label>
              <input matInput [matDatepicker]="targetPicker" formControlName="targetDate">
              <mat-datepicker-toggle matIconSuffix [for]="targetPicker"></mat-datepicker-toggle>
              <mat-datepicker #targetPicker></mat-datepicker>
            </mat-form-field>

            <mat-form-field class="project-form__field">
              <mat-label>Tags</mat-label>
              <input matInput formControlName="tags" placeholder="comma, separated, tags">
            </mat-form-field>
          </form>
        </mat-card-content>
        <mat-card-actions class="project-form__actions">
          <button mat-button (click)="cancel()">Cancel</button>
          <button mat-raised-button color="primary" (click)="save()" [disabled]="form.invalid">
            {{ isEditMode ? 'Update' : 'Create' }}
          </button>
        </mat-card-actions>
      </mat-card>
    </div>
  `,
  styles: [`
    .project-form {
      padding: 2rem;
      display: flex;
      justify-content: center;
    }

    .project-form__card {
      width: 100%;
      max-width: 600px;
    }

    .project-form__form {
      display: flex;
      flex-direction: column;
      gap: 1rem;
      padding-top: 1rem;
    }

    .project-form__field {
      width: 100%;
    }

    .project-form__actions {
      display: flex;
      justify-content: flex-end;
      gap: 0.5rem;
      padding: 1rem;
    }
  `]
})
export class ProjectForm implements OnInit {
  private fb = inject(FormBuilder);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  private projectService = inject(ProjectService);

  form!: FormGroup;
  isEditMode = false;
  projectId: string | null = null;

  statuses = Object.keys(ProjectStatus)
    .filter(key => isNaN(Number(key)))
    .map(key => ({
      value: ProjectStatus[key as keyof typeof ProjectStatus],
      label: ProjectStatusLabels[ProjectStatus[key as keyof typeof ProjectStatus]]
    }));

  priorities = Object.keys(ProjectPriority)
    .filter(key => isNaN(Number(key)))
    .map(key => ({
      value: ProjectPriority[key as keyof typeof ProjectPriority],
      label: ProjectPriorityLabels[ProjectPriority[key as keyof typeof ProjectPriority]]
    }));

  ngOnInit(): void {
    this.initForm();

    this.projectId = this.route.snapshot.paramMap.get('id');
    this.isEditMode = this.projectId !== null && this.projectId !== 'new';

    if (this.isEditMode && this.projectId) {
      this.projectService.getProjectById(this.projectId).subscribe(project => {
        this.form.patchValue({
          name: project.name,
          description: project.description,
          status: project.status,
          priority: project.priority,
          startDate: project.startDate ? new Date(project.startDate) : null,
          targetDate: project.targetDate ? new Date(project.targetDate) : null,
          tags: project.tags
        });
      });
    }
  }

  initForm(): void {
    this.form = this.fb.group({
      name: ['', Validators.required],
      description: [''],
      status: [ProjectStatus.Idea, Validators.required],
      priority: [ProjectPriority.Medium, Validators.required],
      startDate: [null],
      targetDate: [null],
      tags: ['']
    });
  }

  save(): void {
    if (this.form.invalid) return;

    const formValue = this.form.value;
    const data = {
      ...formValue,
      startDate: formValue.startDate ? formValue.startDate.toISOString() : null,
      targetDate: formValue.targetDate ? formValue.targetDate.toISOString() : null
    };

    if (this.isEditMode && this.projectId) {
      this.projectService.updateProject({
        projectId: this.projectId,
        ...data
      }).subscribe(() => {
        this.router.navigate(['/projects']);
      });
    } else {
      this.projectService.createProject({
        userId: '00000000-0000-0000-0000-000000000000', // Default user ID
        ...data
      }).subscribe(() => {
        this.router.navigate(['/projects']);
      });
    }
  }

  cancel(): void {
    this.router.navigate(['/projects']);
  }
}
