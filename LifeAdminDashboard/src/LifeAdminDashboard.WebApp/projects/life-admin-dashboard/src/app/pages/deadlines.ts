import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatChipsModule } from '@angular/material/chips';
import { provideNativeDateAdapter } from '@angular/material/core';
import { DeadlineService } from '../services';
import { Deadline, CreateDeadline, UpdateDeadline } from '../models';

@Component({
  selector: 'app-deadlines',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatCheckboxModule,
    MatChipsModule
  ],
  providers: [provideNativeDateAdapter()],
  template: `
    <div class="deadlines">
      <div class="deadlines__header">
        <h1 class="deadlines__title">Deadlines</h1>
        <button mat-raised-button color="primary" (click)="showForm = !showForm" class="deadlines__add-btn">
          <mat-icon>{{ showForm ? 'close' : 'add' }}</mat-icon>
          {{ showForm ? 'Cancel' : 'Add Deadline' }}
        </button>
      </div>

      <mat-card *ngIf="showForm" class="deadlines__form-card">
        <mat-card-header>
          <mat-card-title>{{ editingDeadline ? 'Edit' : 'Create' }} Deadline</mat-card-title>
        </mat-card-header>
        <mat-card-content>
          <form [formGroup]="deadlineForm" class="deadlines__form">
            <mat-form-field class="deadlines__form-field">
              <mat-label>Title</mat-label>
              <input matInput formControlName="title" required>
            </mat-form-field>

            <mat-form-field class="deadlines__form-field">
              <mat-label>Description</mat-label>
              <textarea matInput formControlName="description" rows="3"></textarea>
            </mat-form-field>

            <mat-form-field class="deadlines__form-field">
              <mat-label>Category</mat-label>
              <input matInput formControlName="category">
            </mat-form-field>

            <mat-form-field class="deadlines__form-field">
              <mat-label>Deadline Date & Time</mat-label>
              <input matInput [matDatepicker]="picker" formControlName="deadlineDateTime" required>
              <mat-datepicker-toggle matSuffix [for]="picker"></mat-datepicker-toggle>
              <mat-datepicker #picker></mat-datepicker>
            </mat-form-field>

            <div class="deadlines__form-checkbox">
              <mat-checkbox formControlName="remindersEnabled">Enable Reminders</mat-checkbox>
            </div>

            <mat-form-field *ngIf="deadlineForm.value.remindersEnabled" class="deadlines__form-field">
              <mat-label>Reminder Days in Advance</mat-label>
              <input matInput type="number" formControlName="reminderDaysAdvance" min="1">
            </mat-form-field>

            <mat-form-field class="deadlines__form-field">
              <mat-label>Notes</mat-label>
              <textarea matInput formControlName="notes" rows="3"></textarea>
            </mat-form-field>
          </form>
        </mat-card-content>
        <mat-card-actions>
          <button mat-raised-button color="primary" (click)="saveDeadline()" [disabled]="deadlineForm.invalid">
            {{ editingDeadline ? 'Update' : 'Create' }}
          </button>
          <button mat-button (click)="cancelEdit()">Cancel</button>
        </mat-card-actions>
      </mat-card>

      <mat-card class="deadlines__table-card">
        <table mat-table [dataSource]="deadlines$ | async" class="deadlines__table">
          <ng-container matColumnDef="title">
            <th mat-header-cell *matHeaderCellDef>Title</th>
            <td mat-cell *matCellDef="let deadline">{{ deadline.title }}</td>
          </ng-container>

          <ng-container matColumnDef="category">
            <th mat-header-cell *matHeaderCellDef>Category</th>
            <td mat-cell *matCellDef="let deadline">{{ deadline.category || '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="deadlineDateTime">
            <th mat-header-cell *matHeaderCellDef>Deadline</th>
            <td mat-cell *matCellDef="let deadline">{{ deadline.deadlineDateTime | date: 'short' }}</td>
          </ng-container>

          <ng-container matColumnDef="reminders">
            <th mat-header-cell *matHeaderCellDef>Reminders</th>
            <td mat-cell *matCellDef="let deadline">
              {{ deadline.remindersEnabled ? deadline.reminderDaysAdvance + ' days' : 'Disabled' }}
            </td>
          </ng-container>

          <ng-container matColumnDef="status">
            <th mat-header-cell *matHeaderCellDef>Status</th>
            <td mat-cell *matCellDef="let deadline">
              <mat-chip [class]="deadline.isCompleted ? 'status-completed' : 'status-active'">
                {{ deadline.isCompleted ? 'Completed' : 'Active' }}
              </mat-chip>
            </td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let deadline">
              <button mat-icon-button (click)="editDeadline(deadline)" [disabled]="deadline.isCompleted">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button (click)="completeDeadline(deadline)" [disabled]="deadline.isCompleted">
                <mat-icon>check_circle</mat-icon>
              </button>
              <button mat-icon-button (click)="deleteDeadline(deadline)" color="warn">
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
    .deadlines {
      padding: 2rem;

      &__header {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-bottom: 2rem;
      }

      &__title {
        margin: 0;
        font-size: 2rem;
        font-weight: 500;
      }

      &__add-btn {
        display: flex;
        align-items: center;
        gap: 0.5rem;
      }

      &__form-card {
        margin-bottom: 2rem;
      }

      &__form {
        display: flex;
        flex-direction: column;
        gap: 1rem;
      }

      &__form-field {
        width: 100%;
      }

      &__form-checkbox {
        margin: 0.5rem 0;
      }

      &__table-card {
        overflow-x: auto;
      }

      &__table {
        width: 100%;
      }
    }

    .status-active { background-color: #e8f5e9; color: #388e3c; }
    .status-completed { background-color: #f5f5f5; color: #757575; }
  `]
})
export class Deadlines implements OnInit {
  private deadlineService = inject(DeadlineService);
  private fb = inject(FormBuilder);

  deadlines$ = this.deadlineService.deadlines$;
  showForm = false;
  editingDeadline: Deadline | null = null;

  displayedColumns = ['title', 'category', 'deadlineDateTime', 'reminders', 'status', 'actions'];

  deadlineForm: FormGroup = this.fb.group({
    title: ['', Validators.required],
    description: [''],
    category: [''],
    deadlineDateTime: ['', Validators.required],
    remindersEnabled: [true],
    reminderDaysAdvance: [7, [Validators.min(1)]],
    notes: ['']
  });

  ngOnInit(): void {
    this.deadlineService.getAll().subscribe();
  }

  saveDeadline(): void {
    if (this.deadlineForm.invalid) return;

    const formValue = this.deadlineForm.value;
    const deadlineData = {
      ...formValue,
      deadlineDateTime: new Date(formValue.deadlineDateTime).toISOString(),
      userId: '00000000-0000-0000-0000-000000000000' // Placeholder user ID
    };

    if (this.editingDeadline) {
      const updateDeadline: UpdateDeadline = {
        ...deadlineData,
        deadlineId: this.editingDeadline.deadlineId
      };
      this.deadlineService.update(updateDeadline).subscribe(() => {
        this.cancelEdit();
      });
    } else {
      const createDeadline: CreateDeadline = deadlineData;
      this.deadlineService.create(createDeadline).subscribe(() => {
        this.cancelEdit();
      });
    }
  }

  editDeadline(deadline: Deadline): void {
    this.editingDeadline = deadline;
    this.showForm = true;
    this.deadlineForm.patchValue({
      title: deadline.title,
      description: deadline.description,
      category: deadline.category,
      deadlineDateTime: new Date(deadline.deadlineDateTime),
      remindersEnabled: deadline.remindersEnabled,
      reminderDaysAdvance: deadline.reminderDaysAdvance,
      notes: deadline.notes
    });
  }

  completeDeadline(deadline: Deadline): void {
    this.deadlineService.complete(deadline.deadlineId).subscribe();
  }

  deleteDeadline(deadline: Deadline): void {
    if (confirm(`Are you sure you want to delete "${deadline.title}"?`)) {
      this.deadlineService.delete(deadline.deadlineId).subscribe();
    }
  }

  cancelEdit(): void {
    this.editingDeadline = null;
    this.showForm = false;
    this.deadlineForm.reset({
      remindersEnabled: true,
      reminderDaysAdvance: 7
    });
  }
}
