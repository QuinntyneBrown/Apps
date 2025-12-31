import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { TopicService, MeetingService } from '../services';
import { TopicCategory, TopicCategoryLabels } from '../models';

@Component({
  selector: 'app-topic-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule
  ],
  template: `
    <div class="topic-form">
      <h1 class="topic-form__title">{{ isEditMode ? 'Edit Topic' : 'New Topic' }}</h1>

      <mat-card class="topic-form__card">
        <form [formGroup]="form" (ngSubmit)="onSubmit()" class="topic-form__form">
          <mat-form-field appearance="outline" class="topic-form__field" *ngIf="!isEditMode">
            <mat-label>Meeting (Optional)</mat-label>
            <mat-select formControlName="meetingId">
              <mat-option [value]="null">None</mat-option>
              <mat-option *ngFor="let meeting of meetings$ | async" [value]="meeting.meetingId">
                {{ meeting.title }}
              </mat-option>
            </mat-select>
          </mat-form-field>

          <mat-form-field appearance="outline" class="topic-form__field">
            <mat-label>Title</mat-label>
            <input matInput formControlName="title" required>
            <mat-error *ngIf="form.get('title')?.hasError('required')">Title is required</mat-error>
          </mat-form-field>

          <mat-form-field appearance="outline" class="topic-form__field">
            <mat-label>Description</mat-label>
            <textarea matInput formControlName="description" rows="3"></textarea>
          </mat-form-field>

          <mat-form-field appearance="outline" class="topic-form__field">
            <mat-label>Category</mat-label>
            <mat-select formControlName="category" required>
              <mat-option *ngFor="let category of categories" [value]="category.value">
                {{ category.label }}
              </mat-option>
            </mat-select>
            <mat-error *ngIf="form.get('category')?.hasError('required')">Category is required</mat-error>
          </mat-form-field>

          <mat-form-field appearance="outline" class="topic-form__field">
            <mat-label>Discussion Notes</mat-label>
            <textarea matInput formControlName="discussionNotes" rows="5"></textarea>
          </mat-form-field>

          <div class="topic-form__actions">
            <button mat-raised-button type="button" (click)="cancel()">Cancel</button>
            <button mat-raised-button color="primary" type="submit" [disabled]="form.invalid">
              {{ isEditMode ? 'Update' : 'Create' }}
            </button>
          </div>
        </form>
      </mat-card>
    </div>
  `,
  styles: [`
    .topic-form {
      padding: 2rem;
      max-width: 800px;
      margin: 0 auto;

      &__title {
        margin: 0 0 2rem 0;
        font-size: 2rem;
        font-weight: 400;
      }

      &__card {
        padding: 2rem;
      }

      &__form {
        display: flex;
        flex-direction: column;
        gap: 1rem;
      }

      &__field {
        width: 100%;
      }

      &__actions {
        display: flex;
        justify-content: flex-end;
        gap: 1rem;
        margin-top: 1rem;
      }
    }
  `]
})
export class TopicForm implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly topicService = inject(TopicService);
  private readonly meetingService = inject(MeetingService);
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);

  form: FormGroup;
  isEditMode = false;
  topicId: string | null = null;
  meetings$ = this.meetingService.meetings$;
  categories = Object.keys(TopicCategory)
    .filter(key => !isNaN(Number(key)))
    .map(key => ({
      value: Number(key),
      label: TopicCategoryLabels[Number(key) as TopicCategory]
    }));

  constructor() {
    this.form = this.fb.group({
      meetingId: [null],
      title: ['', Validators.required],
      description: [''],
      category: [TopicCategory.Other, Validators.required],
      discussionNotes: ['']
    });
  }

  ngOnInit(): void {
    this.meetingService.getAll().subscribe();

    this.topicId = this.route.snapshot.paramMap.get('id');
    this.isEditMode = !!this.topicId && this.topicId !== 'new';

    if (this.isEditMode && this.topicId) {
      this.topicService.getById(this.topicId).subscribe(topic => {
        this.form.patchValue({
          meetingId: topic.meetingId,
          title: topic.title,
          description: topic.description,
          category: topic.category,
          discussionNotes: topic.discussionNotes
        });
      });
    }
  }

  onSubmit(): void {
    if (this.form.invalid) return;

    const formValue = this.form.value;

    if (this.isEditMode && this.topicId) {
      this.topicService.update({
        topicId: this.topicId,
        title: formValue.title,
        description: formValue.description,
        category: formValue.category,
        discussionNotes: formValue.discussionNotes
      }).subscribe(() => {
        this.router.navigate(['/topics']);
      });
    } else {
      this.topicService.create({
        meetingId: formValue.meetingId,
        userId: '00000000-0000-0000-0000-000000000000', // TODO: Replace with actual user ID
        title: formValue.title,
        description: formValue.description,
        category: formValue.category,
        discussionNotes: formValue.discussionNotes
      }).subscribe(() => {
        this.router.navigate(['/topics']);
      });
    }
  }

  cancel(): void {
    this.router.navigate(['/topics']);
  }
}
