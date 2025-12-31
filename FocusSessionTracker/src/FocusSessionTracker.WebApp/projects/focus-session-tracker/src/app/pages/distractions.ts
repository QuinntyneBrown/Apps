import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatTableModule } from '@angular/material/table';
import { MatChipModule } from '@angular/material/chip';
import { Observable } from 'rxjs';
import { DistractionsService, FocusSessionsService } from '../services';
import { Distraction, FocusSession } from '../models';

@Component({
  selector: 'app-distractions',
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
    MatCheckboxModule,
    MatTableModule,
    MatChipModule
  ],
  template: `
    <div class="distractions">
      <div class="distractions__header">
        <h1 class="distractions__title">Distractions</h1>
        <button mat-raised-button color="primary" (click)="showCreateForm = !showCreateForm">
          <mat-icon>{{ showCreateForm ? 'close' : 'add' }}</mat-icon>
          {{ showCreateForm ? 'Cancel' : 'Log Distraction' }}
        </button>
      </div>

      <mat-card *ngIf="showCreateForm" class="distractions__form-card">
        <mat-card-header>
          <mat-card-title>Log New Distraction</mat-card-title>
        </mat-card-header>
        <mat-card-content>
          <form [formGroup]="distractionForm" (ngSubmit)="createDistraction()" class="distractions__form">
            <mat-form-field appearance="outline" class="distractions__form-field">
              <mat-label>Focus Session</mat-label>
              <mat-select formControlName="focusSessionId">
                <mat-option *ngFor="let session of sessions$ | async" [value]="session.focusSessionId">
                  {{ session.name }} - {{ session.startTime | date:'short' }}
                </mat-option>
              </mat-select>
            </mat-form-field>

            <mat-form-field appearance="outline" class="distractions__form-field">
              <mat-label>Type</mat-label>
              <mat-select formControlName="type">
                <mat-option value="Phone Call">Phone Call</mat-option>
                <mat-option value="Email">Email</mat-option>
                <mat-option value="Social Media">Social Media</mat-option>
                <mat-option value="Conversation">Conversation</mat-option>
                <mat-option value="Notification">Notification</mat-option>
                <mat-option value="Mental Wandering">Mental Wandering</mat-option>
                <mat-option value="Other">Other</mat-option>
              </mat-select>
            </mat-form-field>

            <mat-form-field appearance="outline" class="distractions__form-field">
              <mat-label>Duration (minutes)</mat-label>
              <input matInput type="number" formControlName="durationMinutes" placeholder="e.g., 5">
            </mat-form-field>

            <mat-form-field appearance="outline" class="distractions__form-field distractions__form-field--full">
              <mat-label>Description</mat-label>
              <textarea matInput formControlName="description" rows="2" placeholder="Optional description..."></textarea>
            </mat-form-field>

            <div class="distractions__form-field distractions__form-field--full">
              <mat-checkbox formControlName="isInternal">Internal Distraction</mat-checkbox>
            </div>

            <div class="distractions__form-actions">
              <button mat-raised-button type="button" (click)="resetForm()">Reset</button>
              <button mat-raised-button color="primary" type="submit" [disabled]="!distractionForm.valid">
                Log Distraction
              </button>
            </div>
          </form>
        </mat-card-content>
      </mat-card>

      <mat-card class="distractions__list-card">
        <mat-card-header>
          <mat-card-title>All Distractions</mat-card-title>
        </mat-card-header>
        <mat-card-content>
          <div class="distractions__table-container">
            <table mat-table [dataSource]="distractions$ | async" class="distractions__table">
              <ng-container matColumnDef="type">
                <th mat-header-cell *matHeaderCellDef>Type</th>
                <td mat-cell *matCellDef="let distraction">
                  <mat-chip>{{ distraction.type }}</mat-chip>
                </td>
              </ng-container>

              <ng-container matColumnDef="description">
                <th mat-header-cell *matHeaderCellDef>Description</th>
                <td mat-cell *matCellDef="let distraction">{{ distraction.description || '-' }}</td>
              </ng-container>

              <ng-container matColumnDef="occurredAt">
                <th mat-header-cell *matHeaderCellDef>Occurred At</th>
                <td mat-cell *matCellDef="let distraction">{{ distraction.occurredAt | date:'short' }}</td>
              </ng-container>

              <ng-container matColumnDef="duration">
                <th mat-header-cell *matHeaderCellDef>Duration</th>
                <td mat-cell *matCellDef="let distraction">
                  {{ distraction.durationMinutes ? distraction.durationMinutes + ' min' : 'N/A' }}
                </td>
              </ng-container>

              <ng-container matColumnDef="source">
                <th mat-header-cell *matHeaderCellDef>Source</th>
                <td mat-cell *matCellDef="let distraction">
                  <mat-chip [class.distractions__chip--internal]="distraction.isInternal"
                            [class.distractions__chip--external]="!distraction.isInternal">
                    {{ distraction.isInternal ? 'Internal' : 'External' }}
                  </mat-chip>
                </td>
              </ng-container>

              <ng-container matColumnDef="actions">
                <th mat-header-cell *matHeaderCellDef>Actions</th>
                <td mat-cell *matCellDef="let distraction">
                  <button mat-icon-button color="warn" (click)="deleteDistraction(distraction.distractionId)">
                    <mat-icon>delete</mat-icon>
                  </button>
                </td>
              </ng-container>

              <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
              <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
            </table>
          </div>

          <div *ngIf="(distractions$ | async)?.length === 0" class="distractions__empty">
            <mat-icon>notifications_off</mat-icon>
            <p>No distractions logged yet. Stay focused!</p>
          </div>
        </mat-card-content>
      </mat-card>
    </div>
  `,
  styles: [`
    .distractions {
      padding: 24px;
      max-width: 1400px;
      margin: 0 auto;
    }

    .distractions__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 24px;
    }

    .distractions__title {
      margin: 0;
      font-size: 32px;
      font-weight: 500;
    }

    .distractions__form-card {
      margin-bottom: 24px;
    }

    .distractions__form {
      display: grid;
      grid-template-columns: 1fr 1fr 1fr;
      gap: 16px;
      padding: 16px 0;
    }

    .distractions__form-field--full {
      grid-column: 1 / -1;
    }

    .distractions__form-actions {
      grid-column: 1 / -1;
      display: flex;
      justify-content: flex-end;
      gap: 8px;
      margin-top: 8px;
    }

    .distractions__table-container {
      overflow-x: auto;
    }

    .distractions__table {
      width: 100%;
    }

    .distractions__chip--internal {
      background-color: #ff9800 !important;
      color: white !important;
    }

    .distractions__chip--external {
      background-color: #9c27b0 !important;
      color: white !important;
    }

    .distractions__empty {
      text-align: center;
      padding: 64px 32px;
      color: rgba(0, 0, 0, 0.54);

      mat-icon {
        font-size: 64px;
        width: 64px;
        height: 64px;
        margin-bottom: 16px;
        opacity: 0.3;
      }

      p {
        font-size: 16px;
      }
    }

    @media (max-width: 768px) {
      .distractions__form {
        grid-template-columns: 1fr;
      }
    }
  `]
})
export class Distractions implements OnInit {
  distractions$: Observable<Distraction[]>;
  sessions$: Observable<FocusSession[]>;
  distractionForm: FormGroup;
  showCreateForm = false;
  displayedColumns = ['type', 'description', 'occurredAt', 'duration', 'source', 'actions'];

  constructor(
    private distractionsService: DistractionsService,
    private focusSessionsService: FocusSessionsService,
    private fb: FormBuilder
  ) {
    this.distractions$ = this.distractionsService.distractions$;
    this.sessions$ = this.focusSessionsService.sessions$;

    this.distractionForm = this.fb.group({
      focusSessionId: ['', Validators.required],
      type: ['', Validators.required],
      description: [''],
      durationMinutes: [null],
      isInternal: [false]
    });
  }

  ngOnInit(): void {
    this.distractionsService.getDistractions().subscribe();
    this.focusSessionsService.getSessions().subscribe();
  }

  createDistraction(): void {
    if (this.distractionForm.valid) {
      const command = {
        ...this.distractionForm.value,
        occurredAt: new Date().toISOString()
      };
      this.distractionsService.createDistraction(command).subscribe(() => {
        this.resetForm();
        this.showCreateForm = false;
      });
    }
  }

  deleteDistraction(id: string): void {
    if (confirm('Are you sure you want to delete this distraction?')) {
      this.distractionsService.deleteDistraction(id).subscribe();
    }
  }

  resetForm(): void {
    this.distractionForm.reset({
      isInternal: false
    });
  }
}
