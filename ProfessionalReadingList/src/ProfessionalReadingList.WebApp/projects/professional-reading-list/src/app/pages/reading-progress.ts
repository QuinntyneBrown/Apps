import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatSliderModule } from '@angular/material/slider';
import { ReadingProgressService, ResourceService } from '../services';
import { ReadingProgress, Resource } from '../models';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-reading-progress-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatSliderModule
  ],
  template: `
    <h2 mat-dialog-title>{{ data ? 'Edit Reading Progress' : 'Add Reading Progress' }}</h2>
    <mat-dialog-content>
      <form [formGroup]="form" class="progress-dialog__form">
        <mat-form-field appearance="outline" class="progress-dialog__field progress-dialog__field--full">
          <mat-label>Resource</mat-label>
          <mat-select formControlName="resourceId" required>
            <mat-option *ngFor="let resource of resources$ | async" [value]="resource.resourceId">
              {{ resource.title }}
            </mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field appearance="outline" class="progress-dialog__field">
          <mat-label>Status</mat-label>
          <mat-select formControlName="status" required>
            <mat-option value="Not Started">Not Started</mat-option>
            <mat-option value="In Progress">In Progress</mat-option>
            <mat-option value="Completed">Completed</mat-option>
            <mat-option value="On Hold">On Hold</mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field appearance="outline" class="progress-dialog__field">
          <mat-label>Current Page</mat-label>
          <input matInput formControlName="currentPage" type="number">
        </mat-form-field>

        <div class="progress-dialog__field progress-dialog__field--full">
          <label class="progress-dialog__label">Progress Percentage: {{ form.value.progressPercentage }}%</label>
          <mat-slider min="0" max="100" step="5" showTickMarks discrete class="progress-dialog__slider">
            <input matSliderThumb formControlName="progressPercentage">
          </mat-slider>
        </div>

        <mat-form-field appearance="outline" class="progress-dialog__field">
          <mat-label>Rating (1-5)</mat-label>
          <input matInput formControlName="rating" type="number" min="1" max="5">
        </mat-form-field>

        <mat-form-field appearance="outline" class="progress-dialog__field progress-dialog__field--full">
          <mat-label>Review</mat-label>
          <textarea matInput formControlName="review" rows="4"></textarea>
        </mat-form-field>
      </form>
    </mat-dialog-content>
    <mat-dialog-actions align="end">
      <button mat-button mat-dialog-close>Cancel</button>
      <button mat-raised-button color="primary" (click)="save()" [disabled]="!form.valid">
        Save
      </button>
    </mat-dialog-actions>
  `,
  styles: [`
    .progress-dialog__form {
      display: grid;
      grid-template-columns: 1fr 1fr;
      gap: 16px;
      min-width: 600px;
      padding: 16px 0;
    }

    .progress-dialog__field {
      width: 100%;
    }

    .progress-dialog__field--full {
      grid-column: 1 / -1;
    }

    .progress-dialog__label {
      display: block;
      font-size: 14px;
      color: rgba(0, 0, 0, 0.6);
      margin-bottom: 8px;
    }

    .progress-dialog__slider {
      width: 100%;
    }
  `]
})
export class ReadingProgressDialog implements OnInit {
  private fb = inject(FormBuilder);
  private dialogRef = inject(MatDialog);
  private resourceService = inject(ResourceService);

  data: ReadingProgress | null = null;
  resources$: Observable<Resource[]> = this.resourceService.resources$;

  form = this.fb.group({
    resourceId: ['', Validators.required],
    status: ['Not Started', Validators.required],
    currentPage: [null as number | null],
    progressPercentage: [0, Validators.required],
    rating: [null as number | null],
    review: ['']
  });

  ngOnInit() {
    if (this.data) {
      this.form.patchValue({
        resourceId: this.data.resourceId,
        status: this.data.status,
        currentPage: this.data.currentPage || null,
        progressPercentage: this.data.progressPercentage,
        rating: this.data.rating || null,
        review: this.data.review || ''
      });
    }
  }

  save() {
    if (this.form.valid) {
      this.dialogRef.closeAll();
    }
  }
}

@Component({
  selector: 'app-reading-progress',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule
  ],
  template: `
    <div class="reading-progress">
      <div class="reading-progress__header">
        <h1 class="reading-progress__title">Reading Progress</h1>
        <button mat-raised-button color="primary" (click)="openDialog()">
          <mat-icon>add</mat-icon>
          Add Progress
        </button>
      </div>

      <div class="reading-progress__table-container">
        <table mat-table [dataSource]="progress$ | async" class="reading-progress__table">
          <ng-container matColumnDef="status">
            <th mat-header-cell *matHeaderCellDef>Status</th>
            <td mat-cell *matCellDef="let progress">
              <span [class]="'reading-progress__status reading-progress__status--' + getStatusClass(progress.status)">
                {{ progress.status }}
              </span>
            </td>
          </ng-container>

          <ng-container matColumnDef="currentPage">
            <th mat-header-cell *matHeaderCellDef>Current Page</th>
            <td mat-cell *matCellDef="let progress">{{ progress.currentPage || '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="progressPercentage">
            <th mat-header-cell *matHeaderCellDef>Progress</th>
            <td mat-cell *matCellDef="let progress">{{ progress.progressPercentage }}%</td>
          </ng-container>

          <ng-container matColumnDef="rating">
            <th mat-header-cell *matHeaderCellDef>Rating</th>
            <td mat-cell *matCellDef="let progress">
              <span *ngIf="progress.rating">{{ progress.rating }}/5</span>
              <span *ngIf="!progress.rating">-</span>
            </td>
          </ng-container>

          <ng-container matColumnDef="startDate">
            <th mat-header-cell *matHeaderCellDef>Start Date</th>
            <td mat-cell *matCellDef="let progress">{{ progress.startDate | date }}</td>
          </ng-container>

          <ng-container matColumnDef="completionDate">
            <th mat-header-cell *matHeaderCellDef>Completion Date</th>
            <td mat-cell *matCellDef="let progress">{{ progress.completionDate | date }}</td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let progress">
              <button mat-icon-button (click)="openDialog(progress)">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" (click)="delete(progress.readingProgressId)">
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
    .reading-progress {
      padding: 24px;
    }

    .reading-progress__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 24px;
    }

    .reading-progress__title {
      margin: 0;
      font-size: 32px;
      font-weight: 400;
    }

    .reading-progress__table-container {
      overflow-x: auto;
    }

    .reading-progress__table {
      width: 100%;
    }

    .reading-progress__status {
      padding: 4px 12px;
      border-radius: 12px;
      font-size: 12px;
      font-weight: 500;
    }

    .reading-progress__status--not-started {
      background-color: #e0e0e0;
      color: #424242;
    }

    .reading-progress__status--in-progress {
      background-color: #bbdefb;
      color: #1565c0;
    }

    .reading-progress__status--completed {
      background-color: #c8e6c9;
      color: #2e7d32;
    }

    .reading-progress__status--on-hold {
      background-color: #fff9c4;
      color: #f57f17;
    }
  `]
})
export class ReadingProgressPage implements OnInit {
  private progressService = inject(ReadingProgressService);
  private dialog = inject(MatDialog);

  progress$: Observable<ReadingProgress[]> = this.progressService.progress$;
  displayedColumns = ['status', 'currentPage', 'progressPercentage', 'rating', 'startDate', 'completionDate', 'actions'];

  ngOnInit() {
    this.progressService.getAll().subscribe();
  }

  getStatusClass(status: string): string {
    return status.toLowerCase().replace(/\s+/g, '-');
  }

  openDialog(progress?: ReadingProgress) {
    const dialogRef = this.dialog.open(ReadingProgressDialog, {
      width: '700px',
      data: progress
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (progress) {
          this.progressService.update(progress.readingProgressId, {
            readingProgressId: progress.readingProgressId,
            ...result
          }).subscribe();
        } else {
          this.progressService.create({
            userId: '00000000-0000-0000-0000-000000000000',
            ...result
          }).subscribe();
        }
      }
    });
  }

  delete(id: string) {
    if (confirm('Are you sure you want to delete this reading progress?')) {
      this.progressService.delete(id).subscribe();
    }
  }
}
