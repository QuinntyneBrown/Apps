import { Component, OnInit, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule, MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatCardModule } from '@angular/material/card';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { TimeEntryService, ProjectService } from '../services';
import { TimeEntry, Project } from '../models';

@Component({
  selector: 'app-time-entries',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatCheckboxModule,
    MatCardModule,
    MatSnackBarModule
  ],
  template: `
    <div class="time-entries">
      <div class="time-entries__header">
        <h1 class="time-entries__title">Time Tracking</h1>
        <button mat-raised-button color="primary" (click)="openDialog()" class="time-entries__add-btn">
          <mat-icon>add</mat-icon>
          Add Time Entry
        </button>
      </div>

      <mat-card class="time-entries__card">
        <table mat-table [dataSource]="timeEntries$ | async" class="time-entries__table">
          <ng-container matColumnDef="workDate">
            <th mat-header-cell *matHeaderCellDef>Date</th>
            <td mat-cell *matCellDef="let entry">{{ entry.workDate | date:'shortDate' }}</td>
          </ng-container>

          <ng-container matColumnDef="description">
            <th mat-header-cell *matHeaderCellDef>Description</th>
            <td mat-cell *matCellDef="let entry">{{ entry.description }}</td>
          </ng-container>

          <ng-container matColumnDef="hours">
            <th mat-header-cell *matHeaderCellDef>Hours</th>
            <td mat-cell *matCellDef="let entry">{{ entry.hours }}</td>
          </ng-container>

          <ng-container matColumnDef="isBillable">
            <th mat-header-cell *matHeaderCellDef>Billable</th>
            <td mat-cell *matCellDef="let entry">
              <mat-icon [class.time-entries__icon--billable]="entry.isBillable">
                {{ entry.isBillable ? 'check_circle' : 'cancel' }}
              </mat-icon>
            </td>
          </ng-container>

          <ng-container matColumnDef="isInvoiced">
            <th mat-header-cell *matHeaderCellDef>Invoiced</th>
            <td mat-cell *matCellDef="let entry">
              <mat-icon [class.time-entries__icon--invoiced]="entry.isInvoiced">
                {{ entry.isInvoiced ? 'check_circle' : 'cancel' }}
              </mat-icon>
            </td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let entry">
              <button mat-icon-button color="primary" (click)="openDialog(entry)">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" (click)="deleteTimeEntry(entry)">
                <mat-icon>delete</mat-icon>
              </button>
            </td>
          </ng-container>

          <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
          <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
        </table>
      </mat-card>
    </div>
  `,
  styles: [`
    .time-entries {
      padding: 2rem;
    }

    .time-entries__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 2rem;
    }

    .time-entries__title {
      margin: 0;
      font-size: 2rem;
      font-weight: 500;
    }

    .time-entries__add-btn {
      display: flex;
      gap: 0.5rem;
    }

    .time-entries__card {
      padding: 0;
    }

    .time-entries__table {
      width: 100%;
    }

    .time-entries__icon--billable {
      color: #4caf50;
    }

    .time-entries__icon--invoiced {
      color: #2196f3;
    }
  `]
})
export class TimeEntries implements OnInit {
  displayedColumns: string[] = ['workDate', 'description', 'hours', 'isBillable', 'isInvoiced', 'actions'];
  timeEntries$ = this.timeEntryService.timeEntries$;
  private userId = '00000000-0000-0000-0000-000000000000';

  constructor(
    private timeEntryService: TimeEntryService,
    private projectService: ProjectService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.loadTimeEntries();
    this.loadProjects();
  }

  loadTimeEntries(): void {
    this.timeEntryService.getTimeEntries(this.userId).subscribe();
  }

  loadProjects(): void {
    this.projectService.getProjects(this.userId).subscribe();
  }

  openDialog(timeEntry?: TimeEntry): void {
    const dialogRef = this.dialog.open(TimeEntryDialog, {
      width: '600px',
      data: timeEntry
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (timeEntry) {
          this.updateTimeEntry(timeEntry.timeEntryId, result);
        } else {
          this.createTimeEntry(result);
        }
      }
    });
  }

  createTimeEntry(data: any): void {
    this.timeEntryService.createTimeEntry({ ...data, userId: this.userId }).subscribe({
      next: () => {
        this.snackBar.open('Time entry created successfully', 'Close', { duration: 3000 });
      },
      error: () => {
        this.snackBar.open('Error creating time entry', 'Close', { duration: 3000 });
      }
    });
  }

  updateTimeEntry(id: string, data: any): void {
    this.timeEntryService.updateTimeEntry(id, { ...data, timeEntryId: id, userId: this.userId }).subscribe({
      next: () => {
        this.snackBar.open('Time entry updated successfully', 'Close', { duration: 3000 });
      },
      error: () => {
        this.snackBar.open('Error updating time entry', 'Close', { duration: 3000 });
      }
    });
  }

  deleteTimeEntry(timeEntry: TimeEntry): void {
    if (confirm('Are you sure you want to delete this time entry?')) {
      this.timeEntryService.deleteTimeEntry(timeEntry.timeEntryId, this.userId).subscribe({
        next: () => {
          this.snackBar.open('Time entry deleted successfully', 'Close', { duration: 3000 });
        },
        error: () => {
          this.snackBar.open('Error deleting time entry', 'Close', { duration: 3000 });
        }
      });
    }
  }
}

@Component({
  selector: 'app-time-entry-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatCheckboxModule,
    MatButtonModule
  ],
  template: `
    <h2 mat-dialog-title>{{ data ? 'Edit Time Entry' : 'Add Time Entry' }}</h2>
    <mat-dialog-content>
      <form [formGroup]="form" class="time-entry-dialog__form">
        <mat-form-field appearance="outline" class="time-entry-dialog__field">
          <mat-label>Project</mat-label>
          <mat-select formControlName="projectId" required>
            <mat-option *ngFor="let project of projects$ | async" [value]="project.projectId">
              {{ project.name }}
            </mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field appearance="outline" class="time-entry-dialog__field">
          <mat-label>Work Date</mat-label>
          <input matInput [matDatepicker]="datePicker" formControlName="workDate" required>
          <mat-datepicker-toggle matSuffix [for]="datePicker"></mat-datepicker-toggle>
          <mat-datepicker #datePicker></mat-datepicker>
        </mat-form-field>

        <mat-form-field appearance="outline" class="time-entry-dialog__field">
          <mat-label>Hours</mat-label>
          <input matInput type="number" formControlName="hours" required step="0.25" min="0">
        </mat-form-field>

        <mat-form-field appearance="outline" class="time-entry-dialog__field">
          <mat-label>Description</mat-label>
          <textarea matInput formControlName="description" rows="4" required></textarea>
        </mat-form-field>

        <mat-checkbox formControlName="isBillable">Billable</mat-checkbox>

        <div *ngIf="data">
          <mat-checkbox formControlName="isInvoiced">Invoiced</mat-checkbox>
        </div>
      </form>
    </mat-dialog-content>
    <mat-dialog-actions align="end">
      <button mat-button mat-dialog-close>Cancel</button>
      <button mat-raised-button color="primary" [disabled]="!form.valid" (click)="save()">Save</button>
    </mat-dialog-actions>
  `,
  styles: [`
    .time-entry-dialog__form {
      display: flex;
      flex-direction: column;
      gap: 1rem;
      min-width: 500px;
    }

    .time-entry-dialog__field {
      width: 100%;
    }
  `]
})
export class TimeEntryDialog {
  form: FormGroup;
  projects$ = this.projectService.projects$;

  constructor(
    private fb: FormBuilder,
    private projectService: ProjectService,
    public dialogRef: MatDialogRef<TimeEntryDialog>,
    @Inject(MAT_DIALOG_DATA) public data: TimeEntry
  ) {
    this.form = this.fb.group({
      projectId: [data?.projectId || '', Validators.required],
      workDate: [data?.workDate || new Date(), Validators.required],
      hours: [data?.hours || 0, [Validators.required, Validators.min(0)]],
      description: [data?.description || '', Validators.required],
      isBillable: [data?.isBillable ?? true],
      isInvoiced: [data?.isInvoiced ?? false],
      invoiceId: [data?.invoiceId || null]
    });
  }

  save(): void {
    if (this.form.valid) {
      this.dialogRef.close(this.form.value);
    }
  }
}
