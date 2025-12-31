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
import { MatChipModule } from '@angular/material/chip';
import { ScheduleService, ActivityService } from '../services';
import { Schedule } from '../models';

@Component({
  selector: 'app-schedule-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatCheckboxModule
  ],
  template: `
    <h2 mat-dialog-title>{{ data ? 'Edit Schedule' : 'New Schedule' }}</h2>
    <mat-dialog-content>
      <form [formGroup]="form" class="schedule-form">
        <mat-form-field class="schedule-form__field">
          <mat-label>Activity</mat-label>
          <mat-select formControlName="activityId" required>
            <mat-option *ngFor="let activity of (activityService.activities$ | async)" [value]="activity.activityId">
              {{ activity.name }} ({{ activity.childName }})
            </mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field class="schedule-form__field">
          <mat-label>Event Type</mat-label>
          <input matInput formControlName="eventType" required>
        </mat-form-field>

        <mat-form-field class="schedule-form__field">
          <mat-label>Date & Time</mat-label>
          <input matInput type="datetime-local" formControlName="dateTime" required>
        </mat-form-field>

        <mat-form-field class="schedule-form__field">
          <mat-label>Duration (minutes)</mat-label>
          <input matInput type="number" formControlName="durationMinutes">
        </mat-form-field>

        <mat-form-field class="schedule-form__field schedule-form__field--full">
          <mat-label>Location</mat-label>
          <input matInput formControlName="location">
        </mat-form-field>

        <div class="schedule-form__field schedule-form__field--full">
          <mat-checkbox formControlName="isConfirmed">Confirmed</mat-checkbox>
        </div>

        <mat-form-field class="schedule-form__field schedule-form__field--full">
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
    .schedule-form {
      display: grid;
      grid-template-columns: repeat(2, 1fr);
      gap: 16px;
      padding: 16px 0;
    }

    .schedule-form__field {
      width: 100%;
    }

    .schedule-form__field--full {
      grid-column: 1 / -1;
    }
  `]
})
export class ScheduleDialog {
  private readonly fb = inject(FormBuilder);
  readonly activityService = inject(ActivityService);

  data?: Schedule;
  form: FormGroup;

  constructor() {
    this.form = this.fb.group({
      activityId: ['', Validators.required],
      eventType: ['', Validators.required],
      dateTime: ['', Validators.required],
      location: [''],
      durationMinutes: [''],
      notes: [''],
      isConfirmed: [false]
    });

    if (this.data) {
      this.form.patchValue({
        activityId: this.data.activityId,
        eventType: this.data.eventType,
        dateTime: this.data.dateTime ? this.formatDateTimeLocal(this.data.dateTime) : '',
        location: this.data.location,
        durationMinutes: this.data.durationMinutes,
        notes: this.data.notes,
        isConfirmed: this.data.isConfirmed
      });
    }
  }

  formatDateTimeLocal(dateString: string): string {
    const date = new Date(dateString);
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const day = String(date.getDate()).padStart(2, '0');
    const hours = String(date.getHours()).padStart(2, '0');
    const minutes = String(date.getMinutes()).padStart(2, '0');
    return `${year}-${month}-${day}T${hours}:${minutes}`;
  }

  onSave(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const result = {
        ...formValue,
        dateTime: formValue.dateTime ? new Date(formValue.dateTime).toISOString() : null
      };
    }
  }
}

@Component({
  selector: 'app-schedules',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatChipModule
  ],
  template: `
    <div class="schedules">
      <div class="schedules__header">
        <h1 class="schedules__title">Schedules</h1>
        <button mat-raised-button color="primary" (click)="openDialog()">
          <mat-icon>add</mat-icon>
          Add Schedule
        </button>
      </div>

      <div class="schedules__table-container">
        <table mat-table [dataSource]="(scheduleService.schedules$ | async) || []" class="schedules__table">
          <ng-container matColumnDef="eventType">
            <th mat-header-cell *matHeaderCellDef>Event Type</th>
            <td mat-cell *matCellDef="let schedule">{{ schedule.eventType }}</td>
          </ng-container>

          <ng-container matColumnDef="dateTime">
            <th mat-header-cell *matHeaderCellDef>Date & Time</th>
            <td mat-cell *matCellDef="let schedule">{{ schedule.dateTime | date:'medium' }}</td>
          </ng-container>

          <ng-container matColumnDef="location">
            <th mat-header-cell *matHeaderCellDef>Location</th>
            <td mat-cell *matCellDef="let schedule">{{ schedule.location || '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="durationMinutes">
            <th mat-header-cell *matHeaderCellDef>Duration</th>
            <td mat-cell *matCellDef="let schedule">
              {{ schedule.durationMinutes ? schedule.durationMinutes + ' min' : '-' }}
            </td>
          </ng-container>

          <ng-container matColumnDef="isConfirmed">
            <th mat-header-cell *matHeaderCellDef>Status</th>
            <td mat-cell *matCellDef="let schedule">
              <mat-chip [class.schedules__chip--confirmed]="schedule.isConfirmed">
                {{ schedule.isConfirmed ? 'Confirmed' : 'Pending' }}
              </mat-chip>
            </td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let schedule">
              <button mat-icon-button (click)="openDialog(schedule)" class="schedules__action-btn">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" (click)="deleteSchedule(schedule.scheduleId)" class="schedules__action-btn">
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
    .schedules {
      padding: 24px;
    }

    .schedules__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 24px;
    }

    .schedules__title {
      margin: 0;
      font-size: 32px;
      font-weight: 500;
    }

    .schedules__table-container {
      overflow-x: auto;
    }

    .schedules__table {
      width: 100%;
    }

    .schedules__action-btn {
      margin-right: 8px;
    }

    .schedules__chip--confirmed {
      background-color: #4caf50;
      color: white;
    }
  `]
})
export class Schedules implements OnInit {
  readonly scheduleService = inject(ScheduleService);
  readonly activityService = inject(ActivityService);
  private readonly dialog = inject(MatDialog);

  displayedColumns = ['eventType', 'dateTime', 'location', 'durationMinutes', 'isConfirmed', 'actions'];

  ngOnInit(): void {
    this.scheduleService.loadSchedules().subscribe();
    this.activityService.loadActivities().subscribe();
  }

  openDialog(schedule?: Schedule): void {
    console.log('Open dialog for schedule:', schedule);
  }

  deleteSchedule(id: string): void {
    if (confirm('Are you sure you want to delete this schedule?')) {
      this.scheduleService.deleteSchedule(id).subscribe();
    }
  }
}
