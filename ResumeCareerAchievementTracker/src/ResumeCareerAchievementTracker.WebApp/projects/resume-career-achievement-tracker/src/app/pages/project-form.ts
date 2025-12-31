import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, FormArray } from '@angular/forms';
import { Router, ActivatedRoute, RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatChipsModule } from '@angular/material/chips';
import { MatIconModule } from '@angular/material/icon';
import { ProjectService } from '../services';

@Component({
  selector: 'app-project-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterLink,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatChipsModule,
    MatIconModule
  ],
  template: `
    <div class="project-form">
      <div class="project-form__header">
        <h1 class="project-form__title">{{ isEditMode ? 'Edit Project' : 'New Project' }}</h1>
        <p class="project-form__subtitle">{{ isEditMode ? 'Update project details' : 'Add a new professional project' }}</p>
      </div>

      <mat-card class="project-form__card">
        <mat-card-content>
          <form [formGroup]="form" (ngSubmit)="onSubmit()" class="project-form__form">
            <div class="project-form__row">
              <mat-form-field class="project-form__field project-form__field--full">
                <mat-label>Project Name</mat-label>
                <input matInput formControlName="name" placeholder="Enter project name" required>
                @if (form.get('name')?.hasError('required') && form.get('name')?.touched) {
                  <mat-error>Name is required</mat-error>
                }
              </mat-form-field>
            </div>

            <div class="project-form__row">
              <mat-form-field class="project-form__field project-form__field--full">
                <mat-label>Description</mat-label>
                <textarea
                  matInput
                  formControlName="description"
                  placeholder="Describe the project"
                  rows="4"
                  required></textarea>
                @if (form.get('description')?.hasError('required') && form.get('description')?.touched) {
                  <mat-error>Description is required</mat-error>
                }
              </mat-form-field>
            </div>

            <div class="project-form__row project-form__row--two">
              <mat-form-field class="project-form__field">
                <mat-label>Organization</mat-label>
                <input matInput formControlName="organization" placeholder="Company or organization">
              </mat-form-field>

              <mat-form-field class="project-form__field">
                <mat-label>Your Role</mat-label>
                <input matInput formControlName="role" placeholder="Your role in the project">
              </mat-form-field>
            </div>

            <div class="project-form__row project-form__row--two">
              <mat-form-field class="project-form__field">
                <mat-label>Start Date</mat-label>
                <input matInput [matDatepicker]="startPicker" formControlName="startDate" required>
                <mat-datepicker-toggle matIconSuffix [for]="startPicker"></mat-datepicker-toggle>
                <mat-datepicker #startPicker></mat-datepicker>
                @if (form.get('startDate')?.hasError('required') && form.get('startDate')?.touched) {
                  <mat-error>Start date is required</mat-error>
                }
              </mat-form-field>

              <mat-form-field class="project-form__field">
                <mat-label>End Date</mat-label>
                <input matInput [matDatepicker]="endPicker" formControlName="endDate">
                <mat-datepicker-toggle matIconSuffix [for]="endPicker"></mat-datepicker-toggle>
                <mat-datepicker #endPicker></mat-datepicker>
                <mat-hint>Leave empty if ongoing</mat-hint>
              </mat-form-field>
            </div>

            <div class="project-form__row">
              <mat-form-field class="project-form__field project-form__field--full">
                <mat-label>Project URL</mat-label>
                <input matInput formControlName="projectUrl" placeholder="https://project-url.com" type="url">
              </mat-form-field>
            </div>

            <div class="project-form__row">
              <div class="project-form__field project-form__field--full">
                <mat-label class="project-form__label">Technologies</mat-label>
                <div class="project-form__chips">
                  @for (tech of technologies.controls; track $index) {
                    <mat-chip class="project-form__chip">
                      {{ tech.value }}
                      <button matChipRemove (click)="removeTechnology($index)" type="button">
                        <mat-icon>cancel</mat-icon>
                      </button>
                    </mat-chip>
                  }
                </div>
                <div class="project-form__chip-input">
                  <mat-form-field class="project-form__field">
                    <mat-label>Add Technology</mat-label>
                    <input
                      matInput
                      #techInput
                      placeholder="Enter technology and press Enter"
                      (keyup.enter)="addTechnology(techInput.value); techInput.value = ''">
                  </mat-form-field>
                </div>
              </div>
            </div>

            <div class="project-form__row">
              <div class="project-form__field project-form__field--full">
                <mat-label class="project-form__label">Outcomes</mat-label>
                <div class="project-form__chips">
                  @for (outcome of outcomes.controls; track $index) {
                    <mat-chip class="project-form__chip">
                      {{ outcome.value }}
                      <button matChipRemove (click)="removeOutcome($index)" type="button">
                        <mat-icon>cancel</mat-icon>
                      </button>
                    </mat-chip>
                  }
                </div>
                <div class="project-form__chip-input">
                  <mat-form-field class="project-form__field">
                    <mat-label>Add Outcome</mat-label>
                    <input
                      matInput
                      #outcomeInput
                      placeholder="Enter outcome and press Enter"
                      (keyup.enter)="addOutcome(outcomeInput.value); outcomeInput.value = ''">
                  </mat-form-field>
                </div>
              </div>
            </div>

            <div class="project-form__actions">
              <a mat-button routerLink="/projects" type="button">Cancel</a>
              <button mat-raised-button color="primary" type="submit" [disabled]="form.invalid || saving">
                {{ saving ? 'Saving...' : (isEditMode ? 'Update' : 'Create') }}
              </button>
            </div>
          </form>
        </mat-card-content>
      </mat-card>
    </div>
  `,
  styles: [`
    .project-form {
      padding: 2rem;
      max-width: 900px;
      margin: 0 auto;
    }

    .project-form__header {
      margin-bottom: 2rem;
    }

    .project-form__title {
      font-size: 2rem;
      font-weight: 500;
      margin: 0 0 0.5rem 0;
      color: rgba(0, 0, 0, 0.87);
    }

    .project-form__subtitle {
      font-size: 1rem;
      margin: 0;
      color: rgba(0, 0, 0, 0.6);
    }

    .project-form__card {
      margin-bottom: 2rem;
    }

    .project-form__form {
      display: flex;
      flex-direction: column;
      gap: 1rem;
    }

    .project-form__row {
      display: flex;
      gap: 1rem;
    }

    .project-form__row--two {
      display: grid;
      grid-template-columns: 1fr 1fr;
    }

    .project-form__field {
      flex: 1;
    }

    .project-form__field--full {
      width: 100%;
    }

    .project-form__label {
      display: block;
      font-size: 0.875rem;
      color: rgba(0, 0, 0, 0.6);
      margin-bottom: 0.5rem;
    }

    .project-form__chips {
      display: flex;
      flex-wrap: wrap;
      gap: 0.5rem;
      margin-bottom: 1rem;
    }

    .project-form__chip {
      display: flex;
      align-items: center;
      gap: 0.25rem;
    }

    .project-form__chip-input {
      max-width: 400px;
    }

    .project-form__actions {
      display: flex;
      justify-content: flex-end;
      gap: 1rem;
      margin-top: 1rem;
      padding-top: 1rem;
      border-top: 1px solid rgba(0, 0, 0, 0.12);
    }

    @media (max-width: 768px) {
      .project-form {
        padding: 1rem;
      }

      .project-form__row--two {
        grid-template-columns: 1fr;
      }
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
  saving = false;
  projectId?: string;

  get technologies(): FormArray {
    return this.form.get('technologies') as FormArray;
  }

  get outcomes(): FormArray {
    return this.form.get('outcomes') as FormArray;
  }

  ngOnInit(): void {
    this.form = this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      organization: [''],
      role: [''],
      startDate: ['', Validators.required],
      endDate: [''],
      projectUrl: [''],
      technologies: this.fb.array([]),
      outcomes: this.fb.array([])
    });

    this.projectId = this.route.snapshot.paramMap.get('id') || undefined;
    this.isEditMode = !!this.projectId && this.projectId !== 'new';

    if (this.isEditMode && this.projectId) {
      this.loadProject(this.projectId);
    }
  }

  loadProject(id: string): void {
    this.projectService.getProjectById(id).subscribe({
      next: (project) => {
        this.form.patchValue({
          name: project.name,
          description: project.description,
          organization: project.organization,
          role: project.role,
          startDate: new Date(project.startDate),
          endDate: project.endDate ? new Date(project.endDate) : null,
          projectUrl: project.projectUrl
        });

        project.technologies.forEach(tech => {
          this.technologies.push(this.fb.control(tech));
        });

        project.outcomes.forEach(outcome => {
          this.outcomes.push(this.fb.control(outcome));
        });
      },
      error: () => {
        alert('Failed to load project');
        this.router.navigate(['/projects']);
      }
    });
  }

  addTechnology(value: string): void {
    const trimmedValue = value.trim();
    if (trimmedValue && !this.technologies.value.includes(trimmedValue)) {
      this.technologies.push(this.fb.control(trimmedValue));
    }
  }

  removeTechnology(index: number): void {
    this.technologies.removeAt(index);
  }

  addOutcome(value: string): void {
    const trimmedValue = value.trim();
    if (trimmedValue && !this.outcomes.value.includes(trimmedValue)) {
      this.outcomes.push(this.fb.control(trimmedValue));
    }
  }

  removeOutcome(index: number): void {
    this.outcomes.removeAt(index);
  }

  onSubmit(): void {
    if (this.form.invalid) {
      return;
    }

    this.saving = true;
    const formValue = this.form.value;
    const projectData = {
      ...formValue,
      startDate: new Date(formValue.startDate).toISOString(),
      endDate: formValue.endDate ? new Date(formValue.endDate).toISOString() : null,
      userId: '00000000-0000-0000-0000-000000000000' // This should be replaced with actual user ID
    };

    const request = this.isEditMode && this.projectId
      ? this.projectService.updateProject({ ...projectData, projectId: this.projectId })
      : this.projectService.createProject(projectData);

    request.subscribe({
      next: () => {
        this.router.navigate(['/projects']);
      },
      error: () => {
        this.saving = false;
        alert('Failed to save project');
      }
    });
  }
}
