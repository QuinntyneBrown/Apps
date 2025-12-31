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
import { MatCheckboxModule } from '@angular/material/checkbox';
import { ProjectTaskService, ProjectService, MilestoneService } from '../services';

@Component({
  selector: 'app-task-form',
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
    MatNativeDateModule,
    MatCheckboxModule
  ],
  template: `
    <div class="task-form">
      <mat-card class="task-form__card">
        <mat-card-header>
          <mat-card-title>{{ isEditMode ? 'Edit Task' : 'New Task' }}</mat-card-title>
        </mat-card-header>
        <mat-card-content>
          <form [formGroup]="form" class="task-form__form">
            <mat-form-field class="task-form__field">
              <mat-label>Project</mat-label>
              <mat-select formControlName="projectId" required [disabled]="isEditMode">
                @for (project of (projectService.projects$ | async) || []; track project.projectId) {
                  <mat-option [value]="project.projectId">{{ project.name }}</mat-option>
                }
              </mat-select>
            </mat-form-field>

            <mat-form-field class="task-form__field">
              <mat-label>Milestone (Optional)</mat-label>
              <mat-select formControlName="milestoneId">
                <mat-option [value]="null">None</mat-option>
                @for (milestone of getFilteredMilestones(); track milestone.milestoneId) {
                  <mat-option [value]="milestone.milestoneId">{{ milestone.name }}</mat-option>
                }
              </mat-select>
            </mat-form-field>

            <mat-form-field class="task-form__field">
              <mat-label>Title</mat-label>
              <input matInput formControlName="title" required>
              @if (form.get('title')?.hasError('required') && form.get('title')?.touched) {
                <mat-error>Title is required</mat-error>
              }
            </mat-form-field>

            <mat-form-field class="task-form__field">
              <mat-label>Description</mat-label>
              <textarea matInput formControlName="description" rows="4"></textarea>
            </mat-form-field>

            <mat-form-field class="task-form__field">
              <mat-label>Due Date</mat-label>
              <input matInput [matDatepicker]="duePicker" formControlName="dueDate">
              <mat-datepicker-toggle matIconSuffix [for]="duePicker"></mat-datepicker-toggle>
              <mat-datepicker #duePicker></mat-datepicker>
            </mat-form-field>

            <mat-form-field class="task-form__field">
              <mat-label>Estimated Hours</mat-label>
              <input matInput type="number" formControlName="estimatedHours" min="0" step="0.5">
            </mat-form-field>

            @if (isEditMode) {
              <div class="task-form__checkbox">
                <mat-checkbox formControlName="isCompleted">Mark as Completed</mat-checkbox>
              </div>
            }
          </form>
        </mat-card-content>
        <mat-card-actions class="task-form__actions">
          <button mat-button (click)="cancel()">Cancel</button>
          <button mat-raised-button color="primary" (click)="save()" [disabled]="form.invalid">
            {{ isEditMode ? 'Update' : 'Create' }}
          </button>
        </mat-card-actions>
      </mat-card>
    </div>
  `,
  styles: [`
    .task-form {
      padding: 2rem;
      display: flex;
      justify-content: center;
    }

    .task-form__card {
      width: 100%;
      max-width: 600px;
    }

    .task-form__form {
      display: flex;
      flex-direction: column;
      gap: 1rem;
      padding-top: 1rem;
    }

    .task-form__field {
      width: 100%;
    }

    .task-form__checkbox {
      padding: 0.5rem 0;
    }

    .task-form__actions {
      display: flex;
      justify-content: flex-end;
      gap: 0.5rem;
      padding: 1rem;
    }
  `]
})
export class TaskForm implements OnInit {
  private fb = inject(FormBuilder);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  private taskService = inject(ProjectTaskService);
  projectService = inject(ProjectService);
  milestoneService = inject(MilestoneService);

  form!: FormGroup;
  isEditMode = false;
  taskId: string | null = null;

  ngOnInit(): void {
    this.projectService.getProjects().subscribe();
    this.milestoneService.getMilestones().subscribe();
    this.initForm();

    this.taskId = this.route.snapshot.paramMap.get('id');
    this.isEditMode = this.taskId !== null && this.taskId !== 'new';

    if (this.isEditMode && this.taskId) {
      this.taskService.getTaskById(this.taskId).subscribe(task => {
        this.form.patchValue({
          projectId: task.projectId,
          milestoneId: task.milestoneId,
          title: task.title,
          description: task.description,
          dueDate: task.dueDate ? new Date(task.dueDate) : null,
          estimatedHours: task.estimatedHours,
          isCompleted: task.isCompleted
        });
      });
    }
  }

  initForm(): void {
    this.form = this.fb.group({
      projectId: ['', Validators.required],
      milestoneId: [null],
      title: ['', Validators.required],
      description: [''],
      dueDate: [null],
      estimatedHours: [null],
      isCompleted: [false]
    });
  }

  getFilteredMilestones() {
    const projectId = this.form.get('projectId')?.value;
    const allMilestones = this.milestoneService['milestonesSubject'].value;
    return projectId ? allMilestones.filter(m => m.projectId === projectId) : [];
  }

  save(): void {
    if (this.form.invalid) return;

    const formValue = this.form.value;
    const data = {
      ...formValue,
      dueDate: formValue.dueDate ? formValue.dueDate.toISOString() : null,
      milestoneId: formValue.milestoneId || undefined
    };

    if (this.isEditMode && this.taskId) {
      this.taskService.updateTask({
        projectTaskId: this.taskId,
        title: data.title,
        description: data.description,
        dueDate: data.dueDate,
        estimatedHours: data.estimatedHours,
        isCompleted: data.isCompleted
      }).subscribe(() => {
        this.router.navigate(['/tasks']);
      });
    } else {
      this.taskService.createTask({
        projectId: data.projectId,
        milestoneId: data.milestoneId,
        title: data.title,
        description: data.description,
        dueDate: data.dueDate,
        estimatedHours: data.estimatedHours
      }).subscribe(() => {
        this.router.navigate(['/tasks']);
      });
    }
  }

  cancel(): void {
    this.router.navigate(['/tasks']);
  }
}
