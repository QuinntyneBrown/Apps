import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { ProjectService } from '../services';

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
    MatCheckboxModule,
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
            </mat-form-field>

            <mat-form-field class="project-form__field">
              <mat-label>Description</mat-label>
              <textarea matInput formControlName="description" rows="3"></textarea>
            </mat-form-field>

            <mat-form-field class="project-form__field">
              <mat-label>Due Date</mat-label>
              <input matInput [matDatepicker]="picker" formControlName="dueDate">
              <mat-datepicker-toggle matSuffix [for]="picker"></mat-datepicker-toggle>
              <mat-datepicker #picker></mat-datepicker>
            </mat-form-field>

            <mat-checkbox formControlName="isCompleted" class="project-form__checkbox">
              Completed
            </mat-checkbox>

            <mat-form-field class="project-form__field">
              <mat-label>Notes</mat-label>
              <textarea matInput formControlName="notes" rows="4"></textarea>
            </mat-form-field>
          </form>
        </mat-card-content>

        <mat-card-actions class="project-form__actions">
          <button mat-button (click)="cancel()">Cancel</button>
          <button mat-raised-button color="primary" (click)="save()" [disabled]="!form.valid">Save</button>
        </mat-card-actions>
      </mat-card>
    </div>
  `,
  styles: [`
    .project-form {
      padding: 2rem;
      max-width: 800px;
      margin: 0 auto;
    }

    .project-form__card {
      width: 100%;
    }

    .project-form__form {
      display: flex;
      flex-direction: column;
      gap: 1rem;
      margin-top: 1rem;
    }

    .project-form__field {
      width: 100%;
    }

    .project-form__checkbox {
      margin-bottom: 1rem;
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
  private readonly fb = inject(FormBuilder);
  private readonly projectService = inject(ProjectService);
  private readonly router = inject(Router);
  private readonly route = inject(ActivatedRoute);

  form: FormGroup;
  isEditMode = false;
  projectId?: string;

  constructor() {
    this.form = this.fb.group({
      name: ['', Validators.required],
      description: [''],
      dueDate: [null],
      isCompleted: [false],
      notes: ['']
    });
  }

  ngOnInit(): void {
    this.projectId = this.route.snapshot.paramMap.get('id') || undefined;
    this.isEditMode = !!this.projectId;

    if (this.isEditMode && this.projectId) {
      this.projectService.getById(this.projectId).subscribe(project => {
        this.form.patchValue({
          name: project.name,
          description: project.description,
          dueDate: project.dueDate ? new Date(project.dueDate) : null,
          isCompleted: project.isCompleted,
          notes: project.notes
        });
      });
    }
  }

  save(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const dueDate = formValue.dueDate ? new Date(formValue.dueDate).toISOString() : undefined;

      if (this.isEditMode && this.projectId) {
        this.projectService.update({
          projectId: this.projectId,
          ...formValue,
          dueDate
        }).subscribe(() => {
          this.router.navigate(['/projects']);
        });
      } else {
        this.projectService.create({
          userId: '00000000-0000-0000-0000-000000000000', // TODO: Get from auth
          ...formValue,
          dueDate
        }).subscribe(() => {
          this.router.navigate(['/projects']);
        });
      }
    }
  }

  cancel(): void {
    this.router.navigate(['/projects']);
  }
}
