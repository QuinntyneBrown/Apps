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
import { ReviewPeriodService } from '../services';
import { ReviewPeriod, CreateReviewPeriod, UpdateReviewPeriod } from '../models';

@Component({
  selector: 'app-review-period-dialog',
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
    MatCheckboxModule
  ],
  template: `
    <h2 mat-dialog-title>{{ data ? 'Edit' : 'Add' }} Review Period</h2>
    <mat-dialog-content>
      <form [formGroup]="form" class="review-period-dialog__form">
        <mat-form-field class="review-period-dialog__field">
          <mat-label>Title</mat-label>
          <input matInput formControlName="title" required>
        </mat-form-field>

        <mat-form-field class="review-period-dialog__field">
          <mat-label>Start Date</mat-label>
          <input matInput [matDatepicker]="startPicker" formControlName="startDate" required>
          <mat-datepicker-toggle matSuffix [for]="startPicker"></mat-datepicker-toggle>
          <mat-datepicker #startPicker></mat-datepicker>
        </mat-form-field>

        <mat-form-field class="review-period-dialog__field">
          <mat-label>End Date</mat-label>
          <input matInput [matDatepicker]="endPicker" formControlName="endDate" required>
          <mat-datepicker-toggle matSuffix [for]="endPicker"></mat-datepicker-toggle>
          <mat-datepicker #endPicker></mat-datepicker>
        </mat-form-field>

        <mat-form-field class="review-period-dialog__field">
          <mat-label>Review Due Date</mat-label>
          <input matInput [matDatepicker]="duePicker" formControlName="reviewDueDate">
          <mat-datepicker-toggle matSuffix [for]="duePicker"></mat-datepicker-toggle>
          <mat-datepicker #duePicker></mat-datepicker>
        </mat-form-field>

        <mat-form-field class="review-period-dialog__field">
          <mat-label>Reviewer Name</mat-label>
          <input matInput formControlName="reviewerName">
        </mat-form-field>

        <mat-checkbox formControlName="isCompleted" *ngIf="data">Completed</mat-checkbox>

        <mat-form-field class="review-period-dialog__field">
          <mat-label>Notes</mat-label>
          <textarea matInput formControlName="notes" rows="3"></textarea>
        </mat-form-field>
      </form>
    </mat-dialog-content>
    <mat-dialog-actions align="end">
      <button mat-button mat-dialog-close>Cancel</button>
      <button mat-raised-button color="primary" (click)="onSave()" [disabled]="!form.valid">Save</button>
    </mat-dialog-actions>
  `,
  styles: [`
    .review-period-dialog__form {
      display: flex;
      flex-direction: column;
      gap: 1rem;
      min-width: 400px;
    }

    .review-period-dialog__field {
      width: 100%;
    }
  `]
})
export class ReviewPeriodDialog {
  private fb = inject(FormBuilder);
  private dialogRef = inject(MatDialog);

  data?: ReviewPeriod;
  form: FormGroup;

  constructor() {
    this.form = this.fb.group({
      title: ['', Validators.required],
      startDate: ['', Validators.required],
      endDate: ['', Validators.required],
      reviewDueDate: [''],
      reviewerName: [''],
      isCompleted: [false],
      notes: ['']
    });

    if (this.data) {
      this.form.patchValue({
        title: this.data.title,
        startDate: new Date(this.data.startDate),
        endDate: new Date(this.data.endDate),
        reviewDueDate: this.data.reviewDueDate ? new Date(this.data.reviewDueDate) : null,
        reviewerName: this.data.reviewerName,
        isCompleted: this.data.isCompleted,
        notes: this.data.notes
      });
    }
  }

  onSave(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const result = {
        ...formValue,
        startDate: formValue.startDate.toISOString(),
        endDate: formValue.endDate.toISOString(),
        reviewDueDate: formValue.reviewDueDate ? formValue.reviewDueDate.toISOString() : undefined
      };

      if (this.data) {
        result.reviewPeriodId = this.data.reviewPeriodId;
      }
    }
  }
}

@Component({
  selector: 'app-review-periods',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule
  ],
  template: `
    <div class="review-periods">
      <div class="review-periods__header">
        <h1 class="review-periods__title">Review Periods</h1>
        <button mat-raised-button color="primary" (click)="openDialog()">
          <mat-icon>add</mat-icon>
          Add Review Period
        </button>
      </div>

      <div class="review-periods__table">
        <table mat-table [dataSource]="reviewPeriods$ | async" class="review-periods__mat-table">
          <ng-container matColumnDef="title">
            <th mat-header-cell *matHeaderCellDef>Title</th>
            <td mat-cell *matCellDef="let period">{{ period.title }}</td>
          </ng-container>

          <ng-container matColumnDef="startDate">
            <th mat-header-cell *matHeaderCellDef>Start Date</th>
            <td mat-cell *matCellDef="let period">{{ period.startDate | date:'short' }}</td>
          </ng-container>

          <ng-container matColumnDef="endDate">
            <th mat-header-cell *matHeaderCellDef>End Date</th>
            <td mat-cell *matCellDef="let period">{{ period.endDate | date:'short' }}</td>
          </ng-container>

          <ng-container matColumnDef="reviewDueDate">
            <th mat-header-cell *matHeaderCellDef>Review Due</th>
            <td mat-cell *matCellDef="let period">{{ period.reviewDueDate ? (period.reviewDueDate | date:'short') : '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="reviewerName">
            <th mat-header-cell *matHeaderCellDef>Reviewer</th>
            <td mat-cell *matCellDef="let period">{{ period.reviewerName || '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="isCompleted">
            <th mat-header-cell *matHeaderCellDef>Status</th>
            <td mat-cell *matCellDef="let period">
              <span [class.review-periods__status--completed]="period.isCompleted">
                {{ period.isCompleted ? 'Completed' : 'Active' }}
              </span>
            </td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let period">
              <button mat-icon-button (click)="openDialog(period)">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" (click)="delete(period.reviewPeriodId)">
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
    .review-periods {
      padding: 2rem;
    }

    .review-periods__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 2rem;
    }

    .review-periods__title {
      margin: 0;
      font-size: 2rem;
      font-weight: 500;
    }

    .review-periods__table {
      background: white;
      border-radius: 4px;
      overflow: hidden;
    }

    .review-periods__mat-table {
      width: 100%;
    }

    .review-periods__status--completed {
      color: green;
      font-weight: 500;
    }
  `]
})
export class ReviewPeriods implements OnInit {
  private reviewPeriodService = inject(ReviewPeriodService);
  private dialog = inject(MatDialog);

  reviewPeriods$ = this.reviewPeriodService.reviewPeriods$;
  displayedColumns = ['title', 'startDate', 'endDate', 'reviewDueDate', 'reviewerName', 'isCompleted', 'actions'];

  ngOnInit(): void {
    this.reviewPeriodService.getAll().subscribe();
  }

  openDialog(reviewPeriod?: ReviewPeriod): void {
    const dialogRef = this.dialog.open(ReviewPeriodDialog, {
      width: '500px',
      data: reviewPeriod
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (reviewPeriod) {
          this.reviewPeriodService.update(result as UpdateReviewPeriod).subscribe();
        } else {
          // Generate a dummy userId for demo purposes
          const createData: CreateReviewPeriod = {
            ...result,
            userId: '00000000-0000-0000-0000-000000000000'
          };
          this.reviewPeriodService.create(createData).subscribe();
        }
      }
    });
  }

  delete(id: string): void {
    if (confirm('Are you sure you want to delete this review period?')) {
      this.reviewPeriodService.delete(id).subscribe();
    }
  }
}
