import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatCardModule } from '@angular/material/card';
import { LessonService, SourceService } from '../services';
import { Lesson, CreateLesson, UpdateLesson, LessonCategory, LessonCategoryLabels } from '../models';

@Component({
  selector: 'app-lesson-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatCheckboxModule,
    MatButtonModule
  ],
  template: `
    <h2 mat-dialog-title>{{ data ? 'Edit Lesson' : 'New Lesson' }}</h2>
    <form [formGroup]="form" (ngSubmit)="onSubmit()">
      <mat-dialog-content class="lesson-dialog">
        <mat-form-field class="lesson-dialog__field">
          <mat-label>Title</mat-label>
          <input matInput formControlName="title" required>
        </mat-form-field>

        <mat-form-field class="lesson-dialog__field">
          <mat-label>Content</mat-label>
          <textarea matInput formControlName="content" rows="4" required></textarea>
        </mat-form-field>

        <mat-form-field class="lesson-dialog__field">
          <mat-label>Category</mat-label>
          <mat-select formControlName="category" required>
            @for (cat of categories; track cat.value) {
              <mat-option [value]="cat.value">{{ cat.label }}</mat-option>
            }
          </mat-select>
        </mat-form-field>

        <mat-form-field class="lesson-dialog__field">
          <mat-label>Source</mat-label>
          <mat-select formControlName="sourceId">
            <mat-option [value]="null">None</mat-option>
            @for (source of sources$ | async; track source.sourceId) {
              <mat-option [value]="source.sourceId">{{ source.title }}</mat-option>
            }
          </mat-select>
        </mat-form-field>

        <mat-form-field class="lesson-dialog__field">
          <mat-label>Date Learned</mat-label>
          <input matInput type="date" formControlName="dateLearned" required>
        </mat-form-field>

        <mat-form-field class="lesson-dialog__field">
          <mat-label>Tags</mat-label>
          <input matInput formControlName="tags">
        </mat-form-field>

        <mat-form-field class="lesson-dialog__field">
          <mat-label>Application</mat-label>
          <textarea matInput formControlName="application" rows="3"></textarea>
        </mat-form-field>

        @if (data) {
          <div class="lesson-dialog__field">
            <mat-checkbox formControlName="isApplied">Is Applied</mat-checkbox>
          </div>
        }
      </mat-dialog-content>
      <mat-dialog-actions align="end">
        <button mat-button type="button" mat-dialog-close>Cancel</button>
        <button mat-raised-button color="primary" type="submit" [disabled]="form.invalid">Save</button>
      </mat-dialog-actions>
    </form>
  `,
  styles: [`
    .lesson-dialog {
      display: flex;
      flex-direction: column;
      min-width: 400px;
      padding: 1rem 0;

      &__field {
        width: 100%;
        margin-bottom: 1rem;
      }
    }
  `]
})
export class LessonDialog {
  private fb = inject(FormBuilder);
  private sourceService = inject(SourceService);
  public dialog = inject(MatDialog);

  data?: Lesson;
  form: FormGroup;
  categories = Object.entries(LessonCategoryLabels).map(([value, label]) => ({
    value: Number(value),
    label
  }));
  sources$ = this.sourceService.sources$;

  constructor() {
    this.form = this.fb.group({
      title: ['', Validators.required],
      content: ['', Validators.required],
      category: [LessonCategory.Other, Validators.required],
      sourceId: [null],
      dateLearned: [new Date().toISOString().split('T')[0], Validators.required],
      tags: [''],
      application: [''],
      isApplied: [false]
    });

    if (this.data) {
      this.form.patchValue({
        ...this.data,
        dateLearned: this.data.dateLearned.split('T')[0]
      });
    }
  }

  onSubmit(): void {
    if (this.form.valid) {
      this.dialog.getDialogById('lesson-dialog')?.close(this.form.value);
    }
  }
}

@Component({
  selector: 'app-lessons',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule
  ],
  templateUrl: './lessons.html',
  styleUrl: './lessons.scss'
})
export class Lessons implements OnInit {
  private lessonService = inject(LessonService);
  private sourceService = inject(SourceService);
  private dialog = inject(MatDialog);

  lessons$ = this.lessonService.lessons$;
  loading$ = this.lessonService.loading$;
  displayedColumns = ['title', 'category', 'dateLearned', 'isApplied', 'actions'];
  categoryLabels = LessonCategoryLabels;

  ngOnInit(): void {
    this.lessonService.getLessons().subscribe();
    this.sourceService.getSources().subscribe();
  }

  openDialog(lesson?: Lesson): void {
    const dialogRef = this.dialog.open(LessonDialog, {
      id: 'lesson-dialog',
      width: '600px',
      data: lesson
    });

    if (lesson) {
      dialogRef.componentInstance.data = lesson;
    }

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (lesson) {
          const updateData: UpdateLesson = {
            lessonId: lesson.lessonId,
            ...result
          };
          this.lessonService.updateLesson(updateData).subscribe();
        } else {
          const createData: CreateLesson = {
            userId: '00000000-0000-0000-0000-000000000000',
            ...result
          };
          this.lessonService.createLesson(createData).subscribe();
        }
      }
    });
  }

  deleteLesson(lesson: Lesson): void {
    if (confirm(`Are you sure you want to delete "${lesson.title}"?`)) {
      this.lessonService.deleteLesson(lesson.lessonId).subscribe();
    }
  }
}
