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
import { AchievementService, ReviewPeriodService } from '../services';
import { Achievement, CreateAchievement, UpdateAchievement } from '../models';

@Component({
  selector: 'app-achievement-dialog',
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
    <h2 mat-dialog-title>{{ data ? 'Edit' : 'Add' }} Achievement</h2>
    <mat-dialog-content>
      <form [formGroup]="form" class="achievement-dialog__form">
        <mat-form-field class="achievement-dialog__field">
          <mat-label>Review Period</mat-label>
          <mat-select formControlName="reviewPeriodId" required>
            <mat-option *ngFor="let period of reviewPeriods$ | async" [value]="period.reviewPeriodId">
              {{ period.title }}
            </mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field class="achievement-dialog__field">
          <mat-label>Title</mat-label>
          <input matInput formControlName="title" required>
        </mat-form-field>

        <mat-form-field class="achievement-dialog__field">
          <mat-label>Description</mat-label>
          <textarea matInput formControlName="description" rows="3" required></textarea>
        </mat-form-field>

        <mat-form-field class="achievement-dialog__field">
          <mat-label>Achieved Date</mat-label>
          <input matInput [matDatepicker]="achievedPicker" formControlName="achievedDate" required>
          <mat-datepicker-toggle matSuffix [for]="achievedPicker"></mat-datepicker-toggle>
          <mat-datepicker #achievedPicker></mat-datepicker>
        </mat-form-field>

        <mat-form-field class="achievement-dialog__field">
          <mat-label>Impact</mat-label>
          <textarea matInput formControlName="impact" rows="2"></textarea>
        </mat-form-field>

        <mat-form-field class="achievement-dialog__field">
          <mat-label>Category</mat-label>
          <input matInput formControlName="category">
        </mat-form-field>

        <mat-checkbox formControlName="isKeyAchievement">Key Achievement</mat-checkbox>
      </form>
    </mat-dialog-content>
    <mat-dialog-actions align="end">
      <button mat-button mat-dialog-close>Cancel</button>
      <button mat-raised-button color="primary" (click)="onSave()" [disabled]="!form.valid">Save</button>
    </mat-dialog-actions>
  `,
  styles: [`
    .achievement-dialog__form {
      display: flex;
      flex-direction: column;
      gap: 1rem;
      min-width: 400px;
    }

    .achievement-dialog__field {
      width: 100%;
    }
  `]
})
export class AchievementDialog {
  private fb = inject(FormBuilder);
  private dialogRef = inject(MatDialog);
  private reviewPeriodService = inject(ReviewPeriodService);

  data?: Achievement;
  form: FormGroup;
  reviewPeriods$ = this.reviewPeriodService.reviewPeriods$;

  constructor() {
    this.form = this.fb.group({
      reviewPeriodId: ['', Validators.required],
      title: ['', Validators.required],
      description: ['', Validators.required],
      achievedDate: ['', Validators.required],
      impact: [''],
      category: [''],
      isKeyAchievement: [false]
    });

    if (this.data) {
      this.form.patchValue({
        reviewPeriodId: this.data.reviewPeriodId,
        title: this.data.title,
        description: this.data.description,
        achievedDate: new Date(this.data.achievedDate),
        impact: this.data.impact,
        category: this.data.category,
        isKeyAchievement: this.data.isKeyAchievement
      });
    }
  }

  onSave(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const result = {
        ...formValue,
        achievedDate: formValue.achievedDate.toISOString()
      };

      if (this.data) {
        result.achievementId = this.data.achievementId;
      }
    }
  }
}

@Component({
  selector: 'app-achievements',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule
  ],
  template: `
    <div class="achievements">
      <div class="achievements__header">
        <h1 class="achievements__title">Achievements</h1>
        <button mat-raised-button color="primary" (click)="openDialog()">
          <mat-icon>add</mat-icon>
          Add Achievement
        </button>
      </div>

      <div class="achievements__table">
        <table mat-table [dataSource]="achievements$ | async" class="achievements__mat-table">
          <ng-container matColumnDef="title">
            <th mat-header-cell *matHeaderCellDef>Title</th>
            <td mat-cell *matCellDef="let achievement">{{ achievement.title }}</td>
          </ng-container>

          <ng-container matColumnDef="description">
            <th mat-header-cell *matHeaderCellDef>Description</th>
            <td mat-cell *matCellDef="let achievement">{{ achievement.description }}</td>
          </ng-container>

          <ng-container matColumnDef="achievedDate">
            <th mat-header-cell *matHeaderCellDef>Achieved Date</th>
            <td mat-cell *matCellDef="let achievement">{{ achievement.achievedDate | date:'short' }}</td>
          </ng-container>

          <ng-container matColumnDef="category">
            <th mat-header-cell *matHeaderCellDef>Category</th>
            <td mat-cell *matCellDef="let achievement">{{ achievement.category || '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="isKeyAchievement">
            <th mat-header-cell *matHeaderCellDef>Key</th>
            <td mat-cell *matCellDef="let achievement">
              <mat-icon *ngIf="achievement.isKeyAchievement" class="achievements__key-icon">star</mat-icon>
            </td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let achievement">
              <button mat-icon-button (click)="openDialog(achievement)">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" (click)="delete(achievement.achievementId)">
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
    .achievements {
      padding: 2rem;
    }

    .achievements__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 2rem;
    }

    .achievements__title {
      margin: 0;
      font-size: 2rem;
      font-weight: 500;
    }

    .achievements__table {
      background: white;
      border-radius: 4px;
      overflow: hidden;
    }

    .achievements__mat-table {
      width: 100%;
    }

    .achievements__key-icon {
      color: gold;
    }
  `]
})
export class Achievements implements OnInit {
  private achievementService = inject(AchievementService);
  private reviewPeriodService = inject(ReviewPeriodService);
  private dialog = inject(MatDialog);

  achievements$ = this.achievementService.achievements$;
  displayedColumns = ['title', 'description', 'achievedDate', 'category', 'isKeyAchievement', 'actions'];

  ngOnInit(): void {
    this.achievementService.getAll().subscribe();
    this.reviewPeriodService.getAll().subscribe();
  }

  openDialog(achievement?: Achievement): void {
    const dialogRef = this.dialog.open(AchievementDialog, {
      width: '500px',
      data: achievement
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (achievement) {
          this.achievementService.update(result as UpdateAchievement).subscribe();
        } else {
          // Generate a dummy userId for demo purposes
          const createData: CreateAchievement = {
            ...result,
            userId: '00000000-0000-0000-0000-000000000000'
          };
          this.achievementService.create(createData).subscribe();
        }
      }
    });
  }

  delete(id: string): void {
    if (confirm('Are you sure you want to delete this achievement?')) {
      this.achievementService.delete(id).subscribe();
    }
  }
}
