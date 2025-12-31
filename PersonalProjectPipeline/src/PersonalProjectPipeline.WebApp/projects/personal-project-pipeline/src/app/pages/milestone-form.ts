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
import { MilestoneService, ProjectService } from '../services';

@Component({
  selector: 'app-milestone-form',
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
    <div class="milestone-form">
      <mat-card class="milestone-form__card">
        <mat-card-header>
          <mat-card-title>{{ isEditMode ? 'Edit Milestone' : 'New Milestone' }}</mat-card-title>
        </mat-card-header>
        <mat-card-content>
          <form [formGroup]="form" class="milestone-form__form">
            <mat-form-field class="milestone-form__field">
              <mat-label>Project</mat-label>
              <mat-select formControlName="projectId" required [disabled]="isEditMode">
                @for (project of (projectService.projects$ | async) || []; track project.projectId) {
                  <mat-option [value]="project.projectId">{{ project.name }}</mat-option>
                }
              </mat-select>
            </mat-form-field>

            <mat-form-field class="milestone-form__field">
              <mat-label>Name</mat-label>
              <input matInput formControlName="name" required>
              @if (form.get('name')?.hasError('required') && form.get('name')?.touched) {
                <mat-error>Name is required</mat-error>
              }
            </mat-form-field>

            <mat-form-field class="milestone-form__field">
              <mat-label>Description</mat-label>
              <textarea matInput formControlName="description" rows="4"></textarea>
            </mat-form-field>

            <mat-form-field class="milestone-form__field">
              <mat-label>Target Date</mat-label>
              <input matInput [matDatepicker]="targetPicker" formControlName="targetDate">
              <mat-datepicker-toggle matIconSuffix [for]="targetPicker"></mat-datepicker-toggle>
              <mat-datepicker #targetPicker></mat-datepicker>
            </mat-form-field>

            @if (isEditMode) {
              <div class="milestone-form__checkbox">
                <mat-checkbox formControlName="isAchieved">Mark as Achieved</mat-checkbox>
              </div>
            }
          </form>
        </mat-card-content>
        <mat-card-actions class="milestone-form__actions">
          <button mat-button (click)="cancel()">Cancel</button>
          <button mat-raised-button color="primary" (click)="save()" [disabled]="form.invalid">
            {{ isEditMode ? 'Update' : 'Create' }}
          </button>
        </mat-card-actions>
      </mat-card>
    </div>
  `,
  styles: [`
    .milestone-form {
      padding: 2rem;
      display: flex;
      justify-content: center;
    }

    .milestone-form__card {
      width: 100%;
      max-width: 600px;
    }

    .milestone-form__form {
      display: flex;
      flex-direction: column;
      gap: 1rem;
      padding-top: 1rem;
    }

    .milestone-form__field {
      width: 100%;
    }

    .milestone-form__checkbox {
      padding: 0.5rem 0;
    }

    .milestone-form__actions {
      display: flex;
      justify-content: flex-end;
      gap: 0.5rem;
      padding: 1rem;
    }
  `]
})
export class MilestoneForm implements OnInit {
  private fb = inject(FormBuilder);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  private milestoneService = inject(MilestoneService);
  projectService = inject(ProjectService);

  form!: FormGroup;
  isEditMode = false;
  milestoneId: string | null = null;

  ngOnInit(): void {
    this.projectService.getProjects().subscribe();
    this.initForm();

    this.milestoneId = this.route.snapshot.paramMap.get('id');
    this.isEditMode = this.milestoneId !== null && this.milestoneId !== 'new';

    if (this.isEditMode && this.milestoneId) {
      this.milestoneService.getMilestoneById(this.milestoneId).subscribe(milestone => {
        this.form.patchValue({
          projectId: milestone.projectId,
          name: milestone.name,
          description: milestone.description,
          targetDate: milestone.targetDate ? new Date(milestone.targetDate) : null,
          isAchieved: milestone.isAchieved
        });
      });
    }
  }

  initForm(): void {
    this.form = this.fb.group({
      projectId: ['', Validators.required],
      name: ['', Validators.required],
      description: [''],
      targetDate: [null],
      isAchieved: [false]
    });
  }

  save(): void {
    if (this.form.invalid) return;

    const formValue = this.form.value;
    const data = {
      ...formValue,
      targetDate: formValue.targetDate ? formValue.targetDate.toISOString() : null
    };

    if (this.isEditMode && this.milestoneId) {
      this.milestoneService.updateMilestone({
        milestoneId: this.milestoneId,
        name: data.name,
        description: data.description,
        targetDate: data.targetDate,
        isAchieved: data.isAchieved
      }).subscribe(() => {
        this.router.navigate(['/milestones']);
      });
    } else {
      this.milestoneService.createMilestone({
        projectId: data.projectId,
        name: data.name,
        description: data.description,
        targetDate: data.targetDate
      }).subscribe(() => {
        this.router.navigate(['/milestones']);
      });
    }
  }

  cancel(): void {
    this.router.navigate(['/milestones']);
  }
}
