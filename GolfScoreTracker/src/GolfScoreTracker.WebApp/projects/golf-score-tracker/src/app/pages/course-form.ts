import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { CoursesService } from '../services';
import { CreateCourseCommand, UpdateCourseCommand } from '../models';

@Component({
  selector: 'app-course-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterLink,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule
  ],
  template: `
    <div class="course-form">
      <div class="course-form__header">
        <button mat-button routerLink="/courses">
          <mat-icon>arrow_back</mat-icon>
          Back to Courses
        </button>
      </div>

      <mat-card class="course-form__card">
        <mat-card-header>
          <mat-card-title>{{ isEditMode ? 'Edit Course' : 'Add Course' }}</mat-card-title>
        </mat-card-header>

        <mat-card-content>
          <form [formGroup]="courseForm" (ngSubmit)="onSubmit()" class="course-form__form">
            <mat-form-field appearance="outline" class="course-form__field">
              <mat-label>Course Name</mat-label>
              <input matInput formControlName="name" required>
              <mat-error *ngIf="courseForm.get('name')?.hasError('required')">
                Course name is required
              </mat-error>
            </mat-form-field>

            <mat-form-field appearance="outline" class="course-form__field">
              <mat-label>Location</mat-label>
              <input matInput formControlName="location">
            </mat-form-field>

            <mat-form-field appearance="outline" class="course-form__field">
              <mat-label>Number of Holes</mat-label>
              <input matInput type="number" formControlName="numberOfHoles" required>
              <mat-error *ngIf="courseForm.get('numberOfHoles')?.hasError('required')">
                Number of holes is required
              </mat-error>
            </mat-form-field>

            <mat-form-field appearance="outline" class="course-form__field">
              <mat-label>Total Par</mat-label>
              <input matInput type="number" formControlName="totalPar" required>
              <mat-error *ngIf="courseForm.get('totalPar')?.hasError('required')">
                Total par is required
              </mat-error>
            </mat-form-field>

            <mat-form-field appearance="outline" class="course-form__field">
              <mat-label>Course Rating</mat-label>
              <input matInput type="number" formControlName="courseRating" step="0.1">
            </mat-form-field>

            <mat-form-field appearance="outline" class="course-form__field">
              <mat-label>Slope Rating</mat-label>
              <input matInput type="number" formControlName="slopeRating">
            </mat-form-field>

            <mat-form-field appearance="outline" class="course-form__field">
              <mat-label>Notes</mat-label>
              <textarea matInput formControlName="notes" rows="3"></textarea>
            </mat-form-field>

            <div class="course-form__actions">
              <button mat-raised-button type="button" routerLink="/courses">Cancel</button>
              <button mat-raised-button color="primary" type="submit" [disabled]="!courseForm.valid">
                {{ isEditMode ? 'Update' : 'Create' }}
              </button>
            </div>
          </form>
        </mat-card-content>
      </mat-card>
    </div>
  `,
  styles: [`
    .course-form {
      padding: 2rem;
      max-width: 800px;
      margin: 0 auto;
    }

    .course-form__header {
      margin-bottom: 1rem;
    }

    .course-form__card {
      margin-top: 1rem;
    }

    .course-form__form {
      display: flex;
      flex-direction: column;
      gap: 1rem;
      margin-top: 1rem;
    }

    .course-form__field {
      width: 100%;
    }

    .course-form__actions {
      display: flex;
      gap: 1rem;
      justify-content: flex-end;
      margin-top: 1rem;
    }
  `]
})
export class CourseForm implements OnInit {
  courseForm: FormGroup;
  isEditMode = false;
  private courseId: string | null = null;

  constructor(
    private fb: FormBuilder,
    private coursesService: CoursesService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.courseForm = this.fb.group({
      name: ['', Validators.required],
      location: [''],
      numberOfHoles: [18, Validators.required],
      totalPar: [72, Validators.required],
      courseRating: [null],
      slopeRating: [null],
      notes: ['']
    });
  }

  ngOnInit(): void {
    this.courseId = this.route.snapshot.paramMap.get('id');
    this.isEditMode = !!this.courseId && this.route.snapshot.url.some(segment => segment.path === 'edit');

    if (this.isEditMode && this.courseId) {
      this.coursesService.getCourseById(this.courseId).subscribe(course => {
        this.courseForm.patchValue({
          name: course.name,
          location: course.location,
          numberOfHoles: course.numberOfHoles,
          totalPar: course.totalPar,
          courseRating: course.courseRating,
          slopeRating: course.slopeRating,
          notes: course.notes
        });
      });
    }
  }

  onSubmit(): void {
    if (this.courseForm.valid) {
      if (this.isEditMode && this.courseId) {
        const command: UpdateCourseCommand = {
          courseId: this.courseId,
          ...this.courseForm.value
        };
        this.coursesService.updateCourse(this.courseId, command).subscribe(() => {
          this.router.navigate(['/courses', this.courseId]);
        });
      } else {
        const command: CreateCourseCommand = this.courseForm.value;
        this.coursesService.createCourse(command).subscribe(course => {
          this.router.navigate(['/courses', course.courseId]);
        });
      }
    }
  }
}
