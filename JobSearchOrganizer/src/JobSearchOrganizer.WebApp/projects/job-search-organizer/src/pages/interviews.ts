import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators, FormControl } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatCardModule } from '@angular/material/card';
import { MatChipModule } from '@angular/material/chip';
import { InterviewService, ApplicationService } from '../services';
import { Interview, CreateInterview, UpdateInterview } from '../models';

@Component({
  selector: 'app-interview-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatCheckboxModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatButtonModule,
    MatChipModule,
    MatIconModule
  ],
  template: `
    <h2 mat-dialog-title>{{ data ? 'Edit Interview' : 'Add Interview' }}</h2>
    <mat-dialog-content>
      <form [formGroup]="form" class="interview-dialog__form">
        <mat-form-field class="interview-dialog__field">
          <mat-label>Application</mat-label>
          <mat-select formControlName="applicationId" required>
            <mat-option *ngFor="let app of (applicationService.applications$ | async)" [value]="app.applicationId">
              {{ app.jobTitle }}
            </mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field class="interview-dialog__field">
          <mat-label>Interview Type</mat-label>
          <input matInput formControlName="interviewType" required placeholder="e.g., Phone Screen, Technical, Final">
        </mat-form-field>

        <mat-form-field class="interview-dialog__field">
          <mat-label>Scheduled Date & Time</mat-label>
          <input matInput [matDatepicker]="picker" formControlName="scheduledDateTime" required>
          <mat-datepicker-toggle matSuffix [for]="picker"></mat-datepicker-toggle>
          <mat-datepicker #picker></mat-datepicker>
        </mat-form-field>

        <mat-form-field class="interview-dialog__field">
          <mat-label>Duration (minutes)</mat-label>
          <input matInput type="number" formControlName="durationMinutes">
        </mat-form-field>

        <div class="interview-dialog__field">
          <label class="interview-dialog__label">Interviewers</label>
          <div class="interview-dialog__chips">
            <mat-chip-set>
              <mat-chip *ngFor="let interviewer of interviewers; let i = index" [removable]="true" (removed)="removeInterviewer(i)">
                {{ interviewer }}
                <button matChipRemove>
                  <mat-icon>cancel</mat-icon>
                </button>
              </mat-chip>
            </mat-chip-set>
          </div>
          <div class="interview-dialog__add-interviewer">
            <mat-form-field class="interview-dialog__interviewer-input">
              <mat-label>Add Interviewer</mat-label>
              <input matInput [formControl]="interviewerInput">
            </mat-form-field>
            <button mat-icon-button (click)="addInterviewer()" type="button">
              <mat-icon>add</mat-icon>
            </button>
          </div>
        </div>

        <mat-form-field class="interview-dialog__field">
          <mat-label>Location</mat-label>
          <input matInput formControlName="location" placeholder="e.g., Zoom, Office, Phone">
        </mat-form-field>

        <mat-form-field class="interview-dialog__field">
          <mat-label>Preparation Notes</mat-label>
          <textarea matInput formControlName="preparationNotes" rows="3"></textarea>
        </mat-form-field>

        <mat-form-field class="interview-dialog__field" *ngIf="data">
          <mat-label>Feedback</mat-label>
          <textarea matInput formControlName="feedback" rows="3"></textarea>
        </mat-form-field>

        <mat-checkbox formControlName="isCompleted" *ngIf="data">
          Mark as Completed
        </mat-checkbox>
      </form>
    </mat-dialog-content>
    <mat-dialog-actions align="end">
      <button mat-button mat-dialog-close>Cancel</button>
      <button mat-raised-button color="primary" [disabled]="!form.valid" (click)="save()">
        Save
      </button>
    </mat-dialog-actions>
  `,
  styles: [`
    .interview-dialog__form {
      display: flex;
      flex-direction: column;
      gap: 16px;
      min-width: 500px;
      padding: 16px 0;
    }

    .interview-dialog__field {
      width: 100%;
    }

    .interview-dialog__label {
      display: block;
      font-size: 14px;
      color: rgba(0, 0, 0, 0.6);
      margin-bottom: 8px;
    }

    .interview-dialog__chips {
      margin-bottom: 8px;
    }

    .interview-dialog__add-interviewer {
      display: flex;
      gap: 8px;
      align-items: center;
    }

    .interview-dialog__interviewer-input {
      flex: 1;
    }
  `]
})
export class InterviewDialog implements OnInit {
  private fb = inject(FormBuilder);
  private dialogRef = inject(MatDialog);
  applicationService = inject(ApplicationService);

  data?: Interview;
  form: FormGroup;
  interviewers: string[] = [];
  interviewerInput = new FormControl('');

  constructor() {
    this.form = this.fb.group({
      applicationId: ['', Validators.required],
      interviewType: ['', Validators.required],
      scheduledDateTime: [new Date(), Validators.required],
      durationMinutes: [null],
      location: [''],
      preparationNotes: [''],
      feedback: [''],
      isCompleted: [false]
    });

    if (this.data) {
      this.interviewers = [...this.data.interviewers];
      this.form.patchValue({
        ...this.data,
        scheduledDateTime: new Date(this.data.scheduledDateTime)
      });
    }
  }

  ngOnInit(): void {
    this.applicationService.getApplications().subscribe();
  }

  addInterviewer(): void {
    const value = this.interviewerInput.value?.trim();
    if (value && !this.interviewers.includes(value)) {
      this.interviewers.push(value);
      this.interviewerInput.setValue('');
    }
  }

  removeInterviewer(index: number): void {
    this.interviewers.splice(index, 1);
  }

  save(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const result = {
        ...formValue,
        scheduledDateTime: formValue.scheduledDateTime.toISOString(),
        interviewers: this.interviewers
      };
      this.dialogRef.closeAll();
    }
  }
}

@Component({
  selector: 'app-interviews',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatChipModule
  ],
  template: `
    <div class="interviews">
      <div class="interviews__header">
        <h1 class="interviews__title">Interviews</h1>
        <button mat-raised-button color="primary" (click)="openDialog()">
          <mat-icon>add</mat-icon>
          Add Interview
        </button>
      </div>

      <mat-card class="interviews__card">
        <table mat-table [dataSource]="(interviewService.interviews$ | async) || []" class="interviews__table">
          <ng-container matColumnDef="application">
            <th mat-header-cell *matHeaderCellDef>Application</th>
            <td mat-cell *matCellDef="let interview">{{ getApplicationTitle(interview.applicationId) }}</td>
          </ng-container>

          <ng-container matColumnDef="interviewType">
            <th mat-header-cell *matHeaderCellDef>Type</th>
            <td mat-cell *matCellDef="let interview">{{ interview.interviewType }}</td>
          </ng-container>

          <ng-container matColumnDef="scheduledDateTime">
            <th mat-header-cell *matHeaderCellDef>Scheduled</th>
            <td mat-cell *matCellDef="let interview">{{ interview.scheduledDateTime | date:'medium' }}</td>
          </ng-container>

          <ng-container matColumnDef="duration">
            <th mat-header-cell *matHeaderCellDef>Duration</th>
            <td mat-cell *matCellDef="let interview">
              {{ interview.durationMinutes ? interview.durationMinutes + ' min' : '-' }}
            </td>
          </ng-container>

          <ng-container matColumnDef="location">
            <th mat-header-cell *matHeaderCellDef>Location</th>
            <td mat-cell *matCellDef="let interview">{{ interview.location || '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="interviewers">
            <th mat-header-cell *matHeaderCellDef>Interviewers</th>
            <td mat-cell *matCellDef="let interview">
              <div class="interviews__interviewers">
                <mat-chip *ngFor="let interviewer of interview.interviewers">
                  {{ interviewer }}
                </mat-chip>
                <span *ngIf="interview.interviewers.length === 0">-</span>
              </div>
            </td>
          </ng-container>

          <ng-container matColumnDef="status">
            <th mat-header-cell *matHeaderCellDef>Status</th>
            <td mat-cell *matCellDef="let interview">
              <mat-chip [class]="interview.isCompleted ? 'interviews__chip--completed' : 'interviews__chip--upcoming'">
                {{ interview.isCompleted ? 'Completed' : 'Upcoming' }}
              </mat-chip>
            </td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let interview">
              <button mat-icon-button (click)="openDialog(interview)">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" (click)="deleteInterview(interview.interviewId)">
                <mat-icon>delete</mat-icon>
              </button>
            </td>
          </ng-container>

          <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
          <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
        </table>

        <div *ngIf="(interviewService.interviews$ | async)?.length === 0" class="interviews__empty">
          <p>No interviews found. Add your first interview to get started!</p>
        </div>
      </mat-card>
    </div>
  `,
  styles: [`
    .interviews {
      padding: 24px;
    }

    .interviews__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 24px;
    }

    .interviews__title {
      margin: 0;
      font-size: 32px;
      font-weight: 500;
    }

    .interviews__card {
      overflow-x: auto;
    }

    .interviews__table {
      width: 100%;
    }

    .interviews__interviewers {
      display: flex;
      flex-wrap: wrap;
      gap: 4px;
    }

    .interviews__chip--upcoming {
      background-color: #2196f3;
      color: white;
    }

    .interviews__chip--completed {
      background-color: #4caf50;
      color: white;
    }

    .interviews__empty {
      padding: 48px;
      text-align: center;
      color: rgba(0, 0, 0, 0.6);
    }
  `]
})
export class Interviews implements OnInit {
  interviewService = inject(InterviewService);
  applicationService = inject(ApplicationService);
  private dialog = inject(MatDialog);

  displayedColumns = ['application', 'interviewType', 'scheduledDateTime', 'duration', 'location', 'interviewers', 'status', 'actions'];

  ngOnInit(): void {
    this.interviewService.getInterviews().subscribe();
    this.applicationService.getApplications().subscribe();
  }

  getApplicationTitle(applicationId: string): string {
    const applications = this.applicationService['applicationsSubject'].value;
    const application = applications.find(a => a.applicationId === applicationId);
    return application?.jobTitle || 'Unknown';
  }

  openDialog(interview?: Interview): void {
    const dialogRef = this.dialog.open(InterviewDialog, {
      width: '600px',
      data: interview
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (interview) {
          const updateData: UpdateInterview = {
            interviewId: interview.interviewId,
            ...result
          };
          this.interviewService.updateInterview(updateData).subscribe();
        } else {
          const createData: CreateInterview = {
            userId: '00000000-0000-0000-0000-000000000000',
            ...result
          };
          this.interviewService.createInterview(createData).subscribe();
        }
      }
    });
  }

  deleteInterview(id: string): void {
    if (confirm('Are you sure you want to delete this interview?')) {
      this.interviewService.deleteInterview(id).subscribe();
    }
  }
}
