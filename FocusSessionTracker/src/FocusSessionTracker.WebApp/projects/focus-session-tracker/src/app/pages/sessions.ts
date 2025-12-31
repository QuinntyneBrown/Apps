import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatTableModule } from '@angular/material/table';
import { MatChipModule } from '@angular/material/chip';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { FocusSessionsService } from '../services';
import { FocusSession, SessionType, SessionTypeLabels } from '../models';

@Component({
  selector: 'app-sessions',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatTableModule,
    MatChipModule,
    MatDialogModule
  ],
  template: `
    <div class="sessions">
      <div class="sessions__header">
        <h1 class="sessions__title">Focus Sessions</h1>
        <button mat-raised-button color="primary" (click)="showCreateForm = !showCreateForm">
          <mat-icon>{{ showCreateForm ? 'close' : 'add' }}</mat-icon>
          {{ showCreateForm ? 'Cancel' : 'New Session' }}
        </button>
      </div>

      <mat-card *ngIf="showCreateForm" class="sessions__form-card">
        <mat-card-header>
          <mat-card-title>Create New Session</mat-card-title>
        </mat-card-header>
        <mat-card-content>
          <form [formGroup]="sessionForm" (ngSubmit)="createSession()" class="sessions__form">
            <mat-form-field appearance="outline" class="sessions__form-field">
              <mat-label>Session Name</mat-label>
              <input matInput formControlName="name" placeholder="e.g., Deep Work on Project X">
            </mat-form-field>

            <mat-form-field appearance="outline" class="sessions__form-field">
              <mat-label>Session Type</mat-label>
              <mat-select formControlName="sessionType">
                <mat-option *ngFor="let type of sessionTypes" [value]="type.value">
                  {{ type.label }}
                </mat-option>
              </mat-select>
            </mat-form-field>

            <mat-form-field appearance="outline" class="sessions__form-field">
              <mat-label>User ID</mat-label>
              <input matInput formControlName="userId" placeholder="Enter user ID">
            </mat-form-field>

            <mat-form-field appearance="outline" class="sessions__form-field">
              <mat-label>Planned Duration (minutes)</mat-label>
              <input matInput type="number" formControlName="plannedDurationMinutes" placeholder="e.g., 90">
            </mat-form-field>

            <mat-form-field appearance="outline" class="sessions__form-field sessions__form-field--full">
              <mat-label>Notes</mat-label>
              <textarea matInput formControlName="notes" rows="3" placeholder="Optional notes..."></textarea>
            </mat-form-field>

            <div class="sessions__form-actions">
              <button mat-raised-button type="button" (click)="resetForm()">Reset</button>
              <button mat-raised-button color="primary" type="submit" [disabled]="!sessionForm.valid">
                Create Session
              </button>
            </div>
          </form>
        </mat-card-content>
      </mat-card>

      <mat-card class="sessions__list-card">
        <mat-card-header>
          <mat-card-title>All Sessions</mat-card-title>
        </mat-card-header>
        <mat-card-content>
          <div class="sessions__table-container">
            <table mat-table [dataSource]="sessions$ | async" class="sessions__table">
              <ng-container matColumnDef="name">
                <th mat-header-cell *matHeaderCellDef>Name</th>
                <td mat-cell *matCellDef="let session">{{ session.name }}</td>
              </ng-container>

              <ng-container matColumnDef="type">
                <th mat-header-cell *matHeaderCellDef>Type</th>
                <td mat-cell *matCellDef="let session">
                  <mat-chip>{{ getSessionTypeLabel(session.sessionType) }}</mat-chip>
                </td>
              </ng-container>

              <ng-container matColumnDef="startTime">
                <th mat-header-cell *matHeaderCellDef>Start Time</th>
                <td mat-cell *matCellDef="let session">{{ session.startTime | date:'short' }}</td>
              </ng-container>

              <ng-container matColumnDef="duration">
                <th mat-header-cell *matHeaderCellDef>Duration</th>
                <td mat-cell *matCellDef="let session">
                  {{ session.isCompleted ? (session.actualDurationMinutes || 0) : session.plannedDurationMinutes }} min
                </td>
              </ng-container>

              <ng-container matColumnDef="status">
                <th mat-header-cell *matHeaderCellDef>Status</th>
                <td mat-cell *matCellDef="let session">
                  <mat-chip [class.sessions__chip--completed]="session.isCompleted"
                            [class.sessions__chip--active]="!session.isCompleted">
                    {{ session.isCompleted ? 'Completed' : 'Active' }}
                  </mat-chip>
                </td>
              </ng-container>

              <ng-container matColumnDef="focusScore">
                <th mat-header-cell *matHeaderCellDef>Score</th>
                <td mat-cell *matCellDef="let session">{{ session.focusScore || 'N/A' }}</td>
              </ng-container>

              <ng-container matColumnDef="distractions">
                <th mat-header-cell *matHeaderCellDef>Distractions</th>
                <td mat-cell *matCellDef="let session">{{ session.distractionCount }}</td>
              </ng-container>

              <ng-container matColumnDef="actions">
                <th mat-header-cell *matHeaderCellDef>Actions</th>
                <td mat-cell *matCellDef="let session">
                  <button mat-icon-button color="primary" *ngIf="!session.isCompleted"
                          (click)="completeSession(session.focusSessionId)">
                    <mat-icon>check_circle</mat-icon>
                  </button>
                  <button mat-icon-button color="warn" (click)="deleteSession(session.focusSessionId)">
                    <mat-icon>delete</mat-icon>
                  </button>
                </td>
              </ng-container>

              <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
              <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
            </table>
          </div>
        </mat-card-content>
      </mat-card>
    </div>
  `,
  styles: [`
    .sessions {
      padding: 24px;
      max-width: 1400px;
      margin: 0 auto;
    }

    .sessions__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 24px;
    }

    .sessions__title {
      margin: 0;
      font-size: 32px;
      font-weight: 500;
    }

    .sessions__form-card {
      margin-bottom: 24px;
    }

    .sessions__form {
      display: grid;
      grid-template-columns: 1fr 1fr;
      gap: 16px;
      padding: 16px 0;
    }

    .sessions__form-field--full {
      grid-column: 1 / -1;
    }

    .sessions__form-actions {
      grid-column: 1 / -1;
      display: flex;
      justify-content: flex-end;
      gap: 8px;
      margin-top: 8px;
    }

    .sessions__table-container {
      overflow-x: auto;
    }

    .sessions__table {
      width: 100%;
    }

    .sessions__chip--completed {
      background-color: #4caf50 !important;
      color: white !important;
    }

    .sessions__chip--active {
      background-color: #2196f3 !important;
      color: white !important;
    }

    @media (max-width: 768px) {
      .sessions__form {
        grid-template-columns: 1fr;
      }
    }
  `]
})
export class Sessions implements OnInit {
  sessions$: Observable<FocusSession[]>;
  sessionForm: FormGroup;
  showCreateForm = false;
  displayedColumns = ['name', 'type', 'startTime', 'duration', 'status', 'focusScore', 'distractions', 'actions'];
  sessionTypes = Object.keys(SessionType)
    .filter(key => isNaN(Number(key)))
    .map(key => ({
      value: SessionType[key as keyof typeof SessionType],
      label: SessionTypeLabels[SessionType[key as keyof typeof SessionType]]
    }));

  constructor(
    private focusSessionsService: FocusSessionsService,
    private fb: FormBuilder
  ) {
    this.sessions$ = this.focusSessionsService.sessions$;
    this.sessionForm = this.fb.group({
      name: ['', Validators.required],
      sessionType: [SessionType.DeepWork, Validators.required],
      userId: ['00000000-0000-0000-0000-000000000000', Validators.required],
      plannedDurationMinutes: [90, [Validators.required, Validators.min(1)]],
      notes: ['']
    });
  }

  ngOnInit(): void {
    this.focusSessionsService.getSessions().subscribe();
  }

  createSession(): void {
    if (this.sessionForm.valid) {
      const command = {
        ...this.sessionForm.value,
        startTime: new Date().toISOString()
      };
      this.focusSessionsService.createSession(command).subscribe(() => {
        this.resetForm();
        this.showCreateForm = false;
      });
    }
  }

  completeSession(id: string): void {
    this.focusSessionsService.completeSession(id, {}).subscribe();
  }

  deleteSession(id: string): void {
    if (confirm('Are you sure you want to delete this session?')) {
      this.focusSessionsService.deleteSession(id).subscribe();
    }
  }

  resetForm(): void {
    this.sessionForm.reset({
      sessionType: SessionType.DeepWork,
      userId: '00000000-0000-0000-0000-000000000000',
      plannedDurationMinutes: 90
    });
  }

  getSessionTypeLabel(type: SessionType): string {
    return SessionTypeLabels[type];
  }
}
