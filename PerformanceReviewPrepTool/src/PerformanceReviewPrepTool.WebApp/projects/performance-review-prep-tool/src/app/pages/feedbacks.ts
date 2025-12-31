import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatSelectModule } from '@angular/material/select';
import { FeedbackService, ReviewPeriodService } from '../services';
import { Feedback, CreateFeedback, UpdateFeedback } from '../models';

@Component({
  selector: 'app-feedback-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatCheckboxModule,
    MatSelectModule
  ],
  template: `
    <h2 mat-dialog-title>{{ data ? 'Edit' : 'Add' }} Feedback</h2>
    <mat-dialog-content>
      <form [formGroup]="form" class="feedback-dialog__form">
        <mat-form-field class="feedback-dialog__field">
          <mat-label>Review Period</mat-label>
          <mat-select formControlName="reviewPeriodId" required>
            <mat-option *ngFor="let period of reviewPeriods$ | async" [value]="period.reviewPeriodId">
              {{ period.title }}
            </mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field class="feedback-dialog__field">
          <mat-label>Source</mat-label>
          <input matInput formControlName="source" required>
        </mat-form-field>

        <mat-form-field class="feedback-dialog__field">
          <mat-label>Content</mat-label>
          <textarea matInput formControlName="content" rows="4" required></textarea>
        </mat-form-field>

        <mat-form-field class="feedback-dialog__field">
          <mat-label>Received Date</mat-label>
          <input matInput [matDatepicker]="receivedPicker" formControlName="receivedDate" required>
          <mat-datepicker-toggle matSuffix [for]="receivedPicker"></mat-datepicker-toggle>
          <mat-datepicker #receivedPicker></mat-datepicker>
        </mat-form-field>

        <mat-form-field class="feedback-dialog__field">
          <mat-label>Feedback Type</mat-label>
          <input matInput formControlName="feedbackType">
        </mat-form-field>

        <mat-form-field class="feedback-dialog__field">
          <mat-label>Category</mat-label>
          <input matInput formControlName="category">
        </mat-form-field>

        <mat-checkbox formControlName="isKeyFeedback">Key Feedback</mat-checkbox>

        <mat-form-field class="feedback-dialog__field">
          <mat-label>Notes</mat-label>
          <textarea matInput formControlName="notes" rows="2"></textarea>
        </mat-form-field>
      </form>
    </mat-dialog-content>
    <mat-dialog-actions align="end">
      <button mat-button mat-dialog-close>Cancel</button>
      <button mat-raised-button color="primary" (click)="onSave()" [disabled]="!form.valid">Save</button>
    </mat-dialog-actions>
  `,
  styles: [`
    .feedback-dialog__form {
      display: flex;
      flex-direction: column;
      gap: 1rem;
      min-width: 400px;
    }

    .feedback-dialog__field {
      width: 100%;
    }
  `]
})
export class FeedbackDialog {
  private fb = inject(FormBuilder);
  private dialogRef = inject(MatDialog);
  private reviewPeriodService = inject(ReviewPeriodService);

  data?: Feedback;
  form: FormGroup;
  reviewPeriods$ = this.reviewPeriodService.reviewPeriods$;

  constructor() {
    this.form = this.fb.group({
      reviewPeriodId: ['', Validators.required],
      source: ['', Validators.required],
      content: ['', Validators.required],
      receivedDate: ['', Validators.required],
      feedbackType: [''],
      category: [''],
      isKeyFeedback: [false],
      notes: ['']
    });

    if (this.data) {
      this.form.patchValue({
        reviewPeriodId: this.data.reviewPeriodId,
        source: this.data.source,
        content: this.data.content,
        receivedDate: new Date(this.data.receivedDate),
        feedbackType: this.data.feedbackType,
        category: this.data.category,
        isKeyFeedback: this.data.isKeyFeedback,
        notes: this.data.notes
      });
    }
  }

  onSave(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const result = {
        ...formValue,
        receivedDate: formValue.receivedDate.toISOString()
      };

      if (this.data) {
        result.feedbackId = this.data.feedbackId;
      }
    }
  }
}

@Component({
  selector: 'app-feedbacks',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule
  ],
  template: `
    <div class="feedbacks">
      <div class="feedbacks__header">
        <h1 class="feedbacks__title">Feedbacks</h1>
        <button mat-raised-button color="primary" (click)="openDialog()">
          <mat-icon>add</mat-icon>
          Add Feedback
        </button>
      </div>

      <div class="feedbacks__table">
        <table mat-table [dataSource]="feedbacks$ | async" class="feedbacks__mat-table">
          <ng-container matColumnDef="source">
            <th mat-header-cell *matHeaderCellDef>Source</th>
            <td mat-cell *matCellDef="let feedback">{{ feedback.source }}</td>
          </ng-container>

          <ng-container matColumnDef="content">
            <th mat-header-cell *matHeaderCellDef>Content</th>
            <td mat-cell *matCellDef="let feedback">{{ feedback.content }}</td>
          </ng-container>

          <ng-container matColumnDef="receivedDate">
            <th mat-header-cell *matHeaderCellDef>Received Date</th>
            <td mat-cell *matCellDef="let feedback">{{ feedback.receivedDate | date:'short' }}</td>
          </ng-container>

          <ng-container matColumnDef="feedbackType">
            <th mat-header-cell *matHeaderCellDef>Type</th>
            <td mat-cell *matCellDef="let feedback">{{ feedback.feedbackType || '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="category">
            <th mat-header-cell *matHeaderCellDef>Category</th>
            <td mat-cell *matCellDef="let feedback">{{ feedback.category || '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="isKeyFeedback">
            <th mat-header-cell *matHeaderCellDef>Key</th>
            <td mat-cell *matCellDef="let feedback">
              <mat-icon *ngIf="feedback.isKeyFeedback" class="feedbacks__key-icon">star</mat-icon>
            </td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let feedback">
              <button mat-icon-button (click)="openDialog(feedback)">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" (click)="delete(feedback.feedbackId)">
                <mat-icon>delete</mat-icon>
              </button>
            </td>
          </ng-container>

          <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
          <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
        </table>
      </div>
    </div>
  `,
  styles: [`
    .feedbacks {
      padding: 2rem;
    }

    .feedbacks__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 2rem;
    }

    .feedbacks__title {
      margin: 0;
      font-size: 2rem;
      font-weight: 500;
    }

    .feedbacks__table {
      background: white;
      border-radius: 4px;
      overflow: hidden;
    }

    .feedbacks__mat-table {
      width: 100%;
    }

    .feedbacks__key-icon {
      color: gold;
    }
  `]
})
export class Feedbacks implements OnInit {
  private feedbackService = inject(FeedbackService);
  private reviewPeriodService = inject(ReviewPeriodService);
  private dialog = inject(MatDialog);

  feedbacks$ = this.feedbackService.feedbacks$;
  displayedColumns = ['source', 'content', 'receivedDate', 'feedbackType', 'category', 'isKeyFeedback', 'actions'];

  ngOnInit(): void {
    this.feedbackService.getAll().subscribe();
    this.reviewPeriodService.getAll().subscribe();
  }

  openDialog(feedback?: Feedback): void {
    const dialogRef = this.dialog.open(FeedbackDialog, {
      width: '500px',
      data: feedback
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (feedback) {
          this.feedbackService.update(result as UpdateFeedback).subscribe();
        } else {
          // Generate a dummy userId for demo purposes
          const createData: CreateFeedback = {
            ...result,
            userId: '00000000-0000-0000-0000-000000000000'
          };
          this.feedbackService.create(createData).subscribe();
        }
      }
    });
  }

  delete(id: string): void {
    if (confirm('Are you sure you want to delete this feedback?')) {
      this.feedbackService.delete(id).subscribe();
    }
  }
}
