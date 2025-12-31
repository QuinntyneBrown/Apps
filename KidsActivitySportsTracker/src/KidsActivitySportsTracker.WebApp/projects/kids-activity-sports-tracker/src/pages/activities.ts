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
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { ActivityService } from '../services';
import { Activity, ActivityType, ActivityTypeLabels, CreateActivity, UpdateActivity } from '../models';

@Component({
  selector: 'app-activity-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  template: `
    <h2 mat-dialog-title>{{ data ? 'Edit Activity' : 'New Activity' }}</h2>
    <mat-dialog-content>
      <form [formGroup]="form" class="activity-form">
        <mat-form-field class="activity-form__field">
          <mat-label>Child Name</mat-label>
          <input matInput formControlName="childName" required>
        </mat-form-field>

        <mat-form-field class="activity-form__field">
          <mat-label>Activity Name</mat-label>
          <input matInput formControlName="name" required>
        </mat-form-field>

        <mat-form-field class="activity-form__field">
          <mat-label>Activity Type</mat-label>
          <mat-select formControlName="activityType" required>
            <mat-option *ngFor="let type of activityTypes" [value]="type.value">
              {{ type.label }}
            </mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field class="activity-form__field">
          <mat-label>Organization</mat-label>
          <input matInput formControlName="organization">
        </mat-form-field>

        <mat-form-field class="activity-form__field">
          <mat-label>Coach Name</mat-label>
          <input matInput formControlName="coachName">
        </mat-form-field>

        <mat-form-field class="activity-form__field">
          <mat-label>Coach Contact</mat-label>
          <input matInput formControlName="coachContact">
        </mat-form-field>

        <mat-form-field class="activity-form__field">
          <mat-label>Season</mat-label>
          <input matInput formControlName="season">
        </mat-form-field>

        <mat-form-field class="activity-form__field">
          <mat-label>Start Date</mat-label>
          <input matInput [matDatepicker]="startPicker" formControlName="startDate">
          <mat-datepicker-toggle matIconSuffix [for]="startPicker"></mat-datepicker-toggle>
          <mat-datepicker #startPicker></mat-datepicker>
        </mat-form-field>

        <mat-form-field class="activity-form__field">
          <mat-label>End Date</mat-label>
          <input matInput [matDatepicker]="endPicker" formControlName="endDate">
          <mat-datepicker-toggle matIconSuffix [for]="endPicker"></mat-datepicker-toggle>
          <mat-datepicker #endPicker></mat-datepicker>
        </mat-form-field>

        <mat-form-field class="activity-form__field activity-form__field--full">
          <mat-label>Notes</mat-label>
          <textarea matInput formControlName="notes" rows="3"></textarea>
        </mat-form-field>
      </form>
    </mat-dialog-content>
    <mat-dialog-actions align="end">
      <button mat-button mat-dialog-close>Cancel</button>
      <button mat-raised-button color="primary" [disabled]="!form.valid" (click)="onSave()">
        Save
      </button>
    </mat-dialog-actions>
  `,
  styles: [`
    .activity-form {
      display: grid;
      grid-template-columns: repeat(2, 1fr);
      gap: 16px;
      padding: 16px 0;
    }

    .activity-form__field {
      width: 100%;
    }

    .activity-form__field--full {
      grid-column: 1 / -1;
    }
  `]
})
export class ActivityDialog {
  private readonly fb = inject(FormBuilder);
  private readonly dialogRef = inject(MatDialog);

  data?: Activity;
  form: FormGroup;
  activityTypes = Object.keys(ActivityType)
    .filter(key => !isNaN(Number(ActivityType[key as keyof typeof ActivityType])))
    .map(key => ({
      value: ActivityType[key as keyof typeof ActivityType],
      label: ActivityTypeLabels[ActivityType[key as keyof typeof ActivityType]]
    }));

  constructor() {
    this.form = this.fb.group({
      childName: ['', Validators.required],
      name: ['', Validators.required],
      activityType: [ActivityType.TeamSports, Validators.required],
      organization: [''],
      coachName: [''],
      coachContact: [''],
      season: [''],
      startDate: [''],
      endDate: [''],
      notes: ['']
    });

    if (this.data) {
      this.form.patchValue({
        childName: this.data.childName,
        name: this.data.name,
        activityType: this.data.activityType,
        organization: this.data.organization,
        coachName: this.data.coachName,
        coachContact: this.data.coachContact,
        season: this.data.season,
        startDate: this.data.startDate ? new Date(this.data.startDate) : null,
        endDate: this.data.endDate ? new Date(this.data.endDate) : null,
        notes: this.data.notes
      });
    }
  }

  onSave(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const result = {
        ...formValue,
        startDate: formValue.startDate ? new Date(formValue.startDate).toISOString() : null,
        endDate: formValue.endDate ? new Date(formValue.endDate).toISOString() : null
      };

      // Note: In a real dialog, this would use MatDialogRef
      // For simplicity, we're just closing here
    }
  }
}

@Component({
  selector: 'app-activities',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule
  ],
  template: `
    <div class="activities">
      <div class="activities__header">
        <h1 class="activities__title">Activities</h1>
        <button mat-raised-button color="primary" (click)="openDialog()">
          <mat-icon>add</mat-icon>
          Add Activity
        </button>
      </div>

      <div class="activities__table-container">
        <table mat-table [dataSource]="(activityService.activities$ | async) || []" class="activities__table">
          <ng-container matColumnDef="childName">
            <th mat-header-cell *matHeaderCellDef>Child Name</th>
            <td mat-cell *matCellDef="let activity">{{ activity.childName }}</td>
          </ng-container>

          <ng-container matColumnDef="name">
            <th mat-header-cell *matHeaderCellDef>Activity</th>
            <td mat-cell *matCellDef="let activity">{{ activity.name }}</td>
          </ng-container>

          <ng-container matColumnDef="activityType">
            <th mat-header-cell *matHeaderCellDef>Type</th>
            <td mat-cell *matCellDef="let activity">{{ getActivityTypeLabel(activity.activityType) }}</td>
          </ng-container>

          <ng-container matColumnDef="organization">
            <th mat-header-cell *matHeaderCellDef>Organization</th>
            <td mat-cell *matCellDef="let activity">{{ activity.organization || '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="coachName">
            <th mat-header-cell *matHeaderCellDef>Coach</th>
            <td mat-cell *matCellDef="let activity">{{ activity.coachName || '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="season">
            <th mat-header-cell *matHeaderCellDef>Season</th>
            <td mat-cell *matCellDef="let activity">{{ activity.season || '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let activity">
              <button mat-icon-button (click)="openDialog(activity)" class="activities__action-btn">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" (click)="deleteActivity(activity.activityId)" class="activities__action-btn">
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
    .activities {
      padding: 24px;
    }

    .activities__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 24px;
    }

    .activities__title {
      margin: 0;
      font-size: 32px;
      font-weight: 500;
    }

    .activities__table-container {
      overflow-x: auto;
    }

    .activities__table {
      width: 100%;
    }

    .activities__action-btn {
      margin-right: 8px;
    }
  `]
})
export class Activities implements OnInit {
  readonly activityService = inject(ActivityService);
  private readonly dialog = inject(MatDialog);

  displayedColumns = ['childName', 'name', 'activityType', 'organization', 'coachName', 'season', 'actions'];

  ngOnInit(): void {
    this.activityService.loadActivities().subscribe();
  }

  getActivityTypeLabel(type: ActivityType): string {
    return ActivityTypeLabels[type];
  }

  openDialog(activity?: Activity): void {
    // Note: In a production app, this would properly open MatDialog with ActivityDialog component
    // and pass data, then handle the result
    console.log('Open dialog for activity:', activity);
  }

  deleteActivity(id: string): void {
    if (confirm('Are you sure you want to delete this activity?')) {
      this.activityService.deleteActivity(id).subscribe();
    }
  }
}
